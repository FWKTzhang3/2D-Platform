using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
     [Header("��������")]
     [Tooltip("���Ѫ��")] public float maxHealthPoint;
     [Tooltip("��ǰѪ��")] public float currentHealthPoint;
     [Tooltip("���ħ��")] public float maxManaPoint;
     [Tooltip("��ǰħ��")] public float currentManaPoint;
     [Tooltip("Ӳֱ����")] public float hardResistance;

     [Header("�޵�״̬")]
     [Tooltip("�޵�״̬")] public bool isMuteki;
     [Tooltip("�޵�ʱ��")] public float mutekiTime;
     [Tooltip("��ǰ�޵�ʱ��")] private float currentMutekiTime;

     [Header("�¼�����")]
     [Tooltip("�����¼�")] public UnityEvent<Attacker> onTakeDamage;
     [Tooltip("�����¼�")] public UnityEvent<Attacker> onDeath;
     [Tooltip("���¼�")] public UnityEvent<Victim> onShake;

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
     /// �˺���ȡ��
     /// </summary>
     /// <param name="attaker"> �����˺� </param>
     public void TakeDamage(Attacker attaker)
     {
          if (isMuteki)                                // �����ǰΪ�޵�״̬
               return;                                      // �򷵻�
          if (currentHealthPoint - attaker.damage > 0) // ������˺�Ѫ�������� 0
          {
               currentHealthPoint -= attaker.damage;        // ����Ѫ��
               onTakeDamage?.Invoke(attaker);            // �� onTakeDamage ���� attaker �����������¼�
               TriggerMuteki();                             // ִ���޵�
          }
          else                                         // ��֮
          {
               currentHealthPoint = 0;                      // ֱ�ӵ��� 0
               onDeath?.Invoke(attaker);                    // �� onDeath ���� attaker �����������¼�
          }
     }

     /// <summary>
     /// ��ȡ��
     /// </summary>
     /// <param name="victim"> ���� Victim �ű����������� </param>
     public void TakeShake(Victim victim)
     {
          if (isMuteki)                 // �����ǰΪ�޵�״̬
               return;                       // �򷵻�
          if (currentHealthPoint > 0)   // �����ǰ����ֵ���� 0
          {
               onShake?.Invoke(victim);      // �� onShake ���� victim �����������¼�
          }
     }

     /// <summary>
     /// �޵д�����
     /// </summary>
     private void TriggerMuteki()
     {
          if (!isMuteki)                          // ��� isMuteki Ϊ��
          {
               isMuteki = true;                        // ��ֵΪ��
               currentMutekiTime = mutekiTime;         // �õ�ǰ�޵�ʱ�����Ŀ���޵�ʱ��
          }
     }

     /// <summary>
     /// �޵м�ʱ��
     /// </summary>
     private void MutekiTimer()
     {
          if (isMuteki)                                // �����ǰ���޵�״̬
          {
               currentMutekiTime -= Time.deltaTime;         // ���� currentMutekiTime �ݼ���ÿ�μ�������ʱ�䣩
               if (currentMutekiTime <= 0)                  // ��� ��ǰ�޵�ʱ��С�ڻ���� 0
               {
                    isMuteki = false;                            // ȡ���޵�״̬
               }
          }
     }
}
