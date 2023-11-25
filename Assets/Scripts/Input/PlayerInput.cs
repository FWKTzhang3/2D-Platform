using UnityEngine;
using UnityEngine.InputSystem;
using DataStructures;

/// <summary>
/// 玩家控制器脚本
/// </summary>
/// <remarks> 就是让控制器系统直接生成脚本，然后在这个脚本里使用各种方法来用，然后让其他脚本读取定义好的方法函数啥的就行了。 </remarks>
public class PlayerInput : MonoBehaviour
{
     // 调用
     private PlayerInputActions playerInputActions;    // 控制器
     private InputCommandHandler inputCommandHandler;  // 指令转换
     private LimitedDeque<string> inputCommandBuffer;  // 指令队列

     #region 控制器方向
     public Vector2 axes => playerInputActions.Gameplay.Axes.ReadValue<Vector2>();   //axes属性：这是一个Vector2类型的属性，用于存储玩家的输入轴值。
     public float axesX => axes.x;                                                   //axesX方法：返回axes属性的x分量。
     public float axesY => axes.y;                                                   // axesY方法: 返回axes属性的y分量。
     #endregion

     #region 摇杆状态
     public bool isStickOffseting => axes != Vector2.zero;
     #endregion

     #region 移动
     public bool move => axesX != 0f;                                 //move方法：这个方法检查axesX是否不等于0（即玩家是否在移动）。
     public bool crouch => axesY < 0f;                                //crouch方法：这个方法检查axesY是否小于0（即玩家是否在下蹲）。
     #endregion

     #region 跳跃
     public bool jump => playerInputActions.Gameplay.Jump.WasPressedThisFrame();          // 检测是否在当前帧按下 Jump
     public bool stopJump => playerInputActions.Gameplay.Jump.WasReleasedThisFrame();     // 检测是否在当前帧抬起 Jump
     public bool holdJump => playerInputActions.Gameplay.Jump.ReadValue<float>() == 1;    // 检测是否按住 Jump
     #endregion

     #region 攻击
     public bool attack => playerInputActions.Gameplay.Attack.WasPerformedThisFrame();    // 检测是否在当前帧按下 Attack
     public bool skill => playerInputActions.Gameplay.Skill.WasPerformedThisFrame();      // 检测是否在当前帧按下 Skill
     #endregion

     #region  其他功能
     public bool interact => playerInputActions.Gameplay.Interact.WasPerformedThisFrame();     // 是否按下交互
     public bool swithcMap => playerInputActions.Gameplay.MapToggle.WasPerformedThisFrame();   // 切换出地图
     public bool swithcMenu => playerInputActions.Gameplay.MenuToggle.WasPerformedThisFrame(); // 切换出菜单
     #endregion

     [Header("指令缓冲区设置")]
     [SerializeField, Tooltip("缓冲区容量")] private int maxControllerBuffer;
     [SerializeField, Tooltip("空输入倒计时")] private float nullCommandTime;
     private float currentNullCommandTime;   // 当前空指令时间

     private void Awake()
     {
          playerInputActions = new PlayerInputActions();
          inputCommandHandler = new InputCommandHandler();
          inputCommandBuffer = new LimitedDeque<string>(maxControllerBuffer);
     }

     private void OnEnable()
     {
          playerInputActions.Gameplay.Axes.performed += OnAxesPerformed;

          playerInputActions.Gameplay.Attack.started += OnActionstarted;
          playerInputActions.Gameplay.Jump.started += OnActionstarted;
          playerInputActions.Gameplay.Skill.started += OnActionstarted;
          playerInputActions.Gameplay.Interact.started += OnActionstarted;
     }

     private void OnDisable()
     {
          playerInputActions.Gameplay.Axes.performed -= OnAxesPerformed;

          playerInputActions.Gameplay.Attack.started -= OnActionstarted;
          playerInputActions.Gameplay.Jump.started -= OnActionstarted;
          playerInputActions.Gameplay.Skill.started -= OnActionstarted;
          playerInputActions.Gameplay.Interact.started -= OnActionstarted;
     }

