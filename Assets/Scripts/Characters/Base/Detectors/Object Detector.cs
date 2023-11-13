using UnityEditor;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
     [Header("���ĵ�����")]
     public GameObject entity;

     [Header("�������")]
     [SerializeField,Tooltip("�����������")] private Vector2 attackDetectionStarPos;
     [SerializeField,Tooltip("�������ӳߴ�")] private Vector2 attackBoxSize;
     [SerializeField,Tooltip("׷���������")] private Vector2 chaseDetectionStarPos;
     [SerializeField,Tooltip("׷�����ӳߴ�")] private Vector2 chaseBoxSize;

     [Header("���Ŀ��")]
     [SerializeField] private LayerMask playerLayer;

     // ��������飨Ŀǰ����Ϊֻ����һ�����ʾ������Լ�ڴ棩
     public RaycastHit2D[] rayCastHits = new RaycastHit2D[1]; // ����
     private Collider2D[] colliders = new Collider2D[1];       // ��ײ

     private Vector2 entityFaceScale => entity.transform.lossyScale; // Ŀ�������泯����
     private Vector2 centerPos => entity.transform.position;     // ���ĵ�����

     [Header("���״̬")]
     public bool isAttackPlayer;   // ����             
     public bool isChasePlayer;    // ׷��          

     private void OnDrawGizmos()
     {
          ChaseBoxDraw();
          AttackBoxDraw();
     }

     private void Update()
     {
          CheckChasePlayer();
          CheckAttackPlayer();
     }

     /// <summary>
     /// ����׷����ⷶΧ
     /// </summary>
     private void ChaseBoxDraw()
     {
          Vector2 starPos = FaceBoxStarPos(chaseDetectionStarPos, chaseBoxSize, entityFaceScale);
          Gizmos.color = Color.red;
          Gizmos.DrawWireCube(centerPos + starPos, chaseBoxSize);
     }

     /// <summary>
     /// ���ƹ�����ⷶΧ
     /// </summary>
     private void AttackBoxDraw()
     {
          Vector2 starPos = FaceBoxStarPos(attackDetectionStarPos, attackBoxSize, entityFaceScale);
          Gizmos.color = Color.yellow;
          Gizmos.DrawWireCube(centerPos + starPos, attackBoxSize);
     }

     /// <summary>
     /// ׷����ⷶΧ
     /// </summary>
     private void CheckChasePlayer()
     {                                                                                     
          Vector2 starPos = FaceBoxStarPos(chaseDetectionStarPos, chaseBoxSize, entityFaceScale);  // ������ʼ����
          isChasePlayer = CheckBox(centerPos + starPos, chaseBoxSize, playerLayer);
     }

     /// <summary>
     /// ������ⷶΧ
     /// </summary>
     private void CheckAttackPlayer()
     {
          Vector2 starPos = FaceBoxStarPos(attackDetectionStarPos, attackBoxSize, entityFaceScale);          // ������ʼ����
          isAttackPlayer = CheckCastBox(centerPos + starPos, attackBoxSize, entityFaceScale, playerLayer);
     }

     /// <summary>
     /// �泯���������ʼ����
     /// </summary>
     /// <param name="detectionStarPos"> �����ʼ���� </param>
     /// <param name="boxSize">          ���ӳߴ�    </param>
     /// <param name="Scale">            �泯����    </param>
     /// <returns> һ����ά���꣨��ʼ���꣩ </returns>
     private Vector2 FaceBoxStarPos(Vector2 detectionStarPos, Vector2 boxSize, Vector2 Scale)
     {
          // ��������㣬��� Y ��Ϊ���ľ�����
          Vector2 starPos = new Vector2(detectionStarPos.x + boxSize.x / 2 * Scale.x, detectionStarPos.y);
          return starPos;
     }

     /// <summary>
     /// ���Ӽ��
     /// </summary>
     /// <param name="boxPos">     ������ʼ����    </param>
     /// <param name="boxSize">    ���ӳߴ�      </param>
     /// <param name="layer">      ���㼶      </param>
     /// <returns> һ�� boolֵ�� 0 Ϊ flase , 1 Ϊ true �� </returns>
     private bool CheckBox(Vector2 boxPos, Vector2 boxSize, LayerMask layer)
     {
          // ��ת�Ƕ�Ҳ�ò�����Ŀǰ OVO ��
          int boxCount = Physics2D.OverlapBoxNonAlloc(boxPos, boxSize, 0, colliders, layer);
          return boxCount != 0;
     }

     /// <summary>
     /// ���ߺ��Ӽ��
     /// </summary>
     /// <param name="boxPos">     ������ʼ����    </param>
     /// <param name="boxSize">    ���ӳߴ�      </param>
     /// <param name="rayDir">     ���߷���      </param>
     /// <param name="layer">      ���㼶      </param>
     /// <returns> һ�� boolֵ�� 0 Ϊ flase , 1 Ϊ true �� </returns>
     private bool CheckCastBox(Vector2 boxPos, Vector2 boxSize, Vector2 rayDir, LayerMask layer)
     {
          // �õĲ��࣬�Ƕ�....���ܻ��õ�������ͻ��....û��Ҫ
          int castBoxCount = Physics2D.BoxCastNonAlloc(boxPos, boxSize, 0f, rayDir, rayCastHits, 0, layer);
          return castBoxCount != 0;
     }
}
