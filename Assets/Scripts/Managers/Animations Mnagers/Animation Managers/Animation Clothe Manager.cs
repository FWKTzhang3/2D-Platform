using UnityEngine;

public class AnimationClotheManager : MonoBehaviour
{
     private SpriteRenderer m_SpriteRenderer;     // ������Ⱦ��
     private Animator m_Animator;                 // ����������

     private void Awake()
     {
          m_SpriteRenderer = GetComponent<SpriteRenderer>();     // ��ȡ��ǰ����ľ�����Ⱦ��
          m_Animator = GetComponent<Animator>();                 // ��ȡ��ǰ����Ķ���������
     }

     /// <summary>
     /// ������Ⱦ����ɫ
     /// </summary>
     /// <param name="R"> �� </param>
     /// <param name="G"> �� </param>
     /// <param name="B"> �� </param>
     /// <param name="A"> ͸ </param>
     public void SetColor(int R, int G, int B, int A)
     {
          m_SpriteRenderer.color = new Color(R, G, B, A);
     }
}
