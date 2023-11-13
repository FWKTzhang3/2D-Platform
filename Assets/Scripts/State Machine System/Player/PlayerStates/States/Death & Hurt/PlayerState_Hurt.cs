using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Hurt", fileName = "PlayerState_Hurt")]
public class PlayerState_Hurt : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          player.SetVelocity(Vector2.zero);                                                    // ��ͣ
          currentKnockbackForceX = player.knockbackForceX;                                     // ��ǰ�������ȵ��ڻ�ȡ�Ļ�������
          currentKnockbackForceY = (player.knockbackDirY > 0) ? player.knockbackForceY : 0;    // ������˷���Y���� 0 �򽫻�ȡ�Ļ������ȸ�ֵ����ǰ�������� ��֮ ��ֵΪ 0 
     }

     public override void LogicUpdate()
     {
          if (stateDuration >= player.knockbackHardTime)                                                                // ������˵�ǰ״̬ʱ����ڻ���Ӳֱʱ��
               stateMachine.SwitchState(groundDetector.isAir ? typeof(PlayerState_Idle) : typeof(PlayerState_Fall));         // ������ڿ��о��л��� idle ��֮�л��� fall

          currentKnockbackForceX = Mathf.MoveTowards(currentKnockbackForceX, 1f, constants.knockbackForceDcelerate * Time.deltaTime);       // ��������X˥��
          currentKnockbackForceY = Mathf.MoveTowards(currentKnockbackForceY, -50f, constants.knockbackForceDcelerate * Time.deltaTime);     // ��������Y˥��
     }

     public override void PhysicUpdate()
     {
          player.SetVelocityX(currentKnockbackForceX * player.knockbackDirX);   // ������ҵ�ˮƽ�ٶ�Ϊ��ǰ����������˷���ĳ˻�
          if (groundDetector.isAir)                                             // �������ڿ���
               player.SetVelocityY(currentKnockbackForceY);                          // ������ҵĴ�ֱ�ٶ�Ϊ��ǰ������
     }

     public override void Exit()
     {
          player.isHurt = false;                                                // �˳�״̬ʱ������ҵ�����״̬��Ϊfalse
     }
}
