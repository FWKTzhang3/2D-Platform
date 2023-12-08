using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
     private CharacterStats _otherCharacter;      // 调用角色状态脚本
     public Vector2 position => transform.position;

     public static event Action<float, float> HitStopEvent;

     [Header("伤害相关")]
     public int damage;

     [Header("击退相关")]
     public Vector2 knockbackForce;

     [Header("硬直相关")]
     public float hitstunTime;

     [Header("顿帧相关")]
     public float hitStopTime;
     public float hitStopRecoveSpeed;

     private void OnTriggerEnter2D(Collider2D other)
     {
          if (other.tag == TagType.HitBox.ToString())                           // 如果目标物体的标签是 HitBox
          {
               _otherCharacter = other.GetComponentInParent<CharacterStats>();       // 获取目标物体的 CharacterStats，并缓存起来
               if (hitStopTime > 0 && _otherCharacter != null)                       // 如果顿帧时间大于 0 
                    HitStopEvent?.Invoke(hitStopTime, hitStopRecoveSpeed);                // 执行顿帧
          }
     }

     private void OnTriggerStay2D(Collider2D other)
     {
          if (other.tag == TagType.HitBox.ToString() && _otherCharacter != null)     // 如果目标物体的标签是 HitBox
          {
               _otherCharacter.TakeDamage(this);                                     // 传递脚本
          }
     }
}
