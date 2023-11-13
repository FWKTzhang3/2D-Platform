using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState  // �̳�PlayerState
{
     public override void Enter()
     {
          base.Enter();
          currentMoveSpeed = player.moveSpeed;   // �õ�ǰ�ƶ��ٶȵ�����ҿ�������ȡ���ƶ��ٶ� 
     }

     public override void LogicUpdate()
     {
          if (player.isHurt || player.isDeath)                                                                // ���������˻�����
          {
               stateMachine.SwitchState(player.isHurt ? typeof(PlayerState_Hurt) : typeof(PlayerState_Death));       // �л�������״̬������״̬
               return;                                                                                             // ��ִ�к����߼� (����һ���Ͻ� ovo)
          }

          if (!groundDetector.isAir)                                       // ��� isAir Ϊ�٣��ڵ����ϣ�
          {
               if (input.move)                                                  // ��� move ������
                    stateMachine.SwitchState(typeof(PlayerState_Move));              // �л��� Move ״̬
               if (input.crouch)                                                // ��� crouch ������
                    stateMachine.SwitchState(typeof(PlayerState_Crouch));            // �л��� Crouch ״̬
               if (input.jump)                                                  // ��� jump ������
                    stateMachine.SwitchState(typeof(PlayerState_Jump));              // �л��� Jump ״̬
               if (input.attack)                                                // ��� attack ������
                    stateMachine.SwitchState(typeof(PlayerState_Attack_1));          // �л��� Attack_1 ״̬
               if (input.skill)
                    stateMachine.SwitchState(typeof(PlayerState_FastMagic));    // �л��� FastMagic ״̬
          }
          else                                                             // ��֮
               stateMachine.SwitchState(typeof(PlayerState_Fall));              // �л��� Fall ״̬

          
          currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, 0f, constants.moveDeceleration * Time.deltaTime);      // �����㷨
          // ʹ�� Mathf.MoveTowards �����𽥼�С currentMoveSpeed ��ֵ��ʹ��ӽ� 0
          // ����1����ǰֵ��currentMoveSpeed��
          // ����2��Ŀ��ֵ��0f��
          // ����3��ÿ���С���ٶȣ�constants.moveDeceleration * Time.deltaTime��
     }

     public override void PhysicUpdate()
     {
          player.SetVelocityX(currentMoveSpeed * player.faceDir);
     }
}
