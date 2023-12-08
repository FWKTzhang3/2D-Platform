using System.Collections;
using UnityEngine;
using Cinemachine;

/// <summary>
/// ���������
/// </summary>
public class CameraManager : MonoBehaviour
{
     // �������
     private Camera _mainCamera;                                  // �����������
     private CinemachineFramingTransposer _framingTransposer;     // ���þ�ͷ��λ��
     private CinemachineConfiner2D _confiner2D;                   // ���ñ߽�������
     private CinemachineTargetGroup _targetGroup;                 // ����Ŀ����
     private BoxCollider2D _bulletBounde;                         // �����ӵ��߽�

     private ShakeManager _shakeManager;

     private Collider2D _cameraCollider;                          // ����߽�

     // ����
     private Coroutine _cameraCenterCoroutine;
     private Coroutine _cameraTrackedOffsetCoroutine;

     [Header("�������")]
     [SerializeField, Tooltip("��ҿ�����")] private InputSystem _input; 

     [Header("��������¼�")]
     [SerializeField, Tooltip("������¼�")] private VoidEventSO _cameraShakeEvent;

     [Header("���׷���������")]
     [SerializeField, Tooltip("�����ʼ����")] private Vector2 _camerDeadZone;
     [SerializeField, Tooltip("��������ӳ�ʱ��")] public float _centeringDelay;
     [SerializeField, Tooltip("��������ٶ�")] public float _centeringSpeed;

     [Header("���׷�ٵ����")]
     [SerializeField, Tooltip("���׷�ٵ����ĵ�")] private Vector2 _camerTrackedOffset;
     [SerializeField, Tooltip("׷�ٵ����ƫ����")] private float _camerTrackedMaxOffset;

