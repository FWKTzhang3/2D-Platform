using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState_FastMagic", fileName = "PlayerState_FastMagic")]
public class PlayerState_FastMagic : PlayerState
{
     public override void Enter()
     {
          base.Enter();
          player.SetVelocity(Vector2.zero);
     }

     public override void LogicUpdate()
     {
          if (isAnimationFinished)
               stateMachine.SwitchState(typeof(PlayerState_Idle));
     }
}
