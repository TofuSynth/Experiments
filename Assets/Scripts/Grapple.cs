using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    [SerializeField] private GameObject m_player;
    [SerializeField] private Rigidbody m_grapple;
    [SerializeField] private GameObject m_grappleDirection;
    [SerializeField] private int m_grappleSpeed;
    private Vector3 RestingPosition;
    private bool m_isGrapplePressed;
    private bool m_isGrappleLifted;
    private bool m_isGrappleExtended = false;
    private SphereCollider m_grappleCollider;

    void ButtonCheck()
    {
        m_isGrapplePressed = Input.GetMouseButton(0);
        m_isGrappleLifted = Input.GetMouseButtonUp(0);
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
        RetractGrapple();
    }

    void FireGrapple()
    {
        if (m_isGrapplePressed && !m_isGrappleExtended)
        {
            Vector3 grappleTarget = m_grappleDirection.transform.position;
            float grappleSpeed = m_grappleSpeed * Time.deltaTime;
            transform.parent = null;
            m_grappleCollider.enabled = true;
            m_grapple.position = Vector3.MoveTowards(m_grapple.position, grappleTarget, grappleSpeed);
            if (Vector3.Distance(m_grapple.position, grappleTarget) < 0.001f)
            {
                m_isGrappleExtended = true;
                ReturnGrapple();
            }
        }
        else if (m_isGrappleLifted)
        {
            m_isGrappleExtended = false;
        }
        else
        {
            ReturnGrapple();
        }
        
    }

    void RetractGrapple()
    {
        if (!m_isGrapplePressed && this.transform.position != RestingPosition)
        {
            ReturnGrapple();
        }
    }

    void ReturnGrapple()
    {
        this.transform.parent = m_player.transform;
        this.transform.localPosition = RestingPosition;
        m_grappleCollider.enabled = false;
        m_grapple.velocity = Vector3.zero;
    }
}
