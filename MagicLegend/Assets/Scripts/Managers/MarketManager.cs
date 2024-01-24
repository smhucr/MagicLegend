using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public RectTransform[] skillButtons;
    public GameObject highlightedBorder;

    private void Start()
    {
        MoveSelectedBorder(GameManager.instance.selectedElement);
    }

    public void GetSelectedButtonIndex(int buttonIndex)
    {
        PlayerPrefs.SetInt("SelectedElement", buttonIndex);
        MoveSelectedBorder(buttonIndex);
    }

    public void MoveSelectedBorder(int button)
    {
        highlightedBorder.transform.SetParent(skillButtons[button].gameObject.transform);
        highlightedBorder.GetComponent<RectTransform>().position = skillButtons[button].position;
    }

}
