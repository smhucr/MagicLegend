using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    public RectTransform[] skillButtons;
    public GameObject highlightedBorder;
    public int selectedElementIndex;
    public Image upgradeableElementImage;

    private void Start()
    {
        MoveSelectedBorder(GameManager.instance.selectedElement);
        upgradeableElementImage.sprite = skillButtons[GameManager.instance.selectedElement].GetComponent<Image>().sprite;
    }

    public void GetSelectedButtonIndex(int buttonIndex)
    {
        selectedElementIndex = buttonIndex;
        MoveSelectedBorder(buttonIndex);
    }

    public void MoveSelectedBorder(int button)
    {
        highlightedBorder.transform.SetParent(skillButtons[button].gameObject.transform);
        highlightedBorder.GetComponent<RectTransform>().position = skillButtons[button].position;
    }

}
