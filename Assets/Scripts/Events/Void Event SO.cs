using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = ("Event/Void Event"), fileName = ("VoidEventSO"))]
public class VoidEventSO : ScriptableObject
{
     public UnityAction OnVoidEventRaised;

     public void RaiseEvent()
     {
          OnVoidEventRaised?.Invoke();
     }
}
