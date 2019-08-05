using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GattlingBullet : MonoBehaviour
{
    public float startVelocity = 50f;
    public float lifetime = 30f;
    public Rigidbody rigidbody;

    private float startTime;
    private void Start()
    { 
        rigidbody.AddForce(transform.right * startVelocity, ForceMode.VelocityChange);
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Terrain>())
        {
            Debug.Log("Hit the floor");
            Destroy(gameObject);
        }
    }
}
