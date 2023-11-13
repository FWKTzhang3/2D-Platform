using UnityEngine;

[CreateAssetMenu(menuName = ("Data/StateMachine/PlayerState/PlayerState_Jump"), fileName = ("PlayerState_Jump"))]
public class PlayerState_Jump : PlayerState
{
     private float targetPos;         // 目标坐标，即跳跃的最高点
     private float currentPos;        // 当前坐标，用于判断是否达到目标坐标

     public override void Enter()
     {
          base.Enter();
          targetPos = player.getRigidbodyPosY + constants.jumpDistance;    // 获取目标坐标（当前刚体坐标 + 目标跳跃距离）
          currentJumpForce = constants.jumpForce;                          // 设置当前跳跃力度等于目标力度
     }

     public override void LogicUpdate()
     {
          currentPos = player.getRigidbodyPosY;                            // 持续更新当前坐标点

          if (player.isHurt)                                               // 如果玩家处于受伤状态
               stateMachine.SwitchState(typeof(PlayerState_Hurt));              // 切换到受伤状态
          else if (player.isDeath)                                         // 如果玩家处于死亡状态
               stateMachine.SwitchState(typeof(PlayerState_Death));             // 切换到死亡状态

          if (input.attack && player.canAirAttack)                         // 如果玩家按下攻击键且可以在空中攻击
               stateMachine.SwitchState(typeof(PlayerState_AirAttack));         // 切换到空中攻击状态
          if (input.stopJump || currentPos >= targetPos)                   // 如果放开跳跃键或当前坐标已达到目标坐标
               stateMachine.SwitchState(typeof(PlayerState_Fall));              // 切换到下落状态
     }

     public override void PhysicUpdate()
     {
          currentJumpForce = Mathf.MoveTowards(currentJumpForce, 0f, constants.jumpForceDcelerate * Time.deltaTime);   // 跳跃力度衰减
          player.SetVelocityY(currentJumpForce);       // 设置玩家纵向速度为当前跳跃力度
          player.Move(constants.moveSpeed);            // 控制玩家水平移动
     }
}
