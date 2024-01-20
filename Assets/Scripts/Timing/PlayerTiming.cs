using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTiming : MonoBehaviour
{
    //Bar is centered so the value ranges between negative and positive
    [SerializeField] private float m_xPositionRange;
    [SerializeField] private float m_greenRange;
    [SerializeField] private float m_yellowRange;
    [SerializeField] private float m_orangeRange;
    [SerializeField] private int m_greenPoints;
    [SerializeField] private int m_yellowPoints;
    [SerializeField] private int m_orangePoints;
    [SerializeField] private int m_redPoints;
    [SerializeField] private float m_speed;
    [SerializeField] private TMP_Text m_scoreText;
    private int m_right = 1;
    private int m_left = -1;
    private int m_movementDirection;
    Vector3 m_timingPosition;
    private bool m_stopTimingEvent = false;
    private bool m_isSpaceDown;
    private bool m_isEnterDown;

    void Start()
    {
        m_timingPosition = this.transform.localPosition;
        Application.targetFrameRate = 60;
        m_movementDirection = m_right;
    }
    
    void Update()
    {
        ButtonCheck();
        StopTiming();
        if (!m_stopTimingEvent)
        {
            CheckIfHittingEdges();
            MoveTimingBar();
        }
    }

    void ButtonCheck()
    {
        m_isSpaceDown = Input.GetKeyDown(KeyCode.Space);
        m_isEnterDown = Input.GetKeyDown(KeyCode.Return);
    }

    void StopTiming()
    {
        if (m_isSpaceDown)
        {
            m_stopTimingEvent = true;
            CalculateScore();
        }

        if (m_isEnterDown)
        {
            m_stopTimingEvent = false;
            ResetScore();
        }
    }

    void CalculateScore()
    {
        if (this.transform.localPosition.x >= -m_greenRange && this.transform.localPosition.x <= m_greenRange)
        {
            m_scoreText.text = "" + m_greenPoints;
        }
        else if (this.transform.localPosition.x >= -m_yellowRange && this.transform.localPosition.x <= m_yellowRange)
        {
            m_scoreText.text = "" + m_yellowPoints;
        }
        else if (this.transform.localPosition.x >= -m_orangeRange && this.transform.localPosition.x <= m_orangeRange)
        {
            m_scoreText.text= "" + m_orangePoints;
        }
        else
        {
            m_scoreText.text = "" + m_redPoints;
        }
    }

    void ResetScore()
    {
        m_scoreText.text = "0";
    }
    
    void MoveTimingBar()
    {
        m_timingPosition.x += m_speed * m_movementDirection * Time.deltaTime;
        this.transform.localPosition = m_timingPosition;
    }
    
    void CheckIfHittingEdges()
    {
        if (this.transform.localPosition.x >= m_xPositionRange)
        {
            ChangeDirection();
            m_movementDirection = m_left;
        }
        else if (this.transform.localPosition.x <= -m_xPositionRange)
        {
            ChangeDirection();
            m_movementDirection = m_right;
        }
    }

    void ChangeDirection()
    {
        m_timingPosition.x = (m_xPositionRange * m_movementDirection); ;
        this.transform.localPosition = m_timingPosition;
    }
}
