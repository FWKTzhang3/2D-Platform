using UnityEngine;

/// <summary>
/// �������岿�ֵĿ���
/// </summary>
public class AnimationBodyManager : MonoBehaviour
{
     private Animator m_Animator;       // ����������

     // ���涯������������ID�������Ż����ܣ��ַ����϶���ֱ����ID����
     private int _velocityX_ID;
     private int _velocityY_ID;

     private int _isAir_ID;
     private int _isAttack_ID;

     private int _trigger_Attack_ID;
     private int _trigger_Hurt_ID;

     private void Awake()
     {
          m_Animator = GetComponent<Animator>();  // ��ȡ��ǰ����Ķ���������
     }

     private void Start()
     {
          // ��ȡ�������������Ӧ������ID
          _velocityX_ID = Animator.StringToHash("VelocityX");              // ��ȡ��Ϊ "VelocityX" �Ĳ����� ID
          _velocityY_ID = Animator.StringToHash("VelocityY");              // ��ȡ��Ϊ "VelocityY" �Ĳ����� ID

          _isAir_ID = Animator.StringToHash("isAir");                      // ��ȡ��Ϊ "isAir" �Ĳ����� ID
          _isAttack_ID = Animator.StringToHash("isAttack");                // ��ȡ��Ϊ "isAttack" �Ĳ����� ID

          _trigger_Attack_ID = Animator.StringToHash("Trigger_Attack");
          _trigger_Hurt_ID = Animator.StringToHash("Trigger_Hurt");

     }

     /// <summary>
     /// ���ö����������ĸ�����
     /// </summary>
     /// <param name="VelocityX"></param>
     /// <param name="VelocityY"></param>
     public void SetAnimatorFloats(float VelocityX, float VelocityY)
     {
          m_Animator.SetFloat(_velocityX_ID, VelocityX);
          m_Animator.SetFloat(_velocityY_ID, VelocityY);
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
