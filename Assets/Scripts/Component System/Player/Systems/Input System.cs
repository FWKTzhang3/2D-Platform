using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DataStructures.LimitDatas;

public class InputSystem : MonoBehaviour
{
     private PlayerInputActions _playerInputActions;    // 控制器
     private InputCommandHandler _inputCommandHandler;  // 指令转换
     private LimitedDeque<string> _inputCommandBuffer;  // 指令队列    

     #region 控制器事件

     public event Action<Vector2> JoystickEvent;            // 摇杆事件

     public event Action<InputAction> ActionStartedEvent;   // 行动按钮按下事件
     public event Action<InputAction> ActionCanceledEvent;  // 行动按钮抬起事件

     #endregion

     #region 摇杆向量

     private Vector2 _joystickVector => _playerInputActions.Gameplay.Axes.ReadValue<Vector2>();
     public float joystickVectorX => _joystickVector.x;
     public float joystickVectorY => _joystickVector.y;

     #endregion

     #region 当前帧指令

     public bool normalAttack => _playerInputActions.Gameplay.Normal_Attack.WasPerformedThisFrame();     // 普通攻击
     public bool specialAttack => _playerInputActions.Gameplay.Special_Attack.WasPerformedThisFrame();   // 远程攻击
     public bool skillAttack => _playerInputActions.Gameplay.Skill_Attack.WasPerformedThisFrame();       // 技能攻击

     #endregion

     [Header("指令缓冲区设置")]
     [SerializeField, Tooltip("缓冲区容量")] private int _maxControllerBuffer;
     [SerializeField, Tooltip("空输入倒计时")] private float _nullCommandTime;
     private float _currentNullCommandTime;   // 当前空指令时间

     private void Awake()
     {
          _playerInputActions = new PlayerInputActions();
          _inputCommandHandler = new InputCommandHandler();
          _inputCommandBuffer = new LimitedDeque<string>(_maxControllerBuffer);
     }

     private void OnEnable()
     {
          EnableGameplayInputs();  // 启动控制器

          // 注册摇杆事件
          _playerInputActions.Gameplay.Axes.performed += OnAxesActions;
          _playerInputActions.Gameplay.Axes.canceled += OnAxesActions;

          // 注册按钮事件（按下）
          _playerInputActions.Gameplay.Jump.started += OnActionStarted;
          _playerInputActions.Gameplay.Normal_Attack.started += OnActionStarted;
          _playerInputActions.Gameplay.Special_Attack.started += OnActionStarted;
          _playerInputActions.Gameplay.Skill_Attack.started += OnActionStarted;
          _playerInputActions.Gameplay.Interact.started += OnActionStarted;

          // 注册按钮事件（抬起）
          _playerInputActions.Gameplay.Jump.canceled += OnActionCanceled;
     }

     private void OnDisable()
     {
          DisableGameplayInputs(); // 关闭控制器

          // 注销摇杆事件
          _playerInputActions.Gameplay.Axes.performed -= OnAxesActions;
          _playerInputActions.Gameplay.Axes.canceled -= OnAxesActions;

          // 注销按钮事件（按下）
          _playerInputActions.Gameplay.Jump.started -= OnActionStarted;
          _playerInputActions.Gameplay.Normal_Attack.started -= OnActionStarted;
          _playerInputActions.Gameplay.Special_Attack.started -= OnActionStarted;
          _playerInputActions.Gameplay.Skill_Attack.started -= OnActionStarted;
          _playerInputActions.Gameplay.Interact.started -= OnActionStarted;

          // 注销按钮事件（抬起）
          _playerInputActions.Gameplay.Jump.canceled -= OnActionCanceled;
     }

     private void Update()
     {
          UpdateInputNullCommand();
     }

     #region 控制器基础设置

     /// <summary>
     /// 启动GameplayInputs
     /// </summary>
     public void EnableGameplayInputs() => _playerInputActions.Gameplay.Enable();

