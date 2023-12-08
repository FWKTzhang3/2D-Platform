using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// </summary>
[CreateAssetMenu(menuName = ("Data/AttackDatas"), fileName = ("AttackDatas"))]
public class AttackData : ScriptableObject
{
     public Attack_Type_Data[] attack_Type_Data;

     public Dictionary<AttackDataType, AttackDetails[]> attackDataDictionary;   // ���������ֵ䣨�� AttackDataType Ϊ������ AttackDetails[] Ϊֵ��

     /// <summary>
     /// ��ʼ������
     /// </summary>
     public void InitializeData()
     {
          // ʵ�����ֵ�
          attackDataDictionary = new Dictionary<AttackDataType, AttackDetails[]>();

          // �������� attack_Type_Data �����ݣ����δ����ֵ�
          foreach (var attackTypeData in attack_Type_Data)
               attackDataDictionary.Add(attackTypeData.attackDataType, attackTypeData.attackDetail);
     }
}

/// <summary>
/// �������ͺ�����
/// </summary>
[Serializable]
public class Attack_Type_Data
{
     public AttackDataType attackDataType;
     public AttackDetails[] attackDetail;
}

/// <summary>
/// ������������
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
/// ��������
/// </summary>
[Serializable]
public class AttackDetails
{
     [Header("������Χ")]
     [Tooltip("����")] public Vector2 hurtBoxOffset;
     [Tooltip("�ߴ�")] public Vector2 hurtBoxSize;

     [Header("�˺�����")]
     public int damage;

     [Header("��������")]
     public Vector2 knockbackForce;

     [Header("Ӳֱʱ��")]
     public float hitstunTime;

     [Header("��֡")]
     [Tooltip("��֡ʱ��")] public float hitStopTime;
     [Tooltip("��֡�ָ��ٶ�")] public float hitStopRecoveSpeed;
}