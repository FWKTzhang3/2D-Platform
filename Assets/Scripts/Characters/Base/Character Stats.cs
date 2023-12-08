using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
     [Header("基本属性")]
     [Tooltip("最高血量")] public float maxHealthPoint;
     [Tooltip("当前血量")] private float _currentHealthPoint;
     [Tooltip("最高魔力")] public float maxManaPoint;
     [Tooltip("当前魔力")] private float _currentManaPoint;
     [Tooltip("硬直抗性")] public float hardResistance;

     [Header("无敌状态")]
     [Tooltip("无敌状态")] private bool _isMuteki;
     [Tooltip("无敌时间")] public float mutekiTime;
     [Tooltip("当前无敌时间")] private float _currentMutekiTime;

     [Header("事件触发")]
     [Tooltip("受伤事件")] public UnityEvent<CharacterStateType,Attacker> onHurt;

     private void Start()
     {
          _currentHealthPoint = maxHealthPoint;
          _currentManaPoint = maxManaPoint;
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
          if (_isMuteki)                                         // 如果当前为无敌状态
               return;                                                // 则返回
          if (_currentHealthPoint - attaker.damage > 0)          // 如果受伤后血量不低于 0
          {
               _currentHealthPoint -= attaker.damage;                 // 减少血量
               onHurt?.Invoke(CharacterStateType.Hurt,attaker);       // 向 受伤传递 传递 受伤状态 和 attaker 并触发所有事件
               TriggerMuteki();                                       // 执行无敌
          }
          else                                                   // 反之
          {
               _currentHealthPoint = 0;                               // 直接归零
               onHurt?.Invoke(CharacterStateType.Death,attaker);      // 向 onHurt 传递 死亡状态 和 attaker 并触发所有事件
          }
     }

     /// <summary>
     /// 无敌触发器
     /// </summary>
     private void TriggerMuteki()
     {
          if (!_isMuteki)                          // 如果 isMuteki 为假
          {
               _isMuteki = true;                        // 则赋值为真
               _currentMutekiTime = mutekiTime;         // 让当前无敌时间等于目标无敌时间
          }
     }

     /// <summary>
     /// 无敌计时器
     /// </summary>
     private void MutekiTimer()
     {
          if (_isMuteki)                                // 如果当前是无敌状态
          {
               _currentMutekiTime -= Time.deltaTime;         // 则让 currentMutekiTime 递减（每次减少修正时间）
               if (_currentMutekiTime <= 0)                  // 如果 当前无敌时间小于或等于 0
               {
                    _isMuteki = false;                            // 取消无敌状态
               }
          }
     }
}
