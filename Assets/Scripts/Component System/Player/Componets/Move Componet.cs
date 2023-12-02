using UnityEngine;

/// <summary>
/// 移动组件
/// </summary>
/// <remarks> 控制玩家左右移动的组件脚本 </remarks>
public class MoveComponet : MonoBehaviour
{
     private Transform _transform;                // 变换器组件
     private Rigidbody2D _rigidbody2D;            // 刚体组件
     private InputSystem _input;                  // 控制器系统
     private DetectionSystem _detection;          // 检测系统

     [SerializeField, Tooltip("移动速度")] private float _moveSpeed;
     [SerializeField, Tooltip("移动加速度")] private float _moveAcceleration;
     [SerializeField, Tooltip("移动减速度")] private float _moveDeceleration;

     private float _targetMoveDirection;     // 目标移动方向
     private float _targetMoveSpeed;         // 目标移动速度
     private float _targetMoveCeleration;    // 目标加速度

     private float _currentMoveSpeed;        // 当前移动速度

     private void Awake()
     {
          _transform = transform.parent;                         // 获取父物体的变换器组件
          _rigidbody2D = GetComponentInParent<Rigidbody2D>();    // 获取父物体的刚体
          _input = GetComponentInParent<InputSystem>();          // 获取父物体的控制器组件
          _detection = GetComponentInParent<DetectionSystem>();
     }

     private void OnEnable()
     {
          // 注册事件
          _input.OnJoyStickEvent += OnMove;
          _input.OnAttackEvent += OnAttack;
     }

     private void OnDisable()
     {
          // 注销事件
          _input.OnJoyStickEvent -= OnMove;
          _input.OnAttackEvent -= OnAttack;
     }

     private void Update()
     {
          // 根据目标移动方向判断目标移动速度
          _targetMoveSpeed = _targetMoveDirection == 0 ? 0 : _moveSpeed;                                 
          // 根据目标移动方向判断目标移动加速度
          _targetMoveCeleration = _targetMoveDirection == 0 ? _moveDeceleration : _moveAcceleration;     
          // 使用 MoveTowards 方法逐渐改变当前移动速度线性插值到目标移动速度
          _currentMoveSpeed = Mathf.MoveTowards(_currentMoveSpeed, _targetMoveSpeed, Time.deltaTime * _targetMoveCeleration);
     }

     private void FixedUpdate()
     {
          SetVelocityX(_currentMoveSpeed * _transform.lossyScale.x);  // 设置水平方向上的速度
     }

     /// <summary>
     /// 移动事件方法
     /// </summary>
     /// <param name="moveInput"> 接收事件的二维向量（控制器摇杆向量） </param>
     private void OnMove(Vector2 moveInput)
     {
          _targetMoveDirection = moveInput.x;     // 当前移动方向等于控制器X轴方向
     }

     private void OnAttack()
     {
          if (!_detection.isAir)
          {
               _currentMoveSpeed = 0;
          }
     }

     /// <summary>
     /// 设置刚体在水平方向上的速度
     /// </summary>
     /// <param name="velocityX">水平方向上的速度值</param>
     private void SetVelocityX(float velocityX)
     {
          _rigidbody2D.velocity = new Vector2(velocityX,_rigidbody2D.velocity.y); // 设置刚体在水平方向上的速度
     }
}
