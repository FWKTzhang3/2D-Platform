using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
     private Transform _transform;           // 转换器组件

     private InputSystem _input;             // 控制器系统
     private ControllSystem _controll;       // 控制系统
     private DetectionSystem _detection;     // 检测系统

     private AnimationsAllManager _animationsAllManager;    // 调用动画控制器

     public AnimatorState animatorState;                    // 调用动画机状态结构体

     private void Awake()
     {
          _transform = transform.parent;                    // 获取父级物体的转换器

          _input = GetComponent<InputSystem>();             // 获取同级物体的控制器系统
          _controll = GetComponent<ControllSystem>();       // 获取同级物体的控制系统
          _detection = GetComponent<DetectionSystem>();     // 获取同级物体的检测系统

          _animationsAllManager = _transform.GetComponentInChildren<AnimationsAllManager>();
     }

     private void OnEnable()
     {
          _input.JoystickEvent += JoystickEvent;
     }

     private void OnDisable()
     {
          _input.JoystickEvent -= JoystickEvent;
     }

     private void Update()
     {
          UpdateAnimationState();

          _animationsAllManager.SetAllAnimatiorValue(animatorState);
     }

     /// <summary>
     /// 摇杆事件方法
     /// </summary>
     /// <param name="inputVecotr"> 摇杆向量 </param>
     private void JoystickEvent(Vector2 inputVecotr)
     {
          animatorState.isMove = inputVecotr.x != 0;   // 动画移动状态等于当前摇杆向量不等于0
          animatorState.isCrouch = inputVecotr.y < 0;  // 动画下蹲状态等于当前摇杆向量小于0
     }

     /// <summary>
     /// 更新动画状态
     /// </summary>
     private void UpdateAnimationState()
     {
          animatorState.moveVelocity = Mathf.Abs(_controll.velocityX);                         // 动画 移动速度 等于 当前速度X的绝对值
          animatorState.airVelocity = _controll.velocityY;                                     // 当前 空中速度 等于 当前速度Y

          animatorState.isAir = _detection.isAir;                                              // 当前 空中状态 等于 当前检测的 空中状态

          animatorState.isHurt = _controll.isHitstun;                                          // 当前 受伤状态 等于 当前控制的 硬直状态
          animatorState.isDeath = _controll.isDeadth;                                          // 当前 死亡状态 等于 当前控制的 死亡状态

          animatorState.normalAttacks = _input.normalAttack || _input.normalAttackBuffer;      // 当前 普通攻击状态 等于 当前检测的 普通攻击指令
          animatorState.specialAttacks = _input.specialAttack || _input.specialAttackBuffer;   // 当前 远程攻击状态 等于 当前检测的 远程攻击指令
          animatorState.skillAttacks = _input.skillAttack || _input.skillAttackBuffer;         // 当前 技能攻击状态 等于 当前检测的 技能攻击指令
     }

     /// <summary>
     /// 启动动画震动方法
     /// </summary>
     /// <param name="shakeVlues"> 震动数值 </param>
     public void OnAnimationShake(ShakeVlues shakeVlues)
     {
          _animationsAllManager.AnimationShake(shakeVlues);
     }
}

/// <summary>
/// 动画机状态
/// </summary>
public struct AnimatorState
{
     public float attackSpeed;     // 攻击动画速度
     public float moveSpeed;       // 移动动画速度

     public float moveVelocity;    // 移动速度
     public float airVelocity;     // 空中速度

     public bool isAir;            // 空中状态
     public bool isMove;           // 移动状态
     public bool isCrouch;         // 下蹲状态

     public bool isHurt;           // 受伤状态
     public bool isDeath;          // 死亡状态

     public bool normalAttacks;    // 普通攻击
     public bool specialAttacks;   // 远程攻击
     public bool skillAttacks;     // 技能攻击
}