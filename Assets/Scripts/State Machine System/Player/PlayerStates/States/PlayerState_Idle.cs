using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState  // 继承PlayerState
{
     public override void Enter()
     {
          base.Enter();
          currentMoveSpeed = player.moveSpeed;   // 让当前移动速度等于玩家控制器获取的移动速度 
     }

     public override void LogicUpdate()
     {
          if (player.isHurt || player.isDeath)                                                                // 如果玩家受伤或死亡
          {
               stateMachine.SwitchState(player.isHurt ? typeof(PlayerState_Hurt) : typeof(PlayerState_Death));       // 切换到受伤状态或死亡状态
               return;                                                                                             // 不执行后续逻辑 (主打一个严谨 ovo)
          }

          if (!groundDetector.isAir)                                       // 如果 isAir 为假（在地面上）
          {
               if (input.move)                                                  // 如果 move 被按下
                    stateMachine.SwitchState(typeof(PlayerState_Move));              // 切换到 Move 状态
               if (input.crouch)                                                // 如果 crouch 被按下
                    stateMachine.SwitchState(typeof(PlayerState_Crouch));            // 切换到 Crouch 状态
               if (input.jump)                                                  // 如果 jump 被按下
                    stateMachine.SwitchState(typeof(PlayerState_Jump));              // 切换到 Jump 状态
               if (input.attack)                                                // 如果 attack 被按下
                    stateMachine.SwitchState(typeof(PlayerState_Attack_1));          // 切换到 Attack_1 状态
               if (input.skill)
                    stateMachine.SwitchState(typeof(PlayerState_FastMagic));    // 切换到 FastMagic 状态
          }
          else                                                             // 反之
               stateMachine.SwitchState(typeof(PlayerState_Fall));              // 切换到 Fall 状态

          
          currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, 0f, constants.moveDeceleration * Time.deltaTime);      // 滑步算法
          // 使用 Mathf.MoveTowards 方法逐渐减小 currentMoveSpeed 的值，使其接近 0
          // 参数1：当前值（currentMoveSpeed）
          // 参数2：目标值（0f）
          // 参数3：每秒减小的速度（constants.moveDeceleration * Time.deltaTime）
     }

     public override void PhysicUpdate()
     {
          player.SetVelocityX(currentMoveSpeed * player.faceDir);
     }
}
