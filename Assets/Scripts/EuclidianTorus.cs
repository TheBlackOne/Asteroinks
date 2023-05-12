using UnityEngine;
using System.Collections;
 
public class EuclidianTorus : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;
    private Camera _mainCamera;
    private Rigidbody2D _rigidBody;

    private void Awake  ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _mainCamera = Camera.main;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update () {
        var halfSpriteWidth = _spriteRenderer.sprite.rect.width / 2;
        var halfSpriteHeight = _spriteRenderer.sprite.rect.height / 2;
        var spriteScreenPos = _mainCamera.WorldToScreenPoint(transform.position);

        var screenWidth = Screen.width;
        var screenHeight = Screen.height;

        // Check right boundary
        if (_rigidBody.velocity.x > 0 && spriteScreenPos.x - halfSpriteWidth > screenWidth){
            var newScreenPosition = new Vector3(0 - halfSpriteWidth, spriteScreenPos.y, spriteScreenPos.z);
            transform.position = _mainCamera.ScreenToWorldPoint(newScreenPosition);
        }
        
        // Check left boundary
        else if (_rigidBody.velocity.x < 0 && spriteScreenPos.x < 0 - halfSpriteWidth){
            var newScreenPosition = new Vector3(screenWidth, spriteScreenPos.y, spriteScreenPos.z);
            transform.position = _mainCamera.ScreenToWorldPoint(newScreenPosition);
        }
 
        // Check top boundary
        else if (_rigidBody.velocity.y > 0 && spriteScreenPos.y - halfSpriteHeight > screenHeight){
            var newScreenPosition = new Vector3(spriteScreenPos.x, 0 - halfSpriteHeight, spriteScreenPos.z);
            transform.position = _mainCamera.ScreenToWorldPoint(newScreenPosition);
        }
 
        // Check bottom boundary
        else if(_rigidBody.velocity.y < 0 && spriteScreenPos.y < 0 - halfSpriteHeight){
            var newScreenPosition = new Vector3(spriteScreenPos.x, screenHeight, spriteScreenPos.z);
            transform.position = _mainCamera.ScreenToWorldPoint(newScreenPosition);
        }
    }
}