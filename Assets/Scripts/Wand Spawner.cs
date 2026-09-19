using System;
using UnityEngine;

public class WandSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject wandPrefab;

    public Transform spawnPosition;

    
    private void Update()
    {
        
        
        
        Instantiate(wandPrefab, spawnPosition);
        

        


    }
}
