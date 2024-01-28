using UnityEngine;

public class TomatoPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddTomato();
            Destroy(gameObject);
        }
    }
}
