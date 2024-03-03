using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperController : MonoBehaviour
{
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimiter = 45f;
    public GameObject idleGun;
    public GameObject readyGun;
    public float aimTimer = 0.4f;
    public GameObject scope;
    public AudioSource sniper_shot;
    public int Ammo;
    public GameObject bulletImpactPrefab;

    float rotationX = 0;
    float rotationY = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        idleGun.SetActive(true);
        readyGun.SetActive(false);
        scope.SetActive(false);
        Ammo = 5;

    }

    void Update()
    {
        // Character Rotation/Character Movement
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY += Input.GetAxis("Mouse X") * lookSpeed;

        rotationX = Mathf.Clamp(rotationX, -lookXLimiter, lookXLimiter);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        gunFunction();
    }

    public void gunFunction()
    {
        RaycastHit hit;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            idleGun.SetActive(false);
            readyGun.SetActive(true);
            aimTimer -= Time.deltaTime;

            if (aimTimer <= 0f)
            {
                scope.SetActive(true);
                playerCamera.fieldOfView = 20;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
                {
                    if (Ammo >= 1)
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            sniper_shot.Play();
                            Ammo -= 1;
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
                    if (Ammo <= 0)
                    {
                        Debug.Log("OUT OF AMMO!");
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            idleGun.SetActive(true);
            readyGun.SetActive(false);
            aimTimer = 0.4f;
            scope.SetActive(false);
            playerCamera.fieldOfView = 60;
        }

        if (Input.GetKeyDown(KeyCode.R) && Ammo != 5)
        {
            Ammo = 5;
            Debug.Log("RELOADED!");
        }
    }
}
