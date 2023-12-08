using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DataStructures.LimitDatas;

public class InputSystem : MonoBehaviour
{
     private PlayerInputActions _playerInputActions;    // ������
     private InputCommandHandler _inputCommandHandler;  // ָ��ת��
     private LimitedDeque<string> _inputCommandBuffer;  // ָ�����    

     #region �������¼�

     public event Action<Vector2> JoystickEvent;            // ҡ���¼�

     public event Action<InputAction> ActionStartedEvent;   // �ж���ť�����¼�
     public event Action<InputAction> ActionCanceledEvent;  // �ж���ţ̌���¼�

     #endregion

     #region ҡ������

     private Vector2 _joystickVector => _playerInputActions.Gameplay.Axes.ReadValue<Vector2>();
     public float joystickVectorX => _joystickVector.x;
     public float joystickVectorY => _joystickVector.y;

     #endregion

     #region ��ǰָ֡��

     public bool normalAttack => _playerInputActions.Gameplay.Normal_Attack.WasPerformedThisFrame();     // ��ͨ����
     public bool specialAttack => _playerInputActions.Gameplay.Special_Attack.WasPerformedThisFrame();   // Զ�̹���
     public bool skillAttack => _playerInputActions.Gameplay.Skill_Attack.WasPerformedThisFrame();       // ���ܹ���

     #endregion

     [Header("ָ���������")]
     [SerializeField, Tooltip("����������")] private int _maxControllerBuffer;
     [SerializeField, Tooltip("�����뵹��ʱ")] private float _nullCommandTime;
     private float _currentNullCommandTime;   // ��ǰ��ָ��ʱ��

     private void Awake()
     {
          _playerInputActions = new PlayerInputActions();
          _inputCommandHandler = new InputCommandHandler();
          _inputCommandBuffer = new LimitedDeque<string>(_maxControllerBuffer);
     }

     private void OnEnable()
     {
          EnableGameplayInputs();  // ����������

          // ע��ҡ���¼�
          _playerInputActions.Gameplay.Axes.performed += OnAxesActions;
          _playerInputActions.Gameplay.Axes.canceled += OnAxesActions;

          // ע�ᰴť�¼������£�
          _playerInputActions.Gameplay.Jump.started += OnActionStarted;
          _playerInputActions.Gameplay.Normal_Attack.started += OnActionStarted;
          _playerInputActions.Gameplay.Special_Attack.started += OnActionStarted;
          _playerInputActions.Gameplay.Skill_Attack.started += OnActionStarted;
          _playerInputActions.Gameplay.Interact.started += OnActionStarted;

          // ע�ᰴť�¼���̧��
          _playerInputActions.Gameplay.Jump.canceled += OnActionCanceled;
     }

     private void OnDisable()
     {
          DisableGameplayInputs(); // �رտ�����

          // ע��ҡ���¼�
          _playerInputActions.Gameplay.Axes.performed -= OnAxesActions;
          _playerInputActions.Gameplay.Axes.canceled -= OnAxesActions;

          // ע����ť�¼������£�
          _playerInputActions.Gameplay.Jump.started -= OnActionStarted;
          _playerInputActions.Gameplay.Normal_Attack.started -= OnActionStarted;
          _playerInputActions.Gameplay.Special_Attack.started -= OnActionStarted;
          _playerInputActions.Gameplay.Skill_Attack.started -= OnActionStarted;
          _playerInputActions.Gameplay.Interact.started -= OnActionStarted;

          // ע����ť�¼���̧��
          _playerInputActions.Gameplay.Jump.canceled -= OnActionCanceled;
     }

     private void Update()
     {
          UpdateInputNullCommand();
     }

     #region ��������������

     /// <summary>
     /// ����GameplayInputs
     /// </summary>
     public void EnableGameplayInputs() => _playerInputActions.Gameplay.Enable();

     /// <summary>
     /// �ر� GameplayInputs
     /// </summary>
     public void DisableGameplayInputs() => _playerInputActions.Gameplay.Disable();

