using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyState_Move", fileName = "EnemyState_Move")]
public class EnemyState_Move : EnemyState
{
     public override void Enter()
     {
          base.Enter();
     }

     public override void LogicUpdate()
     {
          if (enemy.isHurt || enemy.isDeath)                                                                  // 如果敌人受伤或死亡
          {
               stateMachine.SwitchState(enemy.isHurt ? typeof(EnemyState_Hurt) : typeof(EnemyState_Death));   // 切换到受伤状态或死亡状态
               return;                                                                                        // 不执行后续逻辑 (主打一个严谨 ovo)
          }

          if (groundDetector.isTouchWall || groundDetector.isEdge)    // 如果碰到了墙或者走到了悬崖的边缘
               stateMachine.SwitchState(typeof(EnemyState_Idle));          // 则切换到空闲状态

          if (objectDetector.isAttackPlayer && enemy.canAttack)       // 如果检测到敌人距离玩家足够近，并且当前可以进行攻击动作
               stateMachine.SwitchState(typeof(EnemyState_Attack));        // 则切换到攻击状态

          else if (objectDetector.isChasePlayer || enemy.isChasing)   // 如果敌人当前不在攻击范围内，但是检测到敌人距离玩家足够近或正在追逐玩家
               stateMachine.SwitchState(typeof(EnemyState_Chase));         // 则切换到追逐状态
     }

     public override void PhysicUpdate()
     {
          enemy.Move(constants.moveSpeed);
     }
}
