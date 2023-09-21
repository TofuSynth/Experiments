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

    void ButtonCheck()
    {
        m_isGrapplePressed = Input.GetMouseButton(0);
    }
    private void Start()
    {
        ButtonCheck();
        RestingPosition = this.transform.position;
        FireGrapple();
    }

    private void Update()
    {
        FireGrapple();
    }

    void FireGrapple()
    {
        if (m_isGrapplePressed)
        {
            transform.parent = null;
            m_grapple.AddForce(Vector3.forward * (m_grappleSpeed * Time.deltaTime));
        }
    }
}
