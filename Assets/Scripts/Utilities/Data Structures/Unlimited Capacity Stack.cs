using System;
using System.Collections.Generic;

namespace DataStructures.UnlimitDatas
{
     public class UnlimitedDictionary<TKey, TValue>
     {
          private Dictionary<TKey, TValue> dictionary;

          public UnlimitedDictionary()
          {
               this.dictionary = new Dictionary<TKey, TValue>();
          }
     }
}
