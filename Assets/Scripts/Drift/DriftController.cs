using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DriftController : MonoBehaviour
{
    [SerializeField] private Rigidbody m_playerRigidBody;
    [SerializeField] private int m_currentSpeed;
    [SerializeField] private int m_maxSpeed;
    [SerializeField] private int m_acceleration;
    void Update()
    {
        Rotate();
        Acceleration();
    }

    void Rotate()
    {
        Vector3 angles = this.transform.rotation.eulerAngles;
        float turnRotation = (angles.y + 180f) % 360f - 180f;
        if (Input.GetKeyDown(KeyCode.A))
        {
            turnRotation -= 90;
           // m_currentSpeed = 0;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            turnRotation += 90;
            //m_currentSpeed = 0;
        }

        this.transform.rotation = Quaternion.Euler(new Vector3(angles.x, turnRotation, angles.z));
    }

    void Acceleration()
    {
        m_currentSpeed += m_acceleration;

        m_currentSpeed = Mathf.Clamp(m_currentSpeed, 0, m_maxSpeed);
        
        m_playerRigidBody.AddForce(this.transform.forward * m_currentSpeed);
    }
}
