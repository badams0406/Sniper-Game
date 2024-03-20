using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy
    public int currentHealth; // Current health of the enemy
    public MoneyHUD MoneyHUD;
    public BoxCollider[] rooms;
    public AudioSource krabsWalk;

    Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {

        destination = findRandomFloorPositionInRoom(rooms, transform.position.y);
        gameObject.transform.position = destination;

        currentHealth = maxHealth; // Set current health to max health when the enemy starts
        if (MoneyHUD == null)
        {
            // Try to find the MoneyHUD component in the scene if not assigned
            MoneyHUD = FindObjectOfType<MoneyHUD>();
        }
        
        krabsWalk.Play();
    }

    public void Update()
    {

        if (Vector3.Distance(transform.position, destination) < 0.1)
        {
            destination = findRandomFloorPositionInRoom(rooms, transform.position.y);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, 0.1f);
        }

        TakeDamage();
    }
    // Method to handle taking damage
    public void TakeDamage()
    {
        // Check if the enemy is dead
        if (currentHealth <= 0)
        {
            Die(); // Call the Die method if health reaches or falls below 0
        }
    }

    // Method to handle enemy death
    void Die()
    {
        Destroy(gameObject); // Destroy the enemy GameObject
        // Add $500 to the total balance
        if (MoneyHUD != null)
        {
            MoneyHUD.Balance += 500;
        }
    }

    // Method to handle collision with other GameObjects
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding GameObject has the Sniper_Bullet script attached
        if (other.gameObject.GetComponent<Sniper_Bullet>() != null)
        {
            // Get the damage from the bullet
            int bulletDamage = other.gameObject.GetComponent<Sniper_Bullet>().damage;

            // Apply damage to the enemy
            TakeDamage();

            // Destroy the bullet
            Destroy(other.gameObject);
        }
    }

    Vector3 findRandomFloorPositionInRoom(BoxCollider[] rooms, float y)
    {
        if (rooms != null && rooms.Length > 0) // Check if rooms array is not null and has elements
        {
            BoxCollider room = rooms[Random.Range(0, rooms.Length)];

            Vector3 pos = room.transform.position;
            Vector3 size = room.size;

            Vector3 newPos = new Vector3(
                Random.Range(pos.x - size.x / 2, pos.x + size.x / 2),
                y,
                Random.Range(pos.z - size.z / 2, pos.z + size.z / 2)
            );

            transform.LookAt(newPos);
            gameObject.transform.eulerAngles = new Vector3(
                -90,
                gameObject.transform.eulerAngles.y,
                gameObject.transform.eulerAngles.z
            );

            return newPos;
        }
        else
        {
            return Vector3.zero; // Return a default position
        }
    }
}
