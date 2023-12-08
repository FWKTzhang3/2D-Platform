using UnityEngine;
using UnityEngine.InputSystem;

public class JumpComponet: MonoBehaviour
{
     private Transform _transform;           // 变换器组件

     private ControllSystem _controll;       // 控制系统
     private InputSystem _input;             // 控制器系统
     private DetectionSystem _detection;     // 检测器系统

     [SerializeField, Tooltip("跳跃力度")] private float _jumpForce;
     [SerializeField, Tooltip("跳跃减速度")] private float _jumpDeceleration;
     [SerializeField, Tooltip("跳跃次数")] private float _jumpCount;
     [SerializeField, Tooltip("土狼时间")] private float _coyoteTime;

     private float _currentJumpForce;        // 当前跳跃力度
     private float _currentJumpCount;        // 当前跳跃次数
     private float _currentCoyoteTime;       // 当前土狼时间

     private bool _canJump => !_controll.isHitstun && !_controll.isAttack;
     private bool _coyoteState => _currentCoyoteTime >= 0;  // 土狼状态

     private void Awake()
     {
          _transform = transform.parent;                         // 获取父级物体变换器

          _controll = GetComponentInParent<ControllSystem>();    // 获取父级物体的控制系统
          _input = GetComponentInParent<InputSystem>();          // 获取父级物体的控制器系统
          _detection = GetComponentInParent<DetectionSystem>();  // 获取父级物体的检测系统
     }

     private void OnEnable()
     {
          // 注册事件
          _input.ActionStartedEvent += OnJump;
          _input.ActionCanceledEvent += StopJump;
     }

     private void OnDisable()
     {
          // 注销事件
          _input.ActionStartedEvent -= OnJump;
          _input.ActionCanceledEvent -= StopJump;
     }

     private void Update()
     {
          CoyoteTime();
          JumpForceUpdate();
     }

     private void FixedUpdate()
     {
          if (_currentJumpForce > 0)                        // 如果当前跳跃力度大于 0
               _controll.velocityY = _currentJumpForce;          // 设置水平方向上的速度
     }

     /// <summary>
     /// 启动跳跃的方法
     /// </summary>
     private void OnJump(InputAction inputAction)
     {
          if (inputAction.name == "Jump" && _canJump)  // 筛选出跳跃的指令（按下）
          {
               // 如果当前状态不在空中或者土狼状态且当前跳跃次数大于 0
               if ((!_detection.isAir|| _coyoteState) && _currentJumpCount > 0)
               {
                    _currentJumpCount = 0;             // 跳跃次数归零
               }
               // 如果当前跳跃次数小于预设次数
               if (_currentJumpCount < _jumpCount)
               {
                    _currentJumpForce = _jumpForce;    // 施加跳跃力度
                    _currentJumpCount++;               // 跳跃次数 + 1
               }
          }
     }

     /// <summary>
     /// 停止跳跃的方法
     /// </summary>
     private void StopJump(InputAction inputAction)
     {
          if (inputAction.name == "Jump")              // 筛选出跳跃的指令（抬起）
          {
               if (_controll.velocityY > 0 && _currentJumpCount <= 1)   // 如果当前刚体 Y 轴力度大于 0 且 当前跳跃次数小于等于 1
               {
                    _currentJumpForce = 0;                            // 让当前力度归零
                    _controll.velocityY = 0;                          // 让跳跃寸止（阻冲之）
               }
          }
     }

     /// <summary>
     /// 跳跃力度更新方法
     /// </summary>
     private void JumpForceUpdate()
     {
          if (_currentJumpForce > 0)    // 如果当前跳跃力度大于 0
               // 用MoveTowards线性插值算法逐渐递减到 0
               _currentJumpForce = Mathf.MoveTowards(_currentJumpForce, 0, Time.deltaTime * _jumpDeceleration);
     }

     /// <summary>
     /// 土狼时间
     /// </summary>
     private void CoyoteTime()
     {
          if (!_detection.isAir)                                 // 如果不在空中
          {
               _currentCoyoteTime = _coyoteTime;                      // 重置当前土狼时间
          }
          else if (_detection.isAir && _currentCoyoteTime >= 0)  // 如果在空中且当前土狼时间没有开始计时
          {
               _currentCoyoteTime -= Time.deltaTime;                  // 开始计时土狼时间
          }
     }
}
