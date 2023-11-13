using UnityEngine;

/// <summary>
/// ��ҳ���
/// </summary>
public class PlayerConstants : MonoBehaviour
{
     [Header("�ƶ����")]
     [Tooltip("Ŀ���ƶ��ٶ�")] public float moveSpeed;
     [Tooltip("�ƶ�����")] public float moveAcceration;
     [Tooltip("�ƶ��ٶ�˥��")] public float moveDeceleration;
     [Tooltip("�����ƶ��ٶ�˥��")] public float airMoveDeceleration;
     [Tooltip("����ʱ��")] public float coyoteTime;

     [Header("��Ծ���")]
     [Tooltip("��Ծ����")] public float jumpForce;
     [Tooltip("����˥��")] public float jumpForceDcelerate;
     [Tooltip("��Ծ����")] public float jumpDistance;

     [Header("�������")]
     [Tooltip("�����ٶ�����")] public AnimationCurve fallSpeedCurve;
     [Tooltip("���Ӳֱʱ��")] public float hardTime;
     [Tooltip("���Ӳֱʱ����ֵ")] public float minHardTimeThreshold;

     [Header("�������")]
     [Tooltip("�ƶ��ٶ��ƶ�")] public float moveAttackSpeed;
     [Tooltip("���й������ʱ��")] public float airAttackTime;

     [Header("�ܻ����")]
     [Tooltip("������")] public AnimationCurve shakeCurve;

     [Header("�������")]
     [Tooltip("�����������ٶ�")] public float attackAnimationSpeedAcceration;

     [Header("�������")]
     [Tooltip("����˥��")] public float knockbackForceDcelerate;
}
