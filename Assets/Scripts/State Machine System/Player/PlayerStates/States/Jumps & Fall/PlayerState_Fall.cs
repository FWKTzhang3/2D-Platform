using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Fall", fileName = "PlayerState_Fall")]
public class PlayerState_Fall : PlayerState
{
     public override void Enter()
     {
          base.Enter();
     }

     public override void LogicUpdate()
     {
          if (player.isHurt)                                          // 如果玩家受伤
               stateMachine.SwitchState(typeof(PlayerState_Hurt));         // 则切换到受伤状态
          else if (player.isDeath)                                    // 如果玩家死亡
               stateMachine.SwitchState(typeof(PlayerState_Death));        // 则切换到死亡状态

          if (input.jump)                                                                                // 如果玩家按下跳跃键
          {
               if (player.canAirJump)                                                                    // 如果可以进行空中跳跃
                    stateMachine.SwitchState(typeof(PlayerState_AirJump));                                    // 切换到空中跳跃状态
               else                                                                                      // 反之             
                    input.SetInputBuffer(input.CallbackJumpInputBuffer, input.hasJumpInputBufferTime);        // 启动跳跃输入缓冲
          }
          if (input.attack && player.canAirAttack)                                                       // 如果玩家按下攻击键且可以进行空中攻击
               stateMachine.SwitchState(typeof(PlayerState_AirAttack));                                      // 切换到空中攻击状态
          if (!groundDetector.isAir && player.currentLayer == (int)LayerType.Player)                     // 如果玩家非空中且当前层级为 Player
               stateMachine.SwitchState(typeof(PlayerState_Land));                                            // 切换到陆地状态
     }

     public override void PhysicUpdate()
     {
          player.Move(constants.moveSpeed);  // 移动玩家的位置
          player.Fall(stateDuration);
     }
}
