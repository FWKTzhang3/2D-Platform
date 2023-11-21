using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = ("Event/VectorEventSO"), fileName = ("VectorEventSO"))]
public class VectorEventSO : ScriptableObject
{
     public UnityAction<Vector2> OnVectorEventRaised;

     public void RaiseEvent(Vector2 value)
     {
          OnVectorEventRaised?.Invoke(value);
     }
}
