using System;
using UnityEngine;

/// <summary>
/// �������ӵĿ���
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class HurtBoxManager : MonoBehaviour
{
     private BoxCollider2D m_BoxCollider;    // ��ȡ��ǰ����Ĵ�����

     private void Awake()
     {
          m_BoxCollider = GetComponent<BoxCollider2D>();
     }

     private void OnEnable()
     {
          WeaponFrames.EnableHurtBox += EnabledHurtBox;
          WeaponFrames.DisableHurtBox += DisableHurtBox;
     }

     private void OnDisable()
     {
          WeaponFrames.EnableHurtBox -= EnabledHurtBox;
          WeaponFrames.DisableHurtBox -= DisableHurtBox;
     }

     /// <summary>
     /// ���� ����������
     /// </summary>
     public void EnabledHurtBox() => m_BoxCollider.enabled = true;

     /// <summary>
     /// �ر� ����������
     /// </summary>
     public void DisableHurtBox() => m_BoxCollider.enabled = false;

     /// <summary>
     /// ���� ������������ƫ������
     /// </summary>
     /// <param name="offset"> ���� </param>
     public void SetHurtBoxOffset(Vector2 offset) => m_BoxCollider.offset = offset;

     /// <summary>
     /// ���� �����������ĳߴ�
     /// </summary>
     /// <param name="size"> �ߴ� </param>
     public void SetHurtBoxSize(Vector2 size) => m_BoxCollider.size = size;
}
