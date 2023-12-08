using System.Collections;
using UnityEngine;
using Cinemachine;

/// <summary>
/// 相机管理器
/// </summary>
public class CameraManager : MonoBehaviour
{
     // 调用组件
     private Camera _mainCamera;                                  // 调用主摄像机
     private CinemachineFramingTransposer _framingTransposer;     // 调用镜头定位器
     private CinemachineConfiner2D _confiner2D;                   // 调用边界限制器
     private CinemachineTargetGroup _targetGroup;                 // 调用目标组
     private BoxCollider2D _bulletBounde;                         // 调用子弹边界

     private ShakeManager _shakeManager;

     private Collider2D _cameraCollider;                          // 相机边界

     // 缓存
     private Coroutine _cameraCenterCoroutine;
     private Coroutine _cameraTrackedOffsetCoroutine;

     [Header("玩家物体")]
     [SerializeField, Tooltip("玩家控制器")] private InputSystem _input; 

     [Header("相机接收事件")]
     [SerializeField, Tooltip("相机震动事件")] private VoidEventSO _cameraShakeEvent;

     [Header("相机追踪死区相关")]
     [SerializeField, Tooltip("相机初始死区")] private Vector2 _camerDeadZone;
     [SerializeField, Tooltip("相机居中延迟时间")] public float _centeringDelay;
     [SerializeField, Tooltip("相机居中速度")] public float _centeringSpeed;

     [Header("相机追踪点相关")]
     [SerializeField, Tooltip("相机追踪点中心点")] private Vector2 _camerTrackedOffset;
     [SerializeField, Tooltip("追踪点最大偏移量")] private float _camerTrackedMaxOffset;

     [Header("触发器数值")]
     [SerializeField, Tooltip("飞弹存活尺寸倍率")] private float _sizeMultiplier;

     private void Awake()
     {
          _mainCamera = GetComponentInChildren<Camera>();
          _framingTransposer = GetComponentInChildren<CinemachineFramingTransposer>();
          _confiner2D = GetComponentInChildren<CinemachineConfiner2D>();
          
          _shakeManager = GetComponentInChildren<ShakeManager>();

          _bulletBounde = GetComponentInChildren<BoxCollider2D>();

          _targetGroup = GetComponentInChildren<CinemachineTargetGroup>();
     }

     private void OnEnable()
     {
          _input.JoystickEvent += JoystickEvent;
          _cameraShakeEvent.OnVoidEventRaised += OnCameraShake;
     }

     private void OnDisable()
     {
          _input.JoystickEvent -= JoystickEvent;
          _cameraShakeEvent.OnVoidEventRaised -= OnCameraShake;
     }

     private void OnCameraShake()
     {
          _shakeManager.OnIncreasingShake(new Vector2(0, -1), 0.05f, 0.05f);
     }

     private void Start()
     {
          _cameraDeadZone = _camerDeadZone;
          _trackedOffset = _camerTrackedOffset;
          _setBulletLeftBounds = _sizeMultiplier;
          SetNewCameraBounds();
     }

     private void JoystickEvent(Vector2 inputDirection)
     {
          if (_cameraCenterCoroutine != null)          // 如果居中当前协程不为空
          {
               StopCoroutine(_cameraCenterCoroutine);       // 打断协程
               _cameraDeadZone = _camerDeadZone;            // 恢复死区
          }
          OnCameraTrackedOffset(inputDirection);       // 启动相机偏移
     }

     /// <summary>
     /// 设置新的相机边界
     /// </summary>
     private void SetNewCameraBounds()
     {
          var obj = GameObject.FindGameObjectWithTag(TagType.CameraBounds.ToString());    // 搜索并缓存特定标签的物体
          if (obj == null)    // 如果为空
               return;             // 停止运行
          _cameraCollider = obj.GetComponent<Collider2D>();  // 缓存搜索到的物体里的碰撞体
          _confiner2D.m_BoundingShape2D = _cameraCollider;    // 提交给相机边界
          _confiner2D.InvalidateCache();                     // 清除缓存
     }

     /// <summary>
     /// 设置飞弹存活边界
     /// </summary>
     /// <remarks>飞行的子弹的存活范围，用来限制其不会无限飞，但是又能保证与在摄像机范围附近一直正常存在</remarks>
     private float _setBulletLeftBounds
     {
          set
          {
               if (_mainCamera != null && _bulletBounde != null)                               // 检测相机和边界是是否不为空
               { 
                    float cameraWidth = _mainCamera.orthographicSize * 2 * _mainCamera.aspect;      // 计算相机宽度
                    float cameraHeight = _mainCamera.orthographicSize * 2;                          // 计算相机高度
                    _bulletBounde.size = new Vector2(cameraWidth, cameraHeight) * value;            // 赋值给触发器
               }
          }
     }

