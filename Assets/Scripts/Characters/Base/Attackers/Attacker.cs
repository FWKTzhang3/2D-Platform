using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
     private CharacterStats _otherCharacter;      // ���ý�ɫ״̬�ű�
     public Vector2 position => transform.position;

     public static event Action<float, float> HitStopEvent;

     [Header("�˺����")]
     public int damage;

     [Header("�������")]
     public Vector2 knockbackForce;

     [Header("Ӳֱ���")]
     public float hitstunTime;

     [Header("��֡���")]
     public float hitStopTime;
     public float hitStopRecoveSpeed;

     private void OnTriggerEnter2D(Collider2D other)
     {
          if (other.tag == TagType.HitBox.ToString())                           // ���Ŀ������ı�ǩ�� HitBox
          {
               _otherCharacter = other.GetComponentInParent<CharacterStats>();       // ��ȡĿ������� CharacterStats������������
               if (hitStopTime > 0 && _otherCharacter != null)                       // �����֡ʱ����� 0 
                    HitStopEvent?.Invoke(hitStopTime, hitStopRecoveSpeed);                // ִ�ж�֡
          }
     }

     private void OnTriggerStay2D(Collider2D other)
     {
          if (other.tag == TagType.HitBox.ToString() && _otherCharacter != null)     // ���Ŀ������ı�ǩ�� HitBox
          {
               _otherCharacter.TakeDamage(this);                                     // ���ݽű�
          }
     }
}
