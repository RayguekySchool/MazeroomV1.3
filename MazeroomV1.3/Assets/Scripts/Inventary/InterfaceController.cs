using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterfaceController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public TextMeshProUGUI itemText;
    bool invActive;
    void Start()
    {
        itemText.text = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            invActive = !invActive;
        }
        if (invActive)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}