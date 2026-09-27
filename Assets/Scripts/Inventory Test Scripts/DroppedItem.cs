using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DroppedItem : MonoBehaviour
{

    ///IGNORE YAP for understanding
    ///Better explanation is 
    /// this script is a container that displays the mesh and collider of the Item(item class)
    /// it just has to retrieve the data from the item class and show it on screen and be able to give it a body etc etc
    /// in order to make a wand for example you need a specific class like item which has specific data that helps the dropped item or similar containers to render this into the scenne and allow it to be interacted with
    /// ensure you seperate all logic that is not inventory logic to a seperate script and use references wisely


    /// <summary>
    /// Decides whether to place item on scene loading
    /// checks if its been picked up(bool)
    /// can Instantiates Item at the coordinates of the Dropped Item
    /// 
    /// Try to make this into the shelf storage thing
    /// </summary>

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

    //define what to do delay seconds after being picked
    IEnumerator EnablePickup(float dealy)//dealy is just delay rearranged
    {
        yield return new WaitForSeconds(dealy); // delay before enabling the trigger collider so it can be picked up again 
        GetComponent<Collider>().enabled = true;
    }   

}
