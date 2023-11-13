using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = ("Event/FloatEventSO"), fileName = ("FloatEventSO"))]
public class FloatEventSO : ScriptableObject
{
     public UnityAction<float>[] OnFloatEventRaised;

     public void RaiseEvent(float value)
     {
          if (OnFloatEventRaised != null)
          {
               foreach (var action in OnFloatEventRaised)
               {
                    action?.Invoke(value);
               }
          }
     }
}
