using UnityEngine;

/// <summary>
/// ����ϵͳ
/// </summary>
/// <remarks> ����������������� </remarks>
public class ControllSystem : MonoBehaviour
{
     private Transform _transform; // ת�������

     private InputSystem _input;   // ������ϵͳ

     private void Awake()
     {
          _transform = transform.parent;          // ��ȡ�������ת�������
          _input = GetComponent<InputSystem>();   // ��ȡ������ϵͳ
     }

     private void OnEnable()
     {
          _input.OnJoyStickEvent += Flip;
     }

     private void OnDisable()
     {
          _input.OnJoyStickEvent -= Flip;
     }

     /// <summary>
     /// ����ת��ķ��������޸�Scale���ţ�
     /// </summary>
     /// <param name="inputDirection"> ������������� </param>
     private void Flip(Vector2 inputDirection)
     {
          int currentFaceDirection = Mathf.RoundToInt(inputDirection.x);   // ���浱ǰ������ҡ��X�᷽��ת��Ϊ����
          if (currentFaceDirection != 0 && _transform.lossyScale.x != currentFaceDirection)     // �����ǰ����Ϊ�� �� �����ڵ�ǰ����ֵ
               // �����¸�ֵ��ǰX��Scale����
               _transform.localScale = new Vector2(currentFaceDirection, _transform.lossyScale.y);
     }
}