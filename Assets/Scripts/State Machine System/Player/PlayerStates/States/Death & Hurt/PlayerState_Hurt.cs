using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Hurt", fileName = "PlayerState_Hurt")]
public class PlayerState_Hurt : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          player.SetVelocity(Vector2.zero);                                                    // 急停
          currentKnockbackForceX = player.knockbackForceX;                                     // 当前击退力度等于获取的击退力度
          currentKnockbackForceY = (player.knockbackDirY > 0) ? player.knockbackForceY : 0;    // 如果击退方向Y大于 0 则将获取的击退力度赋值给当前击退力度 反之 赋值为 0 
     }

     public override void LogicUpdate()
     {
          if (stateDuration >= player.knockbackHardTime)                                                                // 如果击退当前状态时间大于击退硬直时间
               stateMachine.SwitchState(groundDetector.isAir ? typeof(PlayerState_Idle) : typeof(PlayerState_Fall));         // 如果不在空中就切换到 idle 反之切换到 fall

          currentKnockbackForceX = Mathf.MoveTowards(currentKnockbackForceX, 1f, constants.knockbackForceDcelerate * Time.deltaTime);       // 击退力度X衰减
          currentKnockbackForceY = Mathf.MoveTowards(currentKnockbackForceY, -50f, constants.knockbackForceDcelerate * Time.deltaTime);     // 击退力度Y衰减
     }

     public override void PhysicUpdate()
     {
          player.SetVelocityX(currentKnockbackForceX * player.knockbackDirX);   // 设置玩家的水平速度为当前击退力与击退方向的乘积
          if (groundDetector.isAir)                                             // 如果玩家在空中
               player.SetVelocityY(currentKnockbackForceY);                          // 设置玩家的垂直速度为当前击退力
     }

     public override void Exit()
     {
          player.isHurt = false;                                                // 退出状态时，将玩家的受伤状态设为false
     }
}
