using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string m_projectDescription;
    [SerializeField] private TMP_Text m_descriptionText;
    private string m_tabInstructions;

    private void Start()
    {
        m_tabInstructions = m_descriptionText.text;
    }

    public void OnPointerEnter(PointerEventData pointer)
    {
        m_descriptionText.text = m_projectDescription;
    }

    public void OnPointerExit(PointerEventData pointer)
    {
        m_descriptionText.text = m_tabInstructions;
    }
}
