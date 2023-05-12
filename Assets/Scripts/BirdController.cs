using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float velocity;
    public float maxAngularVelocity;
    public float lifetime;

    private Rigidbody2D rigidBody;
    private BirdsManager birdsManager;
    private float spawnTime;

    public void Reset(Vector2 position, Vector3 direction)
    {
        Debug.LogFormat("Setting position to {0}...", position);
        transform.position = position;


        float newRotationDegrees = Random.Range(0f, 360f);
        Debug.LogFormat("Setting rotation to {0}", newRotationDegrees);
        transform.Rotate(0.0f, 0.0f, newRotationDegrees);

        rigidBody.AddForce(direction * velocity, ForceMode2D.Impulse);

        spawnTime = Time.time;
    }
    public void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        birdsManager = GameObject.Find("BirdsManager").GetComponent<BirdsManager>();
    }
    public void Update()
    {
        if (spawnTime + lifetime < Time.time)
        {
            birdsManager.DespawnBird(gameObject);
        }
    }
}
