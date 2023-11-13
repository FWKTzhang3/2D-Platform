using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.PlayerLoop.PreUpdate;

/// <summary>
/// ״̬������
/// </summary>
public class StateMachine : MonoBehaviour
{
     IState currentState;                         // ���ýӿڣ�����ΪcurrentState
     public Dictionary<Type, IState> stateTable;  // ����һ���ֵ䣨��TypeΪ������IStateΪ��ֵ��������ΪstateTable

     void Update()
     {
          currentState.LogicUpdate();
     }

     void FixedUpdate()
     {
          currentState.PhysicUpdate();
     }

     /// <summary>
     /// �л�����״̬��ִ����ز���
     /// </summary>
     /// <param name="newState">��״̬��ֵ</param>
     protected void SwitchOn(IState newState)
     {
          currentState = newState;   // �л����µ�״̬
          currentState.Enter();      // ������״̬ʱִ�еĲ���
     }

     /// <summary>
     /// �л�״̬
     /// </summary>
     /// <param name="newState">��״̬��ֵ</param>
     public void SwitchState(IState newState)
     {
          currentState.Exit();     // �˳���ǰ״̬ʱִ�еĲ���
          SwitchOn(newState);      // �л����µ�״̬��ִ�н������   
     }

     /// <summary>
     /// �л�״̬
     /// </summary>
     /// <param name="newStateType">��״̬���͵ļ�</param>
     public void SwitchState(Type newStateType)
     {
          SwitchState(stateTable[newStateType]);           // �л����µ�״̬��ִ�н������   
     }
}
