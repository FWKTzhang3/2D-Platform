using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackComponet : MonoBehaviour
{
     private Transform _transform;           // ת�������

     private InputSystem _input;             // ������ϵͳ
     private ControllSystem _controll;       // ����ϵͳ
     private DetectionSystem _detection;     // ���ϵͳ

     private Attacker _attacker;             // �������
     private HurtBoxManager _hurtBoxManager; // �����ж�

     public AttackData attackData;           // ���ñ������ݵĳ����ļ�

     private AttackDataType _current_Attack_Data_Type;                                    // ���浱ǰ������������
     private Dictionary<AttackDataType, AttackDetails[]> _current_Attack_Data_Dictionary; // ���浱ǰ���������ֵ�
     private AttackDetails _current_Attack_Details;                                       // ���浱ǰ��������

     private void Awake()
     {
          _transform = transform.parent.parent;                                      // ��ȡ��������ĸ����������ת����

          _input = GetComponentInParent<InputSystem>();                              // ��ȡ��������Ŀ�����ϵͳ
          _controll = GetComponentInParent<ControllSystem>();                        // ��ȡ��������Ŀ���ϵͳ
          _detection = GetComponentInParent<DetectionSystem>();                      // ��ȡ��������ļ��ϵͳ
          
          _attacker = _transform.GetComponentInChildren<Attacker>();                 // ��ȡ���������������Ĺ������
          _hurtBoxManager = _transform.GetComponentInChildren<HurtBoxManager>();     // ��ȡ���������������Ĺ����ж�

          attackData?.InitializeData();                                              // ��ʼ������
     }

     private void Start()
     {
          _current_Attack_Data_Dictionary = attackData.attackDataDictionary;         // ���������ļ�����ֵ�
     }

     private void OnEnable()
     {
          // �����¼�
          _input.ActionStartedEvent += Attack;         // ��ǰ����ָ���¼�   
          WeaponFrames.NormalAttackEvent += NormalAttackFrames;    // ��ǰ����֡�¼�
          WeaponFrames.SpecialAttackEvent += SpecialFrames;   // ��ǰ����֡�¼�
     }


     private void OnDisable()
     {
          // ע���¼�
          _input.ActionStartedEvent -= Attack;         // ��ǰ����ָ���¼� 
          WeaponFrames.NormalAttackEvent -= NormalAttackFrames;    // ��ǰ����֡�¼�
     }

     /// <summary>
     /// ����ָ���¼�����
     /// </summary>
     /// <param name="attackAction"> �ж�ָ�� </param>
     private void Attack(InputAction attackAction)
     {
          if (!_detection.isAir)        // �����ǰ����ڵ���
               _controll.velocityY = 0;      // ��ͣ
     }

     /// <summary>
     /// ����֡�¼�
     /// </summary>
     /// <param name="count"> �������� </param>
     private void NormalAttackFrames(int count)
     {
          // ���浱ǰ�������ͣ�����ڿ�����Ϊ ������ͨ������������ڿ�����Ϊ ������ͨ������
          _current_Attack_Data_Type = _detection.isAir ? AttackDataType.Normal_Air_Attack : AttackDataType.Normal_Ground_Attack;
          // ���뵱ǰ���������������ݣ������ �����
          if (_current_Attack_Data_Dictionary.TryGetValue(_current_Attack_Data_Type, out AttackDetails[] value))
          {
               _current_Attack_Details = value[count - 1];       // ���浱ǰ�������ݣ��ӹ��������Թ�������Ϊ������
               SetHurtBoxVariables(_current_Attack_Details);     // �������
          }
     }

     /// <summary>
     /// ���ù����ж�����
     /// </summary>
     /// <param name="attackDetails"> ���빥������ </param>
     private void SetHurtBoxVariables(AttackDetails attackDetails)
     {
          _hurtBoxManager.SetHurtBoxOffset(attackDetails.hurtBoxOffset);   // �����ж����꣨���ģ�
          _hurtBoxManager.SetHurtBoxSize(attackDetails.hurtBoxSize);       // �����ж���Χ
          _attacker.damage = attackDetails.damage;                         // �����˺�
          _attacker.knockbackForce = attackDetails.knockbackForce;         // ������������
          _attacker.hitstunTime = attackDetails.hitstunTime;               // Ӳֱʱ��
          _attacker.hitStopTime = attackDetails.hitStopTime;               // ��֡ʱ��
          _attacker.hitStopRecoveSpeed = attackDetails.hitStopRecoveSpeed; // ��֡�ָ��ٶ�
     }

     private void SpecialFrames()
     {
          Debug.Log("Զ�̹���");
     }
}
