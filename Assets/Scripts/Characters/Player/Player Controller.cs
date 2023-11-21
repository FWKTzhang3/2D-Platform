using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// ��ҿ��ƽű�
/// </summary>
/// <remarks> ��ҿ��ƽű���������ɶ���ƶ�������Ծ����ɶ�Ķ�������ͳһ����ʹ�� </remarks>
public class PlayerController : MonoBehaviour
{
     // ���� 
     private Rigidbody2D rigidBody;
     private PlayerInput input;
     private PlayerConstants constants;
     private AnimationManager animationManager;
     private CapsuleCollider2D col;

     private Coroutine changeLayerCoroutine;    // ����Э��

     public int faceDir => (int)transform.lossyScale.x;          // �泯����
     public float moveSpeed => Mathf.Abs(rigidBody.velocity.x);  // ��ǰ�ƶ��ٶȣ�ȡ����ֵ��
     public int currentLayer => gameObject.layer;                // ��ǰ�㼶

     // ״̬����
     public bool canAirJump   { get; set; }  // ������
     public bool canAirAttack { get; set; }  // ���й���
     public bool isCrossing   { get; set; }  // ��Խ״̬
     public bool isHurt       { get; set; }  // ����
     public bool isDeath      { get; set; }  // ����
     public bool isHitEnemy   { get; set; }  // 

     
     public int knockbackDirX           { get; set; }  // �ܻ�����X
     public int knockbackDirY           { get; set; }  // �ܻ�����Y
     public float knockbackForceX       { get; set; }  // ��������X
     public float knockbackForceY       { get; set; }  // ��������Y
     public float knockbackHardTime     { get; set; }  // ����Ӳֱʱ��

     private void Awake()
     {
          // ��ȡ
          rigidBody = GetComponent<Rigidbody2D>();
          input = GetComponent<PlayerInput>();
          col = GetComponent<CapsuleCollider2D>();
          constants = GetComponent<PlayerConstants>();
          animationManager = GetComponentInChildren<AnimationManager>();
     }

     private void Start()
     {
          input.EnableGameplayInputs();
          //playerInput.SetMouseState(CursorLockMode.Locked);
          InitialState();
     }

     /// <summary>
     /// ��ʼ״̬
     /// </summary>
     private void InitialState()
     {
          canAirAttack = true; 
          canAirJump = true;
     }

     #region �ƶ�������

     /// <summary>
     /// ����ƶ�
     /// </summary>
     /// <param name="speed"> ���ո����� </param>
     public void Move(float speed)
     {
          if (input.move)     // �����Ұ������ƶ���ť 
          {
               int faceDir = (input.axesX > 0) ? 1 : -1;         // ��ֵ��ȷ����ֻ��1��-1��
               transform.localScale = new Vector2(faceDir, 1f);  // ����ҵ�ת�����ű�������Ϊ playerInput.axisX ���Ƶķ���1 Ϊ������-1 Ϊ������
          }

          SetVelocityX(speed * input.axesX); // ���� SetVelocityX ����������ҵ�ˮƽ�ٶȣ��ٶ�ֵΪ speed ���� playerInput.axisX ��ֵ��Ҳ���Ǹ����������ķ�����ٶ��������ٶ�ֵ��
     }

     /// <summary>
     /// �������
     /// </summary>
     /// <param name="stateStartTime"> ״̬��ʼ�¼� </param>
     public void Fall(float stateDuration)
     {
          float currentFallSpeed = constants.fallSpeedCurve.Evaluate(stateDuration);  // ����״̬ʱ��������ٶ����߻�ȡ��ǰ�����ٶ�
          SetVelocityY(currentFallSpeed);
     }

     #endregion

     #region ����������

     /// <summary>
     /// ��ȡ�ܻ�����
     /// </summary>
     /// <param name="attacker"> ����һ�� Attacker </param>
     public void OnHurt(Attacker attacker)
     {
          GetAttacker(attacker);
          isHurt = true;
     }

     /// <summary>
     /// ��������
     /// </summary>
     /// <param name="attacker"> ����һ�� Attacker </param>
     public void OnDeath(Attacker attacker)
     {
          GetAttacker(attacker);
          isDeath = true;
          input.DisableGameplayInputs();
     }

     /// <summary>
     /// ������
     /// </summary>
     /// <param name="victim">���� Victim ������</param>
     public void OnShake(Victim victim)
     {
          animationManager.AnimationShake(victim.hitDirection, victim.shakeStrength);
     }

     /// <summary>
     /// ��ȡ�ܻ�����
     /// </summary>
     /// <param name="attacker"></param>
     private void GetAttacker(Attacker attacker)
     {
          // �����ȡ������
          Vector2 currentKnockbackDirX = new Vector2(transform.position.x - attacker.transform.position.x, 0).normalized;
          Vector2 currentKnockbackDirY = new Vector2(0, transform.position.y - attacker.transform.position.y).normalized;

          knockbackDirX = (int)currentKnockbackDirX.x;      // ��ȡX
          knockbackDirY = (int)currentKnockbackDirY.y;      // ��ȡY

          // ����ȡ����ֵ��ֵ����Ӧ����
          knockbackForceX = attacker.knockbackForceX;
          knockbackForceY = attacker.knockbackForceY;
          knockbackHardTime = attacker.knockbackHardTime;
     }

     #endregion

     #region ���ø����ٶ�

