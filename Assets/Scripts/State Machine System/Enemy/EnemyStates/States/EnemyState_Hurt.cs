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
          if (enemy.isHurt || enemy.isDeath)                                                                  // ����������˻�����
          {
               stateMachine.SwitchState(enemy.isHurt ? typeof(EnemyState_Hurt) : typeof(EnemyState_Death));   // �л�������״̬������״̬
               return;                                                                                        // ��ִ�к����߼� (����һ���Ͻ� ovo)
          }

          if (stateDuration >= enemy.knockbackHardTime)
               stateMachine.SwitchState(typeof(EnemyState_Idle));
     }

}
