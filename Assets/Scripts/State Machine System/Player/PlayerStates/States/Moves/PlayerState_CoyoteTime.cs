using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_CoyoteTime", fileName = "PlayerState_CoyoteTime")]
public class PlayerState_CoyoteTime : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          player.SetUseGraviy(0);       // ����Ϊ 0 ����
     }

     public override void LogicUpdate()
     {
          if (player.isHurt)                                               // �����Ҵ�������״̬
               stateMachine.SwitchState(typeof(PlayerState_Hurt));              // �л��� Hurt ״̬
          else if (player.isDeath)                                         // �����Ҵ�������״̬
               stateMachine.SwitchState(typeof(PlayerState_Death));             // �л��� Death ״̬

          if (input.jump)                                                  // �����������Ծ��
               stateMachine.SwitchState(typeof(PlayerState_Jump));              // �л��� Jump ״̬
          else if (stateDuration > constants.coyoteTime || !input.move)    // ��� CoyoteTime ʱ���������û���ƶ�����
               stateMachine.SwitchState(typeof(PlayerState_Fall));              // �л��� Fall ״̬
     }

     public override void PhysicUpdate()
     {
          player.Move(constants.moveSpeed / 2);   // ������ҿ������� Move �����������ƶ��ٶȵ�һ�룬ʵ�ֿ��м��ٹ���
     }

     public override void Exit()
     {
          player.SetUseGraviy(1);       // ����Ϊ 1 ��������׼������
     }
}
