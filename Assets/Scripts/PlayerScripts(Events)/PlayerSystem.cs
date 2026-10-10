using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerSystem : MonoBehaviour
{

    protected PlayerIdentification playerIdentification;

    protected virtual private void Awake()
    {
        playerIdentification = transform.root.GetComponent<PlayerIdentification>();
        //if you want to call this in a function that alr uses Awake use as follows
        //Replace the awake code with =>
        /*protected override void Awake()
        {
            base.Awake();
            ~Do Whatever you need to~
        }*/
    }   //This ensures a reference to the player Identification class does happen and is not just overriden and ignored
}
