using System;
using UnityEngine;

/// <summary>
/// ����֡�¼�
/// </summary>
public class BodyFrames : MonoBehaviour
{
     /// <summary>
     /// ��ͣ֡�¼�
     /// </summary>
     public static event Action EmergencyStopEvent;

     /// <summary>
     /// ��ͣ����
     /// </summary>
     private void EmergencyStop()
     {
          EmergencyStopEvent?.Invoke();
     }

}
