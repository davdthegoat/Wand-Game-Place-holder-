using UnityEngine;
using UnityEngine.UI;
using System;

//seems not useful for me
//remove and check



[RequireComponent(typeof(Button))]
public class ItemUI : MonoBehaviour
{

    //Use this to display an image/Mesh wherever your item is stored

    [Header("References")]
    [SerializeField]
    Image image;
    [SerializeField]
    Button button;

    public void Initialize(string inventoryId, Item item, Action<string> removeItemAction)
    {
        image.sprite = item.icon;
        transform.localScale = Vector3.one;
        button.onClick.AddListener(() => removeItemAction.Invoke(inventoryId));   
    }

}
