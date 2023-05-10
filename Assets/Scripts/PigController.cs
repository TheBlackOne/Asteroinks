using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public float maxVelocity;
    public float maxAngularVelocity;
    public int poolIndex;
    private Rigidbody2D rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Reset()
    {
        var newVelocity = new Vector2(Random.Range(-maxVelocity, maxVelocity), Random.Range(-maxVelocity, maxVelocity));

        Debug.LogFormat("Setting velocity to {0}", newVelocity);
        rb.velocity = newVelocity;

        float newAngularVelocity = Random.Range(-maxAngularVelocity, maxAngularVelocity);
        Debug.LogFormat("Setting angular velocity to {0}", newAngularVelocity);
        rb.angularVelocity = newAngularVelocity;
    }
}
