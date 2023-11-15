using Cinemachine;
using UnityEngine;

/// <summary>
/// ���������
/// </summary>
public class CameraManager : MonoBehaviour
{
     private Camera mainCamera;                             // �����������
     private CinemachineFramingTransposer framingTransposer;// ���þ�ͷ��λ��
     private CinemachineConfiner2D confiner2D;              // ���ñ߽�������
     private BoxCollider2D bulletBounde;                    // �����ӵ��߽�
     [SerializeField] private Collider2D cameraCollider;    // ��������߽�

     // �����ȡ�������������Դ
     public CinemachineImpulseSource[] cameraImpulseSource;

     [Header("��������¼�")]
     [Tooltip("������¼�")] public VoidEventSO cameraShakeEvent;

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
     /// ��ȡ�µ�����߽�
     /// </summary>
     private void GetNewCameraBounds()
     {
          // �����������ض���ǩ������
          var obj = GameObject.FindGameObjectWithTag(TagType.CameraBounds.ToString());

          if (obj == null)    // ���Ϊ��
               return;             // ֹͣ����
          cameraCollider = obj.GetComponent<Collider2D>();  // ���������������������ײ��
          confiner2D.m_BoundingShape2D = cameraCollider;    // �ύ������߽�
          confiner2D.InvalidateCache(); // �������
     }

     /// <summary>
     /// ���������
     /// </summary>
     public void OnCameraShake()
     {
          cameraImpulseSource[1].m_ImpulseDefinition.m_ImpulseDuration = 0.25f;
          cameraImpulseSource[1].GenerateImpulseWithForce(0.25f); 
     }

     /// <summary>
     /// ���÷ɵ�����߽�
     /// </summary>
     private void SetBulletLeftBounds()
     {
          float cameraWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
          float cameraHeight = mainCamera.orthographicSize * 2;
          bulletBounde.size = new Vector2(cameraWidth, cameraHeight) * 1.5f;
     }

}

