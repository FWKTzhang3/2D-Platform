using System.Collections.Generic;
using System.Diagnostics;

namespace DataStructures
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
          /// <para>�ڶ���ͷ�����Ԫ�ء�����������������Ƴ���ɵ�Ԫ�أ�β������</para>
          /// <para>Adds an element to the beginning of the deque. If the deque is full, the oldest element (tail) is removed.</para>
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
                    deque.RemoveLast();           // �Ƴ���ɵ�Ԫ�أ�β����
                                                  // Remove the oldest element (at the tail)
               }
               deque.AddFirst(item);         // ����Ԫ����ӵ�����ͷ��
                                             // Add the new element to the beginning of the deque
          }

          /// <summary>
          /// <para> ���ض�λ�ò���Ԫ�� </para>
          /// <para> Insert element at a specific position. </para>
          /// </summary>
          /// <param name="index"> 
          /// <para>����λ�ã��мǲ�Ҫ�����Զ���ķ�Χ��</para>
          /// <para>Insert position (be careful not to exceed the custom range)</para>
          /// </param>
          /// <param name="list">
          /// <para>����Ԫ��</para>
          /// <para>Insert element</para>
          /// </param>
          public void AddAt(int index, List<T> list)
          {
               // ���λ���Ƿ�������Ʒ�Χ�����˾Ͳ��壩
               if(index > deque.Count)
               {
                    #if UNITY_EDITOR 
                    Debug.WriteLine("���棺��ǰ����λ�ó�������");
                    Debug.WriteLine("Warning��Index is out of range");
                    #endif

                    return;
               }
               //����Ԫ��
               list.Insert(index, default);
          }

          // ����������Ԫ�ص�����
          public int Count => deque.Count;

          // ���ض���ͷ����Ԫ�أ������Ƴ�����
          // Returns the element at the beginning of the deque without removing it.
          public T PeekFirst()
          {
               return deque.First.Value;  
          }

          // ���ض���β����Ԫ�أ������Ƴ�����
          // Returns the element at the end of the deque without removing it.
          public T PeekLast()
          {
               return deque.Last.Value;
          }

          /// <summary>
          /// <para>�������Ԫ��</para>
          /// <para>Clear all elements</para>
          /// </summary>
          public void ClearAll()
          {
               deque.Clear();
          }

          // ʵ��IEnumerable<T>�ӿ�
          // Implement the IEnumerable<T> interface.
          public IEnumerator<T> GetEnumerator()
          {
               return deque.GetEnumerator();
          }
     }
}
