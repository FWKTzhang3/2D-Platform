using UnityEngine;

/// <summary>
/// �ƶ����
/// </summary>
/// <remarks> ������������ƶ�������ű� </remarks>
public class MoveComponet : MonoBehaviour
{
     private Transform _transform;                // �任�����
     private Rigidbody2D _rigidbody2D;            // �������
     private InputSystem _input;                  // ������ϵͳ
     private DetectionSystem _detection;          // ���ϵͳ

     [SerializeField, Tooltip("�ƶ��ٶ�")] private float _moveSpeed;
     [SerializeField, Tooltip("�ƶ����ٶ�")] private float _moveAcceleration;
     [SerializeField, Tooltip("�ƶ����ٶ�")] private float _moveDeceleration;

     private float _targetMoveDirection;     // Ŀ���ƶ�����
     private float _targetMoveSpeed;         // Ŀ���ƶ��ٶ�
     private float _targetMoveCeleration;    // Ŀ����ٶ�

     private float _currentMoveSpeed;        // ��ǰ�ƶ��ٶ�

     private void Awake()
     {
          _transform = transform.parent;                         // ��ȡ������ı任�����
          _rigidbody2D = GetComponentInParent<Rigidbody2D>();    // ��ȡ������ĸ���
          _input = GetComponentInParent<InputSystem>();          // ��ȡ������Ŀ��������
          _detection = GetComponentInParent<DetectionSystem>();
     }

     private void OnEnable()
     {
          // ע���¼�
          _input.OnJoyStickEvent += OnMove;
          _input.OnAttackEvent += OnAttack;
     }

     private void OnDisable()
     {
          // ע���¼�
          _input.OnJoyStickEvent -= OnMove;
          _input.OnAttackEvent -= OnAttack;
     }

     private void Update()
     {
          // ����Ŀ���ƶ������ж�Ŀ���ƶ��ٶ�
          _targetMoveSpeed = _targetMoveDirection == 0 ? 0 : _moveSpeed;                                 
          // ����Ŀ���ƶ������ж�Ŀ���ƶ����ٶ�
          _targetMoveCeleration = _targetMoveDirection == 0 ? _moveDeceleration : _moveAcceleration;     
          // ʹ�� MoveTowards �����𽥸ı䵱ǰ�ƶ��ٶ����Բ�ֵ��Ŀ���ƶ��ٶ�
          _currentMoveSpeed = Mathf.MoveTowards(_currentMoveSpeed, _targetMoveSpeed, Time.deltaTime * _targetMoveCeleration);
     }

     private void FixedUpdate()
     {
          SetVelocityX(_currentMoveSpeed * _transform.lossyScale.x);  // ����ˮƽ�����ϵ��ٶ�
     }

     /// <summary>
     /// �ƶ��¼�����
     /// </summary>
     /// <param name="moveInput"> �����¼��Ķ�ά������������ҡ�������� </param>
     private void OnMove(Vector2 moveInput)
     {
          _targetMoveDirection = moveInput.x;     // ��ǰ�ƶ�������ڿ�����X�᷽��
     }

     private void OnAttack()
     {
          if (!_detection.isAir)
          {
               _currentMoveSpeed = 0;
          }
     }

     /// <summary>
     /// ���ø�����ˮƽ�����ϵ��ٶ�
     /// </summary>
     /// <param name="velocityX">ˮƽ�����ϵ��ٶ�ֵ</param>
     private void SetVelocityX(float velocityX)
     {
          _rigidbody2D.velocity = new Vector2(velocityX,_rigidbody2D.velocity.y); // ���ø�����ˮƽ�����ϵ��ٶ�
     }
}
