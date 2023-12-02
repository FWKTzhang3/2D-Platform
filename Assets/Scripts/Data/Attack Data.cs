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
     [Header("������Χ")]
     [Tooltip("����")] public Vector2 hurtBoxOffset;
     [Tooltip("�ߴ�")] public Vector2 hurtBoxSize;

     [Header("�˺�����")]
     public float damage;

     [Header("��������")]
     public Vector2 knockback;

     [Header("Ӳֱʱ��")]
     public float hardTime;

     [Header("��֡")]
     [Tooltip("��֡ʱ��")] public float hitStopTime;
     [Tooltip("��֡�ָ��ٶ�")] public float hitStopRecoveSpeed;
}