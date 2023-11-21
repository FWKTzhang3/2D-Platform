using System.Collections;
using UnityEngine;
using Cinemachine;

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

     private PlayerInput playerInput;

     // ����
     private Coroutine cameraCenterCoroutine;
     private Coroutine cameraTrackedOffsetCoroutine;

     private CinemachineImpulseSource[] cameraImpulseSource;     // �����Դ��
     private Collider2D cameraCollider;                          // ����߽�

     [Header("��������¼�")]
     [SerializeField, Tooltip("������¼�")] private VoidEventSO cameraShakeEvent;

     [Header("�����״̬")]
     [SerializeField, Tooltip("����״̬")] private bool isCameraCentering;
     [SerializeField, Tooltip("ƫ��״̬")] private bool isCameraTrackedOffseting;

     [Header("���������ֵ")]
     [SerializeField, Tooltip("�������")] private Vector2 camerDeadZone;
     [SerializeField, Tooltip("���׷�ٵ�ƫ��")] private Vector2 camerTrackedOffset;

     [Header("��������ֵ")]
     [SerializeField, Tooltip("�ɵ��ߴ籶��")] private float sizeMultiplier;

     [Header("��ʱ��ֵ")]
     [SerializeField, Tooltip("��������ӳ�ʱ��")] public float centeringDelay;
     [SerializeField, Tooltip("��������ٶ�")] public float centeringSpeed;
     [SerializeField, Tooltip("׷�ٵ����ƫ����")] private float camerTrackedMaxOffset;
     [SerializeField, Tooltip("׷�ٵ�ƫ���ٶ�")] private float camerTrackedOffsetSpeed;

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

     #region ���׷�ٵ�ƫ�����

     /// <summary>
     /// <para>�����������ƫ�ơ�</para>
     /// <para>Update the camera tracked offset.</para>
     /// </summary>
     private void UpdateCameraTrackedOffset()
     {
          // ����������ƫ��δ�����������������ʹ��ҡ��ƫ��
          if (!isCameraTrackedOffseting && playerInput.isStickOffseting)
          {
               isCameraTrackedOffseting = true;             // �����������ƫ��״̬
               OnCameraTrackedOffset();                     // ִ���������ƫ�Ʋ���
          }
          // ����������ƫ���Ѿ��������������ֹͣʹ��ҡ��ƫ��
          else if (isCameraTrackedOffseting && !playerInput.isStickOffseting)
          {
               isCameraTrackedOffseting = false;            // �ر��������ƫ��״̬
          }
     }

     /// <summary>
     /// <para>ִ���������ƫ�Ʋ�����</para>
     /// <para>Execute camera tracked offset operation.</para>
     /// </summary>
     private void OnCameraTrackedOffset()
     {
          if (cameraTrackedOffsetCoroutine != null)                                       // ����������ƫ��Э�̴���
               StopCoroutine(cameraTrackedOffsetCoroutine);                                    // ֹͣ�������ƫ��Э��
          cameraTrackedOffsetCoroutine = StartCoroutine(CameraTrackedOffsetCoroutine());  // Э�̣�������
     }

     /// <summary>
     /// <para>�������ƫ��Э�̺�����</para>
     /// <para>The coroutine function of camera tracked offset.</para>
     /// </summary>
     private IEnumerator CameraTrackedOffsetCoroutine()
     {
          Vector2 currentOffset = GetTrackedOffset();                                                    // ��ȡ��ǰ����ƫ����                        
          while (true)                                                                                        // ����ѭ��                            
          {
               Vector2 targetOffset = camerTrackedOffset + camerTrackedMaxOffset * playerInput.axes;     // ����Ŀ�����ƫ����                        
               currentOffset = Vector2.Lerp(currentOffset, targetOffset, camerTrackedOffsetSpeed);       // ���㵱ǰ����ƫ����                        
               SetTrackedOffset(currentOffset);                                                          // ���ø���ƫ����                            
               float distance = Vector2.Distance(currentOffset, targetOffset);                           // ���㵱ǰ��Ŀ��ƫ�����ľ���                  
               if (distance < 0.025 && !playerInput.isStickOffseting)                                    // �����ǰ����С��һ����ֵ�������û��ʹ��ҡ��ƫ��
                    break;                                                                                    // ����Э��                            
               yield return null;                                                                        // �ȴ���һ֡����                            
          }
          SetTrackedOffset(camerTrackedOffset);                                                          // ������ƫ��������Ϊ��ʼ״̬
     }

     /// <summary>
     /// �������׷�ٵ�
     /// </summary>
     /// <param name="ObjectOffset">���׷�ٵ�����</param>
     private void SetTrackedOffset(Vector2 ObjectOffset)
     {
          framingTransposer.m_TrackedObjectOffset = ObjectOffset;
     }

     /// <summary>
     /// ��ȡ��ǰ���׷�ٵ�
     /// </summary>
     /// <returns> ׷�ٵ����� </returns>
     private Vector2 GetTrackedOffset()
     {
          return framingTransposer.m_TrackedObjectOffset;
     }

     #endregion

     #region ����������

     /// <summary>
     /// �����������
     /// </summary>
     private void UpdateCameraCentering()
     {
          // ������û��ʹ��ҡ��ƫ�ƣ����Ҳû�д��ھ��к͸���ƫ��״̬
          if (!playerInput.isStickOffseting && !isCameraCentering && !isCameraTrackedOffseting)
          {
               isCameraCentering = true;     // ���������״̬����Ϊ��
               OnCenterCamera();             // ִ��������в���
          }
          // ����������ʹ��ҡ��ƫ�ƣ��������ͬʱ���ھ��к͸���ƫ��״̬
          else if (playerInput.isStickOffseting && isCameraCentering && isCameraTrackedOffseting)
          {
               isCameraCentering = false;    // ���������״̬����Ϊ��
          }
     }

     /// <summary>
     /// �������
     /// </summary>
     private void OnCenterCamera()
     {
          if (cameraCenterCoroutine != null)             // �����ǰ�����Э�̲�Ϊ��
               StopCoroutine(cameraCenterCoroutine);          // ֹͣһ��Э�̣���֤Ψһ�ԣ�
          cameraCenterCoroutine = StartCoroutine(CenterCameraCoroutine(centeringDelay, centeringSpeed));  // Э�̣���������������ӵ������︴�ã�
     }

     /// <summary>
     /// �������Э��
     /// </summary>
     /// <param name="centeringDelay"> �ӳ�ʱ�� </param>
     /// <param name="centeringSpeed"> �����ٶ� </param>
     private IEnumerator CenterCameraCoroutine(float centeringDelay, float centeringSpeed)
     {
          yield return new WaitForSeconds(centeringDelay);                           // �ӳپ���
          Vector2 currentDeadZone = GetCameraDeadZone();                                   // ���浱ǰ����
          while (currentDeadZone.x > 0 || currentDeadZone.y > 0)                     // �����ǰ��������������еݼ�ѭ��
          {
               if(playerInput.isStickOffseting)                                        // �����ǰҡ��û�о�ֹ
                    break;                                                                // ���ѭ����������ѭ������ĳ���
               currentDeadZone -= Vector2.one * Time.deltaTime * centeringSpeed;     // �Ի����������ֵ���еݼ�
               SetCameraDeadZone(currentDeadZone);                                         // ��ֵ����
               yield return null;                                                    // �ӳ�һ֡��ʹѭ������̫�죬����һЩϣ��˿��һ��仯�ľ������ͺã������ӳٻ����п��ٸУ�
          }
          if(!playerInput.isStickOffseting)                                             // �����ǰ��������û���ƶ�
               SetCameraDeadZone(Vector2.zero);                                            // ����׼�������������
          SetCameraDeadZone(camerDeadZone);                                                     // ��ԭ����
     }

     /// <summary>
     /// �������׷������
     /// </summary>
     /// <param name="deadZone"> ������Χ </param>
     /// <remarks>
     /// <para>���׷�ٵ��и�������������������ƶ���������κ��ƶ������������������ýű����ƣ���Ϊ���ܶ�̬����������Χ��over </para>
     /// <para>����ʵ�ʴ�С�ǰ��ձ������ģ���ʵҲû��Ҫ���ھ���ʵ�ʳߴ������</para>
     /// </remarks>
     private void SetCameraDeadZone(Vector2 deadZone)
     {
          framingTransposer.m_DeadZoneWidth = deadZone.x;   // �������
          framingTransposer.m_DeadZoneHeight = deadZone.y;  // �����߶�
     }

     /// <summary>
     /// ��ȡ�������
     /// </summary>
     /// <returns>
     /// <para>��������Ķ�ά����</para>
     /// <para>���Լ����ϳɶ�ά����</para>
     /// </returns>
     private Vector2 GetCameraDeadZone()
     {
          float deadZoneX = framingTransposer.m_DeadZoneWidth;
          float deadZoneY = framingTransposer.m_DeadZoneHeight;

          return new Vector2(deadZoneX, deadZoneY);
     }

     #endregion
}