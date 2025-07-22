using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    public GameObject inventoryPanel;

    bool invActive;
    void Start()
    {

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