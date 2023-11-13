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
          initialPosition = transform.position; // 记录子弹初始位置
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
     /// 射程检测
     /// </summary>
     /// <param name="initialPosition"> 初始位置 </param>
     /// <param name="maxDistance"> 极限射程 </param>
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
