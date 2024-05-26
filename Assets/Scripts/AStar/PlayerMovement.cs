using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_playerRigidBody;
    [SerializeField] private int m_speed;
    private bool m_isForwardDown;
    private bool m_isBackDown;
    private bool m_isRightDown;
    private bool m_isLeftDown;
    
    void Start()
    {
        m_playerRigidBody = this.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        ButtonCheck();
    }
    void FixedUpdate()
    {
        Movement();
    }

    void ButtonCheck()
    {
        m_isForwardDown = Input.GetKey(KeyCode.W);
        m_isBackDown = Input.GetKey(KeyCode.S);
        m_isRightDown = Input.GetKey(KeyCode.D);
        m_isLeftDown = Input.GetKey(KeyCode.A);
    }

    void Movement()
    {
        Vector3 inputVector = Vector3.zero;

        float forwardMovement = Convert.ToInt32(m_isForwardDown) - Convert.ToInt32(m_isBackDown);
        float sideMovement = Convert.ToInt32(m_isRightDown) - Convert.ToInt32(m_isLeftDown);

        inputVector += new Vector3(this.transform.forward.x, 0, this.transform.forward.z).normalized
                       * forwardMovement;
        inputVector += new Vector3(this.transform.right.x, 0, this.transform.right.z).normalized
                       * sideMovement;

        inputVector = Vector3.Normalize(inputVector);

        m_playerRigidBody.AddForce(inputVector * (m_speed * Time.deltaTime));
    }
}
