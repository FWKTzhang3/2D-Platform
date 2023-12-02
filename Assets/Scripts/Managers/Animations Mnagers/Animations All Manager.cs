using UnityEngine;

public class AnimationsAllManager : MonoBehaviour
{
     private AnimationBodyManager m_AnimationBodyManager;
     private AnimationWeaponManager m_AnimationWeaponManager;

     private void Awake()
     {
          m_AnimationBodyManager = GetComponentInChildren<AnimationBodyManager>();
          m_AnimationWeaponManager = GetComponentInChildren<AnimationWeaponManager>();
     }

     public void SetAllAnimatorFloats(float VelocityX, float VelocityY)
     {
          m_AnimationBodyManager.SetAnimatorFloats(VelocityX, VelocityY);
     }

     public void SetAllAnimatorBools(bool airState, bool attackState)
     {
          m_AnimationBodyManager.SetAnimatorBools(airState, attackState);
          m_AnimationWeaponManager.SetAnimatorBools(airState, attackState);
     }

     public void SetAllAnimatorTriggers()
     {
          m_AnimationBodyManager.SetAnimatorTrigger();
          m_AnimationWeaponManager.SetAnimatorTrigger();
     }
}
