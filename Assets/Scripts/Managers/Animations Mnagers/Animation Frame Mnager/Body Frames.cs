using System;
using UnityEngine;

/// <summary>
/// 身体帧事件
/// </summary>
public class BodyFrames : MonoBehaviour
{
     /// <summary>
     /// 急停帧事件
     /// </summary>
     public static event Action EmergencyStopEvent;

     /// <summary>
     /// 急停方法
     /// </summary>
     private void EmergencyStop()
     {
          EmergencyStopEvent?.Invoke();
     }

}
