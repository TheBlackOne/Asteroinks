using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float _velocity;
    [SerializeField] private float _maxAngularVelocity;
    [SerializeField] private float _lifetime;

    private Rigidbody2D _rigidBody;
    private BirdsManager _birdsManager;
    private float _spawnTime;

    public void Reset(Vector2 position, Vector3 direction)
    {
        transform.position = position;

        float newRotationDegrees = Random.Range(0f, 360f);
        transform.Rotate(0.0f, 0.0f, newRotationDegrees);

        _rigidBody.AddForce(direction * _velocity, ForceMode2D.Impulse);

        _spawnTime = Time.time;
    }
    public void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _birdsManager = GameObject.Find("BirdsManager").GetComponent<BirdsManager>();
    }
    public void Update()
    {
        if (_spawnTime + _lifetime < Time.time)
        {
            _birdsManager.DespawnBird(gameObject);
        }
    }
}
