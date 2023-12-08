using UnityEngine;

/// <summary>
/// 动画身体部分的控制
/// </summary>
public class AnimationBodyManager : MonoBehaviour
{
     private SpriteRenderer m_SpriteRenderer;     // 精灵渲染器
     private Animator m_Animator;                 // 动画控制器

     // 缓存动画控制器变量ID（启动优化性能，字符串肯定比直接搜ID慢）
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
          m_SpriteRenderer = GetComponent<SpriteRenderer>();     // 获取当前物体的精灵渲染器
          m_Animator = GetComponent<Animator>();                 // 获取当前物体的动画控制器
     }

     private void Start()
     {
          // 获取动画控制器里对应参数的ID
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
     /// 设置渲染器颜色
     /// </summary>
     /// <param name="R"> 红 </param>
     /// <param name="G"> 绿 </param>
     /// <param name="B"> 蓝 </param>
     /// <param name="A"> 透 </param>
     public void SetColor(int R, int G, int B, int A)
     {
          m_SpriteRenderer.color = new Color(R,G,B,A);
     }

     /// <summary>
     /// 设置动画控制器的浮点数
     /// </summary>
     public void SetAnimatorFloats(AnimatorState animatorState)
     {
          m_Animator.SetFloat(_velocityX_ID, animatorState.moveVelocity);
          m_Animator.SetFloat(_velocityY_ID, animatorState.airVelocity);
     }

     /// <summary>
     /// 设置动画控制器的布尔值
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
