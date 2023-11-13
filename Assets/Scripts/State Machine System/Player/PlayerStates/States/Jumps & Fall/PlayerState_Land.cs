using UnityEngine;

[CreateAssetMenu(menuName = ("Data/StateMachine/PlayerState/PlayerState_Land"), fileName = ("PlayerState_Land"))]
public class PlayerState_Land : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          LandingHard(); 
          player.canAirJump = true;     // 玩家可以进行空中跳跃
     }

     public override void LogicUpdate()
     {
          if (player.isHurt)                                               // 如果玩家受伤
               stateMachine.SwitchState(typeof(PlayerState_Hurt));              // 切换到受伤状态
          else if (player.isDeath)                                         // 反之如果玩家死亡
               stateMachine.SwitchState(typeof(PlayerState_Death));             // 切换到死亡状态

          if (stateDuration < currentHardTime)                             // 如果当前状态时间小于硬直时间
               return;                                                          // 停止执行后续逻辑

          if (input.jump || (input.hasJumpInputBuffer & input.holdJump))   // 如果玩家按下跳跃键或处于跳跃输入缓冲状态且持续按住跳跃键
               stateMachine.SwitchState(typeof(PlayerState_Jump));              // 切换到跳跃状态
          if (isAnimationFinished)                                         // 如果动画播放结束
               stateMachine.SwitchState(typeof(PlayerState_Idle));              // 切换到静止状态
          if (input.move)                                                  // 如果玩家按下移动键
               stateMachine.SwitchState(typeof(PlayerState_Move));              // 切换到移动状态
     }

     /// <summary>
     /// 进入落地状态时进行落地硬直处理
     /// </summary>
     private void LandingHard()
     {
          float currentTime = Mathf.Abs(player.getRigibodyVelocityY) / 50;                                                   // 获取玩家当前垂直速度的绝对值，并除以50，获得当前落地硬直时间
          currentHardTime = currentTime > constants.minHardTimeThreshold ? Mathf.Min(currentTime, constants.hardTime) : 0f;  // 判断当前落地硬直时间是否超过阈值，如果超过则取较小值，否则为0
          player.SetVelocity(Vector2.zero);                                                                                  // 将玩家的速度设为零，停止移动                                                                             // 所有向量归零
     }
}
