using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
     // 调用 
     Rigidbody2D rigidBody;
     ObjectDetector objectDetector;
     AnimationManager animationManager;

     // 协程引用
     private Coroutine loopAssCoroutine;     // 循环赋值
     private Coroutine delayAssCoroutine;    // 延迟赋值

     public int faceDir => (int)transform.lossyScale.x;          // 面朝方向

     public bool canAttack;

     public bool isChasing => chaseCancelCountTime > 0;
     public bool isHurt;
     public bool isDeath;

     public float knockbackHardTime;              // 击退硬直时间
     public float chaseCancelCountTime;           // 追击延迟退出时间

     private void Awake()
     {
          // 获取
          rigidBody = GetComponent<Rigidbody2D>();
          objectDetector = GetComponentInChildren<ObjectDetector>();
          animationManager = GetComponentInChildren<AnimationManager>();
     }

     private void Start()
     {
          InitialState();
     }

     private void Update()
     {
          ChsaseDelay();
     }

     /// <summary>
     /// 初始状态
     /// </summary>
     private void InitialState()
     {
          canAttack = true;
     }

     /// <summary>
     /// 移动
     /// </summary>
     /// <param name="speed"> 输入速度 </param>
     public void Move(float speed) => SetVelocityX(speed * faceDir); 

     /// <summary>
     /// 切换面朝方向
     /// </summary>
     public void SwitchFaceDir() => transform.localScale = new Vector2(faceDir * -1, 1f);

     #region 受伤与死亡

     /// <summary>
     /// 获取受击数据
     /// </summary>
     /// <param name="attacker"> 接收一个 Attacker </param>
     public void GetHurt(Attacker attacker)
     {
          GetAttacker(attacker);
          isHurt = true;
     }

     /// <summary>
     /// 启动死亡
     /// </summary>
     /// <param name="attacker"> 接收 Attacker 的数据 </param>
     public void OnDeath(Attacker attacker)
     {
          GetAttacker(attacker);
          isDeath = true;
     }

     /// <summary>
     /// 获取受击数据
     /// </summary>
     /// <param name="attacker"></param>
     private void GetAttacker(Attacker attacker)
     {
          /*
          // 缓存获取的向量
          Vector2 currentKnockbackDirX = new Vector2(transform.position.x - attacker.transform.position.x, 0).normalized;
          Vector2 currentKnockbackDirY = new Vector2(0, transform.position.y - attacker.transform.position.y).normalized;

          knockbackDirX = (int)currentKnockbackDirX.x;      // 提取X
          knockbackDirY = (int)currentKnockbackDirY.y;      // 提取Y

          // 将获取的数值赋值给对应变量
          knockbackForceX = attacker.knockbackForceX;
          knockbackForceY = attacker.knockbackForceY;
          */
          //knockbackHardTime = attacker.hardTime;
     }

     /// <summary>
     /// 销毁物体
     /// </summary>
     public void Destroyed()
     {
          Destroy(gameObject);
     }

     #endregion

     /// <summary>
     /// 追击延迟
     /// </summary>
     private void ChsaseDelay()
     {
          if (objectDetector == null)
               return;

          bool isChasePlayer = objectDetector.isChasePlayer;
          if (!isChasePlayer && chaseCancelCountTime > 0)
          {
               chaseCancelCountTime -= Time.deltaTime;
          }
     }

     #region 循环赋值计时器

     /// <summary>
     /// 芝士个循环赋值计时器方法
     /// </summary>
     /// <param name="lockBool"> 被委托的bool函数 </param>
     /// <param name="startBool"> 回调的初始值 </param>
     /// <param name="overBool"> 回调的结束值 </param>
     /// <param name="intervalTime"> 回调间隔时间 </param>
     public void LoopAssTimer(Action<bool> lockBool, bool startBool, bool overBool, float intervalTime)
     {
          if (loopAssCoroutine != null)                                                                           // 如果引用不为空
               StopCoroutine(loopAssCoroutine);                                                                        // 停止之前的协程 (保证唯一性)

          loopAssCoroutine = StartCoroutine(LoopAssTimerCoroutine(lockBool, startBool, overBool, intervalTime));  // 协程！启动！（并记录引用）
     }

     /// <summary>
     /// 芝士个循环赋值计时器协程
     /// </summary>
     /// <param name="lockBool"> 被委托的bool函数 </param>
     /// <param name="startBool"> 回调的初始值 </param>
     /// <param name="overBool"> 回调的结束值 </param>
     /// <param name="intervalTime"> 回调间隔时间 </param>
     /// <returns> 将结果回调给对应函数 </returns>
     IEnumerator LoopAssTimerCoroutine(Action<bool> lockBool, bool startBool, bool overBool, float intervalTime)
     {
          lockBool.Invoke(startBool);                       // 初始值
          yield return new WaitForSeconds(intervalTime);    // 间隔时间
          lockBool.Invoke(overBool);                        // 结束值
          loopAssCoroutine = null;                          // 协程完成后将引用设置为null
     }

     #endregion

     #region 延迟赋值计时器

     /// <summary>
     /// 延迟赋值计时器方法
     /// </summary>
     /// <param name="lockBool">   回调函数 </param>
     /// <param name="outBool">    回调结果 </param>
     /// <param name="delayTime">  延迟时间 </param>
     private void DelayAssTimer(Action<bool> lockBool, bool outBool, float delayTime)
     {
          if (delayAssCoroutine != null)                                                                 // 如果引用不为空
               StopCoroutine(delayAssCoroutine);                                                         // 停止之前的协程
          delayAssCoroutine = StartCoroutine(DelayAssTimerCoroutine(lockBool, outBool, delayTime));      // 启动新的协程并记录引用
     }

     /// <summary>
     /// 延迟赋值计时器协程
     /// </summary>
     /// <param name="lockBool">   回调函数 </param>
     /// <param name="outBool">    回调结果 </param>
     /// <param name="delayTime">  延迟时间 </param>
     /// <returns> 将结果回调给对应函数 </returns>
     IEnumerator DelayAssTimerCoroutine(Action<bool> lockBool,bool outBool, float delayTime)
     {
          yield return new WaitForSeconds(delayTime);  // 延迟指定时间（秒）
          lockBool.Invoke(outBool);                    // 回调赋值结果
          delayAssCoroutine = null;                    // 协程完成后将引用设置为null
     }
         
     #endregion

     #region 委托方法

     /// <summary>
     /// canAttack 的委托回调
     /// </summary>
     /// <param name="lockBool"> 赋值结果 </param>
     public void Action_CanAttack(bool lockBool) => canAttack = lockBool;

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

     /// <summary>
     /// 改变物体的类
     /// </summary>
     public void SwitchLayer(LayerType changeLayer)
     {
          gameObject.layer = (int)changeLayer;
     }
}