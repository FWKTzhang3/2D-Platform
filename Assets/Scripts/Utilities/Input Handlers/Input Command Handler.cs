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
     public string GetInputDirectionType(Vector2 inputDirection)
     {
          // 向量取整
          int inputDirectionX = Mathf.RoundToInt(inputDirection.x);
          int inputDirectionY = Mathf.RoundToInt(inputDirection.y);

          // Switch检索，返回对应枚举变量
          switch (inputDirectionX, inputDirectionY) 
          {
               case (0, 1):
                    return JoystickDirectionType.Up.ToString();
               case (0, -1):
                    return JoystickDirectionType.Down.ToString();
               case (-1, 0):
                    return JoystickDirectionType.Left.ToString();
               case (1, 0):
                    return JoystickDirectionType.Right.ToString();
               case (-1, 1):
                    return JoystickDirectionType.UpLeft.ToString();
               case (1, 1):
                    return JoystickDirectionType.UpRight.ToString();
               case (-1, -1):
                    return JoystickDirectionType.DownLeft.ToString();
               case (1, -1):
                    return JoystickDirectionType.DownRight.ToString();
               default: 
                    return null;
          }
     }
}
