using UnityEngine;

public class AniamtionSystem : MonoBehaviour
{
     private Transform _transform;           // 转换器组件
     private Rigidbody2D _rigidbody2D;       // 刚体组件

     private DetectionSystem _detection;     // 检测系统
     private InputSystem _inputSystem;       // 控制器系统

     private AnimationsAllManager _animationsAllManager;

     private void Awake()
     {
          _transform = transform.parent;                                   // 获取父级物体的转换器
          _rigidbody2D = GetComponentInParent<Rigidbody2D>();              // 获取父级物体的刚体


          _detection = GetComponent<DetectionSystem>();               // 获取检测系统
          _inputSystem = GetComponent<InputSystem>();                 // 获取控制器系统

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
