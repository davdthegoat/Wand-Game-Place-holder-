//Attached to the tutorial-book.
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
    private Boolean page; //True is left and false is right page. We start from left page
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mrBean.GetComponent<Movement>().enabled = false; //Disable player movement on start.
        page = true;
    }

    void FlipPage()
    {
        if (page == false)
        {
            tutorialBook.transform.position = leftPagePosition.position; //Move the book so that left page is visible
        }
        else
        {
            tutorialBook.transform.position = rightPagePosition.position;
        }
        page = !page; //Flip value of page, preparing it for next instance of FlipPage();
    }

    public void TurnPage() //Going to call this function to turn the page.
    {
        FlipPage();
    }

    void OnMouseDown()
    {
        TurnPage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
