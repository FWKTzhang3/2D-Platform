using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
     public GameObject Prefab => prefab;          // 获取要生成对象池的预制体
     public int Size => size;                     // 预设对象池尺寸
     public int RuntimeSize => queue.Count;       // 游戏运行时的实际尺寸

     [SerializeField] private GameObject prefab;  // 要生成对象池的预制体（通过Unity编辑器赋值）
     [SerializeField] private int size = 1;       // 对象池大小（一次性存放的对象数量）
     private Queue<GameObject> queue;             // 对象池队列，用于存放对象的队列
     private Transform parent;                    // 对象池的父级Transform，新生成的对象将会被设置为其子对象

     /// <summary>
     /// 初始化队列
     /// </summary>
     public void Initialize(Transform parent)
     {
          queue = new Queue<GameObject>();   // 创建一个新的游戏对象队列
          this.parent = parent;              // 将传入的父级 Transform 赋值给当前对象的 parent 成员变量

          // 将复制的对象加入队列
          for (int i = 0; i < size; i++)     // 循环 size 次
          {
               queue.Enqueue(Copy());         // 将调用 Copy 方法创建的副本对象加入队列
          }
     }

     /// <summary>
     /// 复制
     /// </summary>
     /// <returns> 返回复制对象 </returns>
     private GameObject Copy()
     {
          var copy = Object.Instantiate(prefab, parent);    // 调用 Object 类的 Instantiate 函数来创建一个 prefab 的副本，并将其赋值给变量 copy。
          copy.SetActive(false);                            // 将copy对象设为非激活状态
          return copy;                                      // 返回创建的副本对象
     }

     /// <summary>
     /// 可用对象
     /// </summary>
     /// <returns> 返回可用对象 </returns>
     private GameObject AvailableObject()
     {
          GameObject availableObject = null;                // 初始化可用对象为null
          if (queue.Count > 0 && !queue.Peek().activeSelf)   // 如果队列中有元素并且队首元素处于未激活状态
               availableObject = queue.Dequeue();                // 从队列中取出一个对象
          else                                              // 反之（如果队列为空）
               availableObject = Copy();                         // 创建一个新的对象
          queue.Enqueue(availableObject);                   // 将对象加入队列
          return availableObject;                           // 返回可用对象
     }

     #region 准备对象方法

     /// <summary>
     /// 准备对象方法
     /// </summary>
     /// <remarks> 不需要任何参数 </remarks>
     /// <returns> 准备好的对象 </returns>
     public GameObject PreparedObject()
     {
          GameObject preparedObject = AvailableObject();    // 获取一个可用对象
          preparedObject.SetActive(true);                   // 激活准备好的对象
          return preparedObject;                            // 返回准备好的对象
     }

     /// <summary>
     /// 准备对象方法
     /// </summary>
     /// <param name="position"> 对象初始坐标 </param>
     /// <remarks> 第一次重载 </remarks>
     /// <returns> 准备好的对象 </returns>
     public GameObject PreparedObject(Vector2 position)
     {
          GameObject preparedObject = AvailableObject();    // 获取一个可用对象
          preparedObject.SetActive(true);                   // 激活准备好的对象
          preparedObject.transform.position = position;     // 设置对象的位置为传入的position
          return preparedObject;                            // 返回准备好的对象
     }

     /// <summary>
     /// 准备对象方法
     /// </summary>
     /// <param name="position"> 对象初始坐标 </param>
     /// <param name="localScale"> 对象初始旋转 </param>
     /// <remarks> 还要再一次输入，初始坐标、初始方向 </remarks>
     /// <returns> 准备好的对象 </returns>
     public GameObject PreparedObject(Vector2 position, Vector2 localScale)
     {
          GameObject preparedObject = AvailableObject();    // 获取一个可用对象
          preparedObject.SetActive(true);                   // 激活准备好的对象
          preparedObject.transform.position = position;     // 设置对象的位置为传入的position
          preparedObject.transform.localScale = localScale; // 设置对象的缩放比例为传入的localScale
          return preparedObject;                            // 返回准备好的对象
     }

     /// <summary>
     /// 准备对象方法
     /// </summary>
     /// <param name="position"> 对象初始坐标 </param>
     /// <param name="localScale"> 对象初始方向 </param>
     /// <param name="rotation"> 对象初始旋转 </param>
     /// <remarks> 还要再一次输入，初始坐标、初始方向、初始旋转 </remarks>
     /// <returns> 准备好的对象 </returns>
     public GameObject PreparedObject(Vector2 position, Vector2 localScale, Quaternion rotation)
     {
          GameObject preparedObject = AvailableObject();    // 获取一个可用对象
          preparedObject.SetActive(true);                   // 激活准备好的对象
          preparedObject.transform.position = position;     // 设置对象的位置为传入的position
          preparedObject.transform.localScale = localScale; // 设置对象的缩放比例为传入的localScale
          preparedObject.transform.rotation = rotation;     // 设置对象的旋转为传入的rotation
          return preparedObject;                            // 返回准备好的对象
     }

     #endregion
}
