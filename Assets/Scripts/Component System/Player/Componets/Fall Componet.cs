using UnityEngine;

/// <summary>
/// �������
/// </summary>
public class FallComponet : MonoBehaviour
{
     private Transform _transform;           // �任�����
     private ControllSystem _controll;       // ����ϵͳ
     private DetectionSystem _detection;     // ���ϵͳ

     [SerializeField, Tooltip("�����ٶȶ�������")] private AnimationCurve _fallSpeedCurve;

     private float _maxfallCurveTime;        // �����������ʱ��

     private float _currentfallCurveTime;    // ��ǰ��������ʱ��
     private float _currentFallSpeed;        // ��ǰ����Ŷ�ٶ�

     private void Awake()
     {
          _transform = transform.parent;                         // ��ȡ��������ı任�����
          _controll = GetComponentInParent<ControllSystem>();    // ��ȡ��������Ŀ���ϵͳ
          _detection = GetComponentInParent<DetectionSystem>();  // ��ȡ��������Ŀ�����ϵͳ
     }

     private void Start()
     {
          _maxfallCurveTime = _fallSpeedCurve.keys[_fallSpeedCurve.length - 1].time;      // ��ȡ�����������һ֡��ʱ�䣬����Ϊ��ǰ����������ߵ�ʱ��
     }

     private void Update()
     {
          if (_controll.velocityY < 0 && _detection.isAir)    // �����ǰ����С�� 0 �� �ڿ���
          {
               // ��MoveTowards����ǰ����ʱ�����Բ�ֵ���ʱ��
               _currentfallCurveTime = Mathf.MoveTowards(_currentfallCurveTime, _maxfallCurveTime, Time.deltaTime);
               // ͨ����ǰʱ���ȡ�������߶�Ӧ�ٶ�ֵ
               _currentFallSpeed = _fallSpeedCurve.Evaluate(_currentfallCurveTime);  
          }
          else  // ��֮
          {
               _currentfallCurveTime = 0;    // �õ�ǰ���䶯��ʱ�����
          }
     }

     private void FixedUpdate()
     {
          if (_controll.velocityY < 0 && _detection.isAir)  // �����ǰY���ٶ�С�� 0
               _controll.velocityY = _currentFallSpeed;        //���������
     }
}
