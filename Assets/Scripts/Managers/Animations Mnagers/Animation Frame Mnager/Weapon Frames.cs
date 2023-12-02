using System;
using UnityEngine;

/// <summary>
/// ����֡�¼�
/// </summary>
public class WeaponFrames : MonoBehaviour
{
     public static event Action EnableHurtBox;
     public static event Action DisableHurtBox;

     /// <summary>
     /// �����ײ��״̬
     /// </summary>
     /// <param name="currentState"> ������ͣ״̬ </param>
     private void HurtBoxState(TriggerBoxState currentState)
     {
          switch (currentState)              // ��ת�����
          {
               case TriggerBoxState.Enable:       // �����ǰ״̬Ϊ����
                    EnableHurtBox?.Invoke();           // ����������¼�
                    break;                             // ���
               case TriggerBoxState.Disable:      // �����ǰ״̬Ϊ�ر�
                    DisableHurtBox?.Invoke();          // ������ر��¼�
                    break;                             // ���
          }
     }
}

/// <summary>
/// ������״̬
/// </summary>
enum TriggerBoxState
{
     Enable,
     Disable,
}
