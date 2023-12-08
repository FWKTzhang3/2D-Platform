using UnityEngine;

/// <summary>
/// 控制系统
/// </summary>
/// <remarks> 在这里控制整个物体 </remarks>
public class ControllSystem : MonoBehaviour
{
     private Transform _transform;                     // 转换器组件
     private Rigidbody2D _rigidbody2D;                 // 刚体组件
     private CapsuleCollider2D _capsuleCollider2D;     //胶囊碰撞体组件

     private InputSystem _input;   // 控制器系统

     public bool isAttack;

     public bool isHitstun;
     public bool isDeadth;

     private void Awake()
     {
          _transform = transform.parent;                                   // 获取父物体的转换器组件
          _rigidbody2D = GetComponentInParent<Rigidbody2D>();              // 获取父级物体的刚体组件
          _capsuleCollider2D = GetComponentInParent<CapsuleCollider2D>();  // 获取父级物体的胶囊碰撞体组件

          _input = GetComponent<InputSystem>();   // 获取控制器系统
     }

     private void OnEnable()
     {
          BodyAttackState.AttackAnimationEvent += AttackState;
     }

     private void OnDisable()
     {
          BodyAttackState.AttackAnimationEvent -= AttackState;
     }

     private void Update()
     {
          FlipScaleX();
     }

     /// <summary>
     /// 翻转X轴的比例缩放
     /// </summary>
     private void FlipScaleX()
     {
          if (!isAttack && !isHitstun && !isDeadth)
          {
               int currentFaceDirection = Mathf.RoundToInt(_input.joystickVectorX);                 // 缓存当前控制器摇杆X轴方向，转换为整数
               if (currentFaceDirection != 0 && _transform.lossyScale.x != currentFaceDirection)    // 如果当前方向不为零 且 不等于当前缩放值
               {
                    // 则重新赋值当前X轴Scale缩放
                    _transform.localScale = new Vector2(currentFaceDirection, _transform.lossyScale.y);
               }
          }
     }

     /// <summary>
     /// 接收攻击状态机的状态
     /// </summary>
     /// <param name="attackState"> 接收状态 </param>
     private void AttackState(bool state)
     {
          isAttack = state;
     }

     #region 刚体相关

     /// <summary>
     /// 读写刚体速度
     /// </summary>
     private Vector2 _velocity
     {
          set => _rigidbody2D.velocity = value;
          get => _rigidbody2D.velocity;
     }

     /// <summary>
     /// 读写刚体 X 轴速度
     /// </summary>
     public float velocityX
     {
          set => _velocity = new Vector2(value, _velocity.y);
          get => _velocity.x;
     }

     /// <summary>
     /// 读写刚体 Y 轴速度
     /// </summary>
     public float velocityY
     {
          set => _velocity = new Vector2(_velocity.x, value);
          get => _velocity.y;
     }

     #endregion

     #region 胶囊碰撞体相关

          #region 胶囊碰撞体坐标相关
               /// <summary>
     /// 读写胶囊坐标
     /// </summary>
               private Vector2 _capsuleOffset
               {
                    set => _capsuleCollider2D.offset = value;
                    get => _capsuleCollider2D.offset;
               }
               /// <summary>
     /// 修改胶囊坐标 X
     /// </summary>
               public float OffsetX
               {
                    set => _capsuleOffset = new Vector2(value, _capsuleOffset.y);
                    get => _capsuleOffset.x;
               }
               /// <summary>
     /// 修改胶囊坐标 Y
     /// </summary>
               public float OffsetY
               {
                    set => _capsuleOffset = new Vector2(_capsuleOffset.x, value);
                    get => _capsuleOffset.y;
               }
          #endregion

          #region 胶囊碰撞体尺寸相关
               /// <summary>
               /// 胶囊体尺寸
               /// </summary>
               private Vector2 _capsuleSize
               {
                    set => _capsuleCollider2D.offset = value;
                    get => _capsuleCollider2D.offset;
               }
               /// <summary>
               /// 胶囊体尺寸X
               /// </summary>
               public float capsuleSizeX
               {
                    set => _capsuleSize = new Vector2(value, _capsuleSize.y);
                    get => _capsuleSize.x;
               }
               /// <summary>
               /// 胶囊体尺寸Y
               /// </summary>
               public float capsuleSizeY
               {
                    set => _capsuleSize = new Vector2(_capsuleSize.x, value);
                    get => _capsuleSize.y;
               }
          #endregion

     #endregion
}