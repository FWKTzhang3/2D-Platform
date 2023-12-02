using System;
using UnityEngine;

/// <summary>
/// �����������ֵĿ���
/// </summary>
public class AnimationWeaponManager : MonoBehaviour
{
     private Animator m_Animator;       // ����������

     // ���涯������������ID�������Ż����ܣ��ַ����϶���ֱ����ID����
     private int _isAir_ID;
     private int _isAttack_ID;

     private int _trigger_Attack_ID;

     private void Awake()
     {
          m_Animator = GetComponent<Animator>();  // ��ȡ��ǰ����Ķ���������
     }

     private void Start()
     {
          // ��ȡ�������������Ӧ������ID
          _isAir_ID = Animator.StringToHash("isAir");                      // ��ȡ��Ϊ "isAir" �Ĳ����� ID
          _isAttack_ID = Animator.StringToHash("isAttack");                // ��ȡ��Ϊ "isAttack" �Ĳ����� ID

          _trigger_Attack_ID = Animator.StringToHash("Trigger_Attack");
     }

     /// <summary>
     /// ���ö����������Ĳ���ֵ
     /// </summary>
     /// <param name="airState"></param>
     /// <param name="attackState"></param>
     public void SetAnimatorBools(bool airState, bool attackState)
     {
          m_Animator.SetBool(_isAir_ID, airState);
          m_Animator.SetBool(_isAttack_ID, attackState);
     }

     /// <summary>
     /// ���������Ĵ���ֵ
     /// </summary>
     public void SetAnimatorTrigger()
     {
          m_Animator.SetTrigger(_trigger_Attack_ID);
     }

}
