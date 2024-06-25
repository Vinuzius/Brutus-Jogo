using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    private VisualElement m_Healthbar;
    public static UIHandler instance {get; private set; }

    public float displayTime = 4.0f;
    private VisualElement m_NonPlayerDialogue;
    private float m_TimerDisplay;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);

        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;
    }

    private void Update()
    {
       if(m_TimerDisplay > 0)
       {
            m_TimerDisplay -= Time.deltaTime;
       }
       else
            m_NonPlayerDialogue.style.display = DisplayStyle.None;
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent( 100 * percentage );
    }

    public void DisplayDialogue()
    {
        m_TimerDisplay = displayTime;
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
    }

}
