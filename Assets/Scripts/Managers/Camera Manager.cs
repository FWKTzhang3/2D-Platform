using Cinemachine;
using UnityEngine;

/// <summary>
/// ���������
/// </summary>
public class CameraManager : MonoBehaviour
{
     private CinemachineConfiner2D confiner2D;    // ���� confiner2D 
     private Collider2D cameraCollider;           // ���� cameraCollider

     // �����ȡ�������������Դ
     private CinemachineImpulseSource[] cameraImpulseSource;

     [Header("��������¼�")]
     [Tooltip("������¼�")] public VoidEventSO cameraShakeEvent;

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

}

