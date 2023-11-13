using UnityEngine;

public class HurtBoxController : MonoBehaviour
{
     [Header("���������")]
     public BoxCollider2D HurtBox;
     public Attacker attacker;

     [Header("��������")]
     public AttackStats attackStats;
     private int currentAttackCount;

     [Header("�ж���ΧԤ��")]
     [SerializeField] private bool showMeBox;
     [SerializeField] private int showAttackBox;

     private void OnDrawGizmos()
     {
          int boxCount = showAttackBox - 1;

          if (showMeBox && boxCount < attackStats.hurtBoxOffset.Length)
          {
               Gizmos.color = Color.green;
               Vector2 startPos = transform.position;
               Gizmos.DrawWireCube(startPos + attackStats.hurtBoxOffset[boxCount], attackStats.hurtBoxSize[boxCount]);
          }
     }

     /// <summary>
     /// ���� ����������
     /// </summary>
     private void EnabledHurtBox() => HurtBox.enabled = true;

     /// <summary>
     /// �ر� ����������
     /// </summary>
     private void DisableHurtBox() => HurtBox.enabled = false;

     /// <summary>
     /// ����������
     /// </summary>
     /// <param name="Number"> ��� </param>
     private void OutAttackNumber(int Number)
     {
          if (currentAttackCount != Number)            // ����ǰ��Ų�������������
          {
               currentAttackCount = Number;            // �õ�ǰ��ŵ����������
               AssAttackState(currentAttackCount);     // ִ�и�ֵ�������޸ĺ����ţ�
          }
     }

     /// <summary>
     /// ��ֵ
     /// </summary>
     /// <param name="Count"> ��� </param>
     private void AssAttackState(int Count)
     {
          int correctCount = Count - 1;

          // ����������ͳߴ�ĸ�ֵ
          HurtBox.offset = attackStats.hurtBoxOffset[correctCount];
          HurtBox.size = attackStats.hurtBoxSize[correctCount];

          // ��ֵ��ֵ
          attacker.damage = attackStats.damage[correctCount];                        // �����˺�

          attacker.knockbackForceX = attackStats.knockbackForceX[correctCount];      // ��������X
          attacker.knockbackForceY = attackStats.knockbackForceY[correctCount];      // ��������Y
          attacker.knockbackHardTime = attackStats.knockbackHardTime[correctCount];  // ����Ӳֱʱ��

          attacker.hitStopTime = attackStats.hitStopTime[correctCount];                   // ��֡ʱ��
          attacker.hitStopRecoveSpeed = attackStats.hitStopRecoveSpeed[correctCount];     // ��֡�ָ��ٶ�
     }
}
