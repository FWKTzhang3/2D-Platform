using UnityEngine;

public class HurtBoxController : MonoBehaviour
{
     [Header("被控制组件")]
     public BoxCollider2D HurtBox;
     public Attacker attacker;

     [Header("攻击数据")]
     public AttackStats attackStats;
     private int currentAttackCount;

     [Header("判定范围预览")]
     [SerializeField] private bool showMeBox;
     [SerializeField] private int showAttackBox;

     private void OnDrawGizmos()
     {
          int boxCount = showAttackBox - 1;

          if (showMeBox && boxCount < attackStats.hurtBoxOffset.Length)
          {
               Gizmos.color = Color.green;
               Vector2 startPos = transform.position;
               Gizmos.DrawWireCube(startPos + attackStats.hurtBoxOffset[boxCount], attackStats.hurtBoxSize[boxCount]);
          }
     }

     /// <summary>
     /// 启动 攻击触发器
     /// </summary>
     private void EnabledHurtBox() => HurtBox.enabled = true;

     /// <summary>
     /// 关闭 攻击触发器
     /// </summary>
     private void DisableHurtBox() => HurtBox.enabled = false;

     /// <summary>
     /// 输出攻击序号
     /// </summary>
     /// <param name="Number"> 序号 </param>
     private void OutAttackNumber(int Number)
     {
          if (currentAttackCount != Number)            // 若当前序号不等于输入的序号
          {
               currentAttackCount = Number;            // 让当前序号等于输入序号
               AssAttackState(currentAttackCount);     // 执行赋值（输入修改后的序号）
          }
     }

     /// <summary>
     /// 赋值
     /// </summary>
     /// <param name="Count"> 序号 </param>
     private void AssAttackState(int Count)
     {
          int correctCount = Count - 1;

          // 触发器坐标和尺寸的赋值
          HurtBox.offset = attackStats.hurtBoxOffset[correctCount];
          HurtBox.size = attackStats.hurtBoxSize[correctCount];

          // 数值赋值
          attacker.damage = attackStats.damage[correctCount];                        // 攻击伤害

          attacker.knockbackForceX = attackStats.knockbackForceX[correctCount];      // 击退力度X
          attacker.knockbackForceY = attackStats.knockbackForceY[correctCount];      // 击退力度Y
          attacker.knockbackHardTime = attackStats.knockbackHardTime[correctCount];  // 击退硬直时间

          attacker.hitStopTime = attackStats.hitStopTime[correctCount];                   // 顿帧时间
          attacker.hitStopRecoveSpeed = attackStats.hitStopRecoveSpeed[correctCount];     // 顿帧恢复速度
     }
}
