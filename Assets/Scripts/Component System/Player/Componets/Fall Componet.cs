using UnityEngine;

/// <summary>
/// 下落组件
/// </summary>
public class FallComponet : MonoBehaviour
{
     private Transform _transform;           // 变换器组件
     private Rigidbody2D _rigidbody2D;       // 刚体组件
     private DetectionSystem _detection;     // 检测器系统

     [SerializeField, Tooltip("下落速度动画曲线")] private AnimationCurve _fallSpeedCurve;

     private float _maxfallCurveTime;        // 最大下落曲线时间

     private float _currentfallCurveTime;    // 当前下落曲线时间
     private float _currentFallSpeed;        // 当前下落哦速度

     private void Awake()
     {
          _transform = transform.parent;
          _rigidbody2D = GetComponentInParent<Rigidbody2D>();
          _detection = GetComponentInParent<DetectionSystem>();
     }

     private void Start()
     {
          _maxfallCurveTime = _fallSpeedCurve.keys[_fallSpeedCurve.length - 1].time;      // 获取动画曲线最后一帧的时间，储存为当前最大下落曲线的时间
     }

     private void Update()
     {
          if (getVelocityY < 0 && _detection.isAir)    // 如果当前力度小于 0 且 在空中
          {
               // 用MoveTowards将当前下落时间线性插值最大时间
               _currentfallCurveTime = Mathf.MoveTowards(_currentfallCurveTime, _maxfallCurveTime, Time.deltaTime);
               // 通过当前时间获取动画曲线对应速度值
               _currentFallSpeed = _fallSpeedCurve.Evaluate(_currentfallCurveTime);  
          }
          else  // 反之
          {
               _currentfallCurveTime = 0;    // 让当前下落动画时间归零
          }
     }

     private void FixedUpdate()
     {
          if (getVelocityY < 0 && _detection.isAir)    // 如果当前下路速度小于 0
               SetVelocityY(_currentFallSpeed);             //让玩家下落
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
