using System.Collections.Generic;
using System.Diagnostics;

namespace DataStructures
{
     /// <summary>
     /// <para>表示有限容量的双端队列数据结构。</para>
     /// <para>Represents a limited capacity deque data structure.</para>
     /// </summary>
     /// <typeparam name="T">
     /// <para>双端队列中元素的类型。</para>
     /// <para>The type of elements in the deque.</para>
     /// </typeparam>
     public class LimitedDeque<T>
     {
          /// <summary>
          /// <para>队列的容量限制。</para>
          /// <para>capacity limit of the deque.</para>
          /// </summary>
          private readonly int capacity;
          private readonly LinkedList<T> deque;

          /// <summary>
          /// <para>使用指定的容量初始化 LimitedDeque 类的新实例。</para>
          /// <para>Initializes a new instance of the LimitedDeque class with the specified capacity.</para>
          /// </summary>
          /// <param name="capacity">
          /// <para>队列的最大容量。</para>
          /// <para>The maximum capacity of the deque.</para>
          /// </param>
          public LimitedDeque(int capacity)
          {
               this.capacity = capacity;
               this.deque = new LinkedList<T>();
          }

          /// <summary>
          /// <para>在队列头部添加元素。如果队列已满，则移除最旧的元素（尾部）。</para>
          /// <para>Adds an element to the beginning of the deque. If the deque is full, the oldest element (tail) is removed.</para>
          /// </summary>
          /// <param name="item">
          /// <para>要添加到队列的元素。</para>
          /// <para>The element to add to the deque.</para>
          /// </param>
          public void AddFirst(T item)
          {
               if (deque.Count >= capacity)  // 如果队列已满
                                             // If the deque is full
               {
                    deque.RemoveLast();           // 移除最旧的元素（尾部）
                                                  // Remove the oldest element (at the tail)
               }
               deque.AddFirst(item);         // 将新元素添加到队列头部
                                             // Add the new element to the beginning of the deque
          }

          /// <summary>
          /// <para> 在特定位置插入元素 </para>
          /// <para> Insert element at a specific position. </para>
          /// </summary>
          /// <param name="index"> 
          /// <para>插入位置（切记不要超过自定义的范围）</para>
          /// <para>Insert position (be careful not to exceed the custom range)</para>
          /// </param>
          /// <param name="list">
          /// <para>插入元素</para>
          /// <para>Insert element</para>
          /// </param>
          public void AddAt(int index, List<T> list)
          {
               // 检查位置是否大于限制范围（超了就不插）
               if(index > deque.Count)
               {
                    #if UNITY_EDITOR 
                    Debug.WriteLine("警告：当前插入位置超出限制");
                    Debug.WriteLine("Warning：Index is out of range");
                    #endif

                    return;
               }
               //插入元素
               list.Insert(index, default);
          }

          // 返回链表中元素的数量
          public int Count => deque.Count;

          // 返回队列头部的元素，但不移除它。
          // Returns the element at the beginning of the deque without removing it.
          public T PeekFirst()
          {
               return deque.First.Value;  
          }

          // 返回队列尾部的元素，但不移除它。
          // Returns the element at the end of the deque without removing it.
          public T PeekLast()
          {
               return deque.Last.Value;
          }

          /// <summary>
          /// <para>清空所有元素</para>
          /// <para>Clear all elements</para>
          /// </summary>
          public void ClearAll()
          {
               deque.Clear();
          }

          // 实现IEnumerable<T>接口
          // Implement the IEnumerable<T> interface.
          public IEnumerator<T> GetEnumerator()
          {
               return deque.GetEnumerator();
          }
     }
}
