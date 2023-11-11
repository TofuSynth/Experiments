using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text m_buttonName;
    
    public void LoadScene()
    {
        SceneManager.LoadScene(m_buttonName.text);
    }
}
