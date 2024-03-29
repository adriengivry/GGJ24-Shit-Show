using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private EMovementDirection m_initialDirection;

    [Header("References")]
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
        if (directions.Count == 1)
        {
            return directions.First();
        }

        // If more than one direction is available, make sure to always avoid going back on your steps
        var filteredDirections = directions.Where(dir => !MovementUtils.AreOppositeDirections(dir, m_character.CurrentDirection));

        // Check if the HashSet is not empty
        if (filteredDirections.Count() > 0)
        {
            // Get a random index within the HashSet's count
            int randomIndex = UnityEngine.Random.Range(0, filteredDirections.Count());

            // Use an extension method to get the element at the random index
            EMovementDirection randomDirection = filteredDirections.ElementAt(randomIndex);

            return randomDirection;
        }
        else
        {
            // Handle the case where the HashSet is empty
            throw new InvalidOperationException("The HashSet is empty");
        }
    }
}
