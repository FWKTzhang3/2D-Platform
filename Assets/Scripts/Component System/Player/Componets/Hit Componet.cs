using System;
using UnityEngine;

public class HitComponet : MonoBehaviour
{
     private ControllSystem _control;        // ���ÿ���ϵͳ
     private DetectionSystem _detection;     // ���ü��ϵͳ
     private AnimationSystem _animation;     // ���ö���ϵͳ

     [SerializeField, Tooltip("��������˥��")] private int _knockbackForceAttenuation;
     [SerializeField, Tooltip("�𶯱���")] private float _shakeMultiplier;

     private float _currentHitstunTime;                // ��ǰӲֱʱ��
     private Vector2 _currentKnockbackDirection;       // ��ǰ���˷���
     private Vector2 _currentKnockbackForce;           // ��ǰ��������

     public ShakeVlues shakeVlues;                     // ����ֵ

     private void Awake()
     {
          _control = GetComponentInParent<ControllSystem>();     // ��ȡ��������Ŀ���ϵͳ
          _detection = GetComponentInParent<DetectionSystem>();  // ��ȡ��������ļ��ϵͳ
          _animation = GetComponentInParent<AnimationSystem>();  // ��ȡ��������Ķ���ϵͳ   
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
          // �����ǰӲֱʱ������㣬���������״̬
          if (_control.isHitstun = _currentHitstunTime > 0)
               _currentHitstunTime -= Time.deltaTime;

          // ��ǰ��������˥��
          if (_currentKnockbackForce != Vector2.zero)
               _currentKnockbackForce = Vector2.MoveTowards(_currentKnockbackForce, Vector2.zero, Time.deltaTime * _knockbackForceAttenuation);
     }

     private void FixedUpdate()
     {
          Knockback();
     }

     /// <summary>
     /// ��������
     /// </summary>
     /// <param name="characterStateType"> ����״̬ </param>
     /// <param name="attacker"> �����߽ű� </param>
     public void OnHurt(CharacterStateType characterStateType, Attacker attacker)
     {
          EmergencyStop();
          GetKnockbackDirection(attacker.position);

          _currentKnockbackForce = attacker.knockbackForce;      // ��ȡ��������

          if (characterStateType == CharacterStateType.Hurt)     // ��⵱ǰ����״̬�Ƿ�Ϊ����
          {
               _currentHitstunTime = attacker.hitstunTime;       // ��ȡӲֱʱ��
               UpdateShakeVlues(attacker);                       // ��������ֵ
               _animation.OnAnimationShake(shakeVlues);          // ������
          }
          _control.isDeadth = characterStateType == CharacterStateType.Death;   // ��⵱ǰ����״̬�Ƿ�Ϊ����
     }

     /// <summary>
     /// ���˷���
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
     /// ��ͣ
     /// </summary>
     private void EmergencyStop()
     {
          _control.velocityX = 0;
          _control.velocityY = 0;
     }

     /// <summary>
     /// ��ȡ���˷���
     /// </summary>
     private void GetKnockbackDirection(Vector2 attackerPosition)
     {
          Vector2 _currentPosition = transform.position;                                  // ��ȡ��ǰ����
          // ������˷���
          int knockbackX = (int)Mathf.Sign(_currentPosition.x - attackerPosition.x);
          int knockbackY = (int)Mathf.Sign(_currentPosition.y - attackerPosition.y);
          _currentKnockbackDirection = new Vector2(knockbackX, knockbackY);
     }

     /// <summary>
     /// ��������ֵ
     /// </summary>
     private void UpdateShakeVlues(Attacker attacker)
     {
          shakeVlues.isAir = _detection.isAir;
          shakeVlues.knockbackDirectionX = (int)_currentKnockbackDirection.x;
          shakeVlues.shakeStrength = attacker.damage * _shakeMultiplier;
     }

}

/// <summary>
/// ����ֵ
/// </summary>
public struct ShakeVlues
{
     public bool isAir;
     public int knockbackDirectionX;
     public float shakeStrength;
}
