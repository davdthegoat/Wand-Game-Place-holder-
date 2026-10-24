using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WandBoxItem : MonoBehaviour

{

     WandBox CreateInstance()
    {
        WandBox item = Instantiate(itemToInstantiate);
        return item;
    }


    [Header("State")]
    public WandBox wandboxitem=null;


    
    [SerializeField] WandBox itemToInstantiate=null; //Set this in the inspector

    void Awake()
    {
        wandboxitem = CreateInstance();
    }






}
