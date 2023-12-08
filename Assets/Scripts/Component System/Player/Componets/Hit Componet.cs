using System;
using UnityEngine;

public class HitComponet : MonoBehaviour
{
     private ControllSystem _control;        // 调用控制系统
     private DetectionSystem _detection;     // 调用检测系统
     private AnimationSystem _animation;     // 调用动画系统

     [SerializeField, Tooltip("击退力度衰减")] private int _knockbackForceAttenuation;
     [SerializeField, Tooltip("震动倍率")] private float _shakeMultiplier;

     private float _currentHitstunTime;                // 当前硬直时间
     private Vector2 _currentKnockbackDirection;       // 当前击退方向
     private Vector2 _currentKnockbackForce;           // 当前击退力度

     public ShakeVlues shakeVlues;                     // 震动数值

     private void Awake()
     {
          _control = GetComponentInParent<ControllSystem>();     // 获取父级物体的控制系统
          _detection = GetComponentInParent<DetectionSystem>();  // 获取父级物体的检测系统
          _animation = GetComponentInParent<AnimationSystem>();  // 获取父级物体的动画系统   
     }

     private void OnEnable()
     {
          BodyFrames.EmergencyStopEvent += EmergencyStop;
     }

     private void OnDestroy()
     {
          BodyFrames.EmergencyStopEvent -= EmergencyStop;
     }

     private void Update()
     {
          // 如果当前硬直时间大于零，则进入受伤状态
          if (_control.isHitstun = _currentHitstunTime > 0)
               _currentHitstunTime -= Time.deltaTime;

          // 当前击退力度衰减
          if (_currentKnockbackForce != Vector2.zero)
               _currentKnockbackForce = Vector2.MoveTowards(_currentKnockbackForce, Vector2.zero, Time.deltaTime * _knockbackForceAttenuation);
     }

     private void FixedUpdate()
     {
          Knockback();
     }

     /// <summary>
     /// 启动受伤
     /// </summary>
     /// <param name="characterStateType"> 受伤状态 </param>
     /// <param name="attacker"> 攻击者脚本 </param>
     public void OnHurt(CharacterStateType characterStateType, Attacker attacker)
     {
          EmergencyStop();
          GetKnockbackDirection(attacker.position);

          _currentKnockbackForce = attacker.knockbackForce;      // 获取击退力度

          if (characterStateType == CharacterStateType.Hurt)     // 检测当前受伤状态是否为受伤
          {
               _currentHitstunTime = attacker.hitstunTime;       // 获取硬直时间
               UpdateShakeVlues(attacker);                       // 更新震动数值
               _animation.OnAnimationShake(shakeVlues);          // 启动震动
          }
          _control.isDeadth = characterStateType == CharacterStateType.Death;   // 检测当前受伤状态是否为死亡
     }

     /// <summary>
     /// 击退方法
     /// </summary>
     private void Knockback()
     {
          if (_currentKnockbackForce != Vector2.zero)
          {
               _control.velocityX = _currentKnockbackDirection.x * _currentKnockbackForce.x;
               _control.velocityY = _currentKnockbackDirection.y * _currentKnockbackForce.y;
          }
     }

     /// <summary>
     /// 急停
     /// </summary>
     private void EmergencyStop()
     {
          _control.velocityX = 0;
          _control.velocityY = 0;
     }

     /// <summary>
     /// 获取击退方向
     /// </summary>
     private void GetKnockbackDirection(Vector2 attackerPosition)
     {
          Vector2 _currentPosition = transform.position;                                  // 获取当前坐标
          // 计算击退方向
          int knockbackX = (int)Mathf.Sign(_currentPosition.x - attackerPosition.x);
          int knockbackY = (int)Mathf.Sign(_currentPosition.y - attackerPosition.y);
          _currentKnockbackDirection = new Vector2(knockbackX, knockbackY);
     }

     /// <summary>
     /// 更新震动数值
     /// </summary>
     private void UpdateShakeVlues(Attacker attacker)
     {
          shakeVlues.isAir = _detection.isAir;
          shakeVlues.knockbackDirectionX = (int)_currentKnockbackDirection.x;
          shakeVlues.shakeStrength = attacker.damage * _shakeMultiplier;
     }

}

/// <summary>
/// 震动数值
/// </summary>
public struct ShakeVlues
{
     public bool isAir;
     public int knockbackDirectionX;
     public float shakeStrength;
}
