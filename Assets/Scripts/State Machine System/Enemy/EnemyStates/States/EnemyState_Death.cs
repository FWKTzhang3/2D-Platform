using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyState_Death", fileName = "EnemyState_Death")]
public class EnemyState_Death : EnemyState
{
     public override void Enter()
     {
          base.Enter();
          enemy.SwitchLayer(LayerType.Muteki);
     }

     public override void LogicUpdate()
     {
          if (isAnimationFinished)
          {
               enemy.Destroyed();
          }
     }
}
