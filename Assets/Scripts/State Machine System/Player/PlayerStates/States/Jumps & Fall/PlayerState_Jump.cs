using UnityEngine;

[CreateAssetMenu(menuName = ("Data/StateMachine/PlayerState/PlayerState_Jump"), fileName = ("PlayerState_Jump"))]
public class PlayerState_Jump : PlayerState
{
     private float targetPos;         // Ŀ�����꣬����Ծ����ߵ�
     private float currentPos;        // ��ǰ���꣬�����ж��Ƿ�ﵽĿ������

     public override void Enter()
     {
          base.Enter();
          targetPos = player.getRigidbodyPosY + constants.jumpDistance;    // ��ȡĿ�����꣨��ǰ�������� + Ŀ����Ծ���룩
          currentJumpForce = constants.jumpForce;                          // ���õ�ǰ��Ծ���ȵ���Ŀ������
     }

     public override void LogicUpdate()
     {
          currentPos = player.getRigidbodyPosY;                            // �������µ�ǰ�����

          if (player.isHurt)                                               // �����Ҵ�������״̬
               stateMachine.SwitchState(typeof(PlayerState_Hurt));              // �л�������״̬
          else if (player.isDeath)                                         // �����Ҵ�������״̬
               stateMachine.SwitchState(typeof(PlayerState_Death));             // �л�������״̬

          if (input.attack && player.canAirAttack)                         // �����Ұ��¹������ҿ����ڿ��й���
               stateMachine.SwitchState(typeof(PlayerState_AirAttack));         // �л������й���״̬
          if (input.stopJump || currentPos >= targetPos)                   // ����ſ���Ծ����ǰ�����ѴﵽĿ������
               stateMachine.SwitchState(typeof(PlayerState_Fall));              // �л�������״̬
     }

     public override void PhysicUpdate()
     {
          currentJumpForce = Mathf.MoveTowards(currentJumpForce, 0f, constants.jumpForceDcelerate * Time.deltaTime);   // ��Ծ����˥��
          player.SetVelocityY(currentJumpForce);       // ������������ٶ�Ϊ��ǰ��Ծ����
          player.Move(constants.moveSpeed);            // �������ˮƽ�ƶ�
     }
}
