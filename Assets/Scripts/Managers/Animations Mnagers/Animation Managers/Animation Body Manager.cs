using UnityEngine;

/// <summary>
/// �������岿�ֵĿ���
/// </summary>
public class AnimationBodyManager : MonoBehaviour
{
     private SpriteRenderer m_SpriteRenderer;     // ������Ⱦ��
     private Animator m_Animator;                 // ����������

     // ���涯������������ID�������Ż����ܣ��ַ����϶���ֱ����ID����
     private int _attackSpeed_ID;
     private int _moveSpeed_ID;
     private int _velocityX_ID;
     private int _velocityY_ID;

     private int _isAir_ID;
     private int _isMove_ID;
     private int _isCrouch_ID;

     private int _isHurt_ID;
     private int _isDeath_ID;

     private int _NormalAttacks_ID;
     private int _SpecialAttacks_ID;
     private int _SkillAttacks_ID;

     private void Awake()
     {
          m_SpriteRenderer = GetComponent<SpriteRenderer>();     // ��ȡ��ǰ����ľ�����Ⱦ��
          m_Animator = GetComponent<Animator>();                 // ��ȡ��ǰ����Ķ���������
     }

     private void Start()
     {
          // ��ȡ�������������Ӧ������ID
          _attackSpeed_ID = Animator.StringToHash("AttackSpeed");
          _moveSpeed_ID = Animator.StringToHash("MoveSpeed");

          _velocityX_ID = Animator.StringToHash("VelocityX");             
          _velocityY_ID = Animator.StringToHash("VelocityY");             

          _isAir_ID = Animator.StringToHash("isAir");                     
          _isMove_ID = Animator.StringToHash("isMove");                   
          _isCrouch_ID = Animator.StringToHash("isCrouch");

          _isHurt_ID = Animator.StringToHash("isHurt");
          _isDeath_ID = Animator.StringToHash("isDeath");

          _NormalAttacks_ID = Animator.StringToHash("NormalAttacks");
          _SpecialAttacks_ID = Animator.StringToHash("SpecialAttacks");
          _SkillAttacks_ID = Animator.StringToHash("SkillAttacks");
     }

     /// <summary>
     /// ������Ⱦ����ɫ
     /// </summary>
     /// <param name="R"> �� </param>
     /// <param name="G"> �� </param>
     /// <param name="B"> �� </param>
     /// <param name="A"> ͸ </param>
     public void SetColor(int R, int G, int B, int A)
     {
          m_SpriteRenderer.color = new Color(R,G,B,A);
     }

     /// <summary>
     /// ���ö����������ĸ�����
     /// </summary>
     public void SetAnimatorFloats(AnimatorState animatorState)
     {
          m_Animator.SetFloat(_velocityX_ID, animatorState.moveVelocity);
          m_Animator.SetFloat(_velocityY_ID, animatorState.airVelocity);
     }

     /// <summary>
     /// ���ö����������Ĳ���ֵ
     /// </summary>
     public void SetAnimatorBools(AnimatorState animatorState)
     {
          m_Animator.SetBool(_isAir_ID, value: animatorState.isAir);
          m_Animator.SetBool(_isMove_ID, animatorState.isMove);
          m_Animator.SetBool(_isCrouch_ID, animatorState.isCrouch);

          m_Animator.SetBool(_isHurt_ID, animatorState.isHurt);
          m_Animator.SetBool(_isDeath_ID, animatorState.isDeath);

          m_Animator.SetBool(_NormalAttacks_ID, animatorState.normalAttacks);
          m_Animator.SetBool(_SpecialAttacks_ID, animatorState.specialAttacks);
          m_Animator.SetBool(_SkillAttacks_ID, animatorState.skillAttacks);
     }
}
