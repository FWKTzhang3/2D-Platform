using System.Collections.Generic;
using UnityEngine;

public class AttackComponet : MonoBehaviour
{
     public AttackDatas attackDatas; // 通过Inspector面板中拖拽赋值

     private Dictionary<string, AttackData[]> attackDataDictionary = new Dictionary<string, AttackData[]>();

     private void Start()
     {
          ReadAttackData(attackDatas.normalAttackData);
          ReadAttackData(attackDatas.airAttackData);
          ReadAttackData(attackDatas.teleAttackData);
          ReadAttackData(attackDatas.skillAttackData);
     }

     private void ReadAttackData(AttackData[] attackDataArray)
     {
          foreach (var attackData in attackDataArray)
          {
               Debug.Log( attackData.hurtBoxOffset);
               Debug.Log( attackData.hurtBoxSize);
               Debug.Log( attackData.damage);
               Debug.Log( attackData.knockback);
               Debug.Log( attackData.hardTime);
               Debug.Log( attackData.hitStopTime);
               Debug.Log( attackData.hitStopRecoveSpeed);
          }
     }
}
