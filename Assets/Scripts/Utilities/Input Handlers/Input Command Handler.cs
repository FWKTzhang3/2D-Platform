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
     public string GetInputDirectionType(Vector2 inputDirection)
     {
          // ����ȡ��
          int inputDirectionX = Mathf.RoundToInt(inputDirection.x);
          int inputDirectionY = Mathf.RoundToInt(inputDirection.y);

          // Switch���������ض�Ӧö�ٱ���
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
