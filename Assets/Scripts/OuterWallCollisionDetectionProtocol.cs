using UnityEngine;

public class OuterWallCollisionDetectionProtocol : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ItemPositionReset item = other.GetComponent<ItemPositionReset>();

        if (item != null)
        {
            item.ResetItem();
        }
    }
}
