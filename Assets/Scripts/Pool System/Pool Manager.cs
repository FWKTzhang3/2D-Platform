using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
     [SerializeField] private Pool[] playerPools;                // 存放玩家发射物体的对象池数组
     static private Dictionary<GameObject, Pool> dictionary;     // 芝士个静态字典，以物体为键，对象池为值

     private void Awake()
     {
          dictionary = new Dictionary<GameObject, Pool>();       // 实例化字典
     }

     private void Start()
     {
          Initialize(playerPools);                     // 在游戏开始时初始化玩家发射物体的对象池数组
     }


     #if UNITY_EDITOR    // 用来编辑器里Debug用的，检测数组够不够用
     private void OnDestroy()
     {
          CheckPoolSize(playerPools);
     }

     /// <summary>
     /// 检查对象池尺寸
     /// </summary>
     /// <param name="pools"> 输入要检测的对象池 </param>
     private void CheckPoolSize(Pool[] pools)
     {
          foreach(var pool in pools)
          {
               if(pool.RuntimeSize > pool.Size)
               {
                    Debug.LogWarning(
                         string.Format("Pool：{0} 的运行时大小比其初始大小 {2} 大 {1} !",
                         pool.Prefab.name,
                         pool.RuntimeSize,
                         pool.Size
                         ));
               }
          }
     }

     # endif

     /// <summary>
     /// 初始化对象池
     /// </summary>
     /// <param name="pools"> 传入要初始化的数组 </param>
     private void Initialize(Pool[] pools)
     {
          foreach (var pool in pools)   // 历遍数组的所有物体
          {

               #if UNITY_EDITOR         // 用来编辑器里Debug用的，检测预制体有没有重复的
               if (dictionary.ContainsKey(pool.Prefab))
               {
                    Debug.LogError("有重复的预制体：" + pool.Prefab);
                    continue;
               }
               #endif

               dictionary.Add(pool.Prefab, pool); // 给字典加入新的条目（物体为键，对象池为值）
               // 为每个对象池创建一个父级 Transform，并命名为 "Pool: 预制体名称"
               Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;
               poolParent.parent = transform;     // 将新创建的父级 Transform 设置为当前对象的子对象
               pool.Initialize(poolParent);       // 调用对象池的初始化方法，将新创建的父级 Transform 传入进行对象池的初始化
          }
     }

     #region 释放对象

     /// <summary>
     /// <para>Return a specified <paramref name="prefab"></paramref> gameObject in the pool.</para>
     /// <para>根据传入的 <paramref name="prefab"></paramref> 参数，返回对象池中预备好的游戏对象。</para>
     /// </summary>
     /// 
     /// <param name="prefab">
     /// <para>specified gameObject prefab</para>
     /// <para>指定的游戏对象预制体</para>
     /// </param>
     /// 
     /// <returns>
     /// <para>Prepared gameObject in the pool</para>
     /// <para>对象池中预备好的游戏对象</para>
     /// </returns>
     public static GameObject Release(GameObject prefab)
     {
          #if UNITY_EDITOR
          if (!dictionary.ContainsKey(prefab))
          {
               Debug.LogError("对象池管理器找不到预制件：" + prefab.name);

               return null;
          }
          #endif
          return dictionary[prefab].PreparedObject();
     }

     /// <summary>
     /// <para>Return a specified <paramref name="prefab"></paramref> gameObject in the pool.</para>
     /// <para>根据传入的 <paramref name="prefab"></paramref> 参数，返回对象池中预备好的游戏对象。</para>
     /// </summary>
     /// 
     /// <param name="prefab">
     /// <para>Specified gameObject prefab</para>
     /// <para>指定的游戏对象预制体</para>
     /// </param>
     /// 
     /// <param name="position">
     /// <para>Specified gameObject position</para>
     /// <para>指定释放位置</para>
     /// </param>
     /// 
     /// <returns>
     /// <para>Prepared gameObject in the pool</para>
     /// <para>对象池中预备好的游戏对象</para>
     /// </returns>
     public static GameObject Release(GameObject prefab, Vector2 position)
     {
          #if UNITY_EDITOR
          if (!dictionary.ContainsKey(prefab))
          {
               Debug.LogError("没有找到预制体：" + prefab.name);

               return null;
          }
          #endif
          return dictionary[prefab].PreparedObject(position);
     }

     /// <summary>
     /// <para>Return a specified <paramref name="prefab"></paramref> gameObject in the pool.</para>
     /// <para>根据传入的 <paramref name="prefab"></paramref> 参数，返回对象池中预备好的游戏对象。</para>
     /// </summary>
     /// 
     /// <param name="prefab">
     /// <para>Specified gameObject prefab</para>
     /// <para>指定的游戏对象预制体</para>
     /// </param>
     /// 
     /// <param name="position">
     /// <para>Specified gameObject position</para>
     /// <para>指定释放位置</para>
     /// </param>
     /// 
     /// <param name="rotation">
     /// <para>Specified gameObject rotation</para>
     /// <para>指定释放角度</para>
     /// </param>
     /// 
     /// <returns>
     /// <para>Prepared gameObject in the pool</para>
     /// <para>对象池中预备好的游戏对象</para>
     /// </returns>
     public static GameObject Release(GameObject prefab, Vector2 position, Vector2 localScale)
     {
          #if UNITY_EDITOR
          if (!dictionary.ContainsKey(prefab))
          {
               Debug.LogError("没有找到预制体：" + prefab.name);

               return null;
          }
          #endif
          return dictionary[prefab].PreparedObject(position, localScale);
     }

     /// <summary>
     /// <para>Return a specified <paramref name="prefab"></paramref> gameObject in the pool.</para>
     /// <para>根据传入的 <paramref name="prefab"></paramref> 参数，返回对象池中预备好的游戏对象。</para>
     /// </summary>
     /// 
     /// <param name="prefab">
     /// <para>Specified gameObject prefab</para>
     /// <para>指定的游戏对象预制体</para>
     /// </param>
     /// 
     /// <param name="position">
     /// <para>Specified gameObject position</para>
     /// <para>指定释放位置</para>
     /// </param>
     /// 
     /// <param name="rotation">
     /// <para>Specified gameObject rotation</para>
     /// <para>指定释放角度</para>
     /// </param>
     /// 
     /// /// <param name="localScale">
     /// <para>Specified gameObject localScale</para>
     /// <para>指定释放方向</para>
     /// </param>
     /// 
     /// <returns>
     /// <para>Prepared gameObject in the pool</para>
     /// <para>对象池中预备好的游戏对象</para>
     /// </returns>
     public static GameObject Release(GameObject prefab, Vector2 position, Vector2 localScale, Quaternion rotation)
     {
          #if UNITY_EDITOR
          if (!dictionary.ContainsKey(prefab))
          {
               Debug.LogError("没有找到预制体：" + prefab.name);

               return null;
          }
          #endif
          return dictionary[prefab].PreparedObject(position, localScale, rotation);
     }

     #endregion
}
