using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Death", fileName = "PlayerState_Death")]
public class PlayerState_Death : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          player.SetVelocity(Vector2.zero);  // 急停所有向量

          // 更新当前力度
          currentKnockbackForceX = player.knockbackForceX;       
          currentKnockbackForceY = player.knockbackForceY;
     }

     public override void LogicUpdate()
     {
          currentKnockbackForceX = Mathf.MoveTowards(currentKnockbackForceX, 0f, constants.knockbackForceDcelerate * Time.deltaTime);       // 击退力度X衰减
          currentKnockbackForceY = Mathf.MoveTowards(currentKnockbackForceY, -50f, constants.knockbackForceDcelerate * Time.deltaTime);     // 击退力度Y衰减
     }

     public override void PhysicUpdate()
     {
          player.SetVelocityX(currentKnockbackForceX * player.knockbackDirX);   // 设置玩家的水平速度为当前击退力与击退方向的乘积
          if (groundDetector.isAir)                                             // 如果玩家在空中
               player.SetVelocityY(currentKnockbackForceY);                          // 设置玩家的垂直速度为当前击退力
     }
}
