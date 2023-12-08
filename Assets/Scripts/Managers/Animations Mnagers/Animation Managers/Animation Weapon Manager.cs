 using UnityEngine;

/// <summary>
/// 动画武器部分的控制
/// </summary>
public class AnimationWeaponManager : MonoBehaviour
{
     private SpriteRenderer m_SpriteRenderer;     // 精灵渲染器
     private Animator m_Animator;                 // 动画控制器

     // 缓存动画控制器变量ID（启动优化性能，字符串肯定比直接搜ID慢）
     private int _isAir_ID;

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
          _isAir_ID = Animator.StringToHash("isAir");                      

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
          m_SpriteRenderer.color = new Color(R, G, B, A);
     }

     /// <summary>
     /// 设置动画控制器的布尔值
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
