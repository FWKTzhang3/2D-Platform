using System;
using UnityEngine;

public class Victim : MonoBehaviour
{
     private CharacterStats characterStats;

     [Header("受击数据")]
     public int hitDirection;
     public float shakeStrength;

     private void Awake()
     {
          characterStats = GetComponentInParent<CharacterStats>();
     }

     private void OnTriggerEnter2D(Collider2D other)
     {
          if (other.gameObject.tag == TagType.HurtBox.ToString())
          {
               Attacker attacker = other.GetComponent<Attacker>();
               if(attacker != null)
               {
                    hitDirection = GetHitDirection(other);  // 获取受击方向
                    shakeStrength = attacker.damage * 0.01f;
               }
          }
     }

     private void OnTriggerStay2D(Collider2D other)
     {
          if (other.gameObject.tag == TagType.HurtBox.ToString())
          {
               characterStats.TakeShake(this);
          }
     }

     /// <summary>
     /// 获取受击方向
     /// </summary>
     /// <param name="other">  目标碰撞体 </param>
     /// <returns> 一个整数（只有 -1、0、1） </returns>
     private int GetHitDirection(Collider2D other)
     {
          Vector2 hitDirection = (other.transform.position - transform.position).normalized;   // 计算相对方向，并归一化（目标绝对坐标减去当前绝对坐标）
          return (int)Mathf.Sign(hitDirection.x);                                              // 通过 Mathf.Sign 进一步取整值，再强制转为整数后输出
     }
}
