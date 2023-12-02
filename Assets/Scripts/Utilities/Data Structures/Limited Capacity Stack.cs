using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataStructures.LimitDatas
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
          /// <para>在队列头部添加元素。如果队列已满，则移除尾部的元素。</para>
          /// <para>
          /// Adds an element to the beginning of the deque. 
          /// If the deque is full, the oldest element (tail) is removed.
          /// </para>
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
                    deque.RemoveLast();           // 移除尾部的元素
                                                  // Remove the last element
               }
               deque.AddFirst(item);         // 将新元素添加到队列头部
                                             // Add the new element to the beginning of the deque
          }

          /// <summary>
          /// <para>在队列尾部添加元素。如果队列已满，则移除头部的元素。</para>
          /// <para>
          /// Adds an item to the end of the queue.
          /// If the queue is full, removes the item at the head before adding the new item.
          /// </para>
          /// </summary>
          /// <param name="item">
          /// <para>要添加到队列的元素。</para>
          /// <para>The element to add to the deque.</para>
          /// </param>
          public void AddLast(T item)
          {
               if (deque.Count >= capacity)  // 如果队列已满
                                             // If the deque is full
               {
                    deque.RemoveFirst();          // 移除头部的元素
                                                  // Remove the first element
               }
               deque.AddLast(item);          // 将新元素添加到队列尾部
                                             // Add the new element to the end of the deque
          }

          /// <summary>
          /// <para> 返回链表中元素的数量。 </para>
          /// <para> Return the number of elements in the linked list. </para>
          /// </summary>
          public int Count => deque.Count;

          /// <summary>
          /// <para>读取队列尾部的元素。</para>
          /// <para>Retrieve the element at the front of the queue.</para>
          /// </summary>
          public T PeekFirst
          {
               get
               {
                    if (Count > 0)                      // 如果当前队列大于 0
                         return deque.First.Value;          // 返回首位元素
                    else                               // 反之
                         return default;                    // 返回对应数据的默认数值
               }
          }

          /// <summary>
          /// <para>读取队列尾部的元素。</para>
          /// <para>Retrieve the element at the end of the queue.</para>
          /// </summary>
          public T PeekLast
          {
               get
               {
                    if (Count > 0)                      // 如果当前队列大于 0
                         return deque.Last.Value;           // 返回尾部元素
                    else                               // 反之
                         return default;                    // 返回对应数据的默认数值
               }
          }

          /// <summary>
          /// <para>读取特定位置元素。</para>
          /// <para>Retrieve element at specific position.</para>
          /// </summary>
          /// <param name="index">
          /// <para>输入要读取的位置。</para>
          /// <para>Enter the position to read.</para>
          /// </param>
          /// <returns>
          /// <para>返回该位置元素。</para>
          /// <para>Return the element at that position.</para>
          /// </returns>
          public T PeekAt(int index)
          {
               if (index > Count)       // 如果特定位置超过队列容量
               {
                    #if UNITY_EDITOR
                    Debug.WriteLine("警告：目标位置超出限制。");
                    Debug.WriteLine("Warning: Target position exceeds the limit.");
                    #endif
                    return default;          // 返回对应数据默认值
               }

               var item = deque.ElementAt(index); // 缓存检索到的元素
               if (item == null)                  // 如果为空
                    return default;                    // 返回对应数据默认值
               else                               // 反之
                    return item;                       // 返回对应数据
          }

          /// <summary>
          /// <para>清空所有元素</para>
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
          /// <para> 实现IEnumerable<T>接口 </para>
          /// <para> Implement the IEnumerable<T> interface. </para>
          /// </summary>
          public IEnumerator<T> GetEnumerator()
          {
               return deque.GetEnumerator();
          }
     }
}
