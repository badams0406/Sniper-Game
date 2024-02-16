using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject bulletImpactPrefab; // Prefab for the bullet impact

    // Update is called once per frame
    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;

        // Cast a ray from the camera's position forward
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                // If the ray hits an enemy, decrease its health
                hit.transform.gameObject.GetComponent<EnemyController>().currentHealth -= 100;
            }
            else
            {
                // If the ray doesn't hit an enemy, instantiate a bullet impact effect
                Instantiate(bulletImpactPrefab, hit.point, Quaternion.identity);
            }
        }
    }
}
