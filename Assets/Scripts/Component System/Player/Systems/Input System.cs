using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DataStructures.LimitDatas;

public class InputSystem : MonoBehaviour
{
     private PlayerInputActions _playerInputActions;    // 控制器
     private InputCommandHandler _inputCommandHandler;  // 指令转换
     private LimitedDeque<string> _inputCommandBuffer;  // 指令队列    

     public event Action<Vector2> OnJoyStickEvent;     // 摇杆事件

     public event Action OnJumpEvent;                  // 跳跃事件
     public event Action StopJumpEvent;                // 停止跳跃事件

     public event Action OnAttackEvent;                // 攻击事件
     public event Action OnSkillEvent;                  // 技能事件
     public event Action OnInteractEvent;              // 交互事件

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
          _playerInputActions.Gameplay.Axes.performed += OnAxesPerformed;
          _playerInputActions.Gameplay.Axes.canceled += OnAxesPerformed;

          // 注册按钮事件（按下）
          _playerInputActions.Gameplay.Attack.started += OnActionStarted;
          _playerInputActions.Gameplay.Jump.started += OnActionStarted;
          _playerInputActions.Gameplay.Skill.started += OnActionStarted;
          _playerInputActions.Gameplay.Interact.started += OnActionStarted;

          // 注册按钮事件（抬起）
          _playerInputActions.Gameplay.Jump.canceled += OnActionCanceled;
          _playerInputActions.Gameplay.Attack.canceled += OnActionCanceled;
     }

     private void OnDisable()
     {
          DisableGameplayInputs(); // 关闭控制器

          // 注销摇杆事件
          _playerInputActions.Gameplay.Axes.performed -= OnAxesPerformed;
          _playerInputActions.Gameplay.Axes.canceled -= OnAxesPerformed;

          // 注销按钮事件（按下）
          _playerInputActions.Gameplay.Attack.started -= OnActionStarted;
          _playerInputActions.Gameplay.Jump.started -= OnActionStarted;
          _playerInputActions.Gameplay.Skill.started -= OnActionStarted;
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
     private void OnAxesPerformed(InputAction.CallbackContext context)
     {
          Vector2 moveInput = context.ReadValue<Vector2>();      // 获取摇杆向量
          OnJoyStickEvent?.Invoke(moveInput);                    // 传递向量
          if (context.phase == InputActionPhase.Performed)
          {
               string currentCommand = _inputCommandHandler.GetInputDirectionType(moveInput);  // 使用输入向量获取当前的指令类型
               _inputCommandBuffer.AddFirst(currentCommand);                                   // 将当前指令添加到命令缓冲区的开头
               _currentNullCommandTime = _nullCommandTime;                                     // 重置空指令倒计时
          }
     }

     /// <summary>
     /// 行动事件方法(按下)
     /// </summary>
     /// <param name="context"> 接收行动所有回调数据</param>
     private void OnActionStarted(InputAction.CallbackContext context)
     {
          string currentActionName = context.action.name;   // 缓存当前事件名称
          _inputCommandBuffer.AddFirst(currentActionName);   // 加入指令缓冲区
          _currentNullCommandTime = _nullCommandTime;       // 重置空指令倒计时
          switch (currentActionName)       // 检测行动名称
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
     /// 行动事件方法(抬起)
     /// </summary>
     /// <param name="context"> 接收行动所有回调数据</param>
     private void OnActionCanceled(InputAction.CallbackContext context)
     {
          string currentActionName = context.action.name;   // 获取当前事件名称
          switch (currentActionName)       // 检测行动名称
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

     public bool hasAttackInputBuffer   // 攻击
     {
          get
          {
               string firstCommand = _inputCommandBuffer.PeekFirst;
               return firstCommand == ActionType.Attack.ToString();
          }
     }

     public bool hasJumpInputBuffer     // 跳跃
     {
          get
          {
               string firstCommand = _inputCommandBuffer.PeekFirst;
               return firstCommand == ActionType.Jump.ToString();
          }
     }

     public bool hasSkillInputBuffer    // 跳跃
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
          // 遍历队列中的元素，并使用GUILayout.Label()方法绘制出元素名称
          foreach (object item in _inputCommandBuffer)
          {
               string elementString = item != null ? item.ToString() : "null";
               GUILayout.Label(elementString);
          }
     }
#endif
}
