using UnityEngine;

/// <summary>
/// 控制系统
/// </summary>
/// <remarks> 在这里控制整个物体 </remarks>
public class ControllSystem : MonoBehaviour
{
     private Transform _transform; // 转换器组件

     private InputSystem _input;   // 控制器系统

     private void Awake()
     {
          _transform = transform.parent;          // 获取父物体的转换器组件
          _input = GetComponent<InputSystem>();   // 获取控制器系统
     }

     private void OnEnable()
     {
          _input.OnJoyStickEvent += Flip;
     }

     private void OnDisable()
     {
          _input.OnJoyStickEvent -= Flip;
     }

     /// <summary>
     /// 物体转向的方法（用修改Scale缩放）
     /// </summary>
     /// <param name="inputDirection"> 输入控制器方向 </param>
     private void Flip(Vector2 inputDirection)
     {
          int currentFaceDirection = Mathf.RoundToInt(inputDirection.x);   // 缓存当前控制器摇杆X轴方向，转换为整数
          if (currentFaceDirection != 0 && _transform.lossyScale.x != currentFaceDirection)     // 如果当前方向不为零 且 不等于当前缩放值
               // 则重新赋值当前X轴Scale缩放
               _transform.localScale = new Vector2(currentFaceDirection, _transform.lossyScale.y);
     }
}