using UnityEngine;

public class JumpComponet: MonoBehaviour
{
     private Transform _transform;           // 变换器组件
     private Rigidbody2D _rigidbody2D;       // 刚体组件
     private InputSystem _input;             // 控制器系统
     private DetectionSystem _detection;     // 检测器系统

     [SerializeField, Tooltip("跳跃力度")] private float _jumpForce;
     [SerializeField, Tooltip("跳跃减速度")] private float _jumpDeceleration;
     [SerializeField, Tooltip("跳跃次数")] private float _jumpCount;

     private float _currentJumpForce;        // 当前跳跃力度
     private float _currentJumpCount;        // 当前跳跃次数

     private void Awake()
     {
          _transform = transform.parent;                         
          _rigidbody2D = GetComponentInParent<Rigidbody2D>();    
          _input = GetComponentInParent<InputSystem>();
          _detection = GetComponentInParent<DetectionSystem>();
     }

     private void OnEnable()
     {
          // 注册事件
          _input.OnJumpEvent += OnJump;
          _input.StopJumpEvent += StopJump;
     }

     private void OnDisable()
     {
          // 注销事件
          _input.OnJumpEvent -= OnJump;
          _input.StopJumpEvent -= StopJump;
     }

     private void Update()
     {
          if (_currentJumpForce > 0)    // 如果当前跳跃力度大于 0
               // 用MoveTowards线性插值算法逐渐递减到 0
               _currentJumpForce = Mathf.MoveTowards(_currentJumpForce, 0, Time.deltaTime * _jumpDeceleration);
     }

     private void FixedUpdate()
     {
          if (_currentJumpForce > 0)              // 如果当前跳跃力度大于 0
               SetVelocityY(_currentJumpForce);        // 设置水平方向上的速度
     }

     /// <summary>
     /// 启动跳跃的方法
     /// </summary>
     private void OnJump()
     {
          if (!_detection.isAir && _currentJumpCount > 0)   // 如果当前状态不在空中且当前跳跃次数大于 0
               _currentJumpCount = 0;                            // 跳跃次数归零
          if (_currentJumpCount < _jumpCount)               // 如果当前跳跃次数小于于最大值
          {
               _currentJumpForce = _jumpForce;                   // 施加跳跃力度
               _currentJumpCount++;                              // 跳跃次数 + 1
          }
     }

     /// <summary>
     /// 停止跳跃的方法
     /// </summary>
     private void StopJump()
     {
          if (getVelocityY < 0 || _currentJumpCount > 1)    // 如果当前刚体力度小于 0 或者 当前跳跃次数大于 1
               return;                                           // 则返回
          _currentJumpForce = 0;                            // 让当前力度归零
          SetVelocityY(0);                                  // 让跳跃寸止（阻冲之）
     }

     /// <summary>
     /// 设置刚体在垂直方向上的速度
     /// </summary>
     /// <param name="velocityX">水平垂直上的速度值</param>
     private void SetVelocityY(float velocityX)
     {
          _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, velocityX); // 设置刚体在垂直方向上的速度
     }

     /// <summary>
     /// 获取刚体当前Y轴力度
     /// </summary>
     private float getVelocityY => _rigidbody2D.velocity.y;
}
