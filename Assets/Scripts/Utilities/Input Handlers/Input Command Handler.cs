using UnityEngine;

/// <summary>
/// 输入命令处理程序
/// </summary>
public class InputCommandHandler
{
     /// <summary>
     /// <para>根据输入方向返回摇杆方向类型。</para>
     /// <para>Returns the joystick direction based on the input direction.</para>
     /// </summary>
     /// <param name="inputDirection">
     /// <para>输入方向向量。</para>
     /// <para>The input direction vector.</para>
     /// </param>
     /// <returns>
     /// <para>摇杆方向类型。</para>
     /// <para>The joystick direction type.</para>
     /// </returns>
     public JoystickDirectionType GetInputDirectionType(Vector2 inputDirection)
     {
          // 向量取整
          int inputDirectionX = Mathf.RoundToInt(inputDirection.x);
          int inputDirectionY = Mathf.RoundToInt(inputDirection.y);

          // Switch检索，返回对应枚举变量
          switch (inputDirectionX, inputDirectionY) 
          {
               case (0, 1):
                    return JoystickDirectionType.Up;
               case (0, -1):
                    return JoystickDirectionType.Down;
               case (-1, 0):
                    return JoystickDirectionType.Left;
               case (1, 0):
                    return JoystickDirectionType.Right;
               case (-1, 1):
                    return JoystickDirectionType.UpLeft;
               case (1, 1):
                    return JoystickDirectionType.UpRight;
               case (-1, -1):
                    return JoystickDirectionType.DownLeft;
               case (1, -1):
                    return JoystickDirectionType.DownRight;
               default: 
                    return JoystickDirectionType.None;
          }
     }
}
