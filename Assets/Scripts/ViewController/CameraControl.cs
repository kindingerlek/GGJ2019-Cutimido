using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Transform objectToFollow;
    // Movement speed in units/sec.
    public float speed = 1.0F;
    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    private float journeyLength;

    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;
        if (!objectToFollow)
            Debug.LogError("Coloca o player seu dev safado.");

    }

    // Update is called once per frame
    void Update()
    {
        journeyLength = Vector3.Distance(this.transform.position, objectToFollow.position + offset);
        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;
        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

       // print(fracJourney);

        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(objectToFollow.position.x + offset.x,transform.position.y,objectToFollow.position.z + offset.z) , speed * Time.deltaTime);
    }

    public void SetObjectToFollow(Transform obj) {
        objectToFollow = obj;
    }
}
