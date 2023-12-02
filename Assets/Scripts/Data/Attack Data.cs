using System;
using UnityEngine;

[CreateAssetMenu(menuName = ("Data/AttackDatas"), fileName = ("AttackDatas"))]
public class AttackDatas : ScriptableObject
{
     [SerializeField] public AttackData[] normalAttackData;
     [SerializeField] public AttackData[] airAttackData;
     [SerializeField] public AttackData[] teleAttackData;
     [SerializeField] public AttackData[] skillAttackData;
}

[Serializable]
public class AttackData
{
     [Header("攻击范围")]
     [Tooltip("坐标")] public Vector2 hurtBoxOffset;
     [Tooltip("尺寸")] public Vector2 hurtBoxSize;

     [Header("伤害数据")]
     public float damage;

     [Header("击退力度")]
     public Vector2 knockback;

     [Header("硬直时间")]
     public float hardTime;

     [Header("顿帧")]
     [Tooltip("顿帧时间")] public float hitStopTime;
     [Tooltip("顿帧恢复速度")] public float hitStopRecoveSpeed;
}