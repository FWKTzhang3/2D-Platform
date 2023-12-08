using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackComponet : MonoBehaviour
{
     private Transform _transform;           // 转换器组件

     private InputSystem _input;             // 控制器系统
     private ControllSystem _controll;       // 控制系统
     private DetectionSystem _detection;     // 检测系统

     private Attacker _attacker;             // 攻击输出
     private HurtBoxManager _hurtBoxManager; // 攻击判定

     public AttackData attackData;           // 调用保存数据的程序化文件

     private AttackDataType _current_Attack_Data_Type;                                    // 缓存当前攻击数据类型
     private Dictionary<AttackDataType, AttackDetails[]> _current_Attack_Data_Dictionary; // 缓存当前攻击数据字典
     private AttackDetails _current_Attack_Details;                                       // 缓存当前攻击数据

     private void Awake()
     {
          _transform = transform.parent.parent;                                      // 获取父级物体的父级的物体的转换器

          _input = GetComponentInParent<InputSystem>();                              // 获取父级物体的控制器系统
          _controll = GetComponentInParent<ControllSystem>();                        // 获取父级物体的控制系统
          _detection = GetComponentInParent<DetectionSystem>();                      // 获取父级物体的检测系统
          
          _attacker = _transform.GetComponentInChildren<Attacker>();                 // 获取父级物体的子物体的攻击输出
          _hurtBoxManager = _transform.GetComponentInChildren<HurtBoxManager>();     // 获取父级物体的子物体的攻击判定

          attackData?.InitializeData();                                              // 初始化数据
     }

     private void Start()
     {
          _current_Attack_Data_Dictionary = attackData.attackDataDictionary;         // 缓存数据文件里的字典
     }

     private void OnEnable()
     {
          // 订阅事件
          _input.ActionStartedEvent += Attack;         // 当前攻击指令事件   
          WeaponFrames.NormalAttackEvent += NormalAttackFrames;    // 当前攻击帧事件
          WeaponFrames.SpecialAttackEvent += SpecialFrames;   // 当前攻击帧事件
     }


     private void OnDisable()
     {
          // 注销事件
          _input.ActionStartedEvent -= Attack;         // 当前攻击指令事件 
          WeaponFrames.NormalAttackEvent -= NormalAttackFrames;    // 当前攻击帧事件
     }

     /// <summary>
     /// 攻击指令事件方法
     /// </summary>
     /// <param name="attackAction"> 行动指令 </param>
     private void Attack(InputAction attackAction)
     {
          if (!_detection.isAir)        // 如果当前检测在地面
               _controll.velocityY = 0;      // 急停
     }

     /// <summary>
     /// 攻击帧事件
     /// </summary>
     /// <param name="count"> 攻击次数 </param>
     private void NormalAttackFrames(int count)
     {
          // 缓存当前攻击类型（如果在空中则为 空中普通攻击，如果不在空中则为 地面普通攻击）
          _current_Attack_Data_Type = _detection.isAir ? AttackDataType.Normal_Air_Attack : AttackDataType.Normal_Ground_Attack;
          // 输入当前攻击类型搜索数据，如果有 则继续
          if (_current_Attack_Data_Dictionary.TryGetValue(_current_Attack_Data_Type, out AttackDetails[] value))
          {
               _current_Attack_Details = value[count - 1];       // 缓存当前攻击数据（从攻击数据以攻击次数为引索）
               SetHurtBoxVariables(_current_Attack_Details);     // 输出数据
          }
     }

     /// <summary>
     /// 设置攻击判定数据
     /// </summary>
     /// <param name="attackDetails"> 输入攻击数据 </param>
     private void SetHurtBoxVariables(AttackDetails attackDetails)
     {
          _hurtBoxManager.SetHurtBoxOffset(attackDetails.hurtBoxOffset);   // 攻击判定坐标（中心）
          _hurtBoxManager.SetHurtBoxSize(attackDetails.hurtBoxSize);       // 攻击判定范围
          _attacker.damage = attackDetails.damage;                         // 攻击伤害
          _attacker.knockbackForce = attackDetails.knockbackForce;         // 攻击击退力度
          _attacker.hitstunTime = attackDetails.hitstunTime;               // 硬直时间
          _attacker.hitStopTime = attackDetails.hitStopTime;               // 顿帧时间
          _attacker.hitStopRecoveSpeed = attackDetails.hitStopRecoveSpeed; // 顿帧恢复速度
     }

     private void SpecialFrames()
     {
          Debug.Log("远程攻击");
     }
}
