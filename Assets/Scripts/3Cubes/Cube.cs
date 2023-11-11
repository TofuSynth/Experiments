using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody m_cubeBody;
    [SerializeField] private float m_speed;
    [SerializeField] private Material m_cubeMaterial;
    [SerializeField] private Color m_color;
    private bool m_isSelected = false;
    private bool m_isAllSelected = false;
    private bool m_isForwardDown;
    private bool m_isBackDown;
    private bool m_isRightDown;
    private bool m_isLeftDown;
    private bool m_isSelectAllDown;
    
    void Update()
    {
        ButtonCheck();
        Movement();
        SelectThisCube();
    }

    void ButtonCheck()
    {
        m_isForwardDown = Input.GetKey(KeyCode.W);
        m_isBackDown = Input.GetKey(KeyCode.S);
        m_isRightDown = Input.GetKey(KeyCode.D);
        m_isLeftDown = Input.GetKey(KeyCode.A);
        m_isSelectAllDown = Input.GetKeyDown(KeyCode.Space);
    }

    void Movement()
    {
        if (m_isSelected || m_isAllSelected)
        {
            Vector3 inputVector = Vector3.zero;

            float forwardMovement = Convert.ToInt32(m_isForwardDown) - Convert.ToInt32(m_isBackDown);
            float sideMovement = Convert.ToInt32(m_isRightDown) - Convert.ToInt32(m_isLeftDown);

            inputVector += new Vector3(this.transform.forward.x, 0, this.transform.forward.z).normalized
                           * forwardMovement;
            inputVector += new Vector3(this.transform.right.x, 0, this.transform.right.z).normalized
                           * sideMovement;

            inputVector = Vector3.Normalize(inputVector);

            m_cubeBody.AddForce(inputVector * (m_speed * Time.deltaTime));
        }
    }

    void SelectThisCube()
    {
        RaycastHit hit;
        
        if (m_isSelectAllDown && !m_isAllSelected)
        {
            m_isSelected = false;
            m_isAllSelected = true;
            m_cubeMaterial.SetColor("_Color", Color.yellow);
        }
        else if (m_isSelectAllDown && m_isAllSelected)
        {
            m_isSelected = false;
            m_isAllSelected = false;
            m_cubeMaterial.SetColor("_Color", Color.white);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    m_isAllSelected = false;
                    m_isSelected = true;
                    m_cubeMaterial.SetColor("_Color", m_color);
                }
                else
                {
                    m_isAllSelected = false;
                    m_isSelected = false;
                    m_cubeMaterial.SetColor("_Color", Color.white);
                }
            }
        }
    }
    
}
