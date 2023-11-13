using System;
using UnityEngine;

public class EnemyState : ScriptableObject, IState
{
     protected Animator animator;
     protected EnemyController enemy;
     protected EnemyConstants constants;
     protected GroundDetector groundDetector;
     protected ObjectDetector objectDetector;
     protected EnemyStateMachine stateMachine;

     [SerializeField, Tooltip("动画名称")] string animationName;
     [SerializeField, Tooltip("动画哈希值")] int animationHash;
     [SerializeField, Tooltip("动画播放速度")] float animationSpeed;
     [SerializeField, Tooltip("动画过度率"), Range(0, 1)] float transitionDuration;

     float stateStarTime;                                                                                     // 状态开始时间
     protected float stateDuration => Time.time - stateStarTime;                                              // 状态持续时间
     protected bool isAnimationFinished => stateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;   // 判断状态动画是否结束

     private void OnValidate()
     {
          animationHash = Animator.StringToHash(animationName);     // 将动画名称转化成哈希值（主要是省性能占用）
     }

     public void Intialize(Animator animator, EnemyController enemy, EnemyConstants constants, GroundDetector groundDetector ,ObjectDetector objectDetector, EnemyStateMachine stateMachine)
     {
          this.animator = animator;               // 将传入的动画管理器赋值给 animator 属性
          this.enemy = enemy;                     // 将敌人控制类赋值给 enemy 属性
          this.constants = constants;
          this.groundDetector = groundDetector;
          this.objectDetector = objectDetector;
          this.stateMachine = stateMachine;       // 将传入的敌人状态机赋值给 stateMachine 属性
     }

     public virtual void Enter()
     {
          animator.CrossFade(animationHash, transitionDuration);
          animator.speed = animationSpeed;
          stateStarTime = Time.time;
     }
     public virtual void Exit() { }
     public virtual void LogicUpdate() { }
     public virtual void PhysicUpdate() { }
}
