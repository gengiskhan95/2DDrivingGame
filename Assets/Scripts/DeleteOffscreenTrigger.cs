using UnityEngine;

public class DeleteOffscreenTrigger : MonoBehaviour
{
    [SerializeField] private float offscreenDespawnDelay = 3f;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NpcCar"))
        {
            Destroy(other.gameObject, offscreenDespawnDelay);
        }
    }
}
