using System;
using UnityEngine;

public class Victim : MonoBehaviour
{
     private CharacterStats characterStats;

     [Header("�ܻ�����")]
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
                    hitDirection = GetHitDirection(other);  // ��ȡ�ܻ�����
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
     /// ��ȡ�ܻ�����
     /// </summary>
     /// <param name="other">  Ŀ����ײ�� </param>
     /// <returns> һ��������ֻ�� -1��0��1�� </returns>
     private int GetHitDirection(Collider2D other)
     {
          Vector2 hitDirection = (other.transform.position - transform.position).normalized;   // ������Է��򣬲���һ����Ŀ����������ȥ��ǰ�������꣩
          return (int)Mathf.Sign(hitDirection.x);                                              // ͨ�� Mathf.Sign ��һ��ȡ��ֵ����ǿ��תΪ���������
     }
}
