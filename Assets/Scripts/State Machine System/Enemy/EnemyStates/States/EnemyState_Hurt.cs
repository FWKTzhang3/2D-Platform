using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyState_Hurt", fileName = "EnemyState_Hurt")]
public class EnemyState_Hurt : EnemyState
{
     public override void Enter()
     {
          base.Enter();
          enemy.SetVelocity(Vector2.zero);
     }

     public override void Exit()
     {
          enemy.isHurt = false;
     }

     public override void LogicUpdate()
     {
          if (enemy.isHurt || enemy.isDeath)                                                                  // 如果敌人受伤或死亡
          {
               stateMachine.SwitchState(enemy.isHurt ? typeof(EnemyState_Hurt) : typeof(EnemyState_Death));   // 切换到受伤状态或死亡状态
               return;                                                                                        // 不执行后续逻辑 (主打一个严谨 ovo)
          }

          if (stateDuration >= enemy.knockbackHardTime)
               stateMachine.SwitchState(typeof(EnemyState_Idle));
     }

}
