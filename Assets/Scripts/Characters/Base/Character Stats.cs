using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
     [Header("基本属性")]
     [Tooltip("最高血量")] public float maxHealthPoint;
     [Tooltip("当前血量")] public float currentHealthPoint;
     [Tooltip("最高魔力")] public float maxManaPoint;
     [Tooltip("当前魔力")] public float currentManaPoint;
     [Tooltip("硬直抗性")] public float hardResistance;

     [Header("无敌状态")]
     [Tooltip("无敌状态")] public bool isMuteki;
     [Tooltip("无敌时间")] public float mutekiTime;
     [Tooltip("当前无敌时间")] private float currentMutekiTime;

     [Header("事件触发")]
     [Tooltip("受伤事件")] public UnityEvent<Attacker> onTakeDamage;
     [Tooltip("死亡事件")] public UnityEvent<Attacker> onDeath;
     [Tooltip("震动事件")] public UnityEvent<Victim> onShake;

     private void Start()
     {
          currentHealthPoint = maxHealthPoint;
          currentManaPoint = maxManaPoint;
     }

     private void Update()
     {
          MutekiTimer();
     }

     /// <summary>
     /// 伤害获取器
     /// </summary>
     /// <param name="attaker"> 输入伤害 </param>
     public void TakeDamage(Attacker attaker)
     {
          if (isMuteki)                                // 如果当前为无敌状态
               return;                                      // 则返回
          if (currentHealthPoint - attaker.damage > 0) // 如果受伤后血量不低于 0
          {
               currentHealthPoint -= attaker.damage;        // 减少血量
               onTakeDamage?.Invoke(attaker);            // 向 onTakeDamage 传递 attaker 并触发所有事件
               TriggerMuteki();                             // 执行无敌
          }
          else                                         // 反之
          {
               currentHealthPoint = 0;                      // 直接等于 0
               onDeath?.Invoke(attaker);                    // 向 onDeath 传递 attaker 并触发所有事件
          }
     }

     /// <summary>
     /// 获取震动
     /// </summary>
     /// <param name="victim"> 接收 Victim 脚本的所有数据 </param>
     public void TakeShake(Victim victim)
     {
          if (isMuteki)                 // 如果当前为无敌状态
               return;                       // 则返回
          if (currentHealthPoint > 0)   // 如果当前生命值大于 0
          {
               onShake?.Invoke(victim);      // 向 onShake 传递 victim 并触发所有事件
          }
     }

     /// <summary>
     /// 无敌触发器
     /// </summary>
     private void TriggerMuteki()
     {
          if (!isMuteki)                          // 如果 isMuteki 为假
          {
               isMuteki = true;                        // 则赋值为真
               currentMutekiTime = mutekiTime;         // 让当前无敌时间等于目标无敌时间
          }
     }

     /// <summary>
     /// 无敌计时器
     /// </summary>
     private void MutekiTimer()
     {
          if (isMuteki)                                // 如果当前是无敌状态
          {
               currentMutekiTime -= Time.deltaTime;         // 则让 currentMutekiTime 递减（每次减少修正时间）
               if (currentMutekiTime <= 0)                  // 如果 当前无敌时间小于或等于 0
               {
                    isMuteki = false;                            // 取消无敌状态
               }
          }
     }
}
