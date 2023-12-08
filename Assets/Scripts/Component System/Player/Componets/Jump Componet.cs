using UnityEngine;
using UnityEngine.InputSystem;

public class JumpComponet: MonoBehaviour
{
     private Transform _transform;           // �任�����

     private ControllSystem _controll;       // ����ϵͳ
     private InputSystem _input;             // ������ϵͳ
     private DetectionSystem _detection;     // �����ϵͳ

     [SerializeField, Tooltip("��Ծ����")] private float _jumpForce;
     [SerializeField, Tooltip("��Ծ���ٶ�")] private float _jumpDeceleration;
     [SerializeField, Tooltip("��Ծ����")] private float _jumpCount;
     [SerializeField, Tooltip("����ʱ��")] private float _coyoteTime;

     private float _currentJumpForce;        // ��ǰ��Ծ����
     private float _currentJumpCount;        // ��ǰ��Ծ����
     private float _currentCoyoteTime;       // ��ǰ����ʱ��

     private bool _canJump => !_controll.isHitstun && !_controll.isAttack;
     private bool _coyoteState => _currentCoyoteTime >= 0;  // ����״̬

     private void Awake()
     {
          _transform = transform.parent;                         // ��ȡ��������任��

          _controll = GetComponentInParent<ControllSystem>();    // ��ȡ��������Ŀ���ϵͳ
          _input = GetComponentInParent<InputSystem>();          // ��ȡ��������Ŀ�����ϵͳ
          _detection = GetComponentInParent<DetectionSystem>();  // ��ȡ��������ļ��ϵͳ
     }

     private void OnEnable()
     {
          // ע���¼�
          _input.ActionStartedEvent += OnJump;
          _input.ActionCanceledEvent += StopJump;
     }

     private void OnDisable()
     {
          // ע���¼�
          _input.ActionStartedEvent -= OnJump;
          _input.ActionCanceledEvent -= StopJump;
     }

     private void Update()
     {
          CoyoteTime();
          JumpForceUpdate();
     }

     private void FixedUpdate()
     {
          if (_currentJumpForce > 0)                        // �����ǰ��Ծ���ȴ��� 0
               _controll.velocityY = _currentJumpForce;          // ����ˮƽ�����ϵ��ٶ�
     }

     /// <summary>
     /// ������Ծ�ķ���
     /// </summary>
     private void OnJump(InputAction inputAction)
     {
          if (inputAction.name == "Jump" && _canJump)  // ɸѡ����Ծ��ָ����£�
          {
               // �����ǰ״̬���ڿ��л�������״̬�ҵ�ǰ��Ծ�������� 0
               if ((!_detection.isAir|| _coyoteState) && _currentJumpCount > 0)
               {
                    _currentJumpCount = 0;             // ��Ծ��������
               }
               // �����ǰ��Ծ����С��Ԥ�����
               if (_currentJumpCount < _jumpCount)
               {
                    _currentJumpForce = _jumpForce;    // ʩ����Ծ����
                    _currentJumpCount++;               // ��Ծ���� + 1
               }
          }
     }

     /// <summary>
     /// ֹͣ��Ծ�ķ���
     /// </summary>
     private void StopJump(InputAction inputAction)
     {
          if (inputAction.name == "Jump")              // ɸѡ����Ծ��ָ�̧��
          {
               if (_controll.velocityY > 0 && _currentJumpCount <= 1)   // �����ǰ���� Y �����ȴ��� 0 �� ��ǰ��Ծ����С�ڵ��� 1
               {
                    _currentJumpForce = 0;                            // �õ�ǰ���ȹ���
                    _controll.velocityY = 0;                          // ����Ծ��ֹ�����֮��
               }
          }
     }

     /// <summary>
     /// ��Ծ���ȸ��·���
     /// </summary>
     private void JumpForceUpdate()
     {
          if (_currentJumpForce > 0)    // �����ǰ��Ծ���ȴ��� 0
               // ��MoveTowards���Բ�ֵ�㷨�𽥵ݼ��� 0
               _currentJumpForce = Mathf.MoveTowards(_currentJumpForce, 0, Time.deltaTime * _jumpDeceleration);
     }

     /// <summary>
     /// ����ʱ��
     /// </summary>
     private void CoyoteTime()
     {
          if (!_detection.isAir)                                 // ������ڿ���
          {
               _currentCoyoteTime = _coyoteTime;                      // ���õ�ǰ����ʱ��
          }
          else if (_detection.isAir && _currentCoyoteTime >= 0)  // ����ڿ����ҵ�ǰ����ʱ��û�п�ʼ��ʱ
          {
               _currentCoyoteTime -= Time.deltaTime;                  // ��ʼ��ʱ����ʱ��
          }
     }
}
