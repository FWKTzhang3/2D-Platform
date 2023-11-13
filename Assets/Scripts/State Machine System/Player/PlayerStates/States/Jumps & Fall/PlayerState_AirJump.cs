using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_AirJump", fileName = "PlayerState_AirJump")]
public class PlayerState_AirJump : PlayerState
{
     private float targetPos;         // Ŀ�����꣬����Ծ����ߵ�
     private float currentPos;        // ��ǰ���꣬�����ж��Ƿ�ﵽĿ������

     public override void Enter()
     {
          base.Enter();
          targetPos = player.getRigidbodyPosY + constants.jumpDistance;    // ��ʼ��Ŀ������Ϊ��ǰλ�ü��ϳ��� jumpDistance
          currentJumpForce = constants.jumpForce;                          // ����ǰ��Ծ����ֵΪ���� jumpForce
          player.canAirJump = false;                                       // ���ò��ܶ��ο�����Ծ                                    
     }

     public override void LogicUpdate()
     {
          currentPos = player.getRigidbodyPosY;                       // �������µ�ǰ�����                     

          if (player.isHurt)                                          // ����������
               stateMachine.SwitchState(typeof(PlayerState_Hurt));         // �л�������״̬
          else if (player.isDeath)                                    // ����������
               stateMachine.SwitchState(typeof(PlayerState_Death));        // �л�������״̬

          if (input.attack && player.canAirAttack)                    // �����Ұ��¹������ҿ��Խ��п��й���
               stateMachine.SwitchState(typeof(PlayerState_AirAttack));    // �л������й���״̬
          if (currentPos >= targetPos)                                // �����ǰλ���Ѿ��ﵽĿ��λ��  
               stateMachine.SwitchState(typeof(PlayerState_Fall));         // �л�������״̬ 
     }

     public override void PhysicUpdate()
     {
          currentJumpForce = Mathf.MoveTowards(currentJumpForce, 0f, constants.jumpForceDcelerate * Time.deltaTime);    // ���ݳ��� jumpForceDcelerate ��֡ʱ�� delta time ������Ծ��˥��ֵ��ʹ��Ծ���𽥼�С
          player.SetVelocityY(currentJumpForce);                                                                        // ������Ҵ�ֱ�ٶ�Ϊ��ǰ��Ծ��
          player.Move(constants.moveSpeed);                                                                             // �ƶ���ҵ�λ��
     }
}
