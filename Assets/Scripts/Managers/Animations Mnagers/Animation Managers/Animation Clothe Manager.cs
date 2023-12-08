using UnityEngine;

public class AnimationClotheManager : MonoBehaviour
{
     private SpriteRenderer m_SpriteRenderer;     // 精灵渲染器
     private Animator m_Animator;                 // 动画控制器

     private void Awake()
     {
          m_SpriteRenderer = GetComponent<SpriteRenderer>();     // 获取当前物体的精灵渲染器
          m_Animator = GetComponent<Animator>();                 // 获取当前物体的动画控制器
     }

     /// <summary>
     /// 设置渲染器颜色
     /// </summary>
     /// <param name="R"> 红 </param>
     /// <param name="G"> 绿 </param>
     /// <param name="B"> 蓝 </param>
     /// <param name="A"> 透 </param>
     public void SetColor(int R, int G, int B, int A)
     {
          m_SpriteRenderer.color = new Color(R, G, B, A);
     }
}
