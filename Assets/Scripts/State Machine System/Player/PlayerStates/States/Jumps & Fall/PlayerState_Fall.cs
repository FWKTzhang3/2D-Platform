using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Fall", fileName = "PlayerState_Fall")]
public class PlayerState_Fall : PlayerState
{
     public override void Enter()
     {
          base.Enter();
     }

     public override void LogicUpdate()
     {
          if (player.isHurt)                                          // ����������
               stateMachine.SwitchState(typeof(PlayerState_Hurt));         // ���л�������״̬
          else if (player.isDeath)                                    // ����������
               stateMachine.SwitchState(typeof(PlayerState_Death));        // ���л�������״̬

          if (input.jump)                                                                                // �����Ұ�����Ծ��
          {
               if (player.canAirJump)                                                                    // ������Խ��п�����Ծ
                    stateMachine.SwitchState(typeof(PlayerState_AirJump));                                    // �л���������Ծ״̬
               else                                                                                      // ��֮             
                    input.SetInputBuffer(input.CallbackJumpInputBuffer, input.hasJumpInputBufferTime);        // ������Ծ���뻺��
          }
          if (input.attack && player.canAirAttack)                                                       // �����Ұ��¹������ҿ��Խ��п��й���
               stateMachine.SwitchState(typeof(PlayerState_AirAttack));                                      // �л������й���״̬
          if (!groundDetector.isAir && player.currentLayer == (int)LayerType.Player)                     // �����ҷǿ����ҵ�ǰ�㼶Ϊ Player
               stateMachine.SwitchState(typeof(PlayerState_Land));                                            // �л���½��״̬
     }

     public override void PhysicUpdate()
     {
          player.Move(constants.moveSpeed);  // �ƶ���ҵ�λ��
          player.Fall(stateDuration);
     }
}
