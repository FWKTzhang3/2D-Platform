using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.PlayerLoop.PreUpdate;

/// <summary>
/// 状态机基类
/// </summary>
public class StateMachine : MonoBehaviour
{
     IState currentState;                         // 调用接口，命名为currentState
     public Dictionary<Type, IState> stateTable;  // 创建一个字典（以Type为键，以IState为数值），命名为stateTable

     void Update()
     {
          currentState.LogicUpdate();
     }

     void FixedUpdate()
     {
          currentState.PhysicUpdate();
     }

     /// <summary>
     /// 切换至新状态并执行相关操作
     /// </summary>
     /// <param name="newState">新状态的值</param>
     protected void SwitchOn(IState newState)
     {
          currentState = newState;   // 切换到新的状态
          currentState.Enter();      // 进入新状态时执行的操作
     }

     /// <summary>
     /// 切换状态
     /// </summary>
     /// <param name="newState">新状态的值</param>
     public void SwitchState(IState newState)
     {
          currentState.Exit();     // 退出当前状态时执行的操作
          SwitchOn(newState);      // 切换到新的状态并执行进入操作   
     }

     /// <summary>
     /// 切换状态
     /// </summary>
     /// <param name="newStateType">新状态类型的键</param>
     public void SwitchState(Type newStateType)
     {
          SwitchState(stateTable[newStateType]);           // 切换到新的状态并执行进入操作   
     }
}
