using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
     // 调用组件
     private Animator animator;
     private EnemyController enemy;
     private EnemyConstants constants;
     private GroundDetector groundDetector;
     private ObjectDetector objectDetector;

     [SerializeField] private EnemyState[] states;     // 芝士FSM的调用的状态列表
     private EnemyState[] currentState;                // 芝士FSM的本地的状态列表副本

     private void Awake()
     {
          animator = GetComponentInChildren<Animator>();
          groundDetector = GetComponentInChildren<GroundDetector>();
          objectDetector = GetComponentInChildren<ObjectDetector>();

          enemy = GetComponent<EnemyController>();
          constants = GetComponent<EnemyConstants>();

          stateTable = new Dictionary<Type, IState>(states.Length);   // 在这里新建一个字典（长度自动与列表相等）
          currentState = new EnemyState[states.Length];               // 将副本实例化（长度自动与原列表相等）

          int i = 0;                              // 初始化一个索引变量i，用于在currentState数组中跟踪当前状态的位置
          foreach (EnemyState state in states)    // 遍历states列表中的EnemyState对象
          {
               if (state == null)                      // 如果当前状态为空
               {
                    //Debug.LogWarning("当前状态是空的，过。");
                    continue;                               // 跳过当前状态，继续下一个状态的处理
               }
               Type stateType = state.GetType();       // 获取当前状态的类型
               if (stateTable.ContainsKey(stateType))  // 如果状态表中已包含当前状态的类型
               {
                    //Debug.LogError("当前状态 " + stateType + " 是重复的，过。");
                    continue;                               // 跳过当前状态，继续下一个状态的处理
               }
               currentState[i] = Instantiate(state);                                                          // 实例化当前状态并存储在currentState数组中
               currentState[i].Intialize(animator, enemy, constants, groundDetector, objectDetector, this);   // 初始化当前状态
               stateTable.Add(stateType, currentState[i]);                                                    // 将当前状态加入状态表中
               i++;                                                                                           // 索引变量i递增，准备处理下一个状态
          }
     }

     void Start()
     {
          SwitchOn(stateTable[typeof(EnemyState_Idle)]);   // 切换到初始状态
     }
}
