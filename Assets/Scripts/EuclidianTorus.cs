using UnityEngine;
using System.Collections;
 
public class EuclidianTorus : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
    private Rigidbody2D rigidBody;

    private void Awake  ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        var spriteWidth = spriteRenderer.sprite.rect.width;
        var spriteHeight = spriteRenderer.sprite.rect.height;
        var spriteScreenPos = mainCamera.WorldToScreenPoint(transform.position);

        var screenWidth = Screen.width;
        var screenHeight = Screen.height;

        // Teleport the game object
        if (rigidBody.velocity.x > 0 && spriteScreenPos.x > screenWidth){
            Debug.Log("Right");
            var newScreenPosition = new Vector3(0 - spriteWidth, spriteScreenPos.y, spriteScreenPos.z);
            transform.position = mainCamera.ScreenToWorldPoint(newScreenPosition);
        }
        
        else if (rigidBody.velocity.x < 0 && spriteScreenPos.x < 0 - spriteWidth){
            Debug.Log("Left");
            var newScreenPosition = new Vector3(screenWidth, spriteScreenPos.y, spriteScreenPos.z);
            transform.position = mainCamera.ScreenToWorldPoint(newScreenPosition);
        }
 
        else if (rigidBody.velocity.y > 0 && spriteScreenPos.y > screenHeight){
            Debug.Log("Bottom");
            var newScreenPosition = new Vector3(spriteScreenPos.x, 0 - spriteHeight, spriteScreenPos.z);
            transform.position = mainCamera.ScreenToWorldPoint(newScreenPosition);
        }
 
        else if(rigidBody.velocity.y < 0 && spriteScreenPos.y < 0 - spriteHeight){
            Debug.Log("Top");
            var newScreenPosition = new Vector3(spriteScreenPos.x, screenHeight, spriteScreenPos.z);
            transform.position = mainCamera.ScreenToWorldPoint(newScreenPosition);
        }
    }
}