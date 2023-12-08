using UnityEngine;

/// <summary>
/// ����ϵͳ
/// </summary>
/// <remarks> ����������������� </remarks>
public class ControllSystem : MonoBehaviour
{
     private Transform _transform;                     // ת�������
     private Rigidbody2D _rigidbody2D;                 // �������
     private CapsuleCollider2D _capsuleCollider2D;     //������ײ�����

     private InputSystem _input;   // ������ϵͳ

     public bool isAttack;

     public bool isHitstun;
     public bool isDeadth;

     private void Awake()
     {
          _transform = transform.parent;                                   // ��ȡ�������ת�������
          _rigidbody2D = GetComponentInParent<Rigidbody2D>();              // ��ȡ��������ĸ������
          _capsuleCollider2D = GetComponentInParent<CapsuleCollider2D>();  // ��ȡ��������Ľ�����ײ�����

          _input = GetComponent<InputSystem>();   // ��ȡ������ϵͳ
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
     /// ��תX��ı�������
     /// </summary>
     private void FlipScaleX()
     {
          if (!isAttack && !isHitstun && !isDeadth)
          {
               int currentFaceDirection = Mathf.RoundToInt(_input.joystickVectorX);                 // ���浱ǰ������ҡ��X�᷽��ת��Ϊ����
               if (currentFaceDirection != 0 && _transform.lossyScale.x != currentFaceDirection)    // �����ǰ����Ϊ�� �� �����ڵ�ǰ����ֵ
               {
                    // �����¸�ֵ��ǰX��Scale����
                    _transform.localScale = new Vector2(currentFaceDirection, _transform.lossyScale.y);
               }
          }
     }

     /// <summary>
     /// ���չ���״̬����״̬
     /// </summary>
     /// <param name="attackState"> ����״̬ </param>
     private void AttackState(bool state)
     {
          isAttack = state;
     }

     #region �������

     /// <summary>
     /// ��д�����ٶ�
     /// </summary>
     private Vector2 _velocity
     {
          set => _rigidbody2D.velocity = value;
          get => _rigidbody2D.velocity;
     }

     /// <summary>
     /// ��д���� X ���ٶ�
     /// </summary>
     public float velocityX
     {
          set => _velocity = new Vector2(value, _velocity.y);
          get => _velocity.x;
     }

     /// <summary>
     /// ��д���� Y ���ٶ�
     /// </summary>
     public float velocityY
     {
          set => _velocity = new Vector2(_velocity.x, value);
          get => _velocity.y;
     }

     #endregion

     #region ������ײ�����

          #region ������ײ���������
               /// <summary>
     /// ��д��������
     /// </summary>
               private Vector2 _capsuleOffset
               {
                    set => _capsuleCollider2D.offset = value;
                    get => _capsuleCollider2D.offset;
               }
               /// <summary>
     /// �޸Ľ������� X
     /// </summary>
               public float OffsetX
               {
                    set => _capsuleOffset = new Vector2(value, _capsuleOffset.y);
                    get => _capsuleOffset.x;
               }
               /// <summary>
     /// �޸Ľ������� Y
     /// </summary>
               public float OffsetY
               {
                    set => _capsuleOffset = new Vector2(_capsuleOffset.x, value);
                    get => _capsuleOffset.y;
               }
          #endregion

          #region ������ײ��ߴ����
               /// <summary>
               /// ������ߴ�
               /// </summary>
               private Vector2 _capsuleSize
               {
                    set => _capsuleCollider2D.offset = value;
                    get => _capsuleCollider2D.offset;
               }
               /// <summary>
               /// ������ߴ�X
               /// </summary>
               public float capsuleSizeX
               {
                    set => _capsuleSize = new Vector2(value, _capsuleSize.y);
                    get => _capsuleSize.x;
               }
               /// <summary>
               /// ������ߴ�Y
               /// </summary>
               public float capsuleSizeY
               {
                    set => _capsuleSize = new Vector2(_capsuleSize.x, value);
                    get => _capsuleSize.y;
               }
          #endregion

     #endregion
}