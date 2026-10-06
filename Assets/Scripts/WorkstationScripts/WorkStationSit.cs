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
    private bool isSitting = false;
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
            if (hit.collider.CompareTag("WorkBench") && isSitting == false)
            {
                //Trying to figure out why it won't work/jitter right now.
                //Debug.Log("Current postion: sitting");

                //Attempt to stop jittering caused by multi-script interference.
                movement.enabled = false;
                controller.enabled = false;


                player.transform.position = playerSitPosition.position;
                player.transform.rotation = playerSitPosition.rotation;

                isSitting = true;
            }

            if (hit.collider.CompareTag("WorkBench") && isSitting == true)
            {
                movement.enabled = true;
                controller.enabled = true;
                isSitting = false;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //Trying to set it to a different keybind. Maybe pressing F is conflicting with StoreInShelfScript causing the movement-jitters.
        {
            TrySit();
            Debug.Log("Q was pressed"); //I think it's taking like 20 inputs of Q at once, causing this seating-unseating issue...
        }
    }
}
