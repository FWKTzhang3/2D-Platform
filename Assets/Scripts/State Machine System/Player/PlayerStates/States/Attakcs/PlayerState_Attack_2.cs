using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Attack_2", fileName = "PlayerState_Attack_2")]
public class PlayerState_Attack_2 : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          if(input.axesX == player.faceDir)
               player.SetVelocityX(constants.moveAttackSpeed * player.faceDir);
     }

     public override void LogicUpdate()
     {
          if (input.attack)                                                                         // �����Ұ��¹�����
               input.SetInputBuffer(input.CallbackAttackInputBuffer, input.hasAttackInputBufferTime);    // ����ǰ֡��ӵ��������뻺������

          if (isAnimationFinished)                                                                  // �����ǰ���ŵĹ��������Ѿ�����
          {
               if (input.attack || input.hasAttackInputBuffer)                                      // �����Ұ����˹������򹥻����뻺�������п��õĹ�������
                    stateMachine.SwitchState(typeof(PlayerState_Attack_3));                              // �л������� 3 ״̬
               else                                                                                 // ��֮
                    stateMachine.SwitchState(typeof(PlayerState_Idle));                                  // �л�������״̬
          }
     }
}
