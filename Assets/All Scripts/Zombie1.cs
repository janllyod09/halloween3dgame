using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie1 : MonoBehaviour
{
    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform LookPoint;
    public Transform playerBody;
    public LayerMask PlayerLayer;

    [Header("Zombie Guarding Var")]
    public GameObject[] walkPoints;
    int currentZombiePosition = 0;
    public float patrollingSpeed;
    public float chasingSpeed;

    [Header("Zombie Animation")]
    public Animator anim;

    [Header("Zombie Mood/States")]
    public float visionRadius;
    public float attackingRadius;
    public float maxDistanceFromStartingPoint;
    public bool playerInvisionRadius;
    public bool playerInattackingRadius;

    private Vector3 startingPosition;

    private bool isAttacking = false;
    public float attackAnimationDuration = 2.0f;

    private void Awake()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
        startingPosition = transform.position;
    }

    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        if (!playerInvisionRadius && !playerInattackingRadius)
        {
            if (Vector3.Distance(transform.position, startingPosition) > maxDistanceFromStartingPoint)
            {
                ReturnToStartingPosition();
            }
            else
            {
                Guard();
            }
        }
        else if (playerInvisionRadius && !playerInattackingRadius)
        {
            PursuePlayer();
        }
        else if (playerInattackingRadius)
        {
            StopMovement();
            AttackPlayer();
        }
    }

    private void Guard()
    {
        if (zombieAgent.remainingDistance <= zombieAgent.stoppingDistance)
        {
            currentZombiePosition = (currentZombiePosition + 1) % walkPoints.Length;
            zombieAgent.SetDestination(walkPoints[currentZombiePosition].transform.position);
        }

        zombieAgent.speed = patrollingSpeed;
    }

    private void PursuePlayer()
    {
        if (!zombieAgent.isStopped)
        {
            zombieAgent.SetDestination(playerBody.position);
            zombieAgent.speed = chasingSpeed;

            anim.SetBool("Walking", false);
            anim.SetBool("Running", true);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", false);
        }
    }

    private void StopMovement()
    {
        zombieAgent.ResetPath();
        zombieAgent.speed = 0f;

        anim.SetBool("Walking", false);
        anim.SetBool("Running", false);
        anim.SetBool("Attacking", true);
        anim.SetBool("Died", false);
    }

    private void AttackPlayer()
    {
        // Check if the player is within attacking range
        if (Vector3.Distance(transform.position, playerBody.position) <= attackingRadius)
        {
            if (!isAttacking)
            {
                isAttacking = true;

                // Stop the zombie from moving during the attack animation
                zombieAgent.isStopped = true;
                zombieAgent.velocity = Vector3.zero;

                anim.SetBool("Walking", false);
                anim.SetBool("Running", false);
                anim.SetBool("Attacking", true);
                anim.SetBool("Died", false);

                int damageAmount = 10; // Change the value as per your desired damage amount

                // Damage the player
                DamagePlayer(damageAmount);

                // Start a coroutine to resume pursuing the player after a delay
                StartCoroutine(ResumePursuitAfterDelay(attackAnimationDuration));
            }
        }
        else
        {
            // If the player is out of range, resume pursuing immediately
            isAttacking = false;
            PursuePlayer();
        }
    }

    private IEnumerator ResumePursuitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        isAttacking = false;
        PursuePlayer();
    }

    private void ReturnToStartingPosition()
    {
        zombieAgent.SetDestination(startingPosition);
        zombieAgent.speed = patrollingSpeed;

        anim.SetBool("Walking", true);
        anim.SetBool("Running", false);
        anim.SetBool("Attacking", false);
        anim.SetBool("Died", false);
    }

    private void DamagePlayer(int damageAmount)
    {
        // Check if the player has a PlayerHealth script attached
        PlayerHealth playerHealth = playerBody.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            // Call the TakeDamage method on the player's PlayerHealth script
            playerHealth.TakeDamage(damageAmount);
        }
    }
}