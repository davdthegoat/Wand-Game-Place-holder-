using System;
using Mono.Cecil.Cil;
using UnityEngine;

public class WandSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject wandPrefab;

    public Transform spawnPosition;
    private int count = 0;
    private int maxWands = 50;
    private void Update()
    {
        
        if (count <= maxWands)
        {
            Instantiate(wandPrefab, spawnPosition);
            count++;
        }
        
        //Instantiate(wandPrefab, spawnPosition);
        

        


    }
}
