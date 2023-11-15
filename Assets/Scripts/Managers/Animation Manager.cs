using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour
{
     [Header("震动曲线")]
     public AnimationCurve shakeCurve;

     public Transform panterTrans;
     public GameObject prefab;

     private Coroutine shakeCoroutine;  // 缓存协程
     private float shakeCurveMaxLength; // 极限时间
     private float currentShakeLength;  // 当前时间

     private void Start()
     {
          if (shakeCurve.length > 0)
          {
               shakeCurveMaxLength = shakeCurve.keys[shakeCurve.length - 1].time;         // 计算极限长度
          }
     }

     /// <summary>
     /// 动画震动
     /// </summary>
     /// <param name="shakeDirection"> 震动初始方向 </param>
     /// <param name="shakeStrength"> 震动力度 </param>
     /// <param name="shakeSpeed"> 震动速度 </param>
     public void AnimationShake(int shakeDirection, float shakeStrength)
     {
          if (shakeCoroutine != null)             // 如果当前缓存的协程不为空
               StopCoroutine(shakeCoroutine);          // 停止一次协程（保证唯一性）
          shakeCoroutine = StartCoroutine(AnimationShakeCoroutine(shakeDirection, shakeStrength));  // 协程！启动！（并且添加到缓存里复用）
     }

     /// <summary>
     /// 动画震动协程
     /// </summary>
     /// <param name="shakeDirection"> 震动初始方向 </param>
     /// <param name="shakeStrength"> 震动力度 </param>
     /// <param name="shakeSpeed"> 震动速度 </param>
     IEnumerator AnimationShakeCoroutine(int shakeDirection, float shakeStrength)
     {
          // 对当前脚本所在物体的相对坐标进行循环赋值，当前时间直到最大值
          for (currentShakeLength = 0; currentShakeLength <= shakeCurveMaxLength; currentShakeLength += Time.deltaTime * 10f)
          {
               float shakeX = shakeDirection * (shakeCurve.Evaluate(currentShakeLength) * shakeStrength);     // 缓存坐标
               transform.localPosition = new Vector2(shakeX, 0);                                              // 赋值坐标
               yield return null;                                                                             // 协程必备延迟一帧
          }

          transform.localPosition = new Vector2(0, 0);      // 最后还原坐标
     }

     public void Fire()
     {
          Vector2 position = new Vector2 (panterTrans.transform.position.x, panterTrans.transform.position.y + 1);
          PoolManager.Release(prefab, position, panterTrans.transform.lossyScale);
     }
}