using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public float maxVelocity;
    public float maxAngularVelocity;
    public int poolIndex;
    private Rigidbody2D rigidBody;

    public void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Reset(Vector2 position)
    {
        Debug.LogFormat("Setting position to {0}...", position);
        transform.position = position;

        var newVelocity = new Vector2(Random.Range(-maxVelocity, maxVelocity), Random.Range(-maxVelocity, maxVelocity));

        Debug.LogFormat("Setting velocity to {0}", newVelocity);
        rigidBody.velocity = newVelocity;

        float newAngularVelocity = Random.Range(-maxAngularVelocity, maxAngularVelocity);
        Debug.LogFormat("Setting angular velocity to {0}", newAngularVelocity);
        rigidBody.angularVelocity = newAngularVelocity;
    }
}
