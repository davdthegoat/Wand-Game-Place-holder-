using UnityEngine;

public class WorkStationWandBoxColliderManager : MonoBehaviour
{
    [SerializeField] Collider WandBoxCollider;
    [SerializeField] Collider WandLidCollider;
    [SerializeField] Collider WandBoxContainerCollider;

    private void Awake()
    {
        WandLidCollider.enabled = false;
        WandBoxContainerCollider.enabled = false;
        WandBoxCollider.enabled = true;
    }

    public void SetBoxOpen()
    {
        WandLidCollider.enabled = true;
        WandBoxContainerCollider.enabled = true;
        WandBoxCollider.enabled = false;
    }

    public void SetBoxClosed()
    {
        WandLidCollider.enabled = false;
        WandBoxContainerCollider.enabled = false;
        WandBoxCollider.enabled = true;
    }
}
