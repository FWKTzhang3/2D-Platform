using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
     public static event Action<float, float> HitStopEvent;

     [Header("伤害相关")]
     public int damage;

     [Header("击退相关")]
     public float knockbackForceX;
     public float knockbackForceY;
     public float knockbackHardTime;

     [Header("顿帧相关")]
     public float hitStopTime;
     public float hitStopRecoveSpeed;

     private void OnTriggerStay2D(Collider2D other)
     {
          if (other.tag == TagType.HitBox.ToString())                                     // 如果目标物体的标签是 HitBox
          {
               CharacterStats othercharacter = other.GetComponentInParent<CharacterStats>();   // 获取目标物体的 CharacterStats，并缓存起来
               if (othercharacter != null && !othercharacter.isMuteki)                         // 如果 characterStats 不为空，以及目标非无敌状态
               {
                    othercharacter.TakeDamage(this);                                                // 传递数值

                    HitStopEvent?.Invoke(hitStopTime, hitStopRecoveSpeed);                          // 触发事件，并且传递数值
               }
          }
     }
}
