using UnityEngine;

public class ItemPositionReset : MonoBehaviour
{
    private Vector3 startingPos;
    private Quaternion startingRotation;

    private Rigidbody rb;

    private void Start()
    {
        startingPos = transform.position;
        startingRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
    }

    public void ResetItem()
    {
        transform.position = startingPos;
        transform.rotation = startingRotation;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
