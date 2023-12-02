using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataStructures.LimitDatas
{
     /// <summary>
     /// <para>��ʾ����������˫�˶������ݽṹ��</para>
     /// <para>Represents a limited capacity deque data structure.</para>
     /// </summary>
     /// <typeparam name="T">
     /// <para>˫�˶�����Ԫ�ص����͡�</para>
     /// <para>The type of elements in the deque.</para>
     /// </typeparam>
     public class LimitedDeque<T>
     {
          /// <summary>
          /// <para>���е��������ơ�</para>
          /// <para>capacity limit of the deque.</para>
          /// </summary>
          private readonly int capacity;
          private readonly LinkedList<T> deque;

          /// <summary>
          /// <para>ʹ��ָ����������ʼ�� LimitedDeque �����ʵ����</para>
          /// <para>Initializes a new instance of the LimitedDeque class with the specified capacity.</para>
          /// </summary>
          /// <param name="capacity">
          /// <para>���е����������</para>
          /// <para>The maximum capacity of the deque.</para>
          /// </param>
          public LimitedDeque(int capacity)
          {
               this.capacity = capacity;
               this.deque = new LinkedList<T>();
          }

          /// <summary>
          /// <para>�ڶ���ͷ�����Ԫ�ء�����������������Ƴ�β����Ԫ�ء�</para>
          /// <para>
          /// Adds an element to the beginning of the deque. 
          /// If the deque is full, the oldest element (tail) is removed.
          /// </para>
          /// </summary>
          /// <param name="item">
          /// <para>Ҫ��ӵ����е�Ԫ�ء�</para>
          /// <para>The element to add to the deque.</para>
          /// </param>
          public void AddFirst(T item)
          {
               if (deque.Count >= capacity)  // �����������
                                             // If the deque is full
               {
                    deque.RemoveLast();           // �Ƴ�β����Ԫ��
                                                  // Remove the last element
               }
               deque.AddFirst(item);         // ����Ԫ����ӵ�����ͷ��
                                             // Add the new element to the beginning of the deque
          }

          /// <summary>
          /// <para>�ڶ���β�����Ԫ�ء�����������������Ƴ�ͷ����Ԫ�ء�</para>
          /// <para>
          /// Adds an item to the end of the queue.
          /// If the queue is full, removes the item at the head before adding the new item.
          /// </para>
          /// </summary>
          /// <param name="item">
          /// <para>Ҫ��ӵ����е�Ԫ�ء�</para>
          /// <para>The element to add to the deque.</para>
          /// </param>
          public void AddLast(T item)
          {
               if (deque.Count >= capacity)  // �����������
                                             // If the deque is full
               {
                    deque.RemoveFirst();          // �Ƴ�ͷ����Ԫ��
                                                  // Remove the first element
               }
               deque.AddLast(item);          // ����Ԫ����ӵ�����β��
                                             // Add the new element to the end of the deque
          }

          /// <summary>
          /// <para> ����������Ԫ�ص������� </para>
          /// <para> Return the number of elements in the linked list. </para>
          /// </summary>
          public int Count => deque.Count;

          /// <summary>
          /// <para>��ȡ����β����Ԫ�ء�</para>
          /// <para>Retrieve the element at the front of the queue.</para>
          /// </summary>
          public T PeekFirst
          {
               get
               {
                    if (Count > 0)                      // �����ǰ���д��� 0
                         return deque.First.Value;          // ������λԪ��
                    else                               // ��֮
                         return default;                    // ���ض�Ӧ���ݵ�Ĭ����ֵ
               }
          }

          /// <summary>
          /// <para>��ȡ����β����Ԫ�ء�</para>
          /// <para>Retrieve the element at the end of the queue.</para>
          /// </summary>
          public T PeekLast
          {
               get
               {
                    if (Count > 0)                      // �����ǰ���д��� 0
                         return deque.Last.Value;           // ����β��Ԫ��
                    else                               // ��֮
                         return default;                    // ���ض�Ӧ���ݵ�Ĭ����ֵ
               }
          }

          /// <summary>
          /// <para>��ȡ�ض�λ��Ԫ�ء�</para>
          /// <para>Retrieve element at specific position.</para>
          /// </summary>
          /// <param name="index">
          /// <para>����Ҫ��ȡ��λ�á�</para>
          /// <para>Enter the position to read.</para>
          /// </param>
          /// <returns>
          /// <para>���ظ�λ��Ԫ�ء�</para>
          /// <para>Return the element at that position.</para>
          /// </returns>
          public T PeekAt(int index)
          {
               if (index > Count)       // ����ض�λ�ó�����������
               {
                    #if UNITY_EDITOR
                    Debug.WriteLine("���棺Ŀ��λ�ó������ơ�");
                    Debug.WriteLine("Warning: Target position exceeds the limit.");
                    #endif
                    return default;          // ���ض�Ӧ����Ĭ��ֵ
               }

               var item = deque.ElementAt(index); // �����������Ԫ��
               if (item == null)                  // ���Ϊ��
                    return default;                    // ���ض�Ӧ����Ĭ��ֵ
               else                               // ��֮
                    return item;                       // ���ض�Ӧ����
          }

          /// <summary>
          /// <para>�������Ԫ��</para>
          /// <para>Clear all elements</para>
          /// </summary>
          public T ClearAll
          {
               set
               {
                    deque.Clear();
               }
          }

          /// <summary>
          /// <para> ʵ��IEnumerable<T>�ӿ� </para>
          /// <para> Implement the IEnumerable<T> interface. </para>
          /// </summary>
          public IEnumerator<T> GetEnumerator()
          {
               return deque.GetEnumerator();
          }
     }
}
