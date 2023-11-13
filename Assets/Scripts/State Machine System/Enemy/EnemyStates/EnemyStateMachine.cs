using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
     // �������
     private Animator animator;
     private EnemyController enemy;
     private EnemyConstants constants;
     private GroundDetector groundDetector;
     private ObjectDetector objectDetector;

     [SerializeField] private EnemyState[] states;     // ֥ʿFSM�ĵ��õ�״̬�б�
     private EnemyState[] currentState;                // ֥ʿFSM�ı��ص�״̬�б���

     private void Awake()
     {
          animator = GetComponentInChildren<Animator>();
          groundDetector = GetComponentInChildren<GroundDetector>();
          objectDetector = GetComponentInChildren<ObjectDetector>();

          enemy = GetComponent<EnemyController>();
          constants = GetComponent<EnemyConstants>();

          stateTable = new Dictionary<Type, IState>(states.Length);   // �������½�һ���ֵ䣨�����Զ����б���ȣ�
          currentState = new EnemyState[states.Length];               // ������ʵ�����������Զ���ԭ�б���ȣ�

          int i = 0;                              // ��ʼ��һ����������i��������currentState�����и��ٵ�ǰ״̬��λ��
          foreach (EnemyState state in states)    // ����states�б��е�EnemyState����
          {
               if (state == null)                      // �����ǰ״̬Ϊ��
               {
                    //Debug.LogWarning("��ǰ״̬�ǿյģ�����");
                    continue;                               // ������ǰ״̬��������һ��״̬�Ĵ���
               }
               Type stateType = state.GetType();       // ��ȡ��ǰ״̬������
               if (stateTable.ContainsKey(stateType))  // ���״̬�����Ѱ�����ǰ״̬������
               {
                    //Debug.LogError("��ǰ״̬ " + stateType + " ���ظ��ģ�����");
                    continue;                               // ������ǰ״̬��������һ��״̬�Ĵ���
               }
               currentState[i] = Instantiate(state);                                                          // ʵ������ǰ״̬���洢��currentState������
               currentState[i].Intialize(animator, enemy, constants, groundDetector, objectDetector, this);   // ��ʼ����ǰ״̬
               stateTable.Add(stateType, currentState[i]);                                                    // ����ǰ״̬����״̬����
               i++;                                                                                           // ��������i������׼��������һ��״̬
          }
     }

     void Start()
     {
          SwitchOn(stateTable[typeof(EnemyState_Idle)]);   // �л�����ʼ״̬
     }
}