     [Header("��������ֵ")]
     [SerializeField, Tooltip("�ɵ����ߴ籶��")] private float _sizeMultiplier;

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
          if (_cameraCenterCoroutine != null)          // ������е�ǰЭ�̲�Ϊ��
          {
               StopCoroutine(_cameraCenterCoroutine);       // ���Э��
               _cameraDeadZone = _camerDeadZone;            // �ָ�����
          }
          OnCameraTrackedOffset(inputDirection);       // �������ƫ��
     }

     /// <summary>
     /// �����µ�����߽�
     /// </summary>
     private void SetNewCameraBounds()
     {
          var obj = GameObject.FindGameObjectWithTag(TagType.CameraBounds.ToString());    // �����������ض���ǩ������
          if (obj == null)    // ���Ϊ��
               return;             // ֹͣ����
          _cameraCollider = obj.GetComponent<Collider2D>();  // ���������������������ײ��
          _confiner2D.m_BoundingShape2D = _cameraCollider;    // �ύ������߽�
          _confiner2D.InvalidateCache();                     // �������
     }

     /// <summary>
     /// ���÷ɵ����߽�
     /// </summary>
     /// <remarks>���е��ӵ��Ĵ�Χ�����������䲻�����޷ɣ��������ܱ�֤�����������Χ����һֱ��������</remarks>
     private float _setBulletLeftBounds
     {
          set
          {
               if (_mainCamera != null && _bulletBounde != null)                               // �������ͱ߽����Ƿ�Ϊ��
               { 
                    float cameraWidth = _mainCamera.orthographicSize * 2 * _mainCamera.aspect;      // ����������
                    float cameraHeight = _mainCamera.orthographicSize * 2;                          // ��������߶�
                    _bulletBounde.size = new Vector2(cameraWidth, cameraHeight) * value;            // ��ֵ��������
               }
          }
     }

     #region ���׷�ٵ�ƫ�����

     /// <summary>
     /// �������ƫ��
     /// </summary>
     /// <param name="targetDirection"> ����Ŀ�귽�� </param>
     private void OnCameraTrackedOffset(Vector2 targetDirection)
     {
          if (_cameraTrackedOffsetCoroutine != null)             // �����ǰ�����Э�̲�Ϊ��
               StopCoroutine(_cameraTrackedOffsetCoroutine);          // ֹͣһ��Э�̣���֤Ψһ�ԣ�
          _cameraTrackedOffsetCoroutine = StartCoroutine(CameraTrackedOffsetCoroutine(targetDirection));  // Э�̣���������������ӵ������︴�ã�

     }

     /// <summary>
     /// ���ƫ��Э��
     /// </summary>
     /// <param name="targetDirection"> ƫ�Ʒ��� </param>
     private IEnumerator CameraTrackedOffsetCoroutine(Vector2 targetDirection)
     {
          Vector2 targetOffset = _camerTrackedOffset + _camerTrackedMaxOffset * targetDirection;    // ����Ŀ��ƫ��λ��
          while (_trackedOffset != targetOffset)  // �����ǰλ�ò�����Ŀ��λ��
          {
               float distance = Vector2.Distance(_trackedOffset, targetOffset);                // ���������յ��ֱ�߾���
               float clampedSpeed = Mathf.Clamp(distance, 0.05f, _camerTrackedMaxOffset * 2);  // ����ƫ���ٶȣ�������������������ֵ����Сֵ��
               // �� MoveTowards ���Բ�ֵ��Ŀ��λ��
               _trackedOffset = Vector2.MoveTowards(_trackedOffset, targetOffset, Time.deltaTime * clampedSpeed);
               yield return null;       // ������һ��֡�ӳ٣���Ȼֱ�ӱ���
          }
          _trackedOffset = targetOffset;          // ��ȷ��Ŀ���
          if (targetDirection == Vector2.zero)    // �����ǰƫ�Ʒ���������
               OnCenterCamera();                       // ��������
     }

     /// <summary>
     /// ���׷�ٵ�
     /// </summary>
     private Vector2 _trackedOffset
     {
          get => _framingTransposer.m_TrackedObjectOffset; 
          set => _framingTransposer.m_TrackedObjectOffset = value;           
     }

     #endregion

     #region ����������

     /// <summary>
     /// �������
     /// </summary>
     private void OnCenterCamera()
     {
          if (_cameraCenterCoroutine != null)             // �����ǰ�����Э�̲�Ϊ��
               StopCoroutine(_cameraCenterCoroutine);          // ֹͣһ��Э�̣���֤Ψһ�ԣ�
          _cameraCenterCoroutine = StartCoroutine(CenterCameraCoroutine(_centeringDelay, _centeringSpeed));  // Э�̣���������������ӵ������︴�ã�
     }

     /// <summary>
     /// �������Э��
     /// </summary>
     /// <param name="centeringDelay"> �ӳ�ʱ�� </param>
     /// <param name="centeringSpeed"> �����ٶ� </param>
     private IEnumerator CenterCameraCoroutine(float centeringDelay, float centeringSpeed)
     {
          yield return new WaitForSeconds(centeringDelay);                           // �ӳپ���
          Vector2 currentDeadZone = _cameraDeadZone;                                 // ���浱ǰ����
          while (currentDeadZone.x > 0 || currentDeadZone.y > 0)                     // �����ǰ��������������еݼ�ѭ��
          {
               currentDeadZone -= Vector2.one * Time.deltaTime * centeringSpeed;          // �Ի����������ֵ���еݼ�
               _cameraDeadZone = currentDeadZone;                                         // ��ֵ����
               yield return null;                                                         // �ӳ�һ֡��ʹѭ������̫�죬����һЩϣ��˿��һ��仯�ľ������ͺã�
          }
          _cameraDeadZone = Vector2.zero;                                            // ����׼�������������
          _cameraDeadZone = _camerDeadZone;                                          // ��ԭ����
     }

     /// <summary>
     /// �������
     /// </summary>
     /// <returns>
     /// <para>��������Ķ�ά����</para>
     /// <para>���Լ����ϳɶ�ά����</para>
     /// </returns>
     private Vector2 _cameraDeadZone
     {
          get
          {
               // ���沢���Ϸ���
               float deadZoneX = _framingTransposer.m_DeadZoneWidth;      
               float deadZoneY = _framingTransposer.m_DeadZoneHeight;     
               return new Vector2(deadZoneX, deadZoneY);                  
          }
          set
          {
               // ��ȡ���ֱ����
               _framingTransposer.m_DeadZoneWidth = value.x;             
               _framingTransposer.m_DeadZoneHeight = value.y;
          }
     }

     #endregion
}