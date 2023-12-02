using UnityEngine;

/// <summary>
/// 动画身体部分的控制
/// </summary>
public class AnimationBodyManager : MonoBehaviour
{
     private Animator m_Animator;       // 动画控制器

     // 缓存动画控制器变量ID（启动优化性能，字符串肯定比直接搜ID慢）
     private int _velocityX_ID;
     private int _velocityY_ID;

     private int _isAir_ID;
     private int _isAttack_ID;

     private int _trigger_Attack_ID;
     private int _trigger_Hurt_ID;

     private void Awake()
     {
          m_Animator = GetComponent<Animator>();  // 获取当前物体的动画控制器
     }

     private void Start()
     {
          // 获取动画控制器里对应参数的ID
          _velocityX_ID = Animator.StringToHash("VelocityX");              // 获取名为 "VelocityX" 的参数的 ID
          _velocityY_ID = Animator.StringToHash("VelocityY");              // 获取名为 "VelocityY" 的参数的 ID

          _isAir_ID = Animator.StringToHash("isAir");                      // 获取名为 "isAir" 的参数的 ID
          _isAttack_ID = Animator.StringToHash("isAttack");                // 获取名为 "isAttack" 的参数的 ID

          _trigger_Attack_ID = Animator.StringToHash("Trigger_Attack");
          _trigger_Hurt_ID = Animator.StringToHash("Trigger_Hurt");

     }

     /// <summary>
     /// 设置动画控制器的浮点数
     /// </summary>
     /// <param name="VelocityX"></param>
     /// <param name="VelocityY"></param>
     public void SetAnimatorFloats(float VelocityX, float VelocityY)
     {
          m_Animator.SetFloat(_velocityX_ID, VelocityX);
          m_Animator.SetFloat(_velocityY_ID, VelocityY);
     }

     /// <summary>
     /// 设置动画控制器的布尔值
     /// </summary>
     /// <param name="airState"></param>
     /// <param name="attackState"></param>
     public void SetAnimatorBools(bool airState, bool attackState)
     {
          m_Animator.SetBool(_isAir_ID, airState);
          m_Animator.SetBool(_isAttack_ID, attackState);
     }

     /// <summary>
     /// 触发动画的触发值
     /// </summary>
     public void SetAnimatorTrigger()
     {
          m_Animator.SetTrigger(_trigger_Attack_ID);
     }
}
