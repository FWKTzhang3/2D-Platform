using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
     private Coroutine hitStopCoroutine;     // ����Э��

     private void OnEnable()
     {
          Attacker.HitStopEvent += HitStop;  // ע����� HitStopEvent �ķ�������ֵ �����ҽ��� HitStop ����
     }

     private void OnDestroy()
     {
          Attacker.HitStopEvent -= HitStop;  // ע��
     }

     /// <summary>
     /// ��֡����
     /// </summary>
     /// <param name="stopTime"> ��֡ʱ�� </param>
     /// <param name="recoverAcceleration"> ��֡ʱ�� </param>
     private void HitStop(float stopTime, float recoverAcceleration)
     {
          if (hitStopCoroutine != null)           // ����ǰЭ�̲�Ϊ��
               StopCoroutine(hitStopCoroutine);        // ֹͣЭ��
          hitStopCoroutine = StartCoroutine(HitStopCoroutine(stopTime, recoverAcceleration));   // Э�̣� ��������������յ�����ֵ�����һ���Э�̣�
     }

     /// <summary>
     /// ֥ʿ��֡�뻺֡Э��
     /// </summary>
     /// <param name="stutterTime"> ��֡ʱ�� </param>
     /// <param name="timeScaleAcceleration"> ��֡�ٶ� </param>
     private static IEnumerator HitStopCoroutine(float stutterTime, float timeScaleAcceleration)
     {
          Time.timeScale = 0;                                    // ��ͣʱ��
          yield return new WaitForSecondsRealtime(stutterTime);  // ���� timeScale Ӱ���Э���ӳ٣����ɸ��߼��ļ�ʱ
          if (timeScaleAcceleration > 0)     // �� timeScaleAcceleration ���� 0 ʱִ��ѭ��
          {
               do   // ��ִ��һ�Σ�Ȼ�����ж��Ƿ��ظ�
               {
                    Time.timeScale += Time.unscaledDeltaTime * timeScaleAcceleration;     // �õ�ǰʱ���ٶ�����һ��֡ʱ�䣨���� timeScale Ӱ�죩
                    yield return null;                                                    // �ӳ�һ֡��������Ƶ�ʲ���̫�ף�
               } while (Time.timeScale < 1); // �����ǰʱ���ٶ�С�� 1 ,���ظ�ѭ��
          }
          Time.timeScale = 1; // ��ԭʱ��
     }
}
