using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyState_Idel", fileName = "EnemyState_Idel")]
public class EnemyState_Idle : EnemyState
{
     public override void Enter()
     {
          base.Enter();
          enemy.SetVelocityX(0);   // 将敌人的 X 轴速度设置为 0，停止移动
     }

     public override void LogicUpdate()
     {
          if (enemy.isHurt || enemy.isDeath)                                                                  // 如果敌人受伤或死亡
          {
               stateMachine.SwitchState(enemy.isHurt ? typeof(EnemyState_Hurt) : typeof(EnemyState_Death));   // 切换到受伤状态或死亡状态
               return;                                                                                        // 不执行后续逻辑 (主打一个严谨 ovo)
          }

          if (objectDetector.isAttackPlayer && enemy.canAttack)       // 如果检测到敌人距离玩家足够近，并且当前可以进行攻击动作
               stateMachine.SwitchState(typeof(EnemyState_Attack));        // 则切换到攻击状态

          if (objectDetector.isAttackPlayer || stateDuration <= 3)   // 如果敌人当前没有在追逐状态并且状态持续时间小于等于3秒
               return;                                                     // 则直接返回，不执行后续代码

          if (groundDetector.isEdge || groundDetector.isTouchWall)    // 如果检测到敌人走到了悬崖的边缘或者碰到了墙壁
               enemy.SwitchFaceDir();                                      // 则切换敌人的面朝方向
          if (!(groundDetector.isEdge || groundDetector.isTouchWall))  // 如果没有到达地板边缘或者碰到敌人
               stateMachine.SwitchState(typeof(EnemyState_Move));          // 则切换到移动状态
     }
}