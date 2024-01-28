using UnityEngine;

public class TomatoPickup : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_scalingSpeed = 2.0f;

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * m_scalingSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddTomato();
            Destroy(gameObject);
        }
    }
}
