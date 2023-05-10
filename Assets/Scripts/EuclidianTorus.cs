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
        var halfSpriteWidth = spriteRenderer.sprite.rect.width / 2;
        var halfSpriteHeight = spriteRenderer.sprite.rect.height / 2;
        var spriteScreenPos = mainCamera.WorldToScreenPoint(transform.position);

        var screenWidth = Screen.width;
        var screenHeight = Screen.height;

        // Check right boundary
        if (rigidBody.velocity.x > 0 && spriteScreenPos.x - halfSpriteWidth > screenWidth){
            Debug.Log("Right");
            var newScreenPosition = new Vector3(0 - halfSpriteWidth, spriteScreenPos.y, spriteScreenPos.z);
            transform.position = mainCamera.ScreenToWorldPoint(newScreenPosition);
        }
        
        // Check left boundary
        else if (rigidBody.velocity.x < 0 && spriteScreenPos.x < 0 - halfSpriteWidth){
            Debug.Log("Left");
            var newScreenPosition = new Vector3(screenWidth, spriteScreenPos.y, spriteScreenPos.z);
            transform.position = mainCamera.ScreenToWorldPoint(newScreenPosition);
        }
 
        // Check top boundary
        else if (rigidBody.velocity.y > 0 && spriteScreenPos.y - halfSpriteHeight > screenHeight){
            Debug.Log("Top");
            var newScreenPosition = new Vector3(spriteScreenPos.x, 0 - halfSpriteHeight, spriteScreenPos.z);
            transform.position = mainCamera.ScreenToWorldPoint(newScreenPosition);
        }
 
        // Check bottom boundary
        else if(rigidBody.velocity.y < 0 && spriteScreenPos.y < 0 - halfSpriteHeight){
            Debug.Log("Bottom");
            var newScreenPosition = new Vector3(spriteScreenPos.x, screenHeight, spriteScreenPos.z);
            transform.position = mainCamera.ScreenToWorldPoint(newScreenPosition);
        }
    }
}