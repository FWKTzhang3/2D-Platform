using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Crouch", fileName = "PlayerState_Crouch")]
public class PlayerState_Crouch : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          player.SetVelocityX(0);  // �������ˮƽ�ٶ�Ϊ 0
     }

     public override void LogicUpdate()
     {
          if (player.isHurt)                                          // �����Ҵ�������״̬
               stateMachine.SwitchState(typeof(PlayerState_Hurt));         // �л��� Hurt ״̬
          if (player.isDeath)                                         // �����Ҵ�������״̬
               stateMachine.SwitchState(typeof(PlayerState_Death));        // �л��� Death ״̬

          if (!input.crouch)                                          // ������û�ж��µ�����
               stateMachine.SwitchState(typeof(PlayerState_Idle));         // �л��� Idle ״̬
          if (input.jump && groundDetector.isOneWayPlatform)          // �����Ұ�������Ծ��
          {
               // ���� Player ���е� SwitchLayer ��������������ڲ�� Cross ���л��� Player �㣬��������ʱ��Ϊ 0.4s�������� Action_CanAirAttack ����
               player.SwitchLayer(LayerType.Cross, LayerType.Player, 0.5f, player.Action_CanAirAttack);
               stateMachine.SwitchState(typeof(PlayerState_Fall));   // �л��� Fall ״̬
          }
     }

     public override void PhysicUpdate()
     {
          player.Move(0);
     }
}
