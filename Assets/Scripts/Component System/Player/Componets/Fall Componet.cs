using UnityEngine;

/// <summary>
/// �������
/// </summary>
public class FallComponet : MonoBehaviour
{
     private Transform _transform;           // �任�����
     private Rigidbody2D _rigidbody2D;       // �������
     private DetectionSystem _detection;     // �����ϵͳ

     [SerializeField, Tooltip("�����ٶȶ�������")] private AnimationCurve _fallSpeedCurve;

     private float _maxfallCurveTime;        // �����������ʱ��

     private float _currentfallCurveTime;    // ��ǰ��������ʱ��
     private float _currentFallSpeed;        // ��ǰ����Ŷ�ٶ�

     private void Awake()
     {
          _transform = transform.parent;
          _rigidbody2D = GetComponentInParent<Rigidbody2D>();
          _detection = GetComponentInParent<DetectionSystem>();
     }

     private void Start()
     {
          _maxfallCurveTime = _fallSpeedCurve.keys[_fallSpeedCurve.length - 1].time;      // ��ȡ�����������һ֡��ʱ�䣬����Ϊ��ǰ����������ߵ�ʱ��
     }

     private void Update()
     {
          if (getVelocityY < 0 && _detection.isAir)    // �����ǰ����С�� 0 �� �ڿ���
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
          if (getVelocityY < 0 && _detection.isAir)    // �����ǰ��·�ٶ�С�� 0
               SetVelocityY(_currentFallSpeed);             //���������
     }

     /// <summary>
     /// ���ø����ڴ�ֱ�����ϵ��ٶ�
     /// </summary>
     /// <param name="velocityX">ˮƽ��ֱ�ϵ��ٶ�ֵ</param>
     private void SetVelocityY(float velocityX)
     {
          _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, velocityX); // ���ø����ڴ�ֱ�����ϵ��ٶ�
     }

     /// <summary>
     /// ��ȡ���嵱ǰY������
     /// </summary>
     private float getVelocityY => _rigidbody2D.velocity.y;

}
