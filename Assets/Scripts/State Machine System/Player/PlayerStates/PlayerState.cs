using System;
using UnityEngine;

/// <summary>
/// 玩家状态的基类
/// </summary>
/// <remarks> 就所有状态都从这里派生，并且是需要生成对应ScriptableObject程序来使用，就是不使用脚本，本身只使用其生成的程序（节约资源，并且可以自由调用） </remarks>
public class PlayerState : ScriptableObject, IState    // 继承ScriptableObject（可生成一个程序）和IState（接口）
{
     //调用
     protected Animator animator;              
     protected PlayerInput input;              
     protected PlayerController player;
     protected PlayerConstants constants;
     protected GroundDetector groundDetector;
     protected PlayerStateMachine stateMachine;

     [SerializeField, Tooltip("动画名称")] string stateName;                         
     [SerializeField, Tooltip("动画哈希值")] int stateHash;                          
     [SerializeField, Tooltip("动画过度率"), Range(0,1)] float transitionDuration;
     [SerializeField, Tooltip("动画速度")] float animationSpeed;

     float stateStartTime;                                  // 状态开始时间
     protected float stateDuration => Time.time - stateStartTime;     // 状态持续时间
     protected bool isAnimationFinished => stateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;   // 判断状态动画是否结束
     
     protected float currentMoveSpeed;       // 当前移动速度
     protected float currentJumpForce;       // 当前跳跃力度
     protected float currentKnockbackForceX; // 当前击退力度 X
     protected float currentKnockbackForceY; // 当前击退力度 Y
     protected float currentHardTime;        // 当前落地硬直时间

     private void OnValidate()
     {
          stateHash = Animator.StringToHash(stateName);     // 将动画名称转化成哈希值（主要是省性能占用）
     }

     /// <summary>
     /// 初始化函数，用于接收外部传入的属性并将其赋值给对应的属性
     /// </summary>
     /// <param name="animator">        动画管理器</param>
     /// <param name="input">           玩家输入类</param>
     /// <param name="player">          玩家控制器</param>
     /// <param name="constants">       玩家常量</param>
     /// <param name="groundDetector">  玩家检测器</param>
     /// <param name="stateMachine">    玩家状态机</param>
     /// <remarks> 需要按顺序传入，动画管理器、玩家输入类、玩家控制器、玩家常量、玩家检测器、玩家状态机 </remarks>
     public void Intialize(Animator animator, PlayerInput input, PlayerController player, PlayerConstants constants, GroundDetector groundDetector, PlayerStateMachine stateMachine)
     {
          this.animator = animator;               // 将传入的动画管理器赋值给 animator 属性
          this.input = input;                     // 将传入的玩家输入类赋值给 playerInput 属性
          this.player = player;                   // 将传入的玩家控制器赋值给 player 属性
          this.constants = constants;             // 将传入的玩家常量赋值给 constants 属性
          this.groundDetector = groundDetector;   // 将传入的玩家检测赋值给 groundDetector 属性
          this.stateMachine = stateMachine;       // 将传入的玩家状态机赋值给 stateMachine 属性
     }

     public virtual void Enter()
     {
          animator.CrossFade(stateHash, transitionDuration);     // 使用 animator.CrossFade 方法将动画过渡到指定的状态（animationHash）
          animator.speed = animationSpeed;
          stateStartTime = Time.time;                            // 将 stateStartTime 设置为当前时间（Time.time）
     }
     public virtual void Exit() { }
     public virtual void LogicUpdate() { } 
     public virtual void PhysicUpdate() { }
}

