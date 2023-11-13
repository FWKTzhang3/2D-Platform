using UnityEngine;

public class PoolManager : MonoBehaviour
{
     [SerializeField] private Pool[] playerProjectilePools;      // 存放玩家发射物体的对象池数组

     private void Start()
     {
          Initialize(playerProjectilePools);                     // 在游戏开始时初始化玩家发射物体的对象池数组
     }

     /// <summary>
     /// 初始化对象池
     /// </summary>
     /// <param name="pools"> 传入要初始化的数组 </param>
     private void Initialize(Pool[] pools)
     {
          foreach (var pool in pools)   // 历遍数组的所有物体
          {
               // 为每个对象池创建一个父级 Transform，并命名为 "Pool: 预制体名称"
               Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;
               poolParent.parent = transform;     // 将新创建的父级 Transform 设置为当前对象的子对象
               pool.Initialize(poolParent);       // 调用对象池的初始化方法，将新创建的父级 Transform 传入进行对象池的初始化
          }
     }
     /*
     /// <summary>
     /// 
     /// </summary>
     /// <param name="prefab"></param>
     /// <returns></returns>
     public static GameObject Release(GameObject prefab)
     {

     }
     */
}
