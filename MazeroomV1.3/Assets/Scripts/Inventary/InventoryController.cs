using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Objects[] slots;
    public Image[] slotImages;
    public int[] slotAmount;

    private InterfaceController iController;
    void Start()
    {
        iController = FindObjectOfType<InterfaceController>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if(Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.collider.tag == "Object")
            {
                iController.itemText.text = "Press E to pick up " + hit.transform.GetComponent<ObjectType>().objecType.itemName;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 0; i < slots.Length; i++)
                    {
                        if (slots[i] == null || slots[i].name == hit.transform.GetComponent<ObjectType>().objecType.name)
                        {
                            slots[i] = hit.transform.GetComponent<ObjectType>().objecType;
                            slotAmount[i]++;
                            slotImages[i].sprite = slots[i].itemSprite;

                            Destroy(hit.transform.gameObject);
                            break;
                        }
                    }
                }
            }
        }
    }
}
