using UnityEngine;

/// <summary>
/// ��������
/// </summary>
public class GroundDetector : MonoBehaviour
{
     [Header("���ĵ�����")]
     public GameObject entity;                    // Ҫ���ӵ�����

     [Header("����뾶")]
     [SerializeField] float detectionRadius;      // ���뾶

     [Header("��������")]
     [SerializeField] Vector2 bottomOffset;       // ���¼������
     [SerializeField] Vector2 faceOffset;         // �泯����������
     [SerializeField] Vector2 edgeOffset;         // ��Ե�������

     [Header("���Ŀ��")]
     [SerializeField] LayerMask groundLayer;           // �������ڵĲ㼶
     [SerializeField] LayerMask oneWayPlatformLayer;   // ����ƽ̨���ڵĲ㼶

     Collider2D[] colliders = new Collider2D[1];       // �洢���Ǽ�⵽����ײ��[��������]

     [Header("���״̬")]
     public bool isGround;                                  // �Ƿ��ڽӴ�����Ĳ���ֵ
     public bool isGroundEdge;                              // �Ƿ��ڽӴ�����Ĳ���ֵ
     public bool isOneWayPlatform;                          // �Ƿ��ڽӴ�����ƽ̨�Ĳ���ֵ
     public bool isOneWayPlatformEdge;                      // �Ƿ��ڽӴ�����ƽ̨�Ĳ���ֵ
     public bool isTouchWall;                               // �Ƿ��ڽӴ�ǽ��Ĳ���ֵ
     public bool isAir => !isGround && !isOneWayPlatform;   // �Ƿ��ڿ���
     public bool isEdge => isGroundEdge || isOneWayPlatformEdge;

     private void Update()
     {
          CheckGround();
          CheckWall();
          CheckEdge();
     }

     void OnDrawGizmosSelected()
     {
          //��������
          Vector2 SPos = transform.position;                          // �������

          Vector2 Lpos = faceOffset;                                  // ��ߵ�����
          Vector2 Rpos = new Vector2(-faceOffset.x, faceOffset.y);    // �ұߵ�����

          Vector2 Bpos = bottomOffset;                                // �ײ�������
          Vector2 BLpos = edgeOffset;                                 // �ײ���ߵ�����
          Vector2 BRpos = new Vector2(-edgeOffset.x, edgeOffset.y);   // �ײ��ұߵ�����

          Gizmos.color = Color.green;                                 // ��������ɫ����ɫ��

          // �ڳ�����ͼ�л��Ƹ����ߣ����꣬�뾶����
          Gizmos.DrawWireSphere(SPos + Lpos, detectionRadius);        // ���
          Gizmos.DrawWireSphere(SPos + Rpos, detectionRadius);        // �ұ�

          Gizmos.DrawWireSphere(SPos + Bpos, detectionRadius);        // �ŵ�
          Gizmos.DrawWireSphere(SPos + BLpos, detectionRadius);       // �ŵ����
          Gizmos.DrawWireSphere(SPos + BRpos, detectionRadius);       // �ŵ��ұ�
     }

     /// <summary>
     /// ����Ƿ�վ�ڵ�����
     /// </summary>
     private void CheckGround()
     {
          Vector2 detectionPos = (Vector2)transform.position + bottomOffset;                   // ���������

          isGround = CheckCircle(detectionPos, detectionRadius, groundLayer);                 // ����Ƿ��ڵ�����
          isOneWayPlatform = CheckCircle(detectionPos, detectionRadius, oneWayPlatformLayer); // ����Ƿ��ڵ���ƽ̨��
     }

     /// <summary>
     /// ����Ƿ�����ǽ��
     /// </summary>
     private void CheckWall()
     {
          Vector2 detectionPos = (Vector2)transform.position + new Vector2(enityFaceDir() * faceOffset.x, faceOffset.y);     // ���������

          isTouchWall = CheckCircle(detectionPos, detectionRadius, groundLayer);                                            // ����Ƿ�����ǽ��
     }

     /// <summary>
     /// ��Ե���
     /// </summary>
     private void CheckEdge()
     {
          Vector2 detectionPos = (Vector2)transform.position + new Vector2(enityFaceDir() * edgeOffset.x, edgeOffset.y);     // ���������

          isGroundEdge = isGround && !CheckCircle(detectionPos, detectionRadius, groundLayer);                              // ����Ƿ��ڵ����Ե
          isOneWayPlatformEdge = isOneWayPlatform && !CheckCircle(detectionPos, detectionRadius, oneWayPlatformLayer);      // ����Ƿ��ڵ���ƽ̨��Ե
     }

     /// <summary>
     /// ���ָ��λ���Ƿ���ָ�������ײ���ཻ
     /// </summary>
     /// <param name="detectionPos">���λ��</param>
     /// <param name="detectionRadius">���뾶</param>
     /// <param name="layerMask">ָ����</param>
     /// <returns>�Ƿ��ཻ</returns>
     private bool CheckCircle(Vector2 detectionPos, float detectionRadius, LayerMask layerMask)
     {
          int circleCount = Physics2D.OverlapCircleNonAlloc(detectionPos, detectionRadius, colliders, layerMask); // ��ȡָ��λ����ָ�������ײ������
          return circleCount != 0;                                                                                // �����Ƿ��ཻ
     }

     /// <summary>
     /// ��ȡĿ�������泯����
     /// </summary>
     /// <returns>�泯����</returns>
     private int enityFaceDir()
     {
          return (int)entity.transform.lossyScale.x;
     }
}
