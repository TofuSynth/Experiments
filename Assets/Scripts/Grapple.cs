using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] private GameObject m_player;
    [SerializeField] private Rigidbody m_playerRigidBody;
    [SerializeField] private Rigidbody m_grapple;
    [SerializeField] private GameObject m_grappleDirection;
    [SerializeField] private int m_grappleSpeed;
    [SerializeField] private int m_playerGrappleSpeed;
    private Vector3 RestingPosition;
    private bool m_isGrapplePressed;
    private bool m_isGrappleTargeted;
    private bool m_isGrappleLifted;
    private bool m_isGrappleExtended = false;
    private bool m_isGrappleConnected = false;
    private bool m_isPlayerGrapplingToTarget = false;
    private SphereCollider m_grappleCollider;
    private Vector3 m_grappleTarget;

    void ButtonCheck()
    {
        m_isGrapplePressed = Input.GetKey(KeyCode.E);
        m_isGrappleLifted = Input.GetKeyUp(KeyCode.E);
        m_isGrappleTargeted = Input.GetKeyDown(KeyCode.E);
    }
    private void Start()
    {
        m_grappleCollider = m_grapple.GetComponent<SphereCollider>();
        RestingPosition = this.transform.position;
    }

    private void Update()
    {
        ButtonCheck();
        FireGrapple();
        GrappleToTarget();
    }
    
    void FireGrapple()
    {
        if (m_isGrappleTargeted)
        {
            m_grappleTarget = m_grappleDirection.transform.position;
        }
        
        if (m_isGrapplePressed && !m_isGrappleExtended)
        {
            float grappleSpeed = m_grappleSpeed * Time.deltaTime;
            transform.parent = null;
            m_grappleCollider.enabled = true;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, m_grappleDirection.transform.position, out hit,
                    grappleSpeed))
            {
                if (!GrapplePointCollisionCheck(hit))
                {
                    m_isGrappleExtended = true;
                    ReturnGrapple();
                    return;
                }
            }

            if (!m_isGrappleConnected)
            {
                if (Vector3.Distance(m_grapple.position, m_grappleTarget) > grappleSpeed)
                {
                    m_grapple.position = Vector3.MoveTowards(m_grapple.position, m_grappleTarget, grappleSpeed);
                }
                else
                {
                    m_grapple.position = Vector3.MoveTowards(m_grapple.position, m_grappleTarget,
                        Vector3.Distance(m_grapple.position, m_grappleTarget));
                }

                if (Vector3.Distance(m_grapple.position, m_grappleTarget) < 0.001f && !m_isPlayerGrapplingToTarget)
                {
                    m_isGrappleExtended = true;
                    ReturnGrapple();
                }
            }
        }
        else if (m_isGrappleLifted)
        {
            m_isGrappleConnected = false;
            m_isGrappleExtended = false;
            m_isPlayerGrapplingToTarget = false;
        }
        else
        {
            ReturnGrapple();
        }
    }
    
    void ReturnGrapple()
    {
        m_isGrappleConnected = false;
        this.transform.parent = m_player.transform;
        this.transform.localPosition = RestingPosition;
        m_grappleCollider.enabled = false;
        m_grapple.velocity = Vector3.zero;
    }
    
     private void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.tag == "GrapplePoint")
         {
             m_isGrappleConnected = true;
         }
     }
     
    private bool GrapplePointCollisionCheck(RaycastHit hit)
    {
        if (hit.collider.tag == "GrapplePoint")
        {
            m_grappleTarget = hit.point;
            m_isGrappleConnected = true;
            return true;
        }

        if (hit.collider.name == "Grapple")
        {
            return true;
        }
        return false;
    }
    void GrappleToTarget()
    {
        if (m_isGrappleConnected)
        {
            m_isPlayerGrapplingToTarget = true;
            float playerGrappleSpeed = m_playerGrappleSpeed * Time.deltaTime;
            m_playerRigidBody.position = Vector3.MoveTowards(m_playerRigidBody.position, this.transform.position,
                playerGrappleSpeed);
            if (Vector3.Distance(m_playerRigidBody.position, this.transform.position) < 0.001f)
            {
                m_isPlayerGrapplingToTarget = false;
                ReturnGrapple();
            }
        }
    }
}
