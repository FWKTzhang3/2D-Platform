using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
     [SerializeField] private Vector2 moveDirection;
     [SerializeField] private float maxMoveDistance;
     [SerializeField] private float moveSpeed;

     private Vector2 starPos;

     private void Start()
     {
          starPos = transform.position;
          moveDirection = new Vector2 (transform.localScale.x, 0f);
     }

     private void OnEnable()
     {
          StartCoroutine(MoveDirectly());
     }

     IEnumerator MoveDirectly()
     {
          while (gameObject.activeSelf)
          {
               transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
               yield return null;
          }
     }

     private void Update()
     {
          float currentDistance = Vector2.Distance(starPos, transform.position);
          if (currentDistance >= maxMoveDistance)
               gameObject.SetActive(false);
     }

     private void OnTriggerStay2D(Collider2D collision)
     {
          gameObject.SetActive(false);
     }
}
