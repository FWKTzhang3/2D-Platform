using System;
using UnityEngine;

/// <summary>
/// 攻击盒子的控制
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class HurtBoxManager : MonoBehaviour
{
     private BoxCollider2D m_BoxCollider;    // 获取当前物体的触发器

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
     /// 启动 攻击触发器
     /// </summary>
     public void EnabledHurtBox() => m_BoxCollider.enabled = true;

     /// <summary>
     /// 关闭 攻击触发器
     /// </summary>
     public void DisableHurtBox() => m_BoxCollider.enabled = false;

     /// <summary>
     /// 设置 攻击触发器的偏移坐标
     /// </summary>
     /// <param name="offset"> 坐标 </param>
     public void SetHurtBoxOffset(Vector2 offset) => m_BoxCollider.offset = offset;

     /// <summary>
     /// 设置 攻击触发器的尺寸
     /// </summary>
     /// <param name="size"> 尺寸 </param>
     public void SetHurtBoxSize(Vector2 size) => m_BoxCollider.size = size;
}
