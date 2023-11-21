using System.Collections;
using UnityEngine;
using Cinemachine;

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

     private PlayerInput playerInput;

     // 缓存
     private Coroutine cameraCenterCoroutine;
     private Coroutine cameraTrackedOffsetCoroutine;

     private CinemachineImpulseSource[] cameraImpulseSource;     // 相机震源组
     private Collider2D cameraCollider;                          // 相机边界

     [Header("相机接收事件")]
     [SerializeField, Tooltip("相机震动事件")] private VoidEventSO cameraShakeEvent;

     [Header("摄像机状态")]
     [SerializeField, Tooltip("居中状态")] private bool isCameraCentering;
     [SerializeField, Tooltip("偏移状态")] private bool isCameraTrackedOffseting;

     [Header("相机基础数值")]
     [SerializeField, Tooltip("相机死区")] private Vector2 camerDeadZone;
     [SerializeField, Tooltip("相机追踪点偏移")] private Vector2 camerTrackedOffset;

     [Header("触发器数值")]
     [SerializeField, Tooltip("飞弹尺寸倍率")] private float sizeMultiplier;

     [Header("计时数值")]
     [SerializeField, Tooltip("相机居中延迟时间")] public float centeringDelay;
     [SerializeField, Tooltip("相机居中速度")] public float centeringSpeed;
     [SerializeField, Tooltip("追踪点最大偏移量")] private float camerTrackedMaxOffset;
     [SerializeField, Tooltip("追踪点偏移速度")] private float camerTrackedOffsetSpeed;

     private void Awake()
     {
          mainCamera = GetComponentInChildren<Camera>();
          framingTransposer = GetComponentInChildren<CinemachineFramingTransposer>();
          confiner2D = GetComponentInChildren<CinemachineConfiner2D>();

          bulletBounde = GetComponentInChildren<BoxCollider2D>();

          targetGroup = GetComponentInChildren<CinemachineTargetGroup>();
          cameraImpulseSource = GetComponentsInChildren<CinemachineImpulseSource>();

          playerInput = FindObjectOfType<PlayerInput>();
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
          SetCameraDeadZone(camerDeadZone);
          SetTrackedOffset(camerTrackedOffset);
     }

     private void Update()
     {
          UpdateCameraCentering();
          UpdateCameraTrackedOffset();
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

     #region 相机追踪点偏移相关

     /// <summary>
     /// <para>更新相机跟踪偏移。</para>
     /// <para>Update the camera tracked offset.</para>
     /// </summary>
     private void UpdateCameraTrackedOffset()
     {
          // 如果相机跟踪偏移未开启，但是玩家正在使用摇杆偏移
          if (!isCameraTrackedOffseting && playerInput.isStickOffseting)
          {
               isCameraTrackedOffseting = true;             // 开启相机跟踪偏移状态
               OnCameraTrackedOffset();                     // 执行相机跟踪偏移操作
          }
          // 如果相机跟踪偏移已经开启，但是玩家停止使用摇杆偏移
          else if (isCameraTrackedOffseting && !playerInput.isStickOffseting)
          {
               isCameraTrackedOffseting = false;            // 关闭相机跟踪偏移状态
          }
     }

     /// <summary>
     /// <para>执行相机跟踪偏移操作。</para>
     /// <para>Execute camera tracked offset operation.</para>
     /// </summary>
     private void OnCameraTrackedOffset()
     {
          if (cameraTrackedOffsetCoroutine != null)                                       // 如果相机跟踪偏移协程存在
               StopCoroutine(cameraTrackedOffsetCoroutine);                                    // 停止相机跟踪偏移协程
          cameraTrackedOffsetCoroutine = StartCoroutine(CameraTrackedOffsetCoroutine());  // 协程！启动！
     }

     /// <summary>
     /// <para>相机跟踪偏移协程函数。</para>
     /// <para>The coroutine function of camera tracked offset.</para>
     /// </summary>
     private IEnumerator CameraTrackedOffsetCoroutine()
     {
          Vector2 currentOffset = GetTrackedOffset();                                                    // 获取当前跟踪偏移量                        
          while (true)                                                                                        // 启动循环                            
          {
               Vector2 targetOffset = camerTrackedOffset + camerTrackedMaxOffset * playerInput.axes;     // 计算目标跟踪偏移量                        
               currentOffset = Vector2.Lerp(currentOffset, targetOffset, camerTrackedOffsetSpeed);       // 计算当前跟踪偏移量                        
               SetTrackedOffset(currentOffset);                                                          // 设置跟踪偏移量                            
               float distance = Vector2.Distance(currentOffset, targetOffset);                           // 计算当前与目标偏移量的距离                  
               if (distance < 0.025 && !playerInput.isStickOffseting)                                    // 如果当前距离小于一定阈值，且玩家没有使用摇杆偏移
                    break;                                                                                    // 结束协程                            
               yield return null;                                                                        // 等待下一帧更新                            
          }
          SetTrackedOffset(camerTrackedOffset);                                                          // 将跟踪偏移量重置为初始状态
     }

     /// <summary>
     /// 设置相机追踪点
     /// </summary>
     /// <param name="ObjectOffset">相机追踪点坐标</param>
     private void SetTrackedOffset(Vector2 ObjectOffset)
     {
          framingTransposer.m_TrackedObjectOffset = ObjectOffset;
     }

     /// <summary>
     /// 获取当前相机追踪点
     /// </summary>
     /// <returns> 追踪点坐标 </returns>
     private Vector2 GetTrackedOffset()
     {
          return framingTransposer.m_TrackedObjectOffset;
     }

     #endregion

     #region 相机死区相关

     /// <summary>
     /// 更新相机居中
     /// </summary>
     private void UpdateCameraCentering()
     {
          // 如果玩家没有使用摇杆偏移，相机也没有处于居中和跟踪偏移状态
          if (!playerInput.isStickOffseting && !isCameraCentering && !isCameraTrackedOffseting)
          {
               isCameraCentering = true;     // 将相机居中状态声明为真
               OnCenterCamera();             // 执行相机居中操作
          }
          // 如果玩家正在使用摇杆偏移，并且相机同时处于居中和跟踪偏移状态
          else if (playerInput.isStickOffseting && isCameraCentering && isCameraTrackedOffseting)
          {
               isCameraCentering = false;    // 将相机居中状态声明为假
          }
     }

     /// <summary>
     /// 居中相机
     /// </summary>
     private void OnCenterCamera()
     {
          if (cameraCenterCoroutine != null)             // 如果当前缓存的协程不为空
               StopCoroutine(cameraCenterCoroutine);          // 停止一次协程（保证唯一性）
          cameraCenterCoroutine = StartCoroutine(CenterCameraCoroutine(centeringDelay, centeringSpeed));  // 协程！启动！（并且添加到缓存里复用）
     }

     /// <summary>
     /// 相机居中协程
     /// </summary>
     /// <param name="centeringDelay"> 延迟时间 </param>
     /// <param name="centeringSpeed"> 居中速度 </param>
     private IEnumerator CenterCameraCoroutine(float centeringDelay, float centeringSpeed)
     {
          yield return new WaitForSeconds(centeringDelay);                           // 延迟居中
          Vector2 currentDeadZone = GetCameraDeadZone();                                   // 缓存当前死区
          while (currentDeadZone.x > 0 || currentDeadZone.y > 0)                     // 如果当前死区大于零则进行递减循环
          {
               if(playerInput.isStickOffseting)                                        // 如果当前摇杆没有静止
                    break;                                                                // 打断循环继续运行循环后面的程序
               currentDeadZone -= Vector2.one * Time.deltaTime * centeringSpeed;     // 对缓存的死区数值进行递减
               SetCameraDeadZone(currentDeadZone);                                         // 赋值死区
               yield return null;                                                    // 延迟一帧（使循环不会太快，这里一些希望丝滑一点变化的就这样就好，其他延迟会变得有卡顿感）
          }
          if(!playerInput.isStickOffseting)                                             // 如果当前控制器还没有移动
               SetCameraDeadZone(Vector2.zero);                                            // 更精准的让摄像机居中
          SetCameraDeadZone(camerDeadZone);                                                     // 还原死区
     }

     /// <summary>
     /// 设置相机追踪死区
     /// </summary>
     /// <param name="deadZone"> 死区范围 </param>
     /// <remarks>
     /// <para>相机追踪点有个死区，在这个死区里移动相机会有任何移动，但是我这样单独用脚本控制，是为了能动态调整死区范围，over </para>
     /// <para>另外实际大小是按照比例来的，其实也没必要过于纠结实际尺寸就是了</para>
     /// </remarks>
     private void SetCameraDeadZone(Vector2 deadZone)
     {
          framingTransposer.m_DeadZoneWidth = deadZone.x;   // 死区宽度
          framingTransposer.m_DeadZoneHeight = deadZone.y;  // 死区高度
     }

     /// <summary>
     /// 获取相机死区
     /// </summary>
     /// <returns>
     /// <para>相机死区的二维向量</para>
     /// <para>我自己整合成二维向量</para>
     /// </returns>
     private Vector2 GetCameraDeadZone()
     {
          float deadZoneX = framingTransposer.m_DeadZoneWidth;
          float deadZoneY = framingTransposer.m_DeadZoneHeight;

          return new Vector2(deadZoneX, deadZoneY);
     }

     #endregion
}