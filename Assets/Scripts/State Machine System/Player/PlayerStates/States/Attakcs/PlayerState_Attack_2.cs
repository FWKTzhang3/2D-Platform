using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Attack_2", fileName = "PlayerState_Attack_2")]
public class PlayerState_Attack_2 : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          if(input.axesX == player.faceDir)
               player.SetVelocityX(constants.moveAttackSpeed * player.faceDir);
     }

     public override void LogicUpdate()
     {
          /*
          if (input.attack)                                                                         // 如果玩家按下攻击键
               input.SetInputBuffer(input.CallbackAttackInputBuffer, input.hasAttackInputBufferTime);    // 将当前帧添加到攻击输入缓冲区中
          */
          if (isAnimationFinished)                                                                  // 如果当前播放的攻击动画已经结束
          {
               if (input.attack || input.hasAttackInputBuffer)                                      // 如果玩家按下了攻击键或攻击输入缓冲区中有可用的攻击输入
                    stateMachine.SwitchState(typeof(PlayerState_Attack_3));                              // 切换到攻击 3 状态
               else                                                                                 // 反之
                    stateMachine.SwitchState(typeof(PlayerState_Idle));                                  // 切换到待机状态
          }
     }
}
