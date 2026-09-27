using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DroppedItem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    bool autoStart; //controls whether item is auto initialised when game starts i.e auto loaded into scene 

    [SerializeField]
    float enabledPickupDelay = 3.0f; //delay before it can be picked up to prevent glitches and stuff ig

    [Header("State")]
    public Item item; 
    public bool pickedUp = false; //whether object is picked up

    void Start()
    {
        if (autoStart && item != null)
        {
            Initialize(item);
        }
    }

    public void Initialize(Item item)
    {
        this.item = item;
        var droppedItem = Instantiate(item.prefab, transform);
        droppedItem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        StartCoroutine(EnablePickup(enabledPickupDelay));

    }

    IEnumerator EnablePickup(float dealy)//dealy is just delay rearranged
    {
        yield return new WaitForSeconds(dealy); // delay before enabling the trigger collider so it can be picked up again 
        GetComponent<Collider>().enabled = true;
    }   

}
