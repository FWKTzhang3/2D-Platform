using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
     public static event Action<float, float> HitStopEvent;

     [Header("�˺����")]
     public int damage;

     [Header("�������")]
     public float knockbackForceX;
     public float knockbackForceY;
     public float knockbackHardTime;

     [Header("��֡���")]
     public float hitStopTime;
     public float hitStopRecoveSpeed;

     private void OnTriggerStay2D(Collider2D other)
     {
          if (other.tag == TagType.HitBox.ToString())                                     // ���Ŀ������ı�ǩ�� HitBox
          {
               CharacterStats othercharacter = other.GetComponentInParent<CharacterStats>();   // ��ȡĿ������� CharacterStats������������
               if (othercharacter != null && !othercharacter.isMuteki)                         // ��� characterStats ��Ϊ�գ��Լ�Ŀ����޵�״̬
               {
                    othercharacter.TakeDamage(this);                                                // ������ֵ

                    HitStopEvent?.Invoke(hitStopTime, hitStopRecoveSpeed);                          // �����¼������Ҵ�����ֵ
               }
          }
     }
}
