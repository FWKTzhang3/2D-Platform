using System;
using UnityEngine;

/// <summary>
/// �����ϵͳ
/// </summary>
/// <remarks>��ҶԵ���ǽ��ȸ��ּ��</remarks>
public class DetectionSystem : MonoBehaviour
{
     [Header("����뾶")]
     [SerializeField] private float _detectionRadius;      // ���뾶

     [Header("��������")]
     [SerializeField] private Vector2 _bottomOffset;       // ���¼������
     [SerializeField] private Vector2 _faceOffset;         // �泯����������
     [SerializeField] private Vector2 _edgeOffset;         // ��Ե�������

     [Header("���Ŀ��")]
     [SerializeField] private LayerMask _groundLayer;           // �������ڵĲ㼶
     [SerializeField] private LayerMask _oneWayPlatformLayer;   // ����ƽ̨���ڵĲ㼶

     private Collider2D[] _colliders = new Collider2D[1];       // �洢���Ǽ�⵽����ײ��[��������]

     [Header("���״̬")]
     private bool _isGround;                 // �Ƿ��ڽӴ�����Ĳ���ֵ
     private bool _isGroundEdge;             // �Ƿ��ڽӴ�����Ĳ���ֵ
     public bool isOneWayPlatform;           // �Ƿ��ڽӴ�����ƽ̨�Ĳ���ֵ
     private bool _isOneWayPlatformEdge;     // �Ƿ��ڽӴ�����ƽ̨�Ĳ���ֵ
     public bool isTouchWall;                // �Ƿ��ڽӴ�ǽ��Ĳ���ֵ

     public bool isAir => !_isGround && !isOneWayPlatform; // ����Ƿ��ڿ���

     private void Update()
     {
          CheckGround();
          CheckWall();
          CheckEdge();
     }

#if UNITY_EDITOR
     private void OnDrawGizmosSelected()
     {
          //��������
          Vector2 SPos = transform.position;                          // �������

          Vector2 Lpos = _faceOffset;                                  // ��ߵ�����
          Vector2 Rpos = new Vector2(-_faceOffset.x, _faceOffset.y);    // �ұߵ�����

          Vector2 Bpos = _bottomOffset;                                // �ײ�������
          Vector2 BLpos = _edgeOffset;                                 // �ײ���ߵ�����
          Vector2 BRpos = new Vector2(-_edgeOffset.x, _edgeOffset.y);   // �ײ��ұߵ�����

          Gizmos.color = Color.green;                                 // ��������ɫ����ɫ��

          // �ڳ�����ͼ�л��Ƹ����ߣ����꣬�뾶����
          Gizmos.DrawWireSphere(SPos + Lpos, _detectionRadius);        // ���
          Gizmos.DrawWireSphere(SPos + Rpos, _detectionRadius);        // �ұ�

          Gizmos.DrawWireSphere(SPos + Bpos, _detectionRadius);        // �ŵ�
          Gizmos.DrawWireSphere(SPos + BLpos, _detectionRadius);       // �ŵ����
          Gizmos.DrawWireSphere(SPos + BRpos, _detectionRadius);       // �ŵ��ұ�
     }
#endif

     /// <summary>
     /// ����Ƿ�վ�ڵ�����
     /// </summary>
     private void CheckGround()
     {
          Vector2 detectionPos = (Vector2)transform.position + _bottomOffset;                       // ���������

          _isGround = CheckCircle(detectionPos, _detectionRadius, _groundLayer);                    // ����Ƿ��ڵ�����
          isOneWayPlatform = CheckCircle(detectionPos, _detectionRadius, _oneWayPlatformLayer);     // ����Ƿ��ڵ���ƽ̨��
     }

     /// <summary>
     /// ����Ƿ�����ǽ��
     /// </summary>
     private void CheckWall()
     {
          Vector2 detectionPos = (Vector2)transform.position + new Vector2(enityFaceDir * _faceOffset.x, _faceOffset.y);     // ���������

          isTouchWall = CheckCircle(detectionPos, _detectionRadius, _groundLayer);                                           // ����Ƿ�����ǽ��
     }

     /// <summary>
     /// ��Ե���
     /// </summary>
     private void CheckEdge()
     {
          Vector2 detectionPos = (Vector2)transform.position + new Vector2(enityFaceDir * _edgeOffset.x, _edgeOffset.y);     // ���������

          _isGroundEdge = _isGround && !CheckCircle(detectionPos, _detectionRadius, _groundLayer);                           // ����Ƿ��ڵ����Ե
          _isOneWayPlatformEdge = isOneWayPlatform && !CheckCircle(detectionPos, _detectionRadius, _oneWayPlatformLayer);    // ����Ƿ��ڵ���ƽ̨��Ե
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
          int circleCount = Physics2D.OverlapCircleNonAlloc(detectionPos, detectionRadius, _colliders, layerMask); // ��ȡָ��λ����ָ�������ײ������
          return circleCount != 0;                                                                                // �����Ƿ��ཻ
     }

     /// <summary>
     /// ��ȡĿ�������泯����
     /// </summary>
     /// <returns>�泯����</returns>
     private int enityFaceDir => (int)transform.lossyScale.x;
}
