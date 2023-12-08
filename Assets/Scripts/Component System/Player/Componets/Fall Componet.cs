using UnityEngine;

/// <summary>
/// 下落组件
/// </summary>
public class FallComponet : MonoBehaviour
{
     private Transform _transform;           // 变换器组件
     private ControllSystem _controll;       // 控制系统
     private DetectionSystem _detection;     // 检测系统

     [SerializeField, Tooltip("下落速度动画曲线")] private AnimationCurve _fallSpeedCurve;

     private float _maxfallCurveTime;        // 最大下落曲线时间

     private float _currentfallCurveTime;    // 当前下落曲线时间
     private float _currentFallSpeed;        // 当前下落哦速度

     private void Awake()
     {
          _transform = transform.parent;                         // 获取父级物体的变换器组件
          _controll = GetComponentInParent<ControllSystem>();    // 获取父级物体的控制系统
          _detection = GetComponentInParent<DetectionSystem>();  // 获取父级物体的控制器系统
     }

     private void Start()
     {
          _maxfallCurveTime = _fallSpeedCurve.keys[_fallSpeedCurve.length - 1].time;      // 获取动画曲线最后一帧的时间，储存为当前最大下落曲线的时间
     }

     private void Update()
     {
          if (_controll.velocityY < 0 && _detection.isAir)    // 如果当前力度小于 0 且 在空中
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
          if (_controll.velocityY < 0 && _detection.isAir)  // 如果当前Y轴速度小于 0
               _controll.velocityY = _currentFallSpeed;        //让玩家下落
     }
}
