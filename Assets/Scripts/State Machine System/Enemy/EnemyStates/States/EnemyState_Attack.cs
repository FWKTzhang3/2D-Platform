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
          if (enemy.isHurt || enemy.isDeath)                                                                  // ����������˻�����
          {
               stateMachine.SwitchState(enemy.isHurt ? typeof(EnemyState_Hurt) : typeof(EnemyState_Death));   // �л�������״̬������״̬
               return;                                                                                        // ��ִ�к����߼� (����һ���Ͻ� ovo)
          }

          if (isAnimationFinished)                                    // ����������Ž���
               stateMachine.SwitchState(typeof(EnemyState_Idle));          // ���л�������״̬
     }
}
