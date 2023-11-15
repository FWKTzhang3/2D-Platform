using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
     private Rigidbody2D rigidBody;

     public Vector2 moveDirection => transform.localScale;
     public float moveSpeed;

     private void Awake()
     {
          rigidBody = GetComponent<Rigidbody2D>();
     }

     private void FixedUpdate()
     {
          SetVelocity(moveDirection.x * moveSpeed, rigidBody.velocity.y);
     }

     private void OnTriggerStay2D(Collider2D other)
     {
          if (other.gameObject.layer != (int)LayerType.Camera)
               gameObject.SetActive(false);
     }

     private void OnTriggerExit2D(Collider2D other)
     {
          if (other.gameObject.layer == (int)LayerType.Camera)
               gameObject.SetActive(false);
     }

     public void SetVelocity(float velocityX, float velocityY) => rigidBody.velocity = new Vector2(velocityX, velocityY);

}
