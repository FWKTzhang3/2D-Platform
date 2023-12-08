using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击数据
/// </summary>
[CreateAssetMenu(menuName = ("Data/AttackDatas"), fileName = ("AttackDatas"))]
public class AttackData : ScriptableObject
{
     public Attack_Type_Data[] attack_Type_Data;

     public Dictionary<AttackDataType, AttackDetails[]> attackDataDictionary;   // 攻击数据字典（以 AttackDataType 为键，以 AttackDetails[] 为值）

     /// <summary>
     /// 初始化数据
     /// </summary>
     public void InitializeData()
     {
          // 实例化字典
          attackDataDictionary = new Dictionary<AttackDataType, AttackDetails[]>();

          // 遍历所有 attack_Type_Data 的数据，依次存入字典
          foreach (var attackTypeData in attack_Type_Data)
               attackDataDictionary.Add(attackTypeData.attackDataType, attackTypeData.attackDetail);
     }
}

/// <summary>
/// 攻击类型和数据
/// </summary>
[Serializable]
public class Attack_Type_Data
{
     public AttackDataType attackDataType;
     public AttackDetails[] attackDetail;
}

/// <summary>
/// 攻击数据类型
/// </summary>
public enum AttackDataType
{
     Normal_Ground_Attack,
     Normal_Air_Attack,
     Normal_Crouch_Attack,
     Special_Attack,
     Skill_Attack,
}

/// <summary>
/// 攻击数据
/// </summary>
[Serializable]
public class AttackDetails
{
     [Header("攻击范围")]
     [Tooltip("坐标")] public Vector2 hurtBoxOffset;
     [Tooltip("尺寸")] public Vector2 hurtBoxSize;

     [Header("伤害数据")]
     public int damage;

     [Header("击退力度")]
     public Vector2 knockbackForce;

     [Header("硬直时间")]
     public float hitstunTime;

     [Header("顿帧")]
     [Tooltip("顿帧时间")] public float hitStopTime;
     [Tooltip("顿帧恢复速度")] public float hitStopRecoveSpeed;
}