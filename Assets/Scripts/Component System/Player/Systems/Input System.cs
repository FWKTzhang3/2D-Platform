using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DataStructures.LimitDatas;

public class InputSystem : MonoBehaviour
{
     private PlayerInputActions _playerInputActions;    // ������
     private InputCommandHandler _inputCommandHandler;  // ָ��ת��
     private LimitedDeque<string> _inputCommandBuffer;  // ָ�����    

     public event Action<Vector2> OnJoyStickEvent;     // ҡ���¼�

     public event Action OnJumpEvent;                  // ��Ծ�¼�
     public event Action StopJumpEvent;                // ֹͣ��Ծ�¼�

     public event Action OnAttackEvent;                // �����¼�
     public event Action OnSkillEvent;                  // �����¼�
     public event Action OnInteractEvent;              // �����¼�

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
          _playerInputActions.Gameplay.Axes.performed += OnAxesPerformed;
          _playerInputActions.Gameplay.Axes.canceled += OnAxesPerformed;

          // ע�ᰴť�¼������£�
          _playerInputActions.Gameplay.Attack.started += OnActionStarted;
          _playerInputActions.Gameplay.Jump.started += OnActionStarted;
          _playerInputActions.Gameplay.Skill.started += OnActionStarted;
          _playerInputActions.Gameplay.Interact.started += OnActionStarted;

          // ע�ᰴť�¼���̧��
          _playerInputActions.Gameplay.Jump.canceled += OnActionCanceled;
          _playerInputActions.Gameplay.Attack.canceled += OnActionCanceled;
     }

     private void OnDisable()
     {
          DisableGameplayInputs(); // �رտ�����

          // ע��ҡ���¼�
          _playerInputActions.Gameplay.Axes.performed -= OnAxesPerformed;
          _playerInputActions.Gameplay.Axes.canceled -= OnAxesPerformed;

          // ע����ť�¼������£�
          _playerInputActions.Gameplay.Attack.started -= OnActionStarted;
          _playerInputActions.Gameplay.Jump.started -= OnActionStarted;
          _playerInputActions.Gameplay.Skill.started -= OnActionStarted;
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
     private void OnAxesPerformed(InputAction.CallbackContext context)
     {
          Vector2 moveInput = context.ReadValue<Vector2>();      // ��ȡҡ������
          OnJoyStickEvent?.Invoke(moveInput);                    // ��������
          if (context.phase == InputActionPhase.Performed)
          {
               string currentCommand = _inputCommandHandler.GetInputDirectionType(moveInput);  // ʹ������������ȡ��ǰ��ָ������
               _inputCommandBuffer.AddFirst(currentCommand);                                   // ����ǰָ����ӵ���������Ŀ�ͷ
               _currentNullCommandTime = _nullCommandTime;                                     // ���ÿ�ָ���ʱ
          }
     }

     /// <summary>
     /// �ж��¼�����(����)
     /// </summary>
     /// <param name="context"> �����ж����лص�����</param>
     private void OnActionStarted(InputAction.CallbackContext context)
     {
          string currentActionName = context.action.name;   // ���浱ǰ�¼�����
          _inputCommandBuffer.AddFirst(currentActionName);   // ����ָ�����
          _currentNullCommandTime = _nullCommandTime;       // ���ÿ�ָ���ʱ
          switch (currentActionName)       // ����ж�����
          {
               case "Jump":
                    OnJumpEvent?.Invoke();
                    break;
               case "Attack":
                    OnAttackEvent?.Invoke();
                    break;
               case "Skill":
                    OnSkillEvent?.Invoke();
                    break;
               case "Interact":
                    OnInteractEvent?.Invoke();
                    break;
               default:
                    break;
          }
     }

     /// <summary>
     /// �ж��¼�����(̧��)
     /// </summary>
     /// <param name="context"> �����ж����лص�����</param>
     private void OnActionCanceled(InputAction.CallbackContext context)
     {
          string currentActionName = context.action.name;   // ��ȡ��ǰ�¼�����
          switch (currentActionName)       // ����ж�����
          {
               case "Jump":
                    StopJumpEvent?.Invoke();
                    break;
               default:
                    break;
          }
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

     public bool hasAttackInputBuffer   // ����
     {
          get
          {
               string firstCommand = _inputCommandBuffer.PeekFirst;
               return firstCommand == ActionType.Attack.ToString();
          }
     }

     public bool hasJumpInputBuffer     // ��Ծ
     {
          get
          {
               string firstCommand = _inputCommandBuffer.PeekFirst;
               return firstCommand == ActionType.Jump.ToString();
          }
     }

     public bool hasSkillInputBuffer    // ��Ծ
     {
          get
          {
               string firstCommand = _inputCommandBuffer.PeekFirst;
               return firstCommand == ActionType.Skill.ToString();
          }
     }

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
