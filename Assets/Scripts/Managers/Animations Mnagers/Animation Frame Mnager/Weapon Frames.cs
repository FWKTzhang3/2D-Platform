using System;
using UnityEngine;

/// <summary>
/// ����֡�¼�
/// </summary>
public class WeaponFrames : MonoBehaviour
{
     /// <summary>
     /// ���������ж�
     /// </summary>
     public static event Action EnableHurtBox;
     /// <summary>
     /// �رչ����ж�
     /// </summary>
     public static event Action DisableHurtBox;
     /// <summary>
     /// �����ͨ�����¼�
     /// </summary>
     public static event Action<int> NormalAttackEvent;
     /// <summary>
     /// ���Զ�̹����¼�
     /// </summary>
     public static event Action SpecialAttackEvent;

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

     /// <summary>
     /// ������ͨ����
     /// </summary>
     /// <param name="count"> ������� </param>
     private void OnNormalAttack(int count)
     {
          NormalAttackEvent?.Invoke(count);
     }

     /// <summary>
     /// ����Զ�̹���
     /// </summary>
     private void OnSpecialAttack()
     {
          SpecialAttackEvent?.Invoke();
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
