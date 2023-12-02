using UnityEngine;

public class AniamtionSystem : MonoBehaviour
{
     private Transform _transform;           // ת�������
     private Rigidbody2D _rigidbody2D;       // �������

     private DetectionSystem _detection;     // ���ϵͳ
     private InputSystem _inputSystem;       // ������ϵͳ

     private AnimationsAllManager _animationsAllManager;

     private void Awake()
     {
          _transform = transform.parent;                                   // ��ȡ���������ת����
          _rigidbody2D = GetComponentInParent<Rigidbody2D>();              // ��ȡ��������ĸ���


          _detection = GetComponent<DetectionSystem>();               // ��ȡ���ϵͳ
          _inputSystem = GetComponent<InputSystem>();                 // ��ȡ������ϵͳ

          _animationsAllManager = _transform.GetComponentInChildren<AnimationsAllManager>();
     }

     private void OnEnable()
     {
          _inputSystem.OnAttackEvent += OnAttack;
     }

     private void OnDisable()
     {
          _inputSystem.OnAttackEvent -= OnAttack;
     }

     private void Update()
     {
          float moveVelocit = Mathf.Abs(_rigidbody2D.velocity.x);
          _animationsAllManager.SetAllAnimatorBools(_detection.isAir, _inputSystem.hasAttackInputBuffer);
          _animationsAllManager.SetAllAnimatorFloats(moveVelocit, _rigidbody2D.velocity.y);
     }

     private void OnAttack()
     {
          /*
          _animators.SetTrigger(_trigger_Attack_ID);
          */
     }

}
