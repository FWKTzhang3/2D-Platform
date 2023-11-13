using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyState_Idel", fileName = "EnemyState_Idel")]
public class EnemyState_Idle : EnemyState
{
     public override void Enter()
     {
          base.Enter();
          enemy.SetVelocityX(0);   // �����˵� X ���ٶ�����Ϊ 0��ֹͣ�ƶ�
     }

     public override void LogicUpdate()
     {
          if (enemy.isHurt || enemy.isDeath)                                                                  // ����������˻�����
          {
               stateMachine.SwitchState(enemy.isHurt ? typeof(EnemyState_Hurt) : typeof(EnemyState_Death));   // �л�������״̬������״̬
               return;                                                                                        // ��ִ�к����߼� (����һ���Ͻ� ovo)
          }

          if (objectDetector.isAttackPlayer && enemy.canAttack)       // �����⵽���˾�������㹻�������ҵ�ǰ���Խ��й�������
               stateMachine.SwitchState(typeof(EnemyState_Attack));        // ���л�������״̬

          if (objectDetector.isAttackPlayer || stateDuration <= 3)   // ������˵�ǰû����׷��״̬����״̬����ʱ��С�ڵ���3��
               return;                                                     // ��ֱ�ӷ��أ���ִ�к�������

          if (groundDetector.isEdge || groundDetector.isTouchWall)    // �����⵽�����ߵ������µı�Ե����������ǽ��
               enemy.SwitchFaceDir();                                      // ���л����˵��泯����
          if (!(groundDetector.isEdge || groundDetector.isTouchWall))  // ���û�е���ذ��Ե������������
               stateMachine.SwitchState(typeof(EnemyState_Move));          // ���л����ƶ�״̬
     }
}