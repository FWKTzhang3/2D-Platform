using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Death", fileName = "PlayerState_Death")]
public class PlayerState_Death : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          player.SetVelocity(Vector2.zero);  // ��ͣ��������

          // ���µ�ǰ����
          currentKnockbackForceX = player.knockbackForceX;       
          currentKnockbackForceY = player.knockbackForceY;
     }

     public override void LogicUpdate()
     {
          currentKnockbackForceX = Mathf.MoveTowards(currentKnockbackForceX, 0f, constants.knockbackForceDcelerate * Time.deltaTime);       // ��������X˥��
          currentKnockbackForceY = Mathf.MoveTowards(currentKnockbackForceY, -50f, constants.knockbackForceDcelerate * Time.deltaTime);     // ��������Y˥��
     }

     public override void PhysicUpdate()
     {
          player.SetVelocityX(currentKnockbackForceX * player.knockbackDirX);   // ������ҵ�ˮƽ�ٶ�Ϊ��ǰ����������˷���ĳ˻�
          if (groundDetector.isAir)                                             // �������ڿ���
               player.SetVelocityY(currentKnockbackForceY);                          // ������ҵĴ�ֱ�ٶ�Ϊ��ǰ������
     }
}
