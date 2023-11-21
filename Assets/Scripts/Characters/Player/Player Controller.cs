using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 玩家控制脚本
/// </summary>
/// <remarks> 玩家控制脚本，比如那啥，移动啊，跳跃啊，啥的都在这里统一调用使用 </remarks>
public class PlayerController : MonoBehaviour
{
     // 调用 
     private Rigidbody2D rigidBody;
     private PlayerInput input;
     private PlayerConstants constants;
     private AnimationManager animationManager;
     private CapsuleCollider2D col;

     private Coroutine changeLayerCoroutine;    // 缓存协程

     public int faceDir => (int)transform.lossyScale.x;          // 面朝方向
     public float moveSpeed => Mathf.Abs(rigidBody.velocity.x);  // 当前移动速度（取绝对值）
     public int currentLayer => gameObject.layer;                // 当前层级

     // 状态函数
     public bool canAirJump   { get; set; }  // 二段跳
     public bool canAirAttack { get; set; }  // 空中攻击
     public bool isCrossing   { get; set; }  // 穿越状态
     public bool isHurt       { get; set; }  // 受伤
     public bool isDeath      { get; set; }  // 死亡
     public bool isHitEnemy   { get; set; }  // 

     
     public int knockbackDirX           { get; set; }  // 受击方向X
     public int knockbackDirY           { get; set; }  // 受击方向Y
     public float knockbackForceX       { get; set; }  // 击退力度X
     public float knockbackForceY       { get; set; }  // 击退力度Y
     public float knockbackHardTime     { get; set; }  // 击退硬直时间

