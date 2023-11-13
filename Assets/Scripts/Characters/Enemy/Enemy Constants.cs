using UnityEngine;

/// <summary>
/// 敌人常量
/// </summary>
public class EnemyConstants : MonoBehaviour
{
     [Header("移动相关")]
     public float moveSpeed;
     public float chaseSpeed;

     [Header("冷却时间相关")]
     public float attackIntervalTime;
     public float chaseCancelDelayTime;
}
