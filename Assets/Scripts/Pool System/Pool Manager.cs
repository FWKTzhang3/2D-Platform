using UnityEngine;

public class PoolManager : MonoBehaviour
{
     [SerializeField] private Pool[] playerProjectilePools;      // �����ҷ�������Ķ��������

     private void Start()
     {
          Initialize(playerProjectilePools);                     // ����Ϸ��ʼʱ��ʼ����ҷ�������Ķ��������
     }

     /// <summary>
     /// ��ʼ�������
     /// </summary>
     /// <param name="pools"> ����Ҫ��ʼ�������� </param>
     private void Initialize(Pool[] pools)
     {
          foreach (var pool in pools)   // �����������������
          {
               // Ϊÿ������ش���һ������ Transform��������Ϊ "Pool: Ԥ��������"
               Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;
               poolParent.parent = transform;     // ���´����ĸ��� Transform ����Ϊ��ǰ������Ӷ���
               pool.Initialize(poolParent);       // ���ö���صĳ�ʼ�����������´����ĸ��� Transform ������ж���صĳ�ʼ��
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
