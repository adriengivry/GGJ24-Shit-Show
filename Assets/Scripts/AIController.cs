using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EMovementDirection m_initialDirection;
    [SerializeField] private Character m_character;

    // Start is called before the first frame update
    void Start()
    {
        m_character.SetTargetDirection(m_initialDirection);
    }

    private void OnEnable()
    {
        m_character.FlagReachedEvent.AddListener(OnFlagReached);
    }

    private void OnDisable()
    {
        m_character.FlagReachedEvent.RemoveListener(OnFlagReached);
    }

    private void OnFlagReached(Flag flag)
    {
        var direction = GetRandomDirection(flag.AllowedDirections);
        m_character.SetTargetDirection(direction);
    }

    private EMovementDirection GetRandomDirection(HashSet<EMovementDirection> directions)
    {
        // Check if the HashSet is not empty
        if (directions.Count > 0)
        {
            // Get a random index within the HashSet's count
            int randomIndex = UnityEngine.Random.Range(0, directions.Count);

            // Use an extension method to get the element at the random index
            EMovementDirection randomDirection = directions.ElementAt(randomIndex);

            return randomDirection;
        }
        else
        {
            // Handle the case where the HashSet is empty
            throw new InvalidOperationException("The HashSet is empty");
        }
    }
}
