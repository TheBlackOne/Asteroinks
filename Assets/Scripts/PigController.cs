using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public float maxVelocity;
    public float maxAngularVelocity;
    public int poolIndex;

    private Rigidbody2D rigidBody;
    private PigsManager pigsManager;
    private BirdsManager birdsManager;

    public void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        pigsManager = GameObject.Find("PigsManager").GetComponent<PigsManager>();
        birdsManager = GameObject.Find("BirdsManager").GetComponent<BirdsManager>();
    }

    public void Reset(Vector2 position, int _poolIndex)
    {
        transform.position = position;

        float newRotationDegrees = Random.Range(0f, 360f);
        transform.Rotate(0.0f, 0.0f, newRotationDegrees);

        var newVelocity = new Vector2(Random.Range(-maxVelocity, maxVelocity), Random.Range(-maxVelocity, maxVelocity));

        rigidBody.velocity = newVelocity;

        float newAngularVelocity = Random.Range(-maxAngularVelocity, maxAngularVelocity);
        rigidBody.angularVelocity = newAngularVelocity;

        poolIndex = _poolIndex;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bird")
        {
            pigsManager.PigHit(gameObject, poolIndex);
            birdsManager.DespawnBird(collision.gameObject);
        }
    }
}