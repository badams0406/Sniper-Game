using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SniperController : MonoBehaviour
{
    public AudioSource sniper_shot;
    public Camera playerCamera;
    public GameObject idleGun;
    public GameObject readyGun;
    public GameObject scope;
    public GameObject bulletImpactPrefab;

    public Image[] UIBullets;

    public float lookSpeed = 2f;
    public float lookXLimiter = 45f;
    public float aimTimer = 0.4f;
    public int ammo;
    float rotationX = 0;
    float rotationY = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        idleGun.SetActive(true);
        readyGun.SetActive(false);
        scope.SetActive(false);
        ammo = 5;
        sniper_shot.volume = 0.2f;  //0.2f should be 100%
        resetBulletCountUI();

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
                    if (ammo >= 1)
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            Debug.Log(hit.transform.gameObject.name);
                            
                            sniper_shot.Play();
                            ammo -= 1;
                            UIBullets[ammo].enabled = false;
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

        if (Input.GetKeyDown(KeyCode.R) && ammo != 5)
        {
            ammo = 5;
            resetBulletCountUI();
            Debug.Log("RELOADED!");
        }
    }

    public void resetBulletCountUI(){
        for(int i = 0; i < ammo; i++){
            UIBullets[i].enabled = true;
        }
    }

}