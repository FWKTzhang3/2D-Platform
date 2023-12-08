using UnityEngine;

/// <summary>
/// 移动组件
/// </summary>
/// <remarks> 控制玩家左右移动的组件脚本 </remarks>
public class MoveComponet : MonoBehaviour
{
     private Transform _transform;                // 转换器组件

     private ControllSystem _controll;            // 控制系统
     private InputSystem _input;                  // 控制器系统
     private DetectionSystem _detection;          // 检测系统

     [SerializeField, Tooltip("移动速度")] private float _moveSpeed;
     [SerializeField, Tooltip("移动加速度")] private float _moveAcceleration;
     [SerializeField, Tooltip("移动减速度")] private float _moveDeceleration;

     private float _targetMoveDirection;     // 目标移动方向
     private float _targetMoveSpeed;         // 目标移动速度
     private float _targetMoveCeleration;    // 目标加速度

     private float _currentMoveSpeed;        // 当前移动速度

     private bool _isCrouching;              // 当前下蹲状态
     private bool _isGroundAttacking;        // 当前地面攻击状态

     private bool _canMove
     {
          get => !_controll.isHitstun && !_controll.isDeadth && !_isCrouching && !_isGroundAttacking;
     }

     private void Awake()
     {
          _transform = transform.parent;                         // 获取父级物体的转换器组件

          _controll = GetComponentInParent<ControllSystem>();    // 获取父级物体的控制系统
          _input = GetComponentInParent<InputSystem>();          // 获取父级物体的控制器系统
          _detection = GetComponentInParent<DetectionSystem>();  // 获取父级物体的检测系统
     }

     private void OnEnable()
     {
          // 注册事件
          _input.JoystickEvent += JoystickInput;
          BodyGroundAttackState.GroundAttacking += GroundAttacking;
     }

     private void OnDisable()
     {
          // 注销事件
          _input.JoystickEvent -= JoystickInput;
          BodyGroundAttackState.GroundAttacking += GroundAttacking;
     }

     private void Update()
     {
          if (_canMove)
          {
               // 根据目标移动方向判断目标移动速度
               _targetMoveSpeed = _targetMoveDirection == 0 ? 0 : _moveSpeed;   
               // 根据目标移动方向判断目标移动加速度
               _targetMoveCeleration = _targetMoveDirection == 0 ? _moveDeceleration : _moveAcceleration;     
               // 使用 MoveTowards 方法逐渐改变当前移动速度线性插值到目标移动速度
               _currentMoveSpeed = Mathf.MoveTowards(_currentMoveSpeed, _targetMoveSpeed, Time.deltaTime * _targetMoveCeleration);
          }
     }

     private void FixedUpdate()
     {
          // 如果当前 没有下蹲中 且 没有攻击中 且 允许行动时
          if (_canMove)                    
          {
               if(_targetMoveDirection != 0)
                    _controll.velocityX = _currentMoveSpeed * _targetMoveDirection;       // 设置X轴的速度
               else if(_targetMoveDirection == 0 && !_detection.isAir)
                    _controll.velocityX = _currentMoveSpeed * _transform.lossyScale.x;    // 设置X轴的速度

          }
     }

     /// <summary>
     /// 摇杆操作
     /// </summary>
     /// <param name="JoyVector"> 接收事件的摇杆X轴向量 </param>
     private void JoystickInput(Vector2 JoyVector)
     {
          _targetMoveDirection = JoyVector.x;     // 当前移动方向等于控制器X轴方向

          if(_isCrouching = JoyVector.y < 0 && !_detection.isAir)     // 如果当前下蹲状态（控制器Y轴方向小于 0）为真 且 在地面时
          {
               OnCrouch();
          }
     }

     /// <summary>
     /// 地面攻击状态事件
     /// </summary>
     /// <param name="state"> 状态 </param>
     private void GroundAttacking(bool state)
     {
          _controll.velocityX = 0;      // 急停
          _currentMoveSpeed = 0;        // 急停
          _isGroundAttacking = state;   // 赋值状态
     }

     /// <summary>
     /// 启动下蹲方法
     /// </summary>
     private void OnCrouch() 
     {
          // 急停
          _controll.velocityX = 0;         
          _currentMoveSpeed = 0;   
     }
}
