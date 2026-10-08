using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{ 
    //copied and pasted straight from https://github.com/DevUrf/2D-Unity-Enemy-AI/blob/main/Enemy.cs
    public enum AIState
    {
        Patrolling,
        Chasing,
        Attacking,
        Evading,
        Idle,
        Alert,
        Retreating,
        Fleeing
    }

    [Header("AI Configuration")]
    [Tooltip("Current AI state of the enemy.")]
    public AIState currentState = AIState.Patrolling;

    [Header("Movement Speeds")]
    [Tooltip("Patrol speed of the enemy. Recommended values: 1 - 2 for normal, 2 - 5 for fast patrols.")]
    [Range(0.5f, 5f)] public float patrolSpeed = 2f;
    [Tooltip("Chase speed of the enemy. Recommended values: 3 - 5 for normal, 5 - 10 for fast chases.")]
    [Range(0.5f, 10f)] public float chaseSpeed = 5f;

    [Header("Attack Settings")]
    [Tooltip("The range at which the enemy can attack the player. Good range: 1.5 - 2.5 units.")]
    [Range(0.5f, 5f)] public float attackRange = 1.5f;
    [Tooltip("Cooldown between attacks. Recommended values: 0.5 - 2 seconds.")]
    [Range(0f, 3f)] public float attackCooldown = 1f;

    [Header("Sight and Range Settings")]
    [Tooltip("The range at which the enemy detects the player. Values between 5 and 15 units work well.")]
    [Range(5f, 20f)] public float sightRange = 10f;
    [Tooltip("The range at which the enemy tries to evade. Recommended values: 2 - 5 units.")]
    [Range(1f, 5f)] public float evadeRange = 3f;
    [Tooltip("The range at which the enemy becomes alert to the player. Recommended range: 5 - 12 units.")]
    [Range(5f, 15f)] public float aggroRange = 8f;

    [Header("Health and Combat")]
    [Tooltip("Health of the enemy. Typical health for normal enemies: 50 - 100.")]
    [Range(1f, 200f)] public float health = 100f;
    [Tooltip("Can the enemy evade? Enable for more dynamic behavior.")]
    public bool canEvade = true;
    [Tooltip("Can the enemy attack the player?")]
    public bool canAttack = true;
    [Tooltip("Can the enemy chase the player? Enable to allow following behavior.")]
    public bool canChase = true;

    [Header("Behavior Modifiers")]
    [Tooltip("Health percentage threshold for retreating. Set between 0.2 (20%) and 0.5 (50%) for the best results.")]
    [Range(0f, 1f)] public float retreatHealthThreshold = 0.3f;

    [Header("Patrol Points")]
    [Tooltip("Array of patrol points. Set up waypoints in the scene to patrol between. At least 2 points needed.")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex;

    //[Header("Sound and Visuals")]
    //[Tooltip("Footstep sound clip to play during patrolling.")]
    //public AudioClip footstepSound;
    //private AudioSource audioSource;

    private float lastAttackTime;
    private Transform player;
    private bool isDead;
    private bool isRetreating;
    private bool isAlerting;

    private void Start()
    {
        // Initializing references and starting conditions
        player = GameObject.FindWithTag("Player")?.transform;
        lastAttackTime = Time.time;
        currentPatrolIndex = 0;
        isDead = false;
        isRetreating = false;
        isAlerting = false;
       // audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Exit early if the enemy is dead
        if (isDead) return;

        // Get the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Handle state transitions based on distance and other conditions
        HandleStateTransitions(distanceToPlayer);

        // Switch between states and execute behavior
        switch (currentState)
        {
            case AIState.Patrolling:
                PatrolBehavior();
                break;

            case AIState.Chasing:
                ChaseBehavior();
                break;

            case AIState.Attacking:
                AttackBehavior();
                break;

            case AIState.Evading:
                EvadeBehavior();
                break;

            case AIState.Idle:
                IdleBehavior();
                break;

            case AIState.Alert:
                AlertBehavior();
                break;

            case AIState.Retreating:
                RetreatBehavior();
                break;

            case AIState.Fleeing:
                FleeBehavior();
                break;
        }
    }

    // Handle transitions between AI states
    private void HandleStateTransitions(float distanceToPlayer)
    {
        if (isRetreating) return;

        // Transition to chasing state if the player is within sight range
        if (canChase && currentState != AIState.Attacking)
        {
            if (distanceToPlayer < sightRange)
            {
                currentState = AIState.Chasing;
            }
            else if (currentState == AIState.Chasing)
            {
                currentState = AIState.Patrolling;
            }
        }

        // Transition to evading state if the enemy is too close to the player
        if (canEvade && distanceToPlayer < evadeRange)
        {
            currentState = AIState.Evading;
        }

        // Transition to attacking state if the player is within attack range
        if (canAttack && distanceToPlayer <= attackRange)
        {
            currentState = AIState.Attacking;
        }

        // Transition to alert state if the player is within aggro range
        if (distanceToPlayer <= aggroRange && currentState != AIState.Chasing)
        {
            currentState = AIState.Alert;
        }

        // Transition to retreating if health is low enough
        if (health / 100f <= retreatHealthThreshold && currentState != AIState.Retreating)
        {
            currentState = AIState.Retreating;
        }

        // Transition to fleeing if health is critically low
        if (health / 100f <= 0.1f && currentState != AIState.Fleeing)
        {
            currentState = AIState.Fleeing;
        }
    }

    // Patrolling behavior
    private void PatrolBehavior()
    {
        if (patrolPoints.Length == 0) return;

        Vector2 target = patrolPoints[currentPatrolIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, target, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

       // PlayFootstepSound();
    }

    // Chasing behavior
    private void ChaseBehavior()
    {
        Vector2 target = player.position;
        transform.position = Vector2.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);
    }

    // Attack behavior
    private void AttackBehavior()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            // Implement attack logic here (e.g., deal damage to player)
            lastAttackTime = Time.time;
        }
    }

    // Evade behavior
    private void EvadeBehavior()
    {
        Vector2 directionToPlayer = (transform.position - player.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + directionToPlayer, chaseSpeed * Time.deltaTime);
    }

    // Idle behavior
    private void IdleBehavior() { }

    // Alert behavior
    private void AlertBehavior()
    {
        if (!isAlerting)
        {
            isAlerting = true;
            Debug.Log("Enemy is alert and looking for the player!");
        }
    }

    // Retreat behavior (move away from the player)
    private void RetreatBehavior()
    {
        Vector2 retreatPosition = new Vector2(-10f, -10f); // Placeholder for retreat position
        transform.position = Vector2.MoveTowards(transform.position, retreatPosition, patrolSpeed * Time.deltaTime);
    }

    // Flee behavior (move at max speed away from player)
    private void FleeBehavior()
    {
        Vector2 fleeDirection = (transform.position - player.position).normalized * 10f;
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)fleeDirection, patrolSpeed * Time.deltaTime);
    }

    // Take damage and handle death
    public void TakeDamageMethod(float damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            DieBehavior();
        }
    }

    // Handle death logic
    private void DieBehavior()
    {
        isDead = true;
        Debug.Log("Enemy Died");

        // Trigger death effects here, like particle effects or animations
        Destroy(gameObject);
    }

    // Play footstep sounds while patrolling
    //private void PlayFootstepSound()
    //{
    //    if (footstepSound != null && !audioSource.isPlaying)
    //    {
    //        audioSource.PlayOneShot(footstepSound);
    //    }
    //}
}