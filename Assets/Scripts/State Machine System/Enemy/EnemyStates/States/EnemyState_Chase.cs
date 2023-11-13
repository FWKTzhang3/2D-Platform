using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyState_Chase", fileName = "EnemyState_Chase")]
public class EnemyState_Chase : EnemyState
{
     public override void Enter()
     {
          base.Enter();
          enemy.chaseCancelCountTime = constants.chaseCancelDelayTime;
     }

     public override void LogicUpdate()
     {
          if (enemy.isHurt || enemy.isDeath)                                                                  // 如果敌人受伤或死亡
          {
               stateMachine.SwitchState(enemy.isHurt ? typeof(EnemyState_Hurt) : typeof(EnemyState_Death));   // 切换到受伤状态或死亡状态
               return;                                                                                        // 不执行后续逻辑 (主打一个严谨 ovo)
          }

          if (groundDetector.isEdge || groundDetector.isTouchWall)    // 如果检测到敌人走到了悬崖的边缘或者碰到了墙壁
               enemy.SwitchFaceDir();                                      // 则切换敌人的面朝方向

          if (objectDetector.isAttackPlayer && enemy.canAttack)       // 如果检测到敌人距离玩家足够近，并且当前可以进行攻击动作
               stateMachine.SwitchState(typeof(EnemyState_Attack));        // 则切换到攻击状态

          if (!enemy.isChasing)                                       // 如果敌人当前没有在追逐状态
               stateMachine.SwitchState(typeof(EnemyState_Idle));          // 则切换到空闲状态
     }

     public override void PhysicUpdate()
     {
          enemy.Move(constants.chaseSpeed);
     }
}
