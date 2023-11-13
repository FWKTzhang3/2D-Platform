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
          if (enemy.isHurt || enemy.isDeath)                                                                  // ����������˻�����
          {
               stateMachine.SwitchState(enemy.isHurt ? typeof(EnemyState_Hurt) : typeof(EnemyState_Death));   // �л�������״̬������״̬
               return;                                                                                        // ��ִ�к����߼� (����һ���Ͻ� ovo)
          }

          if (groundDetector.isEdge || groundDetector.isTouchWall)    // �����⵽�����ߵ������µı�Ե����������ǽ��
               enemy.SwitchFaceDir();                                      // ���л����˵��泯����

          if (objectDetector.isAttackPlayer && enemy.canAttack)       // �����⵽���˾�������㹻�������ҵ�ǰ���Խ��й�������
               stateMachine.SwitchState(typeof(EnemyState_Attack));        // ���л�������״̬

          if (!enemy.isChasing)                                       // ������˵�ǰû����׷��״̬
               stateMachine.SwitchState(typeof(EnemyState_Idle));          // ���л�������״̬
     }

     public override void PhysicUpdate()
     {
          enemy.Move(constants.chaseSpeed);
     }
}
