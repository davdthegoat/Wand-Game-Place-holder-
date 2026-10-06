using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class WorkStationSit : MonoBehaviour
{
    //This script is meant to seat Mr.Bean to the workstation. Depending on how things unfold, it might also be used for freezing the camera and letting go of Mr.Bean.
     
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSitPosition;
    [SerializeField] private float interactRange = 3f;


    //Previous attempts of transforming and rotating have failed due to interference from another script, likely Movement. This is an attempt to enable and disable that script on Mr.Bean
    [SerializeField] private Movement movement;
    //According to Unity documentation, instantaneous transformation with a character controller component can also cause jittering, so I'll try freezing that too.
    [SerializeField] private CharacterController controller;

    private Camera playerCamera;
    public bool isSitting = false;
    //Someone on Unity discord told me to keep isSitting private, and make a method/getter which returns a public version so that no other script can alter isSitting's value... idk if all'at is necessary tho, seemed like a lot of work. I am just changing it to public.
    //Cuz I need to use it's value in WandBoxSelectionScript.
    //It didn't work, I'll try his method.
    void Start()
    {
        playerCamera = Camera.main; //Makes the main camera linked with var playerCamera.
    }

    private void TrySit()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("WorkBench"))
            {
                //Trying to figure out why it won't work/jitter right now.
                //Debug.Log("Current postion: sitting");

                //Attempt to stop jittering caused by multi-script interference.
                movement.enabled = false;
                controller.enabled = false;


                player.transform.position = playerSitPosition.position;
                player.transform.rotation = playerSitPosition.rotation;

                //Cursor unlocking for item dragging
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    private void TryStand()
    {
        movement.enabled = true;
        controller.enabled = true;
        isSitting = false;

        Cursor.lockState = CursorLockMode.Locked; //Returns the game to FPP, instead of point-and-click
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //Trying to set it to a different keybind. Maybe pressing F is conflicting with StoreInShelfScript causing the movement-jitters.
        {
            if (isSitting == false)
            {
                TrySit();
                isSitting = true;
            }
            else
            {
                TryStand();
                player.transform.rotation = Quaternion.identity; //Should make it so that Mr.Bean stands up-straight when returned from PlayerSittingPosition.
            }
        }
    }

    public bool IsSitting()
    {
        return isSitting;
    }
}
