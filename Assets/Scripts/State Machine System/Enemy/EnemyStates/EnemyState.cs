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

     [SerializeField, Tooltip("��������")] string animationName;
     [SerializeField, Tooltip("������ϣֵ")] int animationHash;
     [SerializeField, Tooltip("���������ٶ�")] float animationSpeed;
     [SerializeField, Tooltip("����������"), Range(0, 1)] float transitionDuration;

     float stateStarTime;                                                                                     // ״̬��ʼʱ��
     protected float stateDuration => Time.time - stateStarTime;                                              // ״̬����ʱ��
     protected bool isAnimationFinished => stateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;   // �ж�״̬�����Ƿ����

     private void OnValidate()
     {
          animationHash = Animator.StringToHash(animationName);     // ����������ת���ɹ�ϣֵ����Ҫ��ʡ����ռ�ã�
     }

     public void Intialize(Animator animator, EnemyController enemy, EnemyConstants constants, GroundDetector groundDetector ,ObjectDetector objectDetector, EnemyStateMachine stateMachine)
     {
          this.animator = animator;               // ������Ķ�����������ֵ�� animator ����
          this.enemy = enemy;                     // �����˿����ำֵ�� enemy ����
          this.constants = constants;
          this.groundDetector = groundDetector;
          this.objectDetector = objectDetector;
          this.stateMachine = stateMachine;       // ������ĵ���״̬����ֵ�� stateMachine ����
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
