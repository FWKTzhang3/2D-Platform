using System;
using UnityEngine;

/// <summary>
/// 武器帧事件
/// </summary>
public class WeaponFrames : MonoBehaviour
{
     public static event Action EnableHurtBox;
     public static event Action DisableHurtBox;

     /// <summary>
     /// 输出碰撞器状态
     /// </summary>
     /// <param name="currentState"> 输入启停状态 </param>
     private void HurtBoxState(TriggerBoxState currentState)
     {
          switch (currentState)              // 用转换检测
          {
               case TriggerBoxState.Enable:       // 如果当前状态为启动
                    EnableHurtBox?.Invoke();           // 则输出启动事件
                    break;                             // 打断
               case TriggerBoxState.Disable:      // 如果当前状态为关闭
                    DisableHurtBox?.Invoke();          // 则输出关闭事件
                    break;                             // 打断
          }
     }
}

/// <summary>
/// 触发盒状态
/// </summary>
enum TriggerBoxState
{
     Enable,
     Disable,
}
