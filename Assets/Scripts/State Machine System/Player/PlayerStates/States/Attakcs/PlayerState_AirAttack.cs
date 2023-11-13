using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_AirAttack", fileName = "PlayerState_AirAttack")]
public class PlayerState_AirAttack : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          player.CooldownTimer(player.Action_CanAirAttack, constants.airAttackTime);
          currentMoveSpeed = constants.moveSpeed;
     }

     public override void LogicUpdate()
     {
          if (isAnimationFinished)
               stateMachine.SwitchState(typeof(PlayerState_Fall));

          currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, 0, constants.airMoveDeceleration * Time.deltaTime);
     }

     public override void PhysicUpdate()
     {
          player.SetVelocityX(currentMoveSpeed * input.axesX);
     }
}
