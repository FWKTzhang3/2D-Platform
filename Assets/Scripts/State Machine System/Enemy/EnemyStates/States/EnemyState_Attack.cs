using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyState_Attack", fileName = "EnemyState_Attack")]
public class EnemyState_Attack : EnemyState
{
     public override void Enter()
     {
          base.Enter();
          enemy.SetVelocityX(0);
          enemy.LoopAssTimer(enemy.Action_CanAttack, false, true, constants.attackIntervalTime);
     }

     public override void LogicUpdate()
     {
          if (enemy.isHurt || enemy.isDeath)                                                                  // 如果敌人受伤或死亡
          {
               stateMachine.SwitchState(enemy.isHurt ? typeof(EnemyState_Hurt) : typeof(EnemyState_Death));   // 切换到受伤状态或死亡状态
               return;                                                                                        // 不执行后续逻辑 (主打一个严谨 ovo)
          }

          if (isAnimationFinished)                                    // 如果动画播放结束
               stateMachine.SwitchState(typeof(EnemyState_Idle));          // 则切换到空闲状态
     }
}
