using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{

    float speed = 0.01f;

    Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        // Set position of pedestrian to a random position within the room
        destination = findRandomFloorPositionInRoom(transform.parent.gameObject, transform.position.y);
        transform.position = destination;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, destination) < 0.1) {
            destination = findRandomFloorPositionInRoom(transform.parent.gameObject, transform.position.y);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed);
        }
    }

    public void onHit () {
        Destroy(gameObject);
    }

    Vector3 findRandomFloorPositionInRoom (GameObject room, float y) {
        Vector3 pos = room.transform.position;
        Vector3 size = room.GetComponent<BoxCollider>().size;

        Vector3 newPos = new Vector3(
            Random.Range(pos.x - size.x / 2, pos.x + size.x / 2), 
            y, 
            Random.Range(pos.z - size.z / 2, pos.z + size.z / 2)
        );

        transform.LookAt(newPos);

        return newPos;
    }
}
