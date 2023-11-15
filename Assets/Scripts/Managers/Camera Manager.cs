using System.Collections;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

/// <summary>
/// 相机管理器
/// </summary>
public class CameraManager : MonoBehaviour
{
     // 调用组件
     private Camera mainCamera;                             // 调用主摄像机
     private CinemachineFramingTransposer framingTransposer;// 调用镜头定位器
     private CinemachineConfiner2D confiner2D;              // 调用边界限制器
     private CinemachineTargetGroup targetGroup;            // 调用目标组
     private BoxCollider2D bulletBounde;                    // 调用子弹边界

     // 调用物体
     public PlayerController player;

     // 缓存
     private Coroutine centerCameraCoroutine;
     private CinemachineImpulseSource[] cameraImpulseSource; // 相机震源组
     private Collider2D cameraCollider;                     // 相机边界

     [Header("相机接收事件")]
     [Tooltip("相机震动事件")] public VoidEventSO cameraShakeEvent;
     [Space]
     [Space]
     [Space]
     [Header("相机数值")]
     public Vector2 deadZone;
     [Space]
     [Space]
     [Space]
     [Header("触发器数值")]
     public float sizeMultiplier;
     [Space]
     [Space]
     [Space]
     [Header("计时数值")]
     public float playerIdleTime;

     private void Awake()
     {
          mainCamera = GetComponentInChildren<Camera>();
          framingTransposer = GetComponentInChildren<CinemachineFramingTransposer>();
          confiner2D = GetComponentInChildren<CinemachineConfiner2D>();

          bulletBounde = GetComponentInChildren<BoxCollider2D>();

          targetGroup = GetComponentInChildren<CinemachineTargetGroup>();
          cameraImpulseSource = GetComponentsInChildren<CinemachineImpulseSource>();
     }

     private void OnEnable()
     {
          cameraShakeEvent.OnVoidEventRaised += OnCameraShake;
     }

     private void OnDisable()
     {
          cameraShakeEvent.OnVoidEventRaised -= OnCameraShake;
     }

     private void Start()
     {
          SetNewCameraBounds();
          SetBulletLeftBounds(sizeMultiplier);
     }

     private void Update()
     {
          if (!player.isIdle && GetDeadZone().x <= 0)
          {
               SetDeadZone(deadZone);
          }

          if(player.isIdle && GetDeadZone().x > 0)
          {
               CenterCamera(deadZone, 0f);
          }
     }

     private void CenterCamera(Vector2 deadZone, float yieldTime)
     {
          if (centerCameraCoroutine != null)             // 如果当前缓存的协程不为空
               StopCoroutine(centerCameraCoroutine);          // 停止一次协程（保证唯一性）
          centerCameraCoroutine = StartCoroutine(CenterCameraCoroutine(deadZone, yieldTime));  // 协程！启动！（并且添加到缓存里复用）

     }

     private IEnumerator CenterCameraCoroutine(Vector2 deadZone, float yieldTime)
     {
          yield return new WaitForSeconds(yieldTime);                 // 延迟时间
          for (Vector2 i = deadZone; i.x > 0 && i.y > 0; i -= Vector2.one * Time.deltaTime)
          {
               SetDeadZone(i);
               yield return null;
          }
     }

     /// <summary>
     /// 启动相机震动
     /// </summary>
     public void OnCameraShake()
     {
          if (cameraImpulseSource == null)                                           // 如果当前震动组为空
               return;                                                                    // 停止运行
          cameraImpulseSource[1].m_ImpulseDefinition.m_ImpulseDuration = 0.25f;
          cameraImpulseSource[1].GenerateImpulseWithForce(0.25f); 
     }

     /// <summary>
     /// 设置相机追踪死区
     /// </summary>
     /// <param name="deadZoneWidth"> 宽 </param>
     /// <param name="deadZoneHeight"> 高 </param>
     /// <remarks>
     /// <para>相机追踪点有个死区，在这个死区里移动相机会有任何移动，但是我这样单独用脚本控制，是为了能动态调整死区范围，over </para>
     /// <para>另外实际大小是按照比例来的，其实也没必要过于纠结实际尺寸就是了</para>
     /// </remarks>
     private void SetDeadZone(Vector2 deadZone)
     {
          framingTransposer.m_DeadZoneWidth = deadZone.x;          // 死区宽度
          framingTransposer.m_DeadZoneHeight = deadZone.y;        // 死区高度
     }

     /// <summary>
     /// 设置新的相机边界
     /// </summary>
     private void SetNewCameraBounds()
     {
          var obj = GameObject.FindGameObjectWithTag(TagType.CameraBounds.ToString());    // 搜索并缓存特定标签的物体
          if (obj == null)    // 如果为空
               return;             // 停止运行
          cameraCollider = obj.GetComponent<Collider2D>();  // 缓存搜索到的物体里的碰撞体
          confiner2D.m_BoundingShape2D = cameraCollider;    // 提交给相机边界
          confiner2D.InvalidateCache();                     // 清除缓存
     }

     /// <summary>
     /// 设置飞弹存活边界
     /// </summary>
     /// <param name="boundsSizeMultiplier"> 尺寸倍率 </param>
     /// <remarks>飞行的子弹的存活范围，用来限制其不会无限飞，但是又能保证与在摄像机范围附近一直正常存在</remarks>
     private void SetBulletLeftBounds(float boundsSizeMultiplier)
     {
          if (mainCamera == null || bulletBounde == null)                            // 如果相机和存活边界为空
               return;                                                                    // 停止运行
          float cameraWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;             // 计算相机宽度
          float cameraHeight = mainCamera.orthographicSize * 2;                                // 计算相机高度
          bulletBounde.size = new Vector2(cameraWidth, cameraHeight) * boundsSizeMultiplier;   // 赋值给触发器
     }

     private Vector2 GetDeadZone()
     {
          float deadZoneX = framingTransposer.m_DeadZoneWidth;
          float deadZoneY = framingTransposer.m_DeadZoneHeight;

          return new Vector2(deadZoneX, deadZoneY);
     }
}

