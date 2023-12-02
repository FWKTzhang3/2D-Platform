using UnityEngine;

public class JumpComponet: MonoBehaviour
{
     private Transform _transform;           // �任�����
     private Rigidbody2D _rigidbody2D;       // �������
     private InputSystem _input;             // ������ϵͳ
     private DetectionSystem _detection;     // �����ϵͳ

     [SerializeField, Tooltip("��Ծ����")] private float _jumpForce;
     [SerializeField, Tooltip("��Ծ���ٶ�")] private float _jumpDeceleration;
     [SerializeField, Tooltip("��Ծ����")] private float _jumpCount;

     private float _currentJumpForce;        // ��ǰ��Ծ����
     private float _currentJumpCount;        // ��ǰ��Ծ����

     private void Awake()
     {
          _transform = transform.parent;                         
          _rigidbody2D = GetComponentInParent<Rigidbody2D>();    
          _input = GetComponentInParent<InputSystem>();
          _detection = GetComponentInParent<DetectionSystem>();
     }

     private void OnEnable()
     {
          // ע���¼�
          _input.OnJumpEvent += OnJump;
          _input.StopJumpEvent += StopJump;
     }

     private void OnDisable()
     {
          // ע���¼�
          _input.OnJumpEvent -= OnJump;
          _input.StopJumpEvent -= StopJump;
     }

     private void Update()
     {
          if (_currentJumpForce > 0)    // �����ǰ��Ծ���ȴ��� 0
               // ��MoveTowards���Բ�ֵ�㷨�𽥵ݼ��� 0
               _currentJumpForce = Mathf.MoveTowards(_currentJumpForce, 0, Time.deltaTime * _jumpDeceleration);
     }

     private void FixedUpdate()
     {
          if (_currentJumpForce > 0)              // �����ǰ��Ծ���ȴ��� 0
               SetVelocityY(_currentJumpForce);        // ����ˮƽ�����ϵ��ٶ�
     }

     /// <summary>
     /// ������Ծ�ķ���
     /// </summary>
     private void OnJump()
     {
          if (!_detection.isAir && _currentJumpCount > 0)   // �����ǰ״̬���ڿ����ҵ�ǰ��Ծ�������� 0
               _currentJumpCount = 0;                            // ��Ծ��������
          if (_currentJumpCount < _jumpCount)               // �����ǰ��Ծ����С�������ֵ
          {
               _currentJumpForce = _jumpForce;                   // ʩ����Ծ����
               _currentJumpCount++;                              // ��Ծ���� + 1
          }
     }

     /// <summary>
     /// ֹͣ��Ծ�ķ���
     /// </summary>
     private void StopJump()
     {
          if (getVelocityY < 0 || _currentJumpCount > 1)    // �����ǰ��������С�� 0 ���� ��ǰ��Ծ�������� 1
               return;                                           // �򷵻�
          _currentJumpForce = 0;                            // �õ�ǰ���ȹ���
          SetVelocityY(0);                                  // ����Ծ��ֹ�����֮��
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