     private void Update()
     {
          UpdateInputNullCommand();
     }

     /// <summary>
     /// 启动GameplayInputs
     /// </summary>
     public void EnableGameplayInputs() => playerInputActions.Gameplay.Enable();

     /// <summary>
     /// 关闭 GameplayInputs
     /// </summary>
     public void DisableGameplayInputs() => playerInputActions.Gameplay.Disable();

     /// <summary>
     /// 设置鼠标状态
     /// </summary>
     /// <param name="mouseStateMode">鼠标锁定模式</param>
     public void SetMouseState(CursorLockMode mouseStateMode) => Cursor.lockState = mouseStateMode;

     /// <summary>
     /// <para>更新输入控制器空指令。</para>
     /// <para>Update Input Controller Null Command.</para>
     /// </summary>
     /// </param>
     private void UpdateInputNullCommand()
     {
          if (inputCommandBuffer.PeekFirst != null)  // 检查输入命令缓冲区中的第一个元素是否为空。
                                                       // Check if the first element in the input command buffer is null.
          {
               currentNullCommandTime -= Time.deltaTime;    // 如果不为空，则当前空指令时间开始倒计时。
                                                            // If it's not null, then start counting down the current null command time.

               if (currentNullCommandTime <= 0)             // 如果当前的空命令倒计时小于等于0
                                                            // If the countdown for the current null command is less than or equal to 0.
               {
                    inputCommandBuffer.AddFirst(null);           // 添加一个空指令到缓冲区
                                                                 // Add a null command to the buffer.
                    currentNullCommandTime = nullCommandTime;    // 重置倒计时
                                                                 // Reset the countdown.
               }
          }
     }

     /// <summary>
     /// <para>当摇杆操作被执行时调用的方法。</para>
     /// <para>Method called when a joystick operation is performed.</para>
     /// </summary>
     private void OnAxesPerformed(InputAction.CallbackContext context)
     {
          Vector2 inputVector = context.ReadValue<Vector2>();                             // 从输入操作数据中读取二维向量输入值
          string currentCommand = inputCommandHandler.GetInputDirectionType(inputVector); // 使用输入向量获取当前的指令类型
          inputCommandBuffer.AddFirst(currentCommand);                                    // 将当前指令添加到命令缓冲区的开头
          currentNullCommandTime = nullCommandTime;                                       // 重置空指令倒计时
     }

     /// <summary>
     /// <para>当行动按钮被执行时调用的方法。</para>
     /// <para>Method called when the action button is executed.</para>
     /// </summary>
     private void OnActionstarted(InputAction.CallbackContext context)
     {
          string actionName = context.action.name;                                        // 获取行动名字
          inputCommandBuffer.AddFirst(actionName);                                        // 添加到队列
          currentNullCommandTime = nullCommandTime;                                       // 重置空指令倒计时
     }

     #region 缓冲指令

     public bool hasAttackInputBuffer   // 攻击
     {
          get
          {
               string firstCommand = inputCommandBuffer.PeekFirst;
               return firstCommand == ActionType.Attack.ToString();
          }
     }

     public bool hasJumpInputBuffer     // 跳跃
     {
          get
          {
               string firstCommand = inputCommandBuffer.PeekFirst;
               return firstCommand == ActionType.Jump.ToString();
          }
     }

     public bool hasSkillInputBuffer    // 跳跃
     {
          get
          {
               string firstCommand = inputCommandBuffer.PeekFirst;
               return firstCommand == ActionType.Skill.ToString();
          }
     }

     #endregion

#if UNITY_EDITOR
     private void OnGUI()
     {
          GUI.skin.label.fontSize = 50;
          // 遍历队列中的元素，并使用GUILayout.Label()方法绘制出元素名称
          foreach (object item in inputCommandBuffer)
          {
               string elementString = item != null ? item.ToString() : "null";
               GUILayout.Label(elementString);
          }
     }
#endif

}