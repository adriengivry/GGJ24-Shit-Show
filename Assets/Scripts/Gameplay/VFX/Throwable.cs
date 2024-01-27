using UnityEngine;

public class Throwable : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_travelTime = 1.0f;

    private Vector2 m_destination;
    private float m_strength;

    public void Throw(Vector2 from, Vector2 to, float strength = 10.0f)
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(from.x, from.y, transform.position.z);
        m_destination = to;
        m_strength = strength;
    }

    private void Update()
    {
        float step = m_strength * Time.deltaTime / m_travelTime; // Calculate the step based on strength and time

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(m_destination.x, m_destination.y, transform.position.z), step);

        // Check if the object has reached the destination
        if (Vector2.Distance(transform.position, m_destination) < 0.01f)
        {
            gameObject.SetActive(false); // Deactivate the object when it reaches the destination
        }
    }
}