     /// <summary>
     /// ���ø����ٶ�
     /// </summary>
     /// <param name="velocity"> ��ά���� </param>
     public void SetVelocity(Vector2 velocity) => rigidBody.velocity = velocity;

     /// <summary>
     /// ���ø���X���ٶ�
     /// </summary>
     /// <param name="velocityX"> ������ </param>
     public void SetVelocityX(float velocityX) => SetVelocity(new Vector2(velocityX, rigidBody.velocity.y));

     /// <summary>
     /// ���ø���Y���ٶ�
     /// </summary>
     /// <param name="velocityY"> ������ </param>
     public void SetVelocityY(float velocityY) => SetVelocity(new Vector2(rigidBody.velocity.x, velocityY));

     #endregion

     #region ��ȡ��������

     public Vector2 getRigidbodyPos => rigidBody.position;
     public float getRigidbodyPosX => getRigidbodyPos.x;
     public float getRigidbodyPosY => getRigidbodyPos.y;

     #endregion

     #region ��ȡ�����ٶ�

     public Vector2 getRigibodyVelocity => rigidBody.velocity;
     public float getRigibodyVelocityX => getRigibodyVelocity.x;
     public float getRigibodyVelocityY => getRigibodyVelocity.y;

     #endregion

     /// <summary>
     /// ���ø�������
     /// </summary>
     /// <param name="value"> ������ </param>
     public void SetUseGraviy(float value) => rigidBody.gravityScale = value;

     #region ����������ײ��

     /// <summary>
     /// ����������ײ�����
     /// </summary>
     /// <param name="sizeX"> X ��ߴ�</param>
     /// <param name="sizeY"> Y ��ߴ�</param>
     public void SetColiderSize(float sizeX, float sizeY) => col.size = new Vector2(sizeX, sizeY);

     /// <summary>
     /// ����������ײ������
     /// </summary>
     /// <param name="offsetX"> X ������ </param>
     /// <param name="offsetY"> Y ������ </param>
     public void SetColiderOffset(float offsetX, float offsetY) => col.offset = new Vector2(offsetX, offsetY);

     #endregion

     #region ����

     // ����������Ǹ���ţ�����ţ�ƣ�����

     /// <summary>
     /// �ı��������
     /// </summary>
     /// <param name="changeLayer"> Ŀ���� </param>
     /// <param name="recoverLayer"> �ظ��� </param>
     /// <param name="duration"> �ӳ�ʱ�� </param>
     /// <param name="stateBool"> ί�к������ص�������Ĳ������� </param>
     /// <returns>��Ҫ���� Ŀ�ꡢ�ظ�������ʱ�䡢ί�к���</returns>
     public void SwitchLayer(LayerType changeLayer, LayerType recoverLayer, float duration, Action<bool> stateBool)
     {
          if(changeLayerCoroutine != null) StopCoroutine(changeLayerCoroutine);                                         // �����ǰЭ�̶��в�Ϊ�գ������һ�Σ���֤Ψһ�ԣ�
          changeLayerCoroutine = StartCoroutine(ChangeLayerCoroutine(changeLayer,recoverLayer, duration, stateBool));   // Э�̣���������ת����Ҫ�����ݣ���ӵ�����Э�̵Ķ����
     }

     /// <summary>
     /// ��ԭ��
     /// </summary>
     /// <param name="recoverLayer"> �ظ��� </param>
     /// <param name="duration"> �ӳ�ʱ�� </param>
     /// <param name="stateBool"> ί�к������ص�������Ĳ������� </param>
     /// <returns>Ҫ���� �ظ����ӳ�ʱ��</returns>
     IEnumerator ChangeLayerCoroutine(LayerType changeLayer, LayerType recoverLayer, float duration, Action<bool> stateBool)
     {
          gameObject.layer = (int)changeLayer;         // �ı���
          stateBool.Invoke(true);                      // �����ص����������ݽ����true��
          yield return new WaitForSeconds(duration);   // �ӳ�ʱ��
          gameObject.layer = (int)recoverLayer;        // �ָ��㼶
          stateBool.Invoke(false);                     // �����ص����������ݽ����false��
     }

     /// <summary>
     /// isCrossing �����ص�����
     /// </summary>
     /// <param name="stateBool"> ����һ���ص��Ľ�� </param>
     /// <remarks>��ѽ���ص��� isCrossing</remarks>
     public void Action_IsCrossing(bool stateBool) => isCrossing = stateBool;

     #endregion

     #region ��ʱ��

     /// <summary>
     /// ѭ����ʱ��
     /// </summary>
     /// <param name="lockBool"> ���� bool ���� </param>
     /// <param name="cooldownTime"> ��ȴʱ�� </param>
     public void CooldownTimer(Action<bool> lockBool, float cooldownTime)
     {
          StopCoroutine(nameof(ICooldownTimer));
          StartCoroutine(ICooldownTimer(lockBool, cooldownTime));
     }

     /// <summary>
     /// ѭ����ʱ��
     /// </summary>
     /// <param name="lockBool"> ���� bool ���� </param>
     /// <param name="cooldownTime"> ��ȴʱ�� </param>
     IEnumerator ICooldownTimer(Action<bool> lockBool, float cooldownTime)
     {
          lockBool.Invoke(false);
          yield return new WaitForSeconds(cooldownTime);
          lockBool.Invoke(true);
     }

     /// <summary>
     /// �ص��� canAirAttack
     /// </summary>
     /// <param name="lockBool"></param>
     public void Action_CanAirAttack(bool lockBool) => canAirAttack = lockBool;

     #endregion
}