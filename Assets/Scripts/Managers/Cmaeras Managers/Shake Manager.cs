using UnityEngine;
using Cinemachine;

public class ShakeManager : MonoBehaviour
{
     private CinemachineImpulseSource[] _allShakes;

     private void Awake()
     {
          _allShakes = GetComponentsInChildren<CinemachineImpulseSource>();
     }

     #region 由弱到强的震动

     /// <summary>
     /// 由弱到强的震动
     /// </summary>
     /// <param name="shakeVelocity"></param>
     /// <param name="shakeForce"></param>
     /// <param name="shakeDuration"></param>
     public void OnGradualShake(Vector2 shakeVelocity, float shakeForce, float shakeDuration)
     {
          _gradualShakeVelocity = shakeVelocity;
          _gradualDuration = shakeDuration;
          _gradualShake = shakeForce;
     }

     /// <summary>
     /// 震动向量
     /// </summary>
     private Vector2 _gradualShakeVelocity
     {
          get => _allShakes[0].m_DefaultVelocity;
          set => _allShakes[0].m_DefaultVelocity = value;
     }

     /// <summary>
     /// 震动缩放时间（震动时长）
     /// </summary>
     private float _gradualDuration
     {
          set => _allShakes[0].m_ImpulseDefinition.m_ImpulseDuration = value;
     }

     /// <summary>
     /// 启动震动（输入力度）
     /// </summary>
     private float _gradualShake
     {
          set => _allShakes[0].GenerateImpulse(value);
     }

     #endregion

     #region 持续的震动

     /// <summary>
     /// 启动持续震动
     /// </summary>
     /// <param name="shakeVelocity"></param>
     /// <param name="shakeForce"></param>
     /// <param name="shakeDuration"></param>
     public void OnSteadyShake(Vector2 shakeVelocity, float shakeForce, float shakeDuration)
     {
          _steadyShakeVelocity = shakeVelocity;
          _steadyDuration = shakeDuration;
          _steadyShake = shakeForce;
     }

     /// <summary>
     /// 震动向量
     /// </summary>
     private Vector2 _steadyShakeVelocity
     {
          get => _allShakes[1].m_DefaultVelocity;
          set => _allShakes[1].m_DefaultVelocity = value;
     }

     /// <summary>
     /// 震动缩放时间（震动时长）
     /// </summary>
     private float _steadyDuration
     {
          set => _allShakes[1].m_ImpulseDefinition.m_ImpulseDuration = value;
     }

     /// <summary>
     /// 启动震动（输入力度）
     /// </summary>
     private float _steadyShake
     {
          set => _allShakes[1].GenerateImpulse(value);
     }

     #endregion

     #region 由强到弱的震动

     /// <summary>
     /// 由强到弱的震动
     /// </summary>
     /// <param name="shakeVelocity"></param>
     /// <param name="shakeForce"></param>
     /// <param name="shakeDuration"></param>
     public void OnIncreasingShake(Vector2 shakeVelocity, float shakeForce, float shakeDuration)
     {
          _increasingShakeVelocity = shakeVelocity;
          _increasingDuration = shakeDuration;
          _increasingShake = shakeForce;
     }

     /// <summary>
     /// 震动向量
     /// </summary>
     private Vector2 _increasingShakeVelocity
     {
          get => _allShakes[2].m_DefaultVelocity;
          set => _allShakes[2].m_DefaultVelocity = value;
     }

     /// <summary>
     /// 震动缩放时间（震动时长）
     /// </summary>
     private float _increasingDuration
     {
          set => _allShakes[2].m_ImpulseDefinition.m_ImpulseDuration = value;
     }

     /// <summary>
     /// 启动震动（输入力度）
     /// </summary>
     private float _increasingShake
     {
          set => _allShakes[2].GenerateImpulse(value);
     }

     #endregion
}