     private void Awake()
     {
          // 获取
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
     /// 初始状态
     /// </summary>
     private void InitialState()
     {
          canAirAttack = true; 
          canAirJump = true;
     }

     #region 移动与下落

     /// <summary>
     /// 玩家移动
     /// </summary>
     /// <param name="speed"> 接收浮点数 </param>
     public void Move(float speed)
     {
          if (input.move)     // 如果玩家按下了移动按钮 
          {
               int faceDir = (input.axesX > 0) ? 1 : -1;         // 数值精确化（只有1和-1）
               transform.localScale = new Vector2(faceDir, 1f);  // 将玩家的转换缩放比例设置为 playerInput.axisX 控制的方向（1 为正方向，-1 为反方向）
          }

          SetVelocityX(speed * input.axesX); // 调用 SetVelocityX 方法设置玩家的水平速度，速度值为 speed 乘以 playerInput.axisX 的值（也就是根据玩家输入的方向和速度来计算速度值）
     }

     /// <summary>
     /// 玩家下落
     /// </summary>
     /// <param name="stateStartTime"> 状态开始事件 </param>
     public void Fall(float stateDuration)
     {
          float currentFallSpeed = constants.fallSpeedCurve.Evaluate(stateDuration);  // 根据状态时间和下落速度曲线获取当前下落速度
          SetVelocityY(currentFallSpeed);
     }

     #endregion

     #region 受伤与死亡

     /// <summary>
     /// 获取受击数据
     /// </summary>
     /// <param name="attacker"> 接收一个 Attacker </param>
     public void OnHurt(Attacker attacker)
     {
          GetAttacker(attacker);
          isHurt = true;
     }

     /// <summary>
     /// 启动死亡
     /// </summary>
     /// <param name="attacker"> 接收一个 Attacker </param>
     public void OnDeath(Attacker attacker)
     {
          GetAttacker(attacker);
          isDeath = true;
          input.DisableGameplayInputs();
     }

     /// <summary>
     /// 启动震动
     /// </summary>
     /// <param name="victim">接收 Victim 的数据</param>
     public void OnShake(Victim victim)
     {
          animationManager.AnimationShake(victim.hitDirection, victim.shakeStrength);
     }

     /// <summary>
     /// 获取受击数据
     /// </summary>
     /// <param name="attacker"></param>
     private void GetAttacker(Attacker attacker)
     {
          // 缓存获取的向量
          Vector2 currentKnockbackDirX = new Vector2(transform.position.x - attacker.transform.position.x, 0).normalized;
          Vector2 currentKnockbackDirY = new Vector2(0, transform.position.y - attacker.transform.position.y).normalized;

          knockbackDirX = (int)currentKnockbackDirX.x;      // 提取X
          knockbackDirY = (int)currentKnockbackDirY.y;      // 提取Y

          // 将获取的数值赋值给对应变量
          knockbackForceX = attacker.knockbackForceX;
          knockbackForceY = attacker.knockbackForceY;
          knockbackHardTime = attacker.knockbackHardTime;
     }

     #endregion

     #region 设置刚体速度

     /// <summary>
     /// 设置刚体速度
     /// </summary>
     /// <param name="velocity"> 二维向量 </param>
     public void SetVelocity(Vector2 velocity) => rigidBody.velocity = velocity;

     /// <summary>
     /// 设置刚体X轴速度
     /// </summary>
     /// <param name="velocityX"> 浮点数 </param>
     public void SetVelocityX(float velocityX) => SetVelocity(new Vector2(velocityX, rigidBody.velocity.y));

     /// <summary>
     /// 设置刚体Y轴速度
     /// </summary>
     /// <param name="velocityY"> 浮点数 </param>
     public void SetVelocityY(float velocityY) => SetVelocity(new Vector2(rigidBody.velocity.x, velocityY));

     #endregion

     #region 读取刚体坐标

     public Vector2 getRigidbodyPos => rigidBody.position;
     public float getRigidbodyPosX => getRigidbodyPos.x;
     public float getRigidbodyPosY => getRigidbodyPos.y;

     #endregion

     #region 读取刚体速度

     public Vector2 getRigibodyVelocity => rigidBody.velocity;
     public float getRigibodyVelocityX => getRigibodyVelocity.x;
     public float getRigibodyVelocityY => getRigibodyVelocity.y;

     #endregion

     /// <summary>
     /// 设置刚体重力
     /// </summary>
     /// <param name="value"> 浮点数 </param>
     public void SetUseGraviy(float value) => rigidBody.gravityScale = value;

     #region 设置物理碰撞体

     /// <summary>
     /// 设置物理碰撞体体积
     /// </summary>
     /// <param name="sizeX"> X 轴尺寸</param>
     /// <param name="sizeY"> Y 轴尺寸</param>
     public void SetColiderSize(float sizeX, float sizeY) => col.size = new Vector2(sizeX, sizeY);

     /// <summary>
     /// 设置物理碰撞体坐标
     /// </summary>
     /// <param name="offsetX"> X 轴坐标 </param>
     /// <param name="offsetY"> Y 轴坐标 </param>
     public void SetColiderOffset(float offsetX, float offsetY) => col.offset = new Vector2(offsetX, offsetY);

     #endregion

     #region 改类

     // 咱真他娘的是个天才！！！牛逼！！！

     /// <summary>
     /// 改变物体的类
     /// </summary>
     /// <param name="changeLayer"> 目标类 </param>
     /// <param name="recoverLayer"> 回复类 </param>
     /// <param name="duration"> 延迟时间 </param>
     /// <param name="stateBool"> 委托函数，回调给输入的布尔函数 </param>
     /// <returns>需要输入 目标、回复、持续时间、委托函数</returns>
     public void SwitchLayer(LayerType changeLayer, LayerType recoverLayer, float duration, Action<bool> stateBool)
     {
          if(changeLayerCoroutine != null) StopCoroutine(changeLayerCoroutine);                                         // 如果当前协程队列不为空，则清除一次（保证唯一性）
          changeLayerCoroutine = StartCoroutine(ChangeLayerCoroutine(changeLayer,recoverLayer, duration, stateBool));   // 协程！启动！（转入需要的数据，添加到缓存协程的队列里）
     }

     /// <summary>
     /// 复原类
     /// </summary>
     /// <param name="recoverLayer"> 回复类 </param>
     /// <param name="duration"> 延迟时间 </param>
     /// <param name="stateBool"> 委托函数，回调给输入的布尔函数 </param>
     /// <returns>要传入 回复、延迟时间</returns>
     IEnumerator ChangeLayerCoroutine(LayerType changeLayer, LayerType recoverLayer, float duration, Action<bool> stateBool)
     {
          gameObject.layer = (int)changeLayer;         // 改变类
          stateBool.Invoke(true);                      // 触发回调函数并传递结果（true）
          yield return new WaitForSeconds(duration);   // 延迟时间
          gameObject.layer = (int)recoverLayer;        // 恢复层级
          stateBool.Invoke(false);                     // 触发回调函数并传递结果（false）
     }

     /// <summary>
     /// isCrossing 函数回调方法
     /// </summary>
     /// <param name="stateBool"> 接收一个回调的结果 </param>
     /// <remarks>会把结果回调给 isCrossing</remarks>
     public void Action_IsCrossing(bool stateBool) => isCrossing = stateBool;

     #endregion

     #region 计时器

     /// <summary>
     /// 循环计时器
     /// </summary>
     /// <param name="lockBool"> 锁定 bool 函数 </param>
     /// <param name="cooldownTime"> 冷却时间 </param>
     public void CooldownTimer(Action<bool> lockBool, float cooldownTime)
     {
          StopCoroutine(nameof(ICooldownTimer));
          StartCoroutine(ICooldownTimer(lockBool, cooldownTime));
     }

     /// <summary>
     /// 循环计时器
     /// </summary>
     /// <param name="lockBool"> 锁定 bool 函数 </param>
     /// <param name="cooldownTime"> 冷却时间 </param>
     IEnumerator ICooldownTimer(Action<bool> lockBool, float cooldownTime)
     {
          lockBool.Invoke(false);
          yield return new WaitForSeconds(cooldownTime);
          lockBool.Invoke(true);
     }

     /// <summary>
     /// 回调给 canAirAttack
     /// </summary>
     /// <param name="lockBool"></param>
     public void Action_CanAirAttack(bool lockBool) => canAirAttack = lockBool;

     #endregion
}