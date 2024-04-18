using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.ParticleSystemJobs;
using Unity.VisualScripting;

public class SniperController : MonoBehaviour
{
    public AudioSource sniper_shot;
    public Camera playerCamera;
    public GameObject idleGun;
    public GameObject readyGun;
    public GameObject scope;
    public GameObject bulletImpactPrefab;

    public UnityEngine.UI.Image[] UIBullets;

    public bool Suppre;
    public bool Extendo;
    public bool Laser;

    public float lookSpeed = 2f;
    public float lookXLimiter = 45f;
    public float aimTimer = 0.4f;
    public int ammo;
    float rotationX = 0;
    float rotationY = 0;

    public float recoilAmount = 10f;  // Smaller, more controlled recoil
    public float recoilRecoverySpeed = 2f;  // Fast recovery to initial position

    private float currentRecoil = 0f;  // Current applied recoil that needs to be recovered
    private float recoilImpact = 0f;

    public float minRecoilAmount = 1f;  // Min recoil amount
    public float maxRecoilAmount = 3f;  // Max recoil amount

    public float minHorizontalRecoil = -2f;
    public float maxHorizontalRecoil = 2f;
    private float horizontalRecoilImpact = 0f;


    public LineRenderer leLine;
    public void Start()
    {
        LoadPlayer();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        idleGun.SetActive(true);
        readyGun.SetActive(false);
        scope.SetActive(false);
        sniper_shot.volume = 0.2f;  //0.2f should be 100%
        if (Extendo == false)
        {
            ammo = 5;
            System.Array.Resize(ref UIBullets, 5);
            for (int i = 0; i < UIBullets.Length; i++) // Show 5 UI Bullets
            {
                UIBullets[i].gameObject.SetActive(true);
            }
        }
        else
        {
            ammo = 10;
            for (int i = 0; i < UIBullets.Length; i++) // Show 10 UI Bullets
            {
                UIBullets[i].gameObject.SetActive(true);
            }
        }
        if (Suppre == true)
        {
            sniper_shot.volume = 0.025f; // Gun sounds quieter after buying suppressor
        }
        if (leLine != null)
        {
            if (Laser == true)
            {
                leLine.gameObject.SetActive(true);
            }
            else
            {
                leLine.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("LineRenderer (leLine) is not assigned. Please assign it in the Inspector.");
        }
        resetBulletCountUI();
    }
    void Update()
    {
        float mouseY = -Input.GetAxis("Mouse Y") * lookSpeed;
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;

        rotationX += mouseY;  // Apply vertical mouse movement (pitch)
        rotationY += mouseX;  // Apply horizontal mouse movement (yaw)

        // Apply immediate horizontal recoil impact to yaw
        rotationY += horizontalRecoilImpact;
        horizontalRecoilImpact = 0; // Reset after applying

        // Manage vertical recoil recovery
        if (currentRecoil > 0)
        {
            float recoveryAmount = Time.deltaTime * recoilRecoverySpeed;
            currentRecoil -= recoveryAmount;
            rotationX += Mathf.Min(recoveryAmount, currentRecoil);  // Recover vertical recoil
        }

        // Clamp the angles to prevent excessive rotation
        rotationX = Mathf.Clamp(rotationX, -lookXLimiter, lookXLimiter);
        rotationY = Mathf.Clamp(rotationY, -360, 360); // Optionally clamp rotationY if needed

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);

        gunFunction();
    }

    private void ApplyRecoil()
    {
        if (ammo >= 1)
        {
            float randomRecoil = Random.Range(minRecoilAmount, maxRecoilAmount);
            float randomHorizontalRecoil = Random.Range(minHorizontalRecoil, maxHorizontalRecoil);

            currentRecoil += randomRecoil;          // Update the total vertical recoil
            recoilImpact += randomRecoil;           // Immediate vertical recoil effect

            horizontalRecoilImpact += randomHorizontalRecoil;  // Immediate horizontal recoil effect
        }
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
                    if (ammo >= 1)
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            // Update speed of pedestrians
                            Pedestrian[] pedestrians = FindObjectsOfType<Pedestrian>();


                            foreach (Pedestrian pedestrian in pedestrians)
                            {
                                pedestrian.speed = 0.05f;
                            }

                            Debug.Log(hit.transform.gameObject.name);

                            sniper_shot.Play();
                            ammo -= 1;
                            UIBullets[ammo].enabled = false;
                            ApplyRecoil();
                            if (hit.transform.gameObject.tag == "Enemy")
                            {
                                // If the ray hits an enemy, decrease its health
                                hit.transform.gameObject.GetComponent<EnemyController>().currentHealth -= 100;
                            }
                            else if (hit.transform.gameObject.tag == "Pedestrian")
                            {
                                hit.transform.gameObject.GetComponent<Pedestrian>().onHit();
                            }
                            else if (hit.transform.gameObject.tag == "Bomb")
                            {
                                hit.transform.gameObject.GetComponent<Bomb>().Explode();
                            }
                            else
                            {
                                // If the ray doesn't hit an enemy, instantiate a bullet impact effect
                                Instantiate(bulletImpactPrefab, hit.point, Quaternion.identity);

                            }
                        }
                    }
                    if (ammo <= 0)
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

        if (Input.GetKeyDown(KeyCode.R) && ammo != 5 && Extendo == false)
        {
            ammo = 5;
            resetBulletCountUI();
            Debug.Log("RELOADED(5)!");
        }
        if (Input.GetKeyDown(KeyCode.R) && ammo != 10 && Extendo == true)
        {
            ammo = 10;
            resetBulletCountUI();
            Debug.Log("RELOADED(10)!");
        }
    }

    public void resetBulletCountUI()
    {
        int ammo = Extendo ? 10 : 5;
        for (int i = 0; i < ammo; i++)
        {
            UIBullets[i].enabled = true;
        }
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            Suppre = data.boughtSuppressor;
            Extendo = data.boughtExtendedMag;
            Laser = data.boughtLaserAttach;
        }
        else
        {
            Debug.Log("Player data not found or could not be loaded. (IGNORE THIS)");
        }
    }

}