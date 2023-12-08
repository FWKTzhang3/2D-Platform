using System.Collections;
using UnityEngine;

/// <summary>
/// ȫ����������
/// </summary>
public class AnimationsAllManager : MonoBehaviour
{
     private AnimationBodyManager m_AnimationBodyManager;
     private AnimationClotheManager m_AnimationClotheManager;
     private AnimationWeaponManager m_AnimationWeaponManager;

     private Coroutine _shakeCoroutine;  // ����Э��

     [Header("������")]
     [SerializeField] private AnimationCurve _shakeCurve;
     [Header("��Ƶ��")]
     [SerializeField] private float _shakeFrequency;

     private float _shakeCurveMaxLength;     // ���޳���
     private float _currentShakeLength;      // ��ǰ����

     private void Awake()
     {
          m_AnimationBodyManager = GetComponentInChildren<AnimationBodyManager>();
          m_AnimationWeaponManager = GetComponentInChildren<AnimationWeaponManager>();
     }

     private void Start()
     {
          if (_shakeCurve.length > 0)
          {
               _shakeCurveMaxLength = _shakeCurve.keys[_shakeCurve.length - 1].time; // ���㼫�޳���
          }
     }

     public void SetAllColor(int R, int G, int B, int A)
     {
          m_AnimationBodyManager.SetColor(R, G, B, A);
          m_AnimationWeaponManager.SetColor(R, G, B, A);
     }

     public void SetAllAnimatiorValue(AnimatorState animatorState)
     {
          SetAllAnimatorFloats(animatorState);
          SetAllAnimatorBools(animatorState);
     }

     private void SetAllAnimatorFloats(AnimatorState animatorState)
     {
          m_AnimationBodyManager.SetAnimatorFloats(animatorState);
     }

     private void SetAllAnimatorBools(AnimatorState animatorState)
     {
          m_AnimationBodyManager.SetAnimatorBools(animatorState);
          m_AnimationWeaponManager.SetAnimatorBools(animatorState);
     }

     /// <summary>
     /// ������
     /// </summary>
     /// <param name="knockbackDirection"> �𶯳�ʼ���� </param>
     /// <param name="shakeStrength"> ������ </param>
     public void AnimationShake(ShakeVlues shakeVlues)
     {
          if (_shakeCoroutine != null)            // �����ǰ�����Э�̲�Ϊ��
               StopCoroutine(_shakeCoroutine);         // ֹͣһ��Э�̣���֤Ψһ�ԣ�
          _shakeCoroutine = StartCoroutine(AnimationShakeCoroutine(shakeVlues));  // Э�̣���������������ӵ������︴�ã�
     }

     /// <summary>
     /// ������Э��
     /// </summary>
     /// <param name="shakeDirection"> �𶯳�ʼ���� </param>
     /// <param name="shakeStrength"> ������ </param>
     private IEnumerator AnimationShakeCoroutine(ShakeVlues shakeVlues)
     {
          // �Ե�ǰ�ű��������������������ѭ����ֵ����ǰʱ��ֱ�����ֵ
          for (_currentShakeLength = 0; _currentShakeLength <= _shakeCurveMaxLength; _currentShakeLength += Time.deltaTime * _shakeFrequency)
          {
               float shakeCurveValue = _shakeCurve.Evaluate(_currentShakeLength);                             // ��������ֵ
               float shake = shakeVlues.knockbackDirectionX * shakeCurveValue * shakeVlues.shakeStrength;     // ��������
               transform.localPosition = new Vector2(shake, transform.localPosition.y);                       // ��ֵ����
               yield return null;                                                                             // Э�̱ر��ӳ�һ֡
          }

          transform.localPosition = new Vector2(0, transform.localPosition.y);                                // ���ԭ����
     }
}
