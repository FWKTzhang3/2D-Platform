using UnityEngine;

[CreateAssetMenu(menuName = ("Data/StateMachine/PlayerState/PlayerState_Move"), fileName = ("PlayerState_Move"))]
public class PlayerState_Move : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          currentMoveSpeed = player.moveSpeed;   // 让当前移动速度等于玩家控制器获取的移动速度 
     }

     public override void LogicUpdate()
     {
          if (player.isHurt)                                          // 如果玩家处于受伤状态
               stateMachine.SwitchState(typeof(PlayerState_Hurt));         // 切换到 Hurt 状态
          else if (player.isDeath)                                    // 反之如果玩家处于死亡状态
               stateMachine.SwitchState(typeof(PlayerState_Death));        // 切换到 Death 状态

          if (!groundDetector.isAir)                                  // 如果玩家在地面上
          {
               if (!input.move)                                            // 如果没有移动输入
                    stateMachine.SwitchState(typeof(PlayerState_Idle));         // 切换到 Idle 状态
               if (input.jump)                                             // 如果按下了跳跃键
                    stateMachine.SwitchState(typeof(PlayerState_Jump));         // 切换到 Jump 状态
               if (input.attack)                                           // 如果按下了攻击键
                    stateMachine.SwitchState(typeof(PlayerState_Attack_1));     // 切换到 Attack_1 状态
               if (input.skill)
                    stateMachine.SwitchState(typeof(PlayerState_FastMagic));    // 切换到 FastMagic 状态
          }
          else                                                             // 反之
               stateMachine.SwitchState(typeof(PlayerState_CoyoteTime));        // 如果在空中，则切换到 CoyoteTime 状态

          currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, constants.moveSpeed, constants.moveAcceration * Time.deltaTime);      // 移动速度递增到目标速度
     }

     public override void PhysicUpdate()
     {
          player.Move(currentMoveSpeed);
     }
}
