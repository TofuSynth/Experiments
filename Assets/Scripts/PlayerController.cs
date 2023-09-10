using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_playerRigidBody;
    [SerializeField] private GameObject m_camera;
    [SerializeField] private int m_speed;
    [SerializeField] private int m_rotateSpeed;
    [SerializeField] private float m_jumpSpeed;
    private bool m_isForwardDown;
    private bool m_isBackDown;
    private bool m_isRightDown;
    private bool m_isLeftDown;
    private bool m_isJumpDown;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        m_playerRigidBody = this.GetComponent<Rigidbody>();
        m_camera.transform.Rotate(0,0,0);
    }

    void Update()
    {
        ButtonCheck();
        Movement();
        Jumping();
        Rotation();
    }

    void ButtonCheck()
    {
        m_isForwardDown = Input.GetKey("w");
        m_isBackDown = Input.GetKey("s");
        m_isRightDown = Input.GetKey("d");
        m_isLeftDown = Input.GetKey("a");
        m_isJumpDown = Input.GetKeyDown("space");
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

    void Jumping()
    {
        float jumping = Convert.ToInt32((m_isJumpDown));
        Vector3 jumpVector = new Vector3(0, 1, 0);
        m_playerRigidBody.AddForce(jumpVector * (jumping * m_jumpSpeed));
    }

    void Rotation()
    {
        Vector3 angles = this.transform.rotation.eulerAngles;

        float horizontal = (angles.y + 180f) % 360f - 180f;
        horizontal += Input.GetAxis("Mouse X") * m_rotateSpeed;

        float vertical = (angles.x + 180f) % 360f - 180f;
        vertical -= Input.GetAxis("Mouse Y") * m_rotateSpeed;
        vertical = Mathf.Clamp(vertical, -89f, 89f);
            
        this.transform.rotation = Quaternion.Euler(new Vector3(vertical, horizontal, angles.z));
    }
}
