using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour
{
     [Header("������")]
     public AnimationCurve shakeCurve;

     public Transform panterTrans;
     public GameObject prefab;

     private Coroutine shakeCoroutine;  // ����Э��
     private float shakeCurveMaxLength; // ����ʱ��
     private float currentShakeLength;  // ��ǰʱ��

     private void Start()
     {
          if (shakeCurve.length > 0)
          {
               shakeCurveMaxLength = shakeCurve.keys[shakeCurve.length - 1].time;         // ���㼫�޳���
          }
     }

     /// <summary>
     /// ������
     /// </summary>
     /// <param name="shakeDirection"> �𶯳�ʼ���� </param>
     /// <param name="shakeStrength"> ������ </param>
     /// <param name="shakeSpeed"> ���ٶ� </param>
     public void AnimationShake(int shakeDirection, float shakeStrength)
     {
          if (shakeCoroutine != null)             // �����ǰ�����Э�̲�Ϊ��
               StopCoroutine(shakeCoroutine);          // ֹͣһ��Э�̣���֤Ψһ�ԣ�
          shakeCoroutine = StartCoroutine(AnimationShakeCoroutine(shakeDirection, shakeStrength));  // Э�̣���������������ӵ������︴�ã�
     }

     /// <summary>
     /// ������Э��
     /// </summary>
     /// <param name="shakeDirection"> �𶯳�ʼ���� </param>
     /// <param name="shakeStrength"> ������ </param>
     /// <param name="shakeSpeed"> ���ٶ� </param>
     IEnumerator AnimationShakeCoroutine(int shakeDirection, float shakeStrength)
     {
          // �Ե�ǰ�ű��������������������ѭ����ֵ����ǰʱ��ֱ�����ֵ
          for (currentShakeLength = 0; currentShakeLength <= shakeCurveMaxLength; currentShakeLength += Time.deltaTime * 10f)
          {
               float shakeX = shakeDirection * (shakeCurve.Evaluate(currentShakeLength) * shakeStrength);     // ��������
               transform.localPosition = new Vector2(shakeX, 0);                                              // ��ֵ����
               yield return null;                                                                             // Э�̱ر��ӳ�һ֡
          }

          transform.localPosition = new Vector2(0, 0);      // ���ԭ����
     }

     public void Fire()
     {
          Vector2 position = new Vector2 (panterTrans.transform.position.x, panterTrans.transform.position.y + 1);
          PoolManager.Release(prefab, position, panterTrans.transform.lossyScale);
     }
}