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
          if (enemy.isHurt || enemy.isDeath)                                                                  // ����������˻�����
          {
               stateMachine.SwitchState(enemy.isHurt ? typeof(EnemyState_Hurt) : typeof(EnemyState_Death));   // �л�������״̬������״̬
               return;                                                                                        // ��ִ�к����߼� (����һ���Ͻ� ovo)
          }

          if (groundDetector.isTouchWall || groundDetector.isEdge)    // ���������ǽ�����ߵ������µı�Ե
               stateMachine.SwitchState(typeof(EnemyState_Idle));          // ���л�������״̬

          if (objectDetector.isAttackPlayer && enemy.canAttack)       // �����⵽���˾�������㹻�������ҵ�ǰ���Խ��й�������
               stateMachine.SwitchState(typeof(EnemyState_Attack));        // ���л�������״̬

          else if (objectDetector.isChasePlayer || enemy.isChasing)   // ������˵�ǰ���ڹ�����Χ�ڣ����Ǽ�⵽���˾�������㹻��������׷�����
               stateMachine.SwitchState(typeof(EnemyState_Chase));         // ���л���׷��״̬
     }

     public override void PhysicUpdate()
     {
          enemy.Move(constants.moveSpeed);
     }
}
