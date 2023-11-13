using UnityEngine;

[CreateAssetMenu(menuName = ("Data/StateMachine/PlayerState/PlayerState_Move"), fileName = ("PlayerState_Move"))]
public class PlayerState_Move : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          currentMoveSpeed = player.moveSpeed;   // �õ�ǰ�ƶ��ٶȵ�����ҿ�������ȡ���ƶ��ٶ� 
     }

     public override void LogicUpdate()
     {
          if (player.isHurt)                                          // �����Ҵ�������״̬
               stateMachine.SwitchState(typeof(PlayerState_Hurt));         // �л��� Hurt ״̬
          else if (player.isDeath)                                    // ��֮�����Ҵ�������״̬
               stateMachine.SwitchState(typeof(PlayerState_Death));        // �л��� Death ״̬

          if (!groundDetector.isAir)                                  // �������ڵ�����
          {
               if (!input.move)                                            // ���û���ƶ�����
                    stateMachine.SwitchState(typeof(PlayerState_Idle));         // �л��� Idle ״̬
               if (input.jump)                                             // �����������Ծ��
                    stateMachine.SwitchState(typeof(PlayerState_Jump));         // �л��� Jump ״̬
               if (input.attack)                                           // ��������˹�����
                    stateMachine.SwitchState(typeof(PlayerState_Attack_1));     // �л��� Attack_1 ״̬
               if (input.skill)
                    stateMachine.SwitchState(typeof(PlayerState_FastMagic));    // �л��� FastMagic ״̬
          }
          else                                                             // ��֮
               stateMachine.SwitchState(typeof(PlayerState_CoyoteTime));        // ����ڿ��У����л��� CoyoteTime ״̬

          currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, constants.moveSpeed, constants.moveAcceration * Time.deltaTime);      // �ƶ��ٶȵ�����Ŀ���ٶ�
     }

     public override void PhysicUpdate()
     {
          player.Move(currentMoveSpeed);
     }
}
