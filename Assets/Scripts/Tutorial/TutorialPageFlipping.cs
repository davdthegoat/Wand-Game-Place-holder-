using System;
using UnityEngine;

public class TutorialPageFlipping : MonoBehaviour
{
    //This script is reasonable for flipping through pages of the opening tutorial, as well as displaying new information as it is unlocked through the game using the "page mechanics".
    //Tutorial book will be visible at the start, then disabled when initial tutorial is finished, and enabled again only when a page is brought to it, meaning it probably needs a connection to heldObjectPlacement script
    //Gonna implement the first bit of what I said after I have breakfast.
    [SerializeField] Transform rightPagePosition;
    [SerializeField] Transform leftPagePosition;
    [SerializeField] GameObject tutorialBook;
    [SerializeField] GameObject mrBean;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mrBean.GetComponent<Movement>().enabled = false; //Disable player movement on start.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