     /// <summary>
     /// �������״̬
     /// </summary>
     /// <param name="mouseStateMode">�������ģʽ</param>
     public void SetMouseState(CursorLockMode mouseStateMode) => Cursor.lockState = mouseStateMode;

     #endregion

     #region �������¼�����

     /// <summary>
     /// ҡ���¼�����
     /// </summary>
     /// <param name="context"> ����ҡ�����лص����� </param>
     private void OnAxesActions(InputAction.CallbackContext context)
     {
          Vector2 moveInput = context.ReadValue<Vector2>();      // ��ȡҡ������
          JoystickEvent?.Invoke(moveInput);                      // ����ҡ���¼�
          /*
          if (context.phase == InputActionPhase.Performed)
          {
               string currentCommand = _inputCommandHandler.GetInputDirectionType(moveInput);  // ʹ������������ȡ��ǰ��ָ������
               _inputCommandBuffer.AddFirst(currentCommand);                                   // ����ǰָ����ӵ���������Ŀ�ͷ
               _currentNullCommandTime = _nullCommandTime;                                     // ���ÿ�ָ���ʱ
          }
          */
     }

     /// <summary>
     /// �ж��¼�����(����)
     /// </summary>
     /// <param name="context"> �����ж����лص�����</param>
     private void OnActionStarted(InputAction.CallbackContext context)
     {
          string currentActionName = context.action.name;   // ���浱ǰ�¼�����
          _inputCommandBuffer?.AddFirst(currentActionName); // ����ָ�����
          _currentNullCommandTime = _nullCommandTime;       // ���ÿ�ָ���ʱ
          ActionStartedEvent?.Invoke(context.action);       // ���ݵ�ǰ�ж�ָ��
     }

     /// <summary>
     /// �ж��¼�����(̧��)
     /// </summary>
     /// <param name="context"> �����ж����лص�����</param>
     private void OnActionCanceled(InputAction.CallbackContext context)
     {
          ActionCanceledEvent?.Invoke(context.action); // ���ݵ�ǰ�ж�ָ��
     }

     #endregion

     /// <summary>
     /// <para>���������������ָ�</para>
     /// <para>Update Input Controller Null Command.</para>
     /// </summary>
     private void UpdateInputNullCommand()
     {
          if (_inputCommandBuffer.PeekFirst != null)        // ���������������еĵ�һ��Ԫ���Ƿ�Ϊ�ա�
          {
               _currentNullCommandTime -= Time.deltaTime;        // �����Ϊ�գ���ǰ��ָ��ʱ�俪ʼ����ʱ��
               if (_currentNullCommandTime <= 0)                 // �����ǰ�Ŀ������ʱС�ڵ���0
               {
                    _inputCommandBuffer.AddFirst(null);               // ���һ����ָ�������
               }
          }
     }

     #region ����ָ��

     public bool jumpBuffer             // ��Ծ
     {
          get
          {
               string firstCommand = _inputCommandBuffer?.PeekFirst;
               return firstCommand == ActionType.Jump.ToString();
          }
     }

     public bool normalAttackBuffer     // ��ͨ����
     {
          get
          {
               string firstCommand = _inputCommandBuffer?.PeekFirst;
               return firstCommand == ActionType.Normal_Attack.ToString();
          }
     }

     public bool specialAttackBuffer    // Զ�̹���
     {
          get
          {
               string firstCommand = _inputCommandBuffer?.PeekFirst;
               return firstCommand == ActionType.Special_Attack.ToString();
          }
     }

     public bool skillAttackBuffer      // ���ܹ���
     {
          get
          {
               string firstCommand = _inputCommandBuffer?.PeekFirst;
               return firstCommand == ActionType.Skill_Attack.ToString();
          }
     }

     #endregion

#if UNITY_EDITOR
     private void OnGUI()
     {
          GUI.skin.label.fontSize = 50;
          // ���������е�Ԫ�أ���ʹ��GUILayout.Label()�������Ƴ�Ԫ������
          foreach (object item in _inputCommandBuffer)
          {
               string elementString = item != null ? item.ToString() : "null";
               GUILayout.Label(elementString);
          }
     }
#endif
}