     #region 相机追踪点偏移相关

     /// <summary>
     /// 启动相机偏移
     /// </summary>
     /// <param name="targetDirection"> 输入目标方向 </param>
     private void OnCameraTrackedOffset(Vector2 targetDirection)
     {
          if (_cameraTrackedOffsetCoroutine != null)             // 如果当前缓存的协程不为空
               StopCoroutine(_cameraTrackedOffsetCoroutine);          // 停止一次协程（保证唯一性）
          _cameraTrackedOffsetCoroutine = StartCoroutine(CameraTrackedOffsetCoroutine(targetDirection));  // 协程！启动！（并且添加到缓存里复用）

     }

     /// <summary>
     /// 相机偏移协程
     /// </summary>
     /// <param name="targetDirection"> 偏移方向 </param>
     private IEnumerator CameraTrackedOffsetCoroutine(Vector2 targetDirection)
     {
          Vector2 targetOffset = _camerTrackedOffset + _camerTrackedMaxOffset * targetDirection;    // 计算目标偏移位置
          while (_trackedOffset != targetOffset)  // 如果当前位置不等于目标位置
          {
               float distance = Vector2.Distance(_trackedOffset, targetOffset);                // 计算起点和终点的直线距离
               float clampedSpeed = Mathf.Clamp(distance, 0.05f, _camerTrackedMaxOffset * 2);  // 计算偏移速度（在两点距离中限制最大值和最小值）
               // 用 MoveTowards 线性插值到目标位置
               _trackedOffset = Vector2.MoveTowards(_trackedOffset, targetOffset, Time.deltaTime * clampedSpeed);
               yield return null;       // 必须有一个帧延迟，不然直接崩溃
          }
          _trackedOffset = targetOffset;          // 精确到目标点
          if (targetDirection == Vector2.zero)    // 如果当前偏移方向是中心
               OnCenterCamera();                       // 启动居中
     }

     /// <summary>
     /// 相机追踪点
     /// </summary>
     private Vector2 _trackedOffset
     {
          get => _framingTransposer.m_TrackedObjectOffset; 
          set => _framingTransposer.m_TrackedObjectOffset = value;           
     }

     #endregion

     #region 相机死区相关

     /// <summary>
     /// 居中相机
     /// </summary>
     private void OnCenterCamera()
     {
          if (_cameraCenterCoroutine != null)             // 如果当前缓存的协程不为空
               StopCoroutine(_cameraCenterCoroutine);          // 停止一次协程（保证唯一性）
          _cameraCenterCoroutine = StartCoroutine(CenterCameraCoroutine(_centeringDelay, _centeringSpeed));  // 协程！启动！（并且添加到缓存里复用）
     }

     /// <summary>
     /// 相机居中协程
     /// </summary>
     /// <param name="centeringDelay"> 延迟时间 </param>
     /// <param name="centeringSpeed"> 居中速度 </param>
     private IEnumerator CenterCameraCoroutine(float centeringDelay, float centeringSpeed)
     {
          yield return new WaitForSeconds(centeringDelay);                           // 延迟居中
          Vector2 currentDeadZone = _cameraDeadZone;                                 // 缓存当前死区
          while (currentDeadZone.x > 0 || currentDeadZone.y > 0)                     // 如果当前死区大于零则进行递减循环
          {
               currentDeadZone -= Vector2.one * Time.deltaTime * centeringSpeed;          // 对缓存的死区数值进行递减
               _cameraDeadZone = currentDeadZone;                                         // 赋值死区
               yield return null;                                                         // 延迟一帧（使循环不会太快，这里一些希望丝滑一点变化的就这样就好）
          }
          _cameraDeadZone = Vector2.zero;                                            // 更精准的让摄像机居中
          _cameraDeadZone = _camerDeadZone;                                          // 还原死区
     }

     /// <summary>
     /// 相机死区
     /// </summary>
     /// <returns>
     /// <para>相机死区的二维向量</para>
     /// <para>我自己整合成二维向量</para>
     /// </returns>
     private Vector2 _cameraDeadZone
     {
          get
          {
               // 缓存并整合返回
               float deadZoneX = _framingTransposer.m_DeadZoneWidth;      
               float deadZoneY = _framingTransposer.m_DeadZoneHeight;     
               return new Vector2(deadZoneX, deadZoneY);                  
          }
          set
          {
               // 获取并分别输出
               _framingTransposer.m_DeadZoneWidth = value.x;             
               _framingTransposer.m_DeadZoneHeight = value.y;
          }
     }

     #endregion
}