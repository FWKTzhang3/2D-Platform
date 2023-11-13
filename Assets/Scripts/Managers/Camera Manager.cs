using Cinemachine;
using UnityEngine;

/// <summary>
/// 相机管理器
/// </summary>
public class CameraManager : MonoBehaviour
{
     private CinemachineConfiner2D confiner2D;    // 调用 confiner2D 
     private Collider2D cameraCollider;           // 缓存 cameraCollider

     // 储存获取到的所有相机震源
     private CinemachineImpulseSource[] cameraImpulseSource;

     [Header("相机接收事件")]
     [Tooltip("相机震动事件")] public VoidEventSO cameraShakeEvent;

     private void Awake()
     {
          confiner2D = GetComponentInChildren<CinemachineConfiner2D>();
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

}

