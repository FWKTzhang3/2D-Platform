using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_AirJump", fileName = "PlayerState_AirJump")]
public class PlayerState_AirJump : PlayerState
{
     private float targetPos;         // 目标坐标，即跳跃的最高点
     private float currentPos;        // 当前坐标，用于判断是否达到目标坐标

     public override void Enter()
     {
          base.Enter();
          targetPos = player.getRigidbodyPosY + constants.jumpDistance;    // 初始化目标坐标为当前位置加上常量 jumpDistance
          currentJumpForce = constants.jumpForce;                          // 将当前跳跃力赋值为常量 jumpForce
          player.canAirJump = false;                                       // 设置不能二段空中跳跃                                    
     }

     public override void LogicUpdate()
     {
          currentPos = player.getRigidbodyPosY;                       // 持续更新当前坐标点                     

          if (player.isHurt)                                          // 如果玩家受伤
               stateMachine.SwitchState(typeof(PlayerState_Hurt));         // 切换到受伤状态
          else if (player.isDeath)                                    // 如果玩家死亡
               stateMachine.SwitchState(typeof(PlayerState_Death));        // 切换到死亡状态

          if (input.attack && player.canAirAttack)                    // 如果玩家按下攻击键且可以进行空中攻击
               stateMachine.SwitchState(typeof(PlayerState_AirAttack));    // 切换到空中攻击状态
          if (currentPos >= targetPos)                                // 如果当前位置已经达到目标位置  
               stateMachine.SwitchState(typeof(PlayerState_Fall));         // 切换到下落状态 
     }

     public override void PhysicUpdate()
     {
          currentJumpForce = Mathf.MoveTowards(currentJumpForce, 0f, constants.jumpForceDcelerate * Time.deltaTime);    // 根据常量 jumpForceDcelerate 和帧时间 delta time 计算跳跃力衰减值，使跳跃力逐渐减小
          player.SetVelocityY(currentJumpForce);                                                                        // 设置玩家垂直速度为当前跳跃力
          player.Move(constants.moveSpeed);                                                                             // 移动玩家的位置
     }
}
