using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
     private Coroutine hitStopCoroutine;     // 缓存协程

     private void OnEnable()
     {
          Attacker.HitStopEvent += HitStop;  // 注册接收 HitStopEvent 的方法和数值 ，并且交给 HitStop 运行
     }

     private void OnDestroy()
     {
          Attacker.HitStopEvent -= HitStop;  // 注销
     }

     /// <summary>
     /// 顿帧方法
     /// </summary>
     /// <param name="stopTime"> 顿帧时间 </param>
     /// <param name="recoverAcceleration"> 缓帧时间 </param>
     private void HitStop(float stopTime, float recoverAcceleration)
     {
          if (hitStopCoroutine != null)           // 若当前协程不为空
               StopCoroutine(hitStopCoroutine);        // 停止协程
          hitStopCoroutine = StartCoroutine(HitStopCoroutine(stopTime, recoverAcceleration));   // 协程！ 启动！（传入接收到的数值，并且缓存协程）
     }

     /// <summary>
     /// 芝士顿帧与缓帧协程
     /// </summary>
     /// <param name="stutterTime"> 顿帧时间 </param>
     /// <param name="timeScaleAcceleration"> 缓帧速度 </param>
     private static IEnumerator HitStopCoroutine(float stutterTime, float timeScaleAcceleration)
     {
          Time.timeScale = 0;                                    // 暂停时间
          yield return new WaitForSecondsRealtime(stutterTime);  // 不受 timeScale 影响的协程延迟，理解成更高级的计时
          if (timeScaleAcceleration > 0)     // 当 timeScaleAcceleration 大于 0 时执行循环
          {
               do   // 先执行一次，然后再判断是否重复
               {
                    Time.timeScale += Time.unscaledDeltaTime * timeScaleAcceleration;     // 让当前时间速度增加一个帧时间（不受 timeScale 影响）
                    yield return null;                                                    // 延迟一帧（让运行频率不会太凶）
               } while (Time.timeScale < 1); // 如果当前时间速度小于 1 ,则重复循环
          }
          Time.timeScale = 1; // 还原时间
     }
}
