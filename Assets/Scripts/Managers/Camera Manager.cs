using Cinemachine;
using UnityEngine;

/// <summary>
/// 相机管理器
/// </summary>
public class CameraManager : MonoBehaviour
{
     private Camera mainCamera;                             // 调用主摄像机
     private CinemachineFramingTransposer framingTransposer;// 调用镜头定位器
     private CinemachineConfiner2D confiner2D;              // 调用边界限制器
     private BoxCollider2D bulletBounde;                    // 调用子弹边界
     [SerializeField] private Collider2D cameraCollider;    // 缓存相机边界

     // 储存获取到的所有相机震源
     public CinemachineImpulseSource[] cameraImpulseSource;

     [Header("相机接收事件")]
     [Tooltip("相机震动事件")] public VoidEventSO cameraShakeEvent;

     private void Awake()
     {
          mainCamera = GetComponentInChildren<Camera>();
          framingTransposer = GetComponentInChildren<CinemachineFramingTransposer>();
          confiner2D = GetComponentInChildren<CinemachineConfiner2D>();

          bulletBounde = GetComponentInChildren<BoxCollider2D>();

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
          GetNewCameraBounds();
          SetBulletLeftBounds();

          Debug.Log(framingTransposer.m_TrackedObjectOffset);
     }

     /// <summary>
     /// 获取新的相机边界
     /// </summary>
     private void GetNewCameraBounds()
     {
          // 搜索并缓存特定标签的物体
          var obj = GameObject.FindGameObjectWithTag(TagType.CameraBounds.ToString());

          if (obj == null)    // 如果为空
               return;             // 停止运行
          cameraCollider = obj.GetComponent<Collider2D>();  // 缓存搜索到的物体里的碰撞体
          confiner2D.m_BoundingShape2D = cameraCollider;    // 提交给相机边界
          confiner2D.InvalidateCache(); // 清除缓存
     }

     /// <summary>
     /// 启动相机震动
     /// </summary>
     public void OnCameraShake()
     {
          cameraImpulseSource[1].m_ImpulseDefinition.m_ImpulseDuration = 0.25f;
          cameraImpulseSource[1].GenerateImpulseWithForce(0.25f); 
     }

     /// <summary>
     /// 设置飞弹存货边界
     /// </summary>
     private void SetBulletLeftBounds()
     {
          float cameraWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
          float cameraHeight = mainCamera.orthographicSize * 2;
          bulletBounde.size = new Vector2(cameraWidth, cameraHeight) * 1.5f;
     }

}

