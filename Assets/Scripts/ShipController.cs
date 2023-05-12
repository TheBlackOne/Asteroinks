using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float thrust;
    public float maxThrust;
    public float turnRate;
    private Rigidbody2D rigidBody;
    private BirdsManager birdsManager;
    private GameObject birdOrigin;

    public void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        birdsManager = GameObject.Find("BirdsManager").GetComponent<BirdsManager>();
        birdOrigin = GameObject.Find("BirdOrigin");
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.AddRelativeForce(new Vector2(0, thrust), ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 rotationToAdd = new Vector3(0, 0, turnRate);
            transform.Rotate(rotationToAdd);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 rotationToAdd = new Vector3(0, 0, -turnRate);
            transform.Rotate(rotationToAdd);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            birdsManager.SpawnBird(birdOrigin.transform.position, transform.up);
        }
    }
}
