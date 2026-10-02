using System;

using UnityEngine;

public class WandSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject wandPrefab;

    [SerializeField] int wandCount;

    public Transform spawnPosition;
    private int count = 0;
    private void Update()
    {
        
        if (count < wandCount)
        {
            Instantiate(wandPrefab, spawnPosition);
            count++;
        }
        
        //Instantiate(wandPrefab, spawnPosition)
        //DVD here, made changes cuz too many wands to keep track of in the spector for debugging. I set it as serialized, change it in the inspector or die
    }
}
