using UnityEngine;

/// <summary>
/// 地面检测器
/// </summary>
public class GroundDetector : MonoBehaviour
{
     [Header("中心点物体")]
     public GameObject entity;                    // 要连接的物体

     [Header("检测点半径")]
     [SerializeField] float detectionRadius;      // 检测半径

     [Header("监测点坐标")]
     [SerializeField] Vector2 bottomOffset;       // 底下检测坐标
     [SerializeField] Vector2 faceOffset;         // 面朝方向检测坐标
     [SerializeField] Vector2 edgeOffset;         // 边缘检测坐标

     [Header("监测目标")]
     [SerializeField] LayerMask groundLayer;           // 地面所在的层级
     [SerializeField] LayerMask oneWayPlatformLayer;   // 单向平台所在的层级

     Collider2D[] colliders = new Collider2D[1];       // 存储覆盖检测到的碰撞器[限制数量]

     [Header("检测状态")]
     public bool isGround;                                  // 是否处于接触地面的布尔值
     public bool isGroundEdge;                              // 是否处于接触地面的布尔值
     public bool isOneWayPlatform;                          // 是否处于接触单向平台的布尔值
     public bool isOneWayPlatformEdge;                      // 是否处于接触单向平台的布尔值
     public bool isTouchWall;                               // 是否处于接触墙面的布尔值
     public bool isAir => !isGround && !isOneWayPlatform;   // 是否在空中
     public bool isEdge => isGroundEdge || isOneWayPlatformEdge;

     private void Update()
     {
          CheckGround();
          CheckWall();
          CheckEdge();
     }

     void OnDrawGizmosSelected()
     {
          //缓存坐标
          Vector2 SPos = transform.position;                          // 起点坐标

          Vector2 Lpos = faceOffset;                                  // 左边的坐标
          Vector2 Rpos = new Vector2(-faceOffset.x, faceOffset.y);    // 右边的坐标

          Vector2 Bpos = bottomOffset;                                // 底部的坐标
          Vector2 BLpos = edgeOffset;                                 // 底部左边的坐标
          Vector2 BRpos = new Vector2(-edgeOffset.x, edgeOffset.y);   // 底部右边的坐标

          Gizmos.color = Color.green;                                 // 辅助线颜色（绿色）

          // 在场景视图中绘制辅助线（坐标，半径）。
          Gizmos.DrawWireSphere(SPos + Lpos, detectionRadius);        // 左边
          Gizmos.DrawWireSphere(SPos + Rpos, detectionRadius);        // 右边

          Gizmos.DrawWireSphere(SPos + Bpos, detectionRadius);        // 脚底
          Gizmos.DrawWireSphere(SPos + BLpos, detectionRadius);       // 脚底左边
          Gizmos.DrawWireSphere(SPos + BRpos, detectionRadius);       // 脚底右边
     }

     /// <summary>
     /// 检测是否站在地面上
     /// </summary>
     private void CheckGround()
     {
          Vector2 detectionPos = (Vector2)transform.position + bottomOffset;                   // 缓存坐标点

          isGround = CheckCircle(detectionPos, detectionRadius, groundLayer);                 // 检测是否在地面上
          isOneWayPlatform = CheckCircle(detectionPos, detectionRadius, oneWayPlatformLayer); // 检测是否在单向平台上
     }

     /// <summary>
     /// 检测是否面向墙体
     /// </summary>
     private void CheckWall()
     {
          Vector2 detectionPos = (Vector2)transform.position + new Vector2(enityFaceDir() * faceOffset.x, faceOffset.y);     // 缓存坐标点

          isTouchWall = CheckCircle(detectionPos, detectionRadius, groundLayer);                                            // 检测是否面向墙体
     }

     /// <summary>
     /// 边缘检测
     /// </summary>
     private void CheckEdge()
     {
          Vector2 detectionPos = (Vector2)transform.position + new Vector2(enityFaceDir() * edgeOffset.x, edgeOffset.y);     // 缓存坐标点

          isGroundEdge = isGround && !CheckCircle(detectionPos, detectionRadius, groundLayer);                              // 检测是否在地面边缘
          isOneWayPlatformEdge = isOneWayPlatform && !CheckCircle(detectionPos, detectionRadius, oneWayPlatformLayer);      // 检测是否在单向平台边缘
     }

     /// <summary>
     /// 检测指定位置是否与指定层的碰撞体相交
     /// </summary>
     /// <param name="detectionPos">检测位置</param>
     /// <param name="detectionRadius">检测半径</param>
     /// <param name="layerMask">指定层</param>
     /// <returns>是否相交</returns>
     private bool CheckCircle(Vector2 detectionPos, float detectionRadius, LayerMask layerMask)
     {
          int circleCount = Physics2D.OverlapCircleNonAlloc(detectionPos, detectionRadius, colliders, layerMask); // 获取指定位置与指定层的碰撞体数量
          return circleCount != 0;                                                                                // 返回是否相交
     }

     /// <summary>
     /// 获取目标个体的面朝方向
     /// </summary>
     /// <returns>面朝方向</returns>
     private int enityFaceDir()
     {
          return (int)entity.transform.lossyScale.x;
     }
}
