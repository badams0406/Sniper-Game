using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper_Bullet : MonoBehaviour
{
    public Vector3 target;
    public float bulletSpeed = 10f;
    public int damage = 100;
    public void setTarget(Vector3 t)
    {
        target = t;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, target) < 0.5f)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, bulletSpeed * Time.deltaTime);
        }
    }
}
