using UnityEngine;

/// <summary>
/// ������������
/// </summary>
public class InputCommandHandler
{
     /// <summary>
     /// <para>�������뷽�򷵻�ҡ�˷������͡�</para>
     /// <para>Returns the joystick direction based on the input direction.</para>
     /// </summary>
     /// <param name="inputDirection">
     /// <para>���뷽��������</para>
     /// <para>The input direction vector.</para>
     /// </param>
     /// <returns>
     /// <para>ҡ�˷������͡�</para>
     /// <para>The joystick direction type.</para>
     /// </returns>
     public JoystickDirectionType GetInputDirectionType(Vector2 inputDirection)
     {
          // ����ȡ��
          int inputDirectionX = Mathf.RoundToInt(inputDirection.x);
          int inputDirectionY = Mathf.RoundToInt(inputDirection.y);

          // Switch���������ض�Ӧö�ٱ���
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
