using UnityEngine;

/// <summary>
/// �ƶ����
/// </summary>
/// <remarks> ������������ƶ�������ű� </remarks>
public class MoveComponet : MonoBehaviour
{
     private Transform _transform;                // ת�������

     private ControllSystem _controll;            // ����ϵͳ
     private InputSystem _input;                  // ������ϵͳ
     private DetectionSystem _detection;          // ���ϵͳ

     [SerializeField, Tooltip("�ƶ��ٶ�")] private float _moveSpeed;
     [SerializeField, Tooltip("�ƶ����ٶ�")] private float _moveAcceleration;
     [SerializeField, Tooltip("�ƶ����ٶ�")] private float _moveDeceleration;

     private float _targetMoveDirection;     // Ŀ���ƶ�����
     private float _targetMoveSpeed;         // Ŀ���ƶ��ٶ�
     private float _targetMoveCeleration;    // Ŀ����ٶ�

     private float _currentMoveSpeed;        // ��ǰ�ƶ��ٶ�

     private bool _isCrouching;              // ��ǰ�¶�״̬
     private bool _isGroundAttacking;        // ��ǰ���湥��״̬

     private bool _canMove
     {
          get => !_controll.isHitstun && !_controll.isDeadth && !_isCrouching && !_isGroundAttacking;
     }

     private void Awake()
     {
          _transform = transform.parent;                         // ��ȡ���������ת�������

          _controll = GetComponentInParent<ControllSystem>();    // ��ȡ��������Ŀ���ϵͳ
          _input = GetComponentInParent<InputSystem>();          // ��ȡ��������Ŀ�����ϵͳ
          _detection = GetComponentInParent<DetectionSystem>();  // ��ȡ��������ļ��ϵͳ
     }

     private void OnEnable()
     {
          // ע���¼�
          _input.JoystickEvent += JoystickInput;
          BodyGroundAttackState.GroundAttacking += GroundAttacking;
     }

     private void OnDisable()
     {
          // ע���¼�
          _input.JoystickEvent -= JoystickInput;
          BodyGroundAttackState.GroundAttacking += GroundAttacking;
     }

     private void Update()
     {
          if (_canMove)
          {
               // ����Ŀ���ƶ������ж�Ŀ���ƶ��ٶ�
               _targetMoveSpeed = _targetMoveDirection == 0 ? 0 : _moveSpeed;   
               // ����Ŀ���ƶ������ж�Ŀ���ƶ����ٶ�
               _targetMoveCeleration = _targetMoveDirection == 0 ? _moveDeceleration : _moveAcceleration;     
               // ʹ�� MoveTowards �����𽥸ı䵱ǰ�ƶ��ٶ����Բ�ֵ��Ŀ���ƶ��ٶ�
               _currentMoveSpeed = Mathf.MoveTowards(_currentMoveSpeed, _targetMoveSpeed, Time.deltaTime * _targetMoveCeleration);
          }
     }

     private void FixedUpdate()
     {
          // �����ǰ û���¶��� �� û�й����� �� �����ж�ʱ
          if (_canMove)                    
          {
               if(_targetMoveDirection != 0)
                    _controll.velocityX = _currentMoveSpeed * _targetMoveDirection;       // ����X����ٶ�
               else if(_targetMoveDirection == 0 && !_detection.isAir)
                    _controll.velocityX = _currentMoveSpeed * _transform.lossyScale.x;    // ����X����ٶ�

          }
     }

     /// <summary>
     /// ҡ�˲���
     /// </summary>
     /// <param name="JoyVector"> �����¼���ҡ��X������ </param>
     private void JoystickInput(Vector2 JoyVector)
     {
          _targetMoveDirection = JoyVector.x;     // ��ǰ�ƶ�������ڿ�����X�᷽��

          if(_isCrouching = JoyVector.y < 0 && !_detection.isAir)     // �����ǰ�¶�״̬��������Y�᷽��С�� 0��Ϊ�� �� �ڵ���ʱ
          {
               OnCrouch();
          }
     }

     /// <summary>
     /// ���湥��״̬�¼�
     /// </summary>
     /// <param name="state"> ״̬ </param>
     private void GroundAttacking(bool state)
     {
          _controll.velocityX = 0;      // ��ͣ
          _currentMoveSpeed = 0;        // ��ͣ
          _isGroundAttacking = state;   // ��ֵ״̬
     }

     /// <summary>
     /// �����¶׷���
     /// </summary>
     private void OnCrouch() 
     {
          // ��ͣ
          _controll.velocityX = 0;         
          _currentMoveSpeed = 0;   
     }
}
