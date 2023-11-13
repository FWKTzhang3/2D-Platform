using UnityEngine;

[CreateAssetMenu(menuName = ("Data/StateMachine/PlayerState/PlayerState_Land"), fileName = ("PlayerState_Land"))]
public class PlayerState_Land : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          LandingHard(); 
          player.canAirJump = true;     // ��ҿ��Խ��п�����Ծ
     }

     public override void LogicUpdate()
     {
          if (player.isHurt)                                               // ����������
               stateMachine.SwitchState(typeof(PlayerState_Hurt));              // �л�������״̬
          else if (player.isDeath)                                         // ��֮����������
               stateMachine.SwitchState(typeof(PlayerState_Death));             // �л�������״̬

          if (stateDuration < currentHardTime)                             // �����ǰ״̬ʱ��С��Ӳֱʱ��
               return;                                                          // ִֹͣ�к����߼�

          if (input.jump || (input.hasJumpInputBuffer & input.holdJump))   // �����Ұ�����Ծ��������Ծ���뻺��״̬�ҳ�����ס��Ծ��
               stateMachine.SwitchState(typeof(PlayerState_Jump));              // �л�����Ծ״̬
          if (isAnimationFinished)                                         // ����������Ž���
               stateMachine.SwitchState(typeof(PlayerState_Idle));              // �л�����ֹ״̬
          if (input.move)                                                  // �����Ұ����ƶ���
               stateMachine.SwitchState(typeof(PlayerState_Move));              // �л����ƶ�״̬
     }

     /// <summary>
     /// �������״̬ʱ�������Ӳֱ����
     /// </summary>
     private void LandingHard()
     {
          float currentTime = Mathf.Abs(player.getRigibodyVelocityY) / 50;                                                   // ��ȡ��ҵ�ǰ��ֱ�ٶȵľ���ֵ��������50����õ�ǰ���Ӳֱʱ��
          currentHardTime = currentTime > constants.minHardTimeThreshold ? Mathf.Min(currentTime, constants.hardTime) : 0f;  // �жϵ�ǰ���Ӳֱʱ���Ƿ񳬹���ֵ�����������ȡ��Сֵ������Ϊ0
          player.SetVelocity(Vector2.zero);                                                                                  // ����ҵ��ٶ���Ϊ�㣬ֹͣ�ƶ�                                                                             // ������������
     }
}
