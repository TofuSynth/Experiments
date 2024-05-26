using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    private bool m_pathfindingState = false;
    private bool m_isTogglePressed = false;
    [SerializeField] private GameObject m_player;
    [SerializeField] private NavMeshAgent m_navMeshAgent;
    void Update()
    {
        ButtonCheck();
        PathfindingState();
        Pathfind();
    }

    void ButtonCheck()
    {
        m_isTogglePressed = Input.GetKeyDown(KeyCode.Space);
    }

    void PathfindingState()
    {
        if (!m_pathfindingState && m_isTogglePressed)
        {
            m_pathfindingState = true;
        }
        else if (m_pathfindingState && m_isTogglePressed)
        {
            m_pathfindingState = false;
        }
    }

    void Pathfind()
    {
        Vector3 m_playerVector = m_player.transform.position;

        if (m_pathfindingState)
        {
            m_navMeshAgent.SetDestination(m_playerVector);
        }
    }
}
