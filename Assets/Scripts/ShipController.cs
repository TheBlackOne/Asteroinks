using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float _thrust;
    [SerializeField] private float _turnRate;

    private Rigidbody2D _rigidBody;
    private BirdsManager _birdsManager;
    private GameObject _birdOrigin;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    public void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _birdsManager = GameObject.Find("BirdsManager").GetComponent<BirdsManager>();
        _birdOrigin = GameObject.Find("BirdOrigin");
        _startPosition = gameObject.transform.position;
        _startRotation = gameObject.transform.rotation;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidBody.AddRelativeForce(new Vector2(0, _thrust), ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 rotationToAdd = new Vector3(0, 0, _turnRate);
            transform.Rotate(rotationToAdd);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 rotationToAdd = new Vector3(0, 0, -_turnRate);
            transform.Rotate(rotationToAdd);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _birdsManager.SpawnBird(_birdOrigin.transform.position, transform.up);
        }
    }

    public void ShipHit()
    {
        gameObject.transform.position = _startPosition;
        gameObject.transform.rotation = _startRotation;
        _rigidBody.velocity = Vector3.zero;
    }
}
