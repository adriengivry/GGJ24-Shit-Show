using UnityEngine;

public class Throwable : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_travelTime = 1.0f;
    [SerializeField] private float m_rotationSpeed = 360.0f;

    [Header("References")]
    [SerializeField] private InstancePool m_splashPool;
    [SerializeField] private AudioSource m_audiosource;

    private Vector2 m_destination;
    private float m_rotationAngle = 0.0f;
    private float m_strength;

    public void Throw(Vector2 from, Vector2 to, float strength)
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(from.x, from.y, transform.position.z);
        m_destination = to;
        m_strength = strength;
        m_rotationAngle = 0.0f;
    }

    private void Update()
    {
        // Rotate the object around the Z-axis
        m_rotationAngle += m_rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, m_rotationAngle);

        float step = m_strength * Time.deltaTime / m_travelTime; // Calculate the step based on strength and time

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(m_destination.x, m_destination.y, transform.position.z), step);

        // Check if the object has reached the destination
        if (Vector2.Distance(transform.position, m_destination) < 0.01f)
        {
            if (m_audiosource.clip != null) m_audiosource.Play();
            GameObject splash = m_splashPool.GetAvailableInstance();
            splash.transform.position = transform.position;
            splash.SetActive(true);
            gameObject.SetActive(false); // Deactivate the object when it reaches the destination
        }
    }
}
