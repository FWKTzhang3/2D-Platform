using System.Collections;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

/// <summary>
/// ���������
/// </summary>
public class CameraManager : MonoBehaviour
{
     // �������
     private Camera mainCamera;                             // �����������
     private CinemachineFramingTransposer framingTransposer;// ���þ�ͷ��λ��
     private CinemachineConfiner2D confiner2D;              // ���ñ߽�������
     private CinemachineTargetGroup targetGroup;            // ����Ŀ����
     private BoxCollider2D bulletBounde;                    // �����ӵ��߽�

     // ��������
     public PlayerController player;

     // ����
     private Coroutine centerCameraCoroutine;
     private CinemachineImpulseSource[] cameraImpulseSource; // �����Դ��
     private Collider2D cameraCollider;                     // ����߽�

     [Header("��������¼�")]
     [Tooltip("������¼�")] public VoidEventSO cameraShakeEvent;
     [Space]
     [Space]
     [Space]
     [Header("�����ֵ")]
     public Vector2 deadZone;
     [Space]
     [Space]
     [Space]
     [Header("��������ֵ")]
     public float sizeMultiplier;
     [Space]
     [Space]
     [Space]
     [Header("��ʱ��ֵ")]
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
          if (centerCameraCoroutine != null)             // �����ǰ�����Э�̲�Ϊ��
               StopCoroutine(centerCameraCoroutine);          // ֹͣһ��Э�̣���֤Ψһ�ԣ�
          centerCameraCoroutine = StartCoroutine(CenterCameraCoroutine(deadZone, yieldTime));  // Э�̣���������������ӵ������︴�ã�

     }

     private IEnumerator CenterCameraCoroutine(Vector2 deadZone, float yieldTime)
     {
          yield return new WaitForSeconds(yieldTime);                 // �ӳ�ʱ��
          for (Vector2 i = deadZone; i.x > 0 && i.y > 0; i -= Vector2.one * Time.deltaTime)
          {
               SetDeadZone(i);
               yield return null;
          }
     }

     /// <summary>
     /// ���������
     /// </summary>
     public void OnCameraShake()
     {
          if (cameraImpulseSource == null)                                           // �����ǰ����Ϊ��
               return;                                                                    // ֹͣ����
          cameraImpulseSource[1].m_ImpulseDefinition.m_ImpulseDuration = 0.25f;
          cameraImpulseSource[1].GenerateImpulseWithForce(0.25f); 
     }

     /// <summary>
     /// �������׷������
     /// </summary>
     /// <param name="deadZoneWidth"> �� </param>
     /// <param name="deadZoneHeight"> �� </param>
     /// <remarks>
     /// <para>���׷�ٵ��и�������������������ƶ���������κ��ƶ������������������ýű����ƣ���Ϊ���ܶ�̬����������Χ��over </para>
     /// <para>����ʵ�ʴ�С�ǰ��ձ������ģ���ʵҲû��Ҫ���ھ���ʵ�ʳߴ������</para>
     /// </remarks>
     private void SetDeadZone(Vector2 deadZone)
     {
          framingTransposer.m_DeadZoneWidth = deadZone.x;          // �������
          framingTransposer.m_DeadZoneHeight = deadZone.y;        // �����߶�
     }

     /// <summary>
     /// �����µ�����߽�
     /// </summary>
     private void SetNewCameraBounds()
     {
          var obj = GameObject.FindGameObjectWithTag(TagType.CameraBounds.ToString());    // �����������ض���ǩ������
          if (obj == null)    // ���Ϊ��
               return;             // ֹͣ����
          cameraCollider = obj.GetComponent<Collider2D>();  // ���������������������ײ��
          confiner2D.m_BoundingShape2D = cameraCollider;    // �ύ������߽�
          confiner2D.InvalidateCache();                     // �������
     }

     /// <summary>
     /// ���÷ɵ����߽�
     /// </summary>
     /// <param name="boundsSizeMultiplier"> �ߴ籶�� </param>
     /// <remarks>���е��ӵ��Ĵ�Χ�����������䲻�����޷ɣ��������ܱ�֤�����������Χ����һֱ��������</remarks>
     private void SetBulletLeftBounds(float boundsSizeMultiplier)
     {
          if (mainCamera == null || bulletBounde == null)                            // �������ʹ��߽�Ϊ��
               return;                                                                    // ֹͣ����
          float cameraWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;             // ����������
          float cameraHeight = mainCamera.orthographicSize * 2;                                // ��������߶�
          bulletBounde.size = new Vector2(cameraWidth, cameraHeight) * boundsSizeMultiplier;   // ��ֵ��������
     }

     private Vector2 GetDeadZone()
     {
          float deadZoneX = framingTransposer.m_DeadZoneWidth;
          float deadZoneY = framingTransposer.m_DeadZoneHeight;

          return new Vector2(deadZoneX, deadZoneY);
     }
}

