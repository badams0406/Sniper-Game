using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public GameObject explosionEffect;
    public AudioSource explosionSound;
    public SphereCollider blastRadius;

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Explode()
    {
        // Create an explosion effect
        // Destroy the bomb
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosionSound.Play();

        // gameObject.MeshRenderer.enabled = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Get all objects within the blast radius
        Collider[] objectsInBlastRadius = Physics.OverlapSphere(transform.position, blastRadius.radius);

        foreach (Collider hit in objectsInBlastRadius)
        {
            if (hit.CompareTag("Pedestrian"))
            {
                // Do something with the pedestrian object
                hit.GetComponent<Pedestrian>().onHit();
            } else if (hit.CompareTag("Enemy"))
            {
                // Do something with the enemy object
                hit.GetComponent<EnemyController>().currentHealth -= 100;
            }
        }
    }
}
