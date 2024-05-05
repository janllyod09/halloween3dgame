using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSaLabas : MonoBehaviour
{
    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public GameObject[] walkPoints;
    public float walkingSpeed = 3f;

    [Header("Zombie Animation")]
    public Animator zombieAnimator;

    private int currentWaypointIndex = 0;
    public AudioSource zombieAudioSource;

    private void Awake()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
        zombieAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SetDestinationToNextWaypoint();
    }

    private void Update()
    {
        // Check if the zombie has reached the current waypoint
        if (!zombieAgent.pathPending && zombieAgent.remainingDistance < 0.5f)
        {
            SetDestinationToNextWaypoint();
            
        }
    }

    private void SetDestinationToNextWaypoint()
    {
        // Check if there are any waypoints
        if (walkPoints.Length == 0)
        {
            Debug.LogWarning("No walk points assigned to the zombie.");
            return;
        }

        // Set the destination to the next waypoint
        zombieAgent.SetDestination(walkPoints[currentWaypointIndex].transform.position);

        // Increment the waypoint index or wrap around to the first waypoint
        currentWaypointIndex = (currentWaypointIndex + 1) % walkPoints.Length;

        // Set the zombie's speed to the walking speed
        zombieAgent.speed = walkingSpeed;
        zombieAnimator.SetBool("Walking", true);
        PlayZombieSound();
    }

    private void PlayZombieSound()
    {
        // Check if an audio clip is assigned to the audio source
        if (zombieAudioSource.clip == null)
        {
            Debug.LogWarning("No zombie sound clip assigned to the audio source.");
            return;
        }

        // Play the zombie sound effect
        zombieAudioSource.Play();
    }
}