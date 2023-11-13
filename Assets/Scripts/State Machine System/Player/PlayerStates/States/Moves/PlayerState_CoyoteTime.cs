using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_CoyoteTime", fileName = "PlayerState_CoyoteTime")]
public class PlayerState_CoyoteTime : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          player.SetUseGraviy(0);       // 设置为 0 重力
     }

     public override void LogicUpdate()
     {
          if (player.isHurt)                                               // 如果玩家处于受伤状态
               stateMachine.SwitchState(typeof(PlayerState_Hurt));              // 切换到 Hurt 状态
          else if (player.isDeath)                                         // 如果玩家处于死亡状态
               stateMachine.SwitchState(typeof(PlayerState_Death));             // 切换到 Death 状态

          if (input.jump)                                                  // 如果按下了跳跃键
               stateMachine.SwitchState(typeof(PlayerState_Jump));              // 切换到 Jump 状态
          else if (stateDuration > constants.coyoteTime || !input.move)    // 如果 CoyoteTime 时间结束或者没有移动输入
               stateMachine.SwitchState(typeof(PlayerState_Fall));              // 切换到 Fall 状态
     }

     public override void PhysicUpdate()
     {
          player.Move(constants.moveSpeed / 2);   // 调用玩家控制器的 Move 方法，传入移动速度的一半，实现空中减速功能
     }

     public override void Exit()
     {
          player.SetUseGraviy(1);       // 设置为 1 重力（标准重力）
     }
}