     /// <summary>
     /// 关闭 GameplayInputs
     /// </summary>
     public void DisableGameplayInputs() => _playerInputActions.Gameplay.Disable();

     /// <summary>
     /// 设置鼠标状态
     /// </summary>
     /// <param name="mouseStateMode">鼠标锁定模式</param>
     public void SetMouseState(CursorLockMode mouseStateMode) => Cursor.lockState = mouseStateMode;

     #endregion

     #region 控制器事件方法

     /// <summary>
     /// 摇杆事件方法
     /// </summary>
     /// <param name="context"> 接收摇杆所有回调数据 </param>
     private void OnAxesActions(InputAction.CallbackContext context)
     {
          Vector2 moveInput = context.ReadValue<Vector2>();      // 获取摇杆向量
          JoystickEvent?.Invoke(moveInput);                      // 传递摇杆事件
          /*
          if (context.phase == InputActionPhase.Performed)
          {
               string currentCommand = _inputCommandHandler.GetInputDirectionType(moveInput);  // 使用输入向量获取当前的指令类型
               _inputCommandBuffer.AddFirst(currentCommand);                                   // 将当前指令添加到命令缓冲区的开头
               _currentNullCommandTime = _nullCommandTime;                                     // 重置空指令倒计时
          }
          */
     }

     /// <summary>
     /// 行动事件方法(按下)
     /// </summary>
     /// <param name="context"> 接收行动所有回调数据</param>
     private void OnActionStarted(InputAction.CallbackContext context)
     {
          string currentActionName = context.action.name;   // 缓存当前事件名称
          _inputCommandBuffer?.AddFirst(currentActionName); // 加入指令缓冲区
          _currentNullCommandTime = _nullCommandTime;       // 重置空指令倒计时
          ActionStartedEvent?.Invoke(context.action);       // 传递当前行动指令
     }

     /// <summary>
     /// 行动事件方法(抬起)
     /// </summary>
     /// <param name="context"> 接收行动所有回调数据</param>
     private void OnActionCanceled(InputAction.CallbackContext context)
     {
          ActionCanceledEvent?.Invoke(context.action); // 传递当前行动指令
     }

     #endregion

     /// <summary>
     /// <para>更新输入控制器空指令。</para>
     /// <para>Update Input Controller Null Command.</para>
     /// </summary>
     private void UpdateInputNullCommand()
     {
          if (_inputCommandBuffer.PeekFirst != null)        // 检查输入命令缓冲区中的第一个元素是否为空。
          {
               _currentNullCommandTime -= Time.deltaTime;        // 如果不为空，则当前空指令时间开始倒计时。
               if (_currentNullCommandTime <= 0)                 // 如果当前的空命令倒计时小于等于0
               {
                    _inputCommandBuffer.AddFirst(null);               // 添加一个空指令到缓冲区
               }
          }
     }

     #region 缓冲指令

     public bool jumpBuffer             // 跳跃
     {
          get
          {
               string firstCommand = _inputCommandBuffer?.PeekFirst;
               return firstCommand == ActionType.Jump.ToString();
          }
     }

     public bool normalAttackBuffer     // 普通攻击
     {
          get
          {
               string firstCommand = _inputCommandBuffer?.PeekFirst;
               return firstCommand == ActionType.Normal_Attack.ToString();
          }
     }

     public bool specialAttackBuffer    // 远程攻击
     {
          get
          {
               string firstCommand = _inputCommandBuffer?.PeekFirst;
               return firstCommand == ActionType.Special_Attack.ToString();
          }
     }

     public bool skillAttackBuffer      // 技能攻击
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
          // 遍历队列中的元素，并使用GUILayout.Label()方法绘制出元素名称
          foreach (object item in _inputCommandBuffer)
          {
               string elementString = item != null ? item.ToString() : "null";
               GUILayout.Label(elementString);
          }
     }
#endif
}
