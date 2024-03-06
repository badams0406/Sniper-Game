using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy
    public int currentHealth; // Current health of the enemy
    public MoneyHUD MoneyHUD;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Set current health to max health when the enemy starts
        if (MoneyHUD == null)
        {
            // Try to find the MoneyHUD component in the scene if not assigned
            MoneyHUD = FindObjectOfType<MoneyHUD>();
        }
    }

    public void Update()
    {
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
        else
        {
            Debug.LogWarning("MoneyHUD reference is not set. Make sure to assign it in the Inspector or ensure it exists in the scene.");
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
}
