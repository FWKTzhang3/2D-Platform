using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
     [Header("��������")]
     [Tooltip("���Ѫ��")] public float maxHealthPoint;
     [Tooltip("��ǰѪ��")] private float _currentHealthPoint;
     [Tooltip("���ħ��")] public float maxManaPoint;
     [Tooltip("��ǰħ��")] private float _currentManaPoint;
     [Tooltip("Ӳֱ����")] public float hardResistance;

     [Header("�޵�״̬")]
     [Tooltip("�޵�״̬")] private bool _isMuteki;
     [Tooltip("�޵�ʱ��")] public float mutekiTime;
     [Tooltip("��ǰ�޵�ʱ��")] private float _currentMutekiTime;

     [Header("�¼�����")]
     [Tooltip("�����¼�")] public UnityEvent<CharacterStateType,Attacker> onHurt;

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
     /// �˺���ȡ��
     /// </summary>
     /// <param name="attaker"> �����˺� </param>
     public void TakeDamage(Attacker attaker)
     {
          if (_isMuteki)                                         // �����ǰΪ�޵�״̬
               return;                                                // �򷵻�
          if (_currentHealthPoint - attaker.damage > 0)          // ������˺�Ѫ�������� 0
          {
               _currentHealthPoint -= attaker.damage;                 // ����Ѫ��
               onHurt?.Invoke(CharacterStateType.Hurt,attaker);       // �� ���˴��� ���� ����״̬ �� attaker �����������¼�
               TriggerMuteki();                                       // ִ���޵�
          }
          else                                                   // ��֮
          {
               _currentHealthPoint = 0;                               // ֱ�ӹ���
               onHurt?.Invoke(CharacterStateType.Death,attaker);      // �� onHurt ���� ����״̬ �� attaker �����������¼�
          }
     }

     /// <summary>
     /// �޵д�����
     /// </summary>
     private void TriggerMuteki()
     {
          if (!_isMuteki)                          // ��� isMuteki Ϊ��
          {
               _isMuteki = true;                        // ��ֵΪ��
               _currentMutekiTime = mutekiTime;         // �õ�ǰ�޵�ʱ�����Ŀ���޵�ʱ��
          }
     }

     /// <summary>
     /// �޵м�ʱ��
     /// </summary>
     private void MutekiTimer()
     {
          if (_isMuteki)                                // �����ǰ���޵�״̬
          {
               _currentMutekiTime -= Time.deltaTime;         // ���� currentMutekiTime �ݼ���ÿ�μ�������ʱ�䣩
               if (_currentMutekiTime <= 0)                  // ��� ��ǰ�޵�ʱ��С�ڻ���� 0
               {
                    _isMuteki = false;                            // ȡ���޵�״̬
               }
          }
     }
}
