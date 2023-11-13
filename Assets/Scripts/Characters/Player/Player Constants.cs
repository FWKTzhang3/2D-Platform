using UnityEngine;

/// <summary>
/// 玩家常量
/// </summary>
public class PlayerConstants : MonoBehaviour
{
     [Header("移动相关")]
     [Tooltip("目标移动速度")] public float moveSpeed;
     [Tooltip("移动增加")] public float moveAcceration;
     [Tooltip("移动速度衰减")] public float moveDeceleration;
     [Tooltip("空中移动速度衰减")] public float airMoveDeceleration;
     [Tooltip("土狼时间")] public float coyoteTime;

     [Header("跳跃相关")]
     [Tooltip("跳跃力度")] public float jumpForce;
     [Tooltip("力度衰减")] public float jumpForceDcelerate;
     [Tooltip("跳跃距离")] public float jumpDistance;

     [Header("掉落相关")]
     [Tooltip("掉落速度曲线")] public AnimationCurve fallSpeedCurve;
     [Tooltip("落地硬直时间")] public float hardTime;
     [Tooltip("最低硬直时间阈值")] public float minHardTimeThreshold;

     [Header("攻击相关")]
     [Tooltip("移动速度移动")] public float moveAttackSpeed;
     [Tooltip("空中攻击间隔时间")] public float airAttackTime;

     [Header("受击相关")]
     [Tooltip("震动曲线")] public AnimationCurve shakeCurve;

     [Header("动画相关")]
     [Tooltip("攻击动画加速度")] public float attackAnimationSpeedAcceration;

     [Header("击退相关")]
     [Tooltip("力度衰减")] public float knockbackForceDcelerate;
}
