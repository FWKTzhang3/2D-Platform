using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = ("Event/CameraShakeEventSO"), fileName = ("CameraShakeEventSO"))]
public class CameraShakeEventSO : ScriptableObject
{
     public UnityAction OnShakeEventRaised;

     public void RaiseEvent()
     {
          OnShakeEventRaised?.Invoke();
     }
}

public struct CameraShakeValue
{
     public Vector2 shakeVelocity;
     public float shakeForce;
     public float shakeDuration;
}
