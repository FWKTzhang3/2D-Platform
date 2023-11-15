using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
     [SerializeField] private Pool[] playerPools;                // �����ҷ�������Ķ��������
     static private Dictionary<GameObject, Pool> dictionary;     // ֥ʿ����̬�ֵ䣬������Ϊ���������Ϊֵ

     private void Awake()
     {
          dictionary = new Dictionary<GameObject, Pool>();       // ʵ�����ֵ�
     }

     private void Start()
     {
          Initialize(playerPools);                     // ����Ϸ��ʼʱ��ʼ����ҷ�������Ķ��������
     }


     #if UNITY_EDITOR    // �����༭����Debug�õģ�������鹻������
     private void OnDestroy()
     {
          CheckPoolSize(playerPools);
     }

     /// <summary>
     /// ������سߴ�
     /// </summary>
     /// <param name="pools"> ����Ҫ���Ķ���� </param>
     private void CheckPoolSize(Pool[] pools)
     {
          foreach(var pool in pools)
          {
               if(pool.RuntimeSize > pool.Size)
               {
                    Debug.LogWarning(
                         string.Format("Pool��{0} ������ʱ��С�����ʼ��С {2} �� {1} !",
                         pool.Prefab.name,
                         pool.RuntimeSize,
                         pool.Size
                         ));
               }
          }
     }

     # endif

     /// <summary>
     /// ��ʼ�������
     /// </summary>
     /// <param name="pools"> ����Ҫ��ʼ�������� </param>
     private void Initialize(Pool[] pools)
     {
          foreach (var pool in pools)   // �����������������
          {

               #if UNITY_EDITOR         // �����༭����Debug�õģ����Ԥ������û���ظ���
               if (dictionary.ContainsKey(pool.Prefab))
               {
                    Debug.LogError("���ظ���Ԥ���壺" + pool.Prefab);
                    continue;
               }
               #endif

               dictionary.Add(pool.Prefab, pool); // ���ֵ�����µ���Ŀ������Ϊ���������Ϊֵ��
               // Ϊÿ������ش���һ������ Transform��������Ϊ "Pool: Ԥ��������"
               Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;
               poolParent.parent = transform;     // ���´����ĸ��� Transform ����Ϊ��ǰ������Ӷ���
               pool.Initialize(poolParent);       // ���ö���صĳ�ʼ�����������´����ĸ��� Transform ������ж���صĳ�ʼ��
          }
     }

     #region �ͷŶ���

     /// <summary>
     /// <para>Return a specified <paramref name="prefab"></paramref> gameObject in the pool.</para>
     /// <para>���ݴ���� <paramref name="prefab"></paramref> ���������ض������Ԥ���õ���Ϸ����</para>
     /// </summary>
     /// 
     /// <param name="prefab">
     /// <para>specified gameObject prefab</para>
     /// <para>ָ������Ϸ����Ԥ����</para>
     /// </param>
     /// 
     /// <returns>
     /// <para>Prepared gameObject in the pool</para>
     /// <para>�������Ԥ���õ���Ϸ����</para>
     /// </returns>
     public static GameObject Release(GameObject prefab)
     {
          #if UNITY_EDITOR
          if (!dictionary.ContainsKey(prefab))
          {
               Debug.LogError("����ع������Ҳ���Ԥ�Ƽ���" + prefab.name);

               return null;
          }
          #endif
          return dictionary[prefab].PreparedObject();
     }

     /// <summary>
     /// <para>Return a specified <paramref name="prefab"></paramref> gameObject in the pool.</para>
     /// <para>���ݴ���� <paramref name="prefab"></paramref> ���������ض������Ԥ���õ���Ϸ����</para>
     /// </summary>
     /// 
     /// <param name="prefab">
     /// <para>Specified gameObject prefab</para>
     /// <para>ָ������Ϸ����Ԥ����</para>
     /// </param>
     /// 
     /// <param name="position">
     /// <para>Specified gameObject position</para>
     /// <para>ָ���ͷ�λ��</para>
     /// </param>
     /// 
     /// <returns>
     /// <para>Prepared gameObject in the pool</para>
     /// <para>�������Ԥ���õ���Ϸ����</para>
     /// </returns>
     public static GameObject Release(GameObject prefab, Vector2 position)
     {
          #if UNITY_EDITOR
          if (!dictionary.ContainsKey(prefab))
          {
               Debug.LogError("û���ҵ�Ԥ���壺" + prefab.name);

               return null;
          }
          #endif
          return dictionary[prefab].PreparedObject(position);
     }

     /// <summary>
     /// <para>Return a specified <paramref name="prefab"></paramref> gameObject in the pool.</para>
     /// <para>���ݴ���� <paramref name="prefab"></paramref> ���������ض������Ԥ���õ���Ϸ����</para>
     /// </summary>
     /// 
     /// <param name="prefab">
     /// <para>Specified gameObject prefab</para>
     /// <para>ָ������Ϸ����Ԥ����</para>
     /// </param>
     /// 
     /// <param name="position">
     /// <para>Specified gameObject position</para>
     /// <para>ָ���ͷ�λ��</para>
     /// </param>
     /// 
     /// <param name="rotation">
     /// <para>Specified gameObject rotation</para>
     /// <para>ָ���ͷŽǶ�</para>
     /// </param>
     /// 
     /// <returns>
     /// <para>Prepared gameObject in the pool</para>
     /// <para>�������Ԥ���õ���Ϸ����</para>
     /// </returns>
     public static GameObject Release(GameObject prefab, Vector2 position, Vector2 localScale)
     {
          #if UNITY_EDITOR
          if (!dictionary.ContainsKey(prefab))
          {
               Debug.LogError("û���ҵ�Ԥ���壺" + prefab.name);

               return null;
          }
          #endif
          return dictionary[prefab].PreparedObject(position, localScale);
     }

     /// <summary>
     /// <para>Return a specified <paramref name="prefab"></paramref> gameObject in the pool.</para>
     /// <para>���ݴ���� <paramref name="prefab"></paramref> ���������ض������Ԥ���õ���Ϸ����</para>
     /// </summary>
     /// 
     /// <param name="prefab">
     /// <para>Specified gameObject prefab</para>
     /// <para>ָ������Ϸ����Ԥ����</para>
     /// </param>
     /// 
     /// <param name="position">
     /// <para>Specified gameObject position</para>
     /// <para>ָ���ͷ�λ��</para>
     /// </param>
     /// 
     /// <param name="rotation">
     /// <para>Specified gameObject rotation</para>
     /// <para>ָ���ͷŽǶ�</para>
     /// </param>
     /// 
     /// /// <param name="localScale">
     /// <para>Specified gameObject localScale</para>
     /// <para>ָ���ͷŷ���</para>
     /// </param>
     /// 
     /// <returns>
     /// <para>Prepared gameObject in the pool</para>
     /// <para>�������Ԥ���õ���Ϸ����</para>
     /// </returns>
     public static GameObject Release(GameObject prefab, Vector2 position, Vector2 localScale, Quaternion rotation)
     {
          #if UNITY_EDITOR
          if (!dictionary.ContainsKey(prefab))
          {
               Debug.LogError("û���ҵ�Ԥ���壺" + prefab.name);

               return null;
          }
          #endif
          return dictionary[prefab].PreparedObject(position, localScale, rotation);
     }

     #endregion
}
