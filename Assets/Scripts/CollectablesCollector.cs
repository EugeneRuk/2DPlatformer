using UnityEngine;

[RequireComponent(typeof(Collectable))]
public class CollectablesCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            Destroy(gameObject);
        }
    }
}