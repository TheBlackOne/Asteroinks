using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _maxAngularVelocity;
    
    private int _poolIndex;
    private Rigidbody2D _rigidBody;
    private PigsManager _pigsManager;
    private BirdsManager _birdsManager;
    private ShipController _shipController;

    public void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _pigsManager = GameObject.Find("PigsManager").GetComponent<PigsManager>();
        _birdsManager = GameObject.Find("BirdsManager").GetComponent<BirdsManager>();
        _shipController = GameObject.Find("Ship").GetComponent<ShipController>();
    }

    public void Reset(Vector2 position, int poolIndex)
    {
        transform.position = position;

        float newRotationDegrees = Random.Range(0f, 360f);
        transform.Rotate(0.0f, 0.0f, newRotationDegrees);

        var newVelocity = new Vector2(Random.Range(-_maxVelocity, _maxVelocity), Random.Range(-_maxVelocity, _maxVelocity));

        _rigidBody.velocity = newVelocity;

        float newAngularVelocity = Random.Range(-_maxAngularVelocity, _maxAngularVelocity);
        _rigidBody.angularVelocity = newAngularVelocity;

        _poolIndex = poolIndex;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bird")
        {
            _pigsManager.PigHit(gameObject, _poolIndex);
            _birdsManager.DespawnBird(collision.gameObject);
        }

        if (collision.gameObject.tag == "Ship")
        {
            _pigsManager.PigHit(gameObject, _poolIndex);
            _shipController.ShipHit();
        }
    }
}