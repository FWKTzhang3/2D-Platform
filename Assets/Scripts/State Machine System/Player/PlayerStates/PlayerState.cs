using System;
using UnityEngine;

/// <summary>
/// ���״̬�Ļ���
/// </summary>
/// <remarks> ������״̬����������������������Ҫ���ɶ�ӦScriptableObject������ʹ�ã����ǲ�ʹ�ýű�������ֻʹ�������ɵĳ��򣨽�Լ��Դ�����ҿ������ɵ��ã� </remarks>
public class PlayerState : ScriptableObject, IState    // �̳�ScriptableObject��������һ�����򣩺�IState���ӿڣ�
{
     //����
     protected Animator animator;              
     protected PlayerInput input;              
     protected PlayerController player;
     protected PlayerConstants constants;
     protected GroundDetector groundDetector;
     protected PlayerStateMachine stateMachine;

     [SerializeField, Tooltip("��������")] string stateName;                         
     [SerializeField, Tooltip("������ϣֵ")] int stateHash;                          
     [SerializeField, Tooltip("����������"), Range(0,1)] float transitionDuration;
     [SerializeField, Tooltip("�����ٶ�")] float animationSpeed;

     float stateStartTime;                                  // ״̬��ʼʱ��
     protected float stateDuration => Time.time - stateStartTime;     // ״̬����ʱ��
     protected bool isAnimationFinished => stateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;   // �ж�״̬�����Ƿ����
     
     protected float currentMoveSpeed;       // ��ǰ�ƶ��ٶ�
     protected float currentJumpForce;       // ��ǰ��Ծ����
     protected float currentKnockbackForceX; // ��ǰ�������� X
     protected float currentKnockbackForceY; // ��ǰ�������� Y
     protected float currentHardTime;        // ��ǰ���Ӳֱʱ��

     private void OnValidate()
     {
          stateHash = Animator.StringToHash(stateName);     // ����������ת���ɹ�ϣֵ����Ҫ��ʡ����ռ�ã�
     }

     /// <summary>
     /// ��ʼ�����������ڽ����ⲿ��������Բ����丳ֵ����Ӧ������
     /// </summary>
     /// <param name="animator">        ����������</param>
     /// <param name="input">           ���������</param>
     /// <param name="player">          ��ҿ�����</param>
     /// <param name="constants">       ��ҳ���</param>
     /// <param name="groundDetector">  ��Ҽ����</param>
     /// <param name="stateMachine">    ���״̬��</param>
     /// <remarks> ��Ҫ��˳���룬��������������������ࡢ��ҿ���������ҳ�������Ҽ���������״̬�� </remarks>
     public void Intialize(Animator animator, PlayerInput input, PlayerController player, PlayerConstants constants, GroundDetector groundDetector, PlayerStateMachine stateMachine)
     {
          this.animator = animator;               // ������Ķ�����������ֵ�� animator ����
          this.input = input;                     // ���������������ำֵ�� playerInput ����
          this.player = player;                   // ���������ҿ�������ֵ�� player ����
          this.constants = constants;             // ���������ҳ�����ֵ�� constants ����
          this.groundDetector = groundDetector;   // ���������Ҽ�⸳ֵ�� groundDetector ����
          this.stateMachine = stateMachine;       // ����������״̬����ֵ�� stateMachine ����
     }

     public virtual void Enter()
     {
          animator.CrossFade(stateHash, transitionDuration);     // ʹ�� animator.CrossFade �������������ɵ�ָ����״̬��animationHash��
          animator.speed = animationSpeed;
          stateStartTime = Time.time;                            // �� stateStartTime ����Ϊ��ǰʱ�䣨Time.time��
     }
     public virtual void Exit() { }
     public virtual void LogicUpdate() { } 
     public virtual void PhysicUpdate() { }
}

