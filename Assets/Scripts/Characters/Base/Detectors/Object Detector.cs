using UnityEditor;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
     [Header("中心点物体")]
     public GameObject entity;

     [Header("检测设置")]
     [SerializeField,Tooltip("攻击盒子起点")] private Vector2 attackDetectionStarPos;
     [SerializeField,Tooltip("攻击盒子尺寸")] private Vector2 attackBoxSize;
     [SerializeField,Tooltip("追击盒子起点")] private Vector2 chaseDetectionStarPos;
     [SerializeField,Tooltip("追击盒子尺寸")] private Vector2 chaseBoxSize;

     [Header("监测目标")]
     [SerializeField] private LayerMask playerLayer;

     // 检测结果数组（目前限制为只储存一个，问就是想节约内存）
     public RaycastHit2D[] rayCastHits = new RaycastHit2D[1]; // 射线
     private Collider2D[] colliders = new Collider2D[1];       // 碰撞

     private Vector2 entityFaceScale => entity.transform.lossyScale; // 目标物体面朝方向
     private Vector2 centerPos => entity.transform.position;     // 中心点坐标

     [Header("检测状态")]
     public bool isAttackPlayer;   // 攻击             
     public bool isChasePlayer;    // 追击          

     private void OnDrawGizmos()
     {
          ChaseBoxDraw();
          AttackBoxDraw();
     }

     private void Update()
     {
          CheckChasePlayer();
          CheckAttackPlayer();
     }

     /// <summary>
     /// 绘制追击检测范围
     /// </summary>
     private void ChaseBoxDraw()
     {
          Vector2 starPos = FaceBoxStarPos(chaseDetectionStarPos, chaseBoxSize, entityFaceScale);
          Gizmos.color = Color.red;
          Gizmos.DrawWireCube(centerPos + starPos, chaseBoxSize);
     }

     /// <summary>
     /// 绘制攻击检测范围
     /// </summary>
     private void AttackBoxDraw()
     {
          Vector2 starPos = FaceBoxStarPos(attackDetectionStarPos, attackBoxSize, entityFaceScale);
          Gizmos.color = Color.yellow;
          Gizmos.DrawWireCube(centerPos + starPos, attackBoxSize);
     }

     /// <summary>
     /// 追击检测范围
     /// </summary>
     private void CheckChasePlayer()
     {                                                                                     
          Vector2 starPos = FaceBoxStarPos(chaseDetectionStarPos, chaseBoxSize, entityFaceScale);  // 缓存起始坐标
          isChasePlayer = CheckBox(centerPos + starPos, chaseBoxSize, playerLayer);
     }

     /// <summary>
     /// 攻击检测范围
     /// </summary>
     private void CheckAttackPlayer()
     {
          Vector2 starPos = FaceBoxStarPos(attackDetectionStarPos, attackBoxSize, entityFaceScale);          // 缓存起始坐标
          isAttackPlayer = CheckCastBox(centerPos + starPos, attackBoxSize, entityFaceScale, playerLayer);
     }

     /// <summary>
     /// 面朝方向盒子起始坐标
     /// </summary>
     /// <param name="detectionStarPos"> 检测起始坐标 </param>
     /// <param name="boxSize">          盒子尺寸    </param>
     /// <param name="Scale">            面朝方向    </param>
     /// <returns> 一个二维坐标（起始坐标） </returns>
     private Vector2 FaceBoxStarPos(Vector2 detectionStarPos, Vector2 boxSize, Vector2 Scale)
     {
          // 大概这样算，嘛，以 Y 轴为轴心就是了
          Vector2 starPos = new Vector2(detectionStarPos.x + boxSize.x / 2 * Scale.x, detectionStarPos.y);
          return starPos;
     }

     /// <summary>
     /// 盒子检测
     /// </summary>
     /// <param name="boxPos">     盒子起始坐标    </param>
     /// <param name="boxSize">    盒子尺寸      </param>
     /// <param name="layer">      检测层级      </param>
     /// <returns> 一个 bool值（ 0 为 flase , 1 为 true ） </returns>
     private bool CheckBox(Vector2 boxPos, Vector2 boxSize, LayerMask layer)
     {
          // 旋转角度也用不到（目前 OVO ）
          int boxCount = Physics2D.OverlapBoxNonAlloc(boxPos, boxSize, 0, colliders, layer);
          return boxCount != 0;
     }

     /// <summary>
     /// 射线盒子检测
     /// </summary>
     /// <param name="boxPos">     盒子起始坐标    </param>
     /// <param name="boxSize">    盒子尺寸      </param>
     /// <param name="rayDir">     射线方向      </param>
     /// <param name="layer">      检测层级      </param>
     /// <returns> 一个 bool值（ 0 为 flase , 1 为 true ） </returns>
     private bool CheckCastBox(Vector2 boxPos, Vector2 boxSize, Vector2 rayDir, LayerMask layer)
     {
          // 用的不多，角度....可能会用到，射线突出....没必要
          int castBoxCount = Physics2D.BoxCastNonAlloc(boxPos, boxSize, 0f, rayDir, rayCastHits, 0, layer);
          return castBoxCount != 0;
     }
}
