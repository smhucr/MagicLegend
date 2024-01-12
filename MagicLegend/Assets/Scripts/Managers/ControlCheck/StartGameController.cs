using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class StartGameController : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) & GameManager.instance.isTransitionOver)
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);

            if (results.Count == 0 && !GameManager.instance.startGame)
            {
                GameManager.instance.StartGame();
            }
            foreach (RaycastResult result in results)
            {

                Debug.Log("Hit " + result.gameObject.name);
            }
        }
    }

}
