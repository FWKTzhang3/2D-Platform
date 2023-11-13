using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Crouch", fileName = "PlayerState_Crouch")]
public class PlayerState_Crouch : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          player.SetVelocityX(0);  // 设置玩家水平速度为 0
     }

     public override void LogicUpdate()
     {
          if (player.isHurt)                                          // 如果玩家处于受伤状态
               stateMachine.SwitchState(typeof(PlayerState_Hurt));         // 切换到 Hurt 状态
          if (player.isDeath)                                         // 如果玩家处于死亡状态
               stateMachine.SwitchState(typeof(PlayerState_Death));        // 切换到 Death 状态

          if (!input.crouch)                                          // 如果玩家没有蹲下的输入
               stateMachine.SwitchState(typeof(PlayerState_Idle));         // 切换到 Idle 状态
          if (input.jump && groundDetector.isOneWayPlatform)          // 如果玩家按下了跳跃键
          {
               // 调用 Player 类中的 SwitchLayer 方法，将玩家所在层从 Cross 层切换回 Player 层，动画过渡时间为 0.4s，并调用 Action_CanAirAttack 方法
               player.SwitchLayer(LayerType.Cross, LayerType.Player, 0.5f, player.Action_CanAirAttack);
               stateMachine.SwitchState(typeof(PlayerState_Fall));   // 切换到 Fall 状态
          }
     }

     public override void PhysicUpdate()
     {
          player.Move(0);
     }
}
