using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class GunShoot : MonoBehaviour {

	public float fireRate = 0.25f;										// Number in seconds which controls how often the player can fire
	public float weaponRange = 999f;                                     // Distance in Unity units over which the player can fire
    public float bulletSpeed = 50f; // Speed of the bullet

    public Transform gunEnd;
	public ParticleSystem muzzleFlash;
	public ParticleSystem cartridgeEjection;
    public GameObject bulletPrefab;

    private float nextFire;												// Float to store the time the player will be allowed to fire again, after firing
	private Animator anim;
	private GunAim gunAim;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		gunAim = GetComponentInParent<GunAim>();
	}

	void Update () 
	{
		if (Input.GetButtonDown("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			//muzzleFlash.Play();
			//cartridgeEjection.Play();
			//anim.SetTrigger ("GunFire");

			Vector3 rayOrigin = gunEnd.position;
			RaycastHit hit;
            if (Physics.Raycast(rayOrigin, gunEnd.forward, out hit, weaponRange))
			{
                HandleHit(hit);
			}
		}
	}

	void HandleHit(RaycastHit hit)
	{
        // Instantiate the bullet prefab at the bullet spawn point
        GameObject bullet = Instantiate(bulletPrefab, gunEnd.position, gunEnd.rotation);

        // Get the rigidbody component of the bullet
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        if (bulletRigidbody != null)
        {
            // Add force to the bullet to shoot it
            bulletRigidbody.AddForce(gunEnd.forward * bulletSpeed, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody component!");
        }
    }

	void SpawnDecal(RaycastHit hit, GameObject prefab)
	{
		GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
		spawnedDecal.transform.SetParent(hit.collider.transform);
	}
}