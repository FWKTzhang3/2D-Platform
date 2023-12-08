using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
     private Transform _transform;           // ת�������

     private InputSystem _input;             // ������ϵͳ
     private ControllSystem _controll;       // ����ϵͳ
     private DetectionSystem _detection;     // ���ϵͳ

     private AnimationsAllManager _animationsAllManager;    // ���ö���������

     public AnimatorState animatorState;                    // ���ö�����״̬�ṹ��

     private void Awake()
     {
          _transform = transform.parent;                    // ��ȡ���������ת����

          _input = GetComponent<InputSystem>();             // ��ȡͬ������Ŀ�����ϵͳ
          _controll = GetComponent<ControllSystem>();       // ��ȡͬ������Ŀ���ϵͳ
          _detection = GetComponent<DetectionSystem>();     // ��ȡͬ������ļ��ϵͳ

          _animationsAllManager = _transform.GetComponentInChildren<AnimationsAllManager>();
     }

     private void OnEnable()
     {
          _input.JoystickEvent += JoystickEvent;
     }

     private void OnDisable()
     {
          _input.JoystickEvent -= JoystickEvent;
     }

     private void Update()
     {
          UpdateAnimationState();

          _animationsAllManager.SetAllAnimatiorValue(animatorState);
     }

     /// <summary>
     /// ҡ���¼�����
     /// </summary>
     /// <param name="inputVecotr"> ҡ������ </param>
     private void JoystickEvent(Vector2 inputVecotr)
     {
          animatorState.isMove = inputVecotr.x != 0;   // �����ƶ�״̬���ڵ�ǰҡ������������0
          animatorState.isCrouch = inputVecotr.y < 0;  // �����¶�״̬���ڵ�ǰҡ������С��0
     }

     /// <summary>
     /// ���¶���״̬
     /// </summary>
     private void UpdateAnimationState()
     {
          animatorState.moveVelocity = Mathf.Abs(_controll.velocityX);                         // ���� �ƶ��ٶ� ���� ��ǰ�ٶ�X�ľ���ֵ
          animatorState.airVelocity = _controll.velocityY;                                     // ��ǰ �����ٶ� ���� ��ǰ�ٶ�Y

          animatorState.isAir = _detection.isAir;                                              // ��ǰ ����״̬ ���� ��ǰ���� ����״̬

          animatorState.isHurt = _controll.isHitstun;                                          // ��ǰ ����״̬ ���� ��ǰ���Ƶ� Ӳֱ״̬
          animatorState.isDeath = _controll.isDeadth;                                          // ��ǰ ����״̬ ���� ��ǰ���Ƶ� ����״̬

          animatorState.normalAttacks = _input.normalAttack || _input.normalAttackBuffer;      // ��ǰ ��ͨ����״̬ ���� ��ǰ���� ��ͨ����ָ��
          animatorState.specialAttacks = _input.specialAttack || _input.specialAttackBuffer;   // ��ǰ Զ�̹���״̬ ���� ��ǰ���� Զ�̹���ָ��
          animatorState.skillAttacks = _input.skillAttack || _input.skillAttackBuffer;         // ��ǰ ���ܹ���״̬ ���� ��ǰ���� ���ܹ���ָ��
     }

     /// <summary>
     /// ���������𶯷���
     /// </summary>
     /// <param name="shakeVlues"> ����ֵ </param>
     public void OnAnimationShake(ShakeVlues shakeVlues)
     {
          _animationsAllManager.AnimationShake(shakeVlues);
     }
}

/// <summary>
/// ������״̬
/// </summary>
public struct AnimatorState
{
     public float attackSpeed;     // ���������ٶ�
     public float moveSpeed;       // �ƶ������ٶ�

     public float moveVelocity;    // �ƶ��ٶ�
     public float airVelocity;     // �����ٶ�

     public bool isAir;            // ����״̬
     public bool isMove;           // �ƶ�״̬
     public bool isCrouch;         // �¶�״̬

     public bool isHurt;           // ����״̬
     public bool isDeath;          // ����״̬

     public bool normalAttacks;    // ��ͨ����
     public bool specialAttacks;   // Զ�̹���
     public bool skillAttacks;     // ���ܹ���
}