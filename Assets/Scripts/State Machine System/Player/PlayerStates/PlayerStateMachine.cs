using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家状态机
/// </summary>
/// <remarks>用于管理游戏角色的状态</remarks>
public class PlayerStateMachine : StateMachine
{
     // 调用组件
     Animator animator;      
     PlayerInput input;      
     PlayerController player;
     GroundDetector groundDetector;
     PlayerConstants constants;

     // 玩家状态数组
     [SerializeField] PlayerState[] states;      
     [SerializeField] PlayerState[] moveStates;
     [SerializeField] PlayerState[] jumpStates;
     [SerializeField] PlayerState[] attackStates;

     void Awake()
     {
          // 获取组件
          animator = GetComponentInChildren<Animator>();
          groundDetector = GetComponentInChildren<GroundDetector>();
          input = GetComponent<PlayerInput>();
          player = GetComponent<PlayerController>();
          constants = GetComponent<PlayerConstants>();

          stateTable = new Dictionary<Type, IState>(states.Length);   // 初始化状态表

          #region 历遍状态表
          // 用历遍初始化所有玩家状态
          foreach (PlayerState state in states)
          {
               state.Intialize(animator, input, player, constants, groundDetector,this);  // 调用玩家状态的初始化函数
               stateTable.Add(state.GetType(), state);                                    // 将状态添加到状态表中
          }

          foreach (PlayerState state in moveStates)
          {
               state.Intialize(animator, input, player, constants, groundDetector, this);  // 调用玩家状态的初始化函数
               stateTable.Add(state.GetType(), state);                                     // 将状态添加到状态表中
          }

          foreach (PlayerState state in jumpStates)
          {
               state.Intialize(animator, input, player, constants, groundDetector, this);  // 调用玩家状态的初始化函数
               stateTable.Add(state.GetType(), state);                                     // 将状态添加到状态表中
          }

          foreach (PlayerState state in attackStates)
          {
               state.Intialize(animator, input, player, constants, groundDetector, this);  // 调用玩家状态的初始化函数
               stateTable.Add(state.GetType(), state);                                     // 将状态添加到状态表中
          }

          #endregion
     }

     void Start()
     {
          SwitchOn(stateTable[typeof(PlayerState_Idle)]);   // 切换到初始状态
     }
}
