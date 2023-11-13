using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_Attack_3", fileName = "PlayerState_Attack_3")]
public class PlayerState_Attack_3 : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          if (input.axesX == player.faceDir)
               player.SetVelocityX(constants.moveAttackSpeed * player.faceDir);
     }

     public override void LogicUpdate()
     {    
          if (isAnimationFinished)                                    // �����������
               stateMachine.SwitchState(typeof(PlayerState_Idle));         // �л��� Idle
     }
}                                                                          
                                                                           
                                                                                