using UnityEngine;

[CreateAssetMenu(menuName = ("Data/CharacterStats/attackStats"), fileName = ("AttackStats_"))]
public class AttackStats : ScriptableObject
{
     [Header("攻击判定范围相关")]
     public Vector2[] hurtBoxOffset;
     public Vector2[] hurtBoxSize;

     [Header("伤害相关")]
     public int[] damage;

     [Header("击退相关")]
     public float[] knockbackForceX;
     public float[] knockbackForceY;
     public float[] knockbackHardTime;

     [Header("顿帧相关")]
     [Tooltip("顿帧持续时间")] public float[] hitStopTime;
     [Tooltip("顿帧恢复速度")]public float[] hitStopRecoveSpeed;
}
