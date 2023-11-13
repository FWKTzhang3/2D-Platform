using UnityEngine;

[CreateAssetMenu(menuName = ("Data/CharacterStats/attackStats"), fileName = ("AttackStats_"))]
public class AttackStats : ScriptableObject
{
     [Header("�����ж���Χ���")]
     public Vector2[] hurtBoxOffset;
     public Vector2[] hurtBoxSize;

     [Header("�˺����")]
     public int[] damage;

     [Header("�������")]
     public float[] knockbackForceX;
     public float[] knockbackForceY;
     public float[] knockbackHardTime;

     [Header("��֡���")]
     [Tooltip("��֡����ʱ��")] public float[] hitStopTime;
     [Tooltip("��֡�ָ��ٶ�")]public float[] hitStopRecoveSpeed;
}
