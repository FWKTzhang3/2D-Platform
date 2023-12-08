using System.Collections;
using UnityEngine;

/// <summary>
/// 全动画控制器
/// </summary>
public class AnimationsAllManager : MonoBehaviour
{
     private AnimationBodyManager m_AnimationBodyManager;
     private AnimationClotheManager m_AnimationClotheManager;
     private AnimationWeaponManager m_AnimationWeaponManager;

     private Coroutine _shakeCoroutine;  // 缓存协程

     [Header("震动曲线")]
     [SerializeField] private AnimationCurve _shakeCurve;
     [Header("震动频率")]
     [SerializeField] private float _shakeFrequency;

     private float _shakeCurveMaxLength;     // 极限长度
     private float _currentShakeLength;      // 当前长度

     private void Awake()
     {
          m_AnimationBodyManager = GetComponentInChildren<AnimationBodyManager>();
          m_AnimationWeaponManager = GetComponentInChildren<AnimationWeaponManager>();
     }

     private void Start()
     {
          if (_shakeCurve.length > 0)
          {
               _shakeCurveMaxLength = _shakeCurve.keys[_shakeCurve.length - 1].time; // 计算极限长度
          }
     }

     public void SetAllColor(int R, int G, int B, int A)
     {
          m_AnimationBodyManager.SetColor(R, G, B, A);
          m_AnimationWeaponManager.SetColor(R, G, B, A);
     }

     public void SetAllAnimatiorValue(AnimatorState animatorState)
     {
          SetAllAnimatorFloats(animatorState);
          SetAllAnimatorBools(animatorState);
     }

     private void SetAllAnimatorFloats(AnimatorState animatorState)
     {
          m_AnimationBodyManager.SetAnimatorFloats(animatorState);
     }

     private void SetAllAnimatorBools(AnimatorState animatorState)
     {
          m_AnimationBodyManager.SetAnimatorBools(animatorState);
          m_AnimationWeaponManager.SetAnimatorBools(animatorState);
     }

     /// <summary>
     /// 动画震动
     /// </summary>
     /// <param name="knockbackDirection"> 震动初始方向 </param>
     /// <param name="shakeStrength"> 震动力度 </param>
     public void AnimationShake(ShakeVlues shakeVlues)
     {
          if (_shakeCoroutine != null)            // 如果当前缓存的协程不为空
               StopCoroutine(_shakeCoroutine);         // 停止一次协程（保证唯一性）
          _shakeCoroutine = StartCoroutine(AnimationShakeCoroutine(shakeVlues));  // 协程！启动！（并且添加到缓存里复用）
     }

     /// <summary>
     /// 动画震动协程
     /// </summary>
     /// <param name="shakeDirection"> 震动初始方向 </param>
     /// <param name="shakeStrength"> 震动力度 </param>
     private IEnumerator AnimationShakeCoroutine(ShakeVlues shakeVlues)
     {
          // 对当前脚本所在物体的相对坐标进行循环赋值，当前时间直到最大值
          for (_currentShakeLength = 0; _currentShakeLength <= _shakeCurveMaxLength; _currentShakeLength += Time.deltaTime * _shakeFrequency)
          {
               float shakeCurveValue = _shakeCurve.Evaluate(_currentShakeLength);                             // 缓存曲线值
               float shake = shakeVlues.knockbackDirectionX * shakeCurveValue * shakeVlues.shakeStrength;     // 缓存坐标
               transform.localPosition = new Vector2(shake, transform.localPosition.y);                       // 赋值坐标
               yield return null;                                                                             // 协程必备延迟一帧
          }

          transform.localPosition = new Vector2(0, transform.localPosition.y);                                // 最后还原坐标
     }
}
