using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Attack_1", fileName = "PlayerState_Attack_1")]
public class PlayerState_Attack_1 : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          player.SetVelocityX(0);  // ������������
     }

     public override void LogicUpdate()
     {
          if (input.attack)                                                                              // �����Ұ��¹�����
               input.SetInputBuffer(input.CallbackAttackInputBuffer, input.hasAttackInputBufferTime);         // ����ǰ֡��ӵ��������뻺������

          if (isAnimationFinished)                                                                       // �����ǰ���ŵĹ��������Ѿ�����
          {
               if (input.attack || input.hasAttackInputBuffer)                                           // �����Ұ����˹������򹥻����뻺�������п��õĹ�������
                    stateMachine.SwitchState(typeof(PlayerState_Attack_2));                                   // �л������� 2 ״̬
               else                                                                                      // ��֮
                    stateMachine.SwitchState(typeof(PlayerState_Idle));                                       // �л�������״̬
          }
     }
}
