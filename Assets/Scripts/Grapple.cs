using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    [SerializeField] private GameObject m_player;
    [SerializeField] private Rigidbody m_grapple;
    [SerializeField] private int m_grappleSpeed;
    private Vector3 RestingPosition;
    private bool m_isGrapplePressed;
    private SphereCollider m_grappleCollider;

    void ButtonCheck()
    {
        m_isGrapplePressed = Input.GetMouseButton(0);
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
        if (m_isGrapplePressed)
        {
            transform.parent = null;
            m_grappleCollider.enabled = true;
            m_grapple.AddForce(m_player.transform.forward * (m_grappleSpeed * Time.deltaTime));
        }
    }

    void RetractGrapple()
    {
        if (!m_isGrapplePressed && this.transform.position != RestingPosition)
        {
            this.transform.parent = m_player.transform;
            this.transform.localPosition = RestingPosition;
            m_grappleCollider.enabled = false;
            m_grapple.velocity = Vector3.zero;
        }
    }
}
