using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESETPLAYERPREFS : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }
}
