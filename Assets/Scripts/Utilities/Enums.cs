/// <summary>
/// 芝士层级枚举
/// </summary>
/// <remarks>用来提前枚举一些字符变量来指代层级数字，这样可以更快的运行</remarks>
public enum LayerType
{
     Default = 0,
     TransparentFX = 1,
     IgnoreRaycast = 2,
     Player = 3,
     Water = 4,
     UI = 5,
     Ground = 6,
     OneWayPlatform = 7,
     Cross = 8,
     Muteki = 9,
     Enemy = 10,
}

/// <summary>
/// 芝士标签枚举
/// </summary>
/// <remarks> 提前枚举出来 </remarks>
public enum TagType
{
     HitBox,
     HurtBox,
     Body,
     Animation,
     Detectors,
     CameraBounds,
}

/// <summary>
/// 芝士角色枚举
/// </summary>
/// <remarks> 先这样留着吧，我也不知道有什么用 </remarks>
public enum CharacterTyper
{
     Player,
     Enemy,
     Boss,
}

/// <summary>
/// 芝士攻击类型枚举
/// </summary>
/// <remarks> 先这样留着吧，我也不知道有什么用 </remarks>
public enum AttackType
{
     Normal,         // 普通攻击
     Skill,          // 技能攻击
     Combo,          // 连击攻击
     Critical,       // 致命攻击
     Counter,        // 反击攻击
     Aerial,         // 空中攻击
     Grab,           // 抓取攻击
     Projectile,     // 抛射物攻击
     AoE,            // 范围攻击
     Charge,         // 冲刺攻击
     Finisher        // 结束攻击
}
