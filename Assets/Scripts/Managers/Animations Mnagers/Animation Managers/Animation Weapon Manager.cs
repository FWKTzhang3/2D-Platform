 using UnityEngine;

/// <summary>
/// �����������ֵĿ���
/// </summary>
public class AnimationWeaponManager : MonoBehaviour
{
     private SpriteRenderer m_SpriteRenderer;     // ������Ⱦ��
     private Animator m_Animator;                 // ����������

     // ���涯������������ID�������Ż����ܣ��ַ����϶���ֱ����ID����
     private int _isAir_ID;

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
          _isAir_ID = Animator.StringToHash("isAir");                      

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
          m_SpriteRenderer.color = new Color(R, G, B, A);
     }

     /// <summary>
     /// ���ö����������Ĳ���ֵ
     /// </summary>
     /// <param name="animatorState"></param>
     public void SetAnimatorBools(AnimatorState animatorState)
     {
          m_Animator.SetBool(_isAir_ID, animatorState.isAir);
          
          m_Animator.SetBool(_isHurt_ID, animatorState.isHurt);
          m_Animator.SetBool(_isDeath_ID, animatorState.isDeath);
          
          m_Animator.SetBool(_NormalAttacks_ID, animatorState.normalAttacks);
          m_Animator.SetBool(_SpecialAttacks_ID, animatorState.specialAttacks);
          m_Animator.SetBool(_SkillAttacks_ID, animatorState.skillAttacks);
     }
}
