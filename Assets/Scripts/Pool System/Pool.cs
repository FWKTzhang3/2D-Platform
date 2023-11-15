using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
     public GameObject Prefab => prefab;          // ��ȡҪ���ɶ���ص�Ԥ����
     public int Size => size;                     // Ԥ�����سߴ�
     public int RuntimeSize => queue.Count;       // ��Ϸ����ʱ��ʵ�ʳߴ�

     [SerializeField] private GameObject prefab;  // Ҫ���ɶ���ص�Ԥ���壨ͨ��Unity�༭����ֵ��
     [SerializeField] private int size = 1;       // ����ش�С��һ���Դ�ŵĶ���������
     private Queue<GameObject> queue;             // ����ض��У����ڴ�Ŷ���Ķ���
     private Transform parent;                    // ����صĸ���Transform�������ɵĶ��󽫻ᱻ����Ϊ���Ӷ���

     /// <summary>
     /// ��ʼ������
     /// </summary>
     public void Initialize(Transform parent)
     {
          queue = new Queue<GameObject>();   // ����һ���µ���Ϸ�������
          this.parent = parent;              // ������ĸ��� Transform ��ֵ����ǰ����� parent ��Ա����

          // �����ƵĶ���������
          for (int i = 0; i < size; i++)     // ѭ�� size ��
          {
               queue.Enqueue(Copy());         // ������ Copy ���������ĸ�������������
          }
     }

     /// <summary>
     /// ����
     /// </summary>
     /// <returns> ���ظ��ƶ��� </returns>
     private GameObject Copy()
     {
          var copy = Object.Instantiate(prefab, parent);    // ���� Object ��� Instantiate ����������һ�� prefab �ĸ����������丳ֵ������ copy��
          copy.SetActive(false);                            // ��copy������Ϊ�Ǽ���״̬
          return copy;                                      // ���ش����ĸ�������
     }

     /// <summary>
     /// ���ö���
     /// </summary>
     /// <returns> ���ؿ��ö��� </returns>
     private GameObject AvailableObject()
     {
          GameObject availableObject = null;                // ��ʼ�����ö���Ϊnull
          if (queue.Count > 0 && !queue.Peek().activeSelf)   // �����������Ԫ�ز��Ҷ���Ԫ�ش���δ����״̬
               availableObject = queue.Dequeue();                // �Ӷ�����ȡ��һ������
          else                                              // ��֮���������Ϊ�գ�
               availableObject = Copy();                         // ����һ���µĶ���
          queue.Enqueue(availableObject);                   // ������������
          return availableObject;                           // ���ؿ��ö���
     }

     #region ׼�����󷽷�

     /// <summary>
     /// ׼�����󷽷�
     /// </summary>
     /// <remarks> ����Ҫ�κβ��� </remarks>
     /// <returns> ׼���õĶ��� </returns>
     public GameObject PreparedObject()
     {
          GameObject preparedObject = AvailableObject();    // ��ȡһ�����ö���
          preparedObject.SetActive(true);                   // ����׼���õĶ���
          return preparedObject;                            // ����׼���õĶ���
     }

     /// <summary>
     /// ׼�����󷽷�
     /// </summary>
     /// <param name="position"> �����ʼ���� </param>
     /// <remarks> ��һ������ </remarks>
     /// <returns> ׼���õĶ��� </returns>
     public GameObject PreparedObject(Vector2 position)
     {
          GameObject preparedObject = AvailableObject();    // ��ȡһ�����ö���
          preparedObject.SetActive(true);                   // ����׼���õĶ���
          preparedObject.transform.position = position;     // ���ö����λ��Ϊ�����position
          return preparedObject;                            // ����׼���õĶ���
     }

     /// <summary>
     /// ׼�����󷽷�
     /// </summary>
     /// <param name="position"> �����ʼ���� </param>
     /// <param name="localScale"> �����ʼ��ת </param>
     /// <remarks> ��Ҫ��һ�����룬��ʼ���ꡢ��ʼ���� </remarks>
     /// <returns> ׼���õĶ��� </returns>
     public GameObject PreparedObject(Vector2 position, Vector2 localScale)
     {
          GameObject preparedObject = AvailableObject();    // ��ȡһ�����ö���
          preparedObject.SetActive(true);                   // ����׼���õĶ���
          preparedObject.transform.position = position;     // ���ö����λ��Ϊ�����position
          preparedObject.transform.localScale = localScale; // ���ö�������ű���Ϊ�����localScale
          return preparedObject;                            // ����׼���õĶ���
     }

     /// <summary>
     /// ׼�����󷽷�
     /// </summary>
     /// <param name="position"> �����ʼ���� </param>
     /// <param name="localScale"> �����ʼ���� </param>
     /// <param name="rotation"> �����ʼ��ת </param>
     /// <remarks> ��Ҫ��һ�����룬��ʼ���ꡢ��ʼ���򡢳�ʼ��ת </remarks>
     /// <returns> ׼���õĶ��� </returns>
     public GameObject PreparedObject(Vector2 position, Vector2 localScale, Quaternion rotation)
     {
          GameObject preparedObject = AvailableObject();    // ��ȡһ�����ö���
          preparedObject.SetActive(true);                   // ����׼���õĶ���
          preparedObject.transform.position = position;     // ���ö����λ��Ϊ�����position
          preparedObject.transform.localScale = localScale; // ���ö�������ű���Ϊ�����localScale
          preparedObject.transform.rotation = rotation;     // ���ö������תΪ�����rotation
          return preparedObject;                            // ����׼���õĶ���
     }

     #endregion
}
