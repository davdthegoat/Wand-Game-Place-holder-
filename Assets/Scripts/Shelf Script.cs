using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ShelfScript : MonoBehaviour
{
    private GameObject storedObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.childCount > 0)
        {
            storedObj = transform.GetChild(0).gameObject;
            storedObj.transform.position = transform.position;
            storedObj.transform.rotation = quaternion.identity;
        }


    }


}