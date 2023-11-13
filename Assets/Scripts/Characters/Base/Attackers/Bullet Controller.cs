using UnityEngine;

public class BulletController : MonoBehaviour
{
     private Rigidbody2D rb;

     [SerializeField] private Vector2 initialPosition;
     [SerializeField] private float maxDistance;

     private void Awake()
     {
          rb = GetComponent<Rigidbody2D>();
     }

     private void Start()
     {
          initialPosition = transform.position; // ��¼�ӵ���ʼλ��
     }

     private void Update()
     {
          CheckRange(initialPosition, maxDistance);
     }

     private void FixedUpdate()
     {
          SetVelocityX(10);
     }

     private void OnTriggerStay2D(Collider2D collision)
     {
          Destroy(gameObject);
     }

     /// <summary>
     /// ��̼��
     /// </summary>
     /// <param name="initialPosition"> ��ʼλ�� </param>
     /// <param name="maxDistance"> ������� </param>
     private void CheckRange(Vector2 initialPosition, float maxDistance)
     {
          float currentDistance = Vector2.Distance(initialPosition, transform.position);
          if (currentDistance >= maxDistance)
               Destroy(gameObject);
     }

     private void SetVelocityX(float velocityX)
     {
          rb.velocity = new Vector2(velocityX, rb.velocity.y);
     }
}
