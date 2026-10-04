using System;

using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Mathf;

public class WandSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject wandPrefab;

    [SerializeField] int wandCount;

    public Transform spawnPosition;

    [SerializeField] float speed = 1;

    private int count = 0;


    private float theta = 0f;
    public int zDeviationfromZero = 18;
    public float step;
    private float ogx;
    private float ogy;

    void Start()
    {
        ogx = transform.position.x;
        ogy = transform.position.y;
    }


    private void Update()
    {
        
        if (count < wandCount)
        {
            Instantiate(wandPrefab, spawnPosition);
            count++;

            transform.position = new Vector3(theta,ogy, Sqrt((zDeviationfromZero*zDeviationfromZero) - (theta*theta)));
            theta += step;
            // -1,1 => cos(x)*18
        }
        
        
        
        //Instantiate(wandPrefab, spawnPosition)
        //DVD here, made changes cuz too many wands to keep track of in the spector for debugging. I set it as serialized, change it in the inspector or die
    }
}
