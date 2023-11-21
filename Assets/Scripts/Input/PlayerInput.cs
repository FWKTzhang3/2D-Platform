using System.Collections;
using UnityEngine;

/// <summary>
/// 玩家控制器脚本
/// </summary>
/// <remarks> 就是让控制器系统直接生成脚本，然后在这个脚本里使用各种方法来用，然后让其他脚本读取定义好的方法函数啥的就行了。 </remarks>
public class PlayerInput : MonoBehaviour
{
     // 调用
     PlayerInputActions playerInputActions;

     private Coroutine inputBufferCoroutine;

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
     public bool hasJumpInputBuffer;                                                      // 预输入指令（攻击）
     public float hasJumpInputBufferTime;                                                // 预输入指令持续时间（攻击）
     #endregion

     #region 攻击
     public bool attack => playerInputActions.Gameplay.Attack.WasPerformedThisFrame();    // 检测是否在当前帧按下 Attack
     public bool hasAttackInputBuffer;                                                    // 预输入指令（攻击）
     public float hasAttackInputBufferTime;                                              // 预输入指令持续时间（攻击）
     public bool skill => playerInputActions.Gameplay.Skill.WasPerformedThisFrame();      // 检测是否在当前帧按下 Skill
     #endregion

     private void Awake()
     {
          // 实例
          playerInputActions = new PlayerInputActions();
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

     #region 指令预输入系统
     // 你也不希望你的指令接收很反人类吧
     /// <summary>
     /// 预输入系统
     /// </summary>
     /// <param name="inputBufferBool"> 对应输入的回调函数 </param>
     /// <param name="inputBufferTime"> bool函数持续时间 </param>
     public void SetInputBuffer(Callback inputBufferBool, float inputBufferTime)
     {
          if(inputBufferCoroutine != null)             // 若当前协程不为空
               StopCoroutine(inputBufferCoroutine);         // 停止协程
          inputBufferCoroutine = StartCoroutine(InputBufferCoroutine(inputBufferBool, inputBufferTime));
     }

     /// <summary>
     /// 预输入系统协程
     /// </summary>
     /// <param name="inputBufferBool"> 对应输入的回调函数 </param>
     /// <param name="inputBufferTime"> bool函数持续时间 </param>
     /// <returns></returns>
     IEnumerator InputBufferCoroutine(Callback inputBufferBool , float inputBufferTime)
     {
          inputBufferBool.Invoke(true);
          yield return new WaitForSeconds(inputBufferTime);
          inputBufferBool.Invoke(false);
     }

     /// <summary>
     /// 回调函数
     /// </summary>
     /// <param name="inputBufferBool"> 只接受bool函数 </param>
     /// <remarks> 回调给输入的bool函数 </remarks>
     public delegate void Callback(bool inputBufferBool);

     /// <summary>
     /// Jump 回调函数的方法
     /// </summary>
     /// <param name="inputBufferBool"> 输入回调出去的值 </param>
     /// <remarks> 回调给 hasJumpInputBuffer </remarks>
     public void CallbackJumpInputBuffer(bool inputBufferBool) => hasJumpInputBuffer = inputBufferBool;

     /// <summary>
     /// Attack 回调函数方法
     /// </summary>
     /// <param name="inputBufferBool"> 输入回调出去的值 </param>
     /// <remarks> 回调给 hasAttackInputBuffer </remarks>
     public void CallbackAttackInputBuffer(bool inputBufferBool) => hasAttackInputBuffer = inputBufferBool;

     #endregion
}
