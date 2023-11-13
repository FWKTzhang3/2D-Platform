using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���״̬��
/// </summary>
/// <remarks>���ڹ�����Ϸ��ɫ��״̬</remarks>
public class PlayerStateMachine : StateMachine
{
     // �������
     Animator animator;      
     PlayerInput input;      
     PlayerController player;
     GroundDetector groundDetector;
     PlayerConstants constants;

     // ���״̬����
     [SerializeField] PlayerState[] states;      
     [SerializeField] PlayerState[] moveStates;
     [SerializeField] PlayerState[] jumpStates;
     [SerializeField] PlayerState[] attackStates;

     void Awake()
     {
          // ��ȡ���
          animator = GetComponentInChildren<Animator>();
          groundDetector = GetComponentInChildren<GroundDetector>();
          input = GetComponent<PlayerInput>();
          player = GetComponent<PlayerController>();
          constants = GetComponent<PlayerConstants>();

          stateTable = new Dictionary<Type, IState>(states.Length);   // ��ʼ��״̬��

          #region ����״̬��
          // �������ʼ���������״̬
          foreach (PlayerState state in states)
          {
               state.Intialize(animator, input, player, constants, groundDetector,this);  // �������״̬�ĳ�ʼ������
               stateTable.Add(state.GetType(), state);                                    // ��״̬��ӵ�״̬����
          }

          foreach (PlayerState state in moveStates)
          {
               state.Intialize(animator, input, player, constants, groundDetector, this);  // �������״̬�ĳ�ʼ������
               stateTable.Add(state.GetType(), state);                                     // ��״̬��ӵ�״̬����
          }

          foreach (PlayerState state in jumpStates)
          {
               state.Intialize(animator, input, player, constants, groundDetector, this);  // �������״̬�ĳ�ʼ������
               stateTable.Add(state.GetType(), state);                                     // ��״̬��ӵ�״̬����
          }

          foreach (PlayerState state in attackStates)
          {
               state.Intialize(animator, input, player, constants, groundDetector, this);  // �������״̬�ĳ�ʼ������
               stateTable.Add(state.GetType(), state);                                     // ��״̬��ӵ�״̬����
          }

          #endregion
     }

     void Start()
     {
          SwitchOn(stateTable[typeof(PlayerState_Idle)]);   // �л�����ʼ״̬
     }
}
