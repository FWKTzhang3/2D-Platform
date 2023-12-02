/// <summary>
/// 芝士层级枚举
/// </summary>
/// <remarks>用来提前枚举一些字符变量来指代层级数字，这样可以更快的运行</remarks>
public enum LayerType
{
     Default,
     TransparentFX,
     IgnoreRaycast,
     Player,
     Water,
     UI,
     Ground,
     OneWayPlatform,
     Cross,
     Muteki,
     Enemy,
     Camera,
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
     CameraShake,
}

/// <summary>
/// 芝士角色枚举
/// </summary>
/// <remarks> 先这样留着吧，我也不知道有什么用 </remarks>
public enum CharacterType
{
     Player,
     Enemy,
     Boss,
}

#region 控制器行为枚举

/// <summary>
/// 摇杆方向
/// </summary>
public enum JoystickDirectionType
{
     Up,
     Down,
     Left,
     Right,
     UpLeft,
     UpRight,
     DownLeft,
     DownRight,
     None,
}

/// <summary>
/// 行为类型
/// </summary>
public enum ActionType
{
     Jump,
     Attack,
     Skill,
     Interact,
}

/// <summary>
/// 十字键类型
/// </summary>
public enum DirectionPadType
{
     Up,
     Down,
     Left,
     Right,
}

/// <summary>
/// 肩部按键类型
/// </summary>
public enum ShoulderButtonType
{
     LeftButton,    // 左肩键
     RightButton,   // 右肩键
}

/// <summary>
/// 扳机按键类型
/// </summary>
public enum TriggerButtonType
{
     LeftTrigger,   // 左扳机
     RightTrigger,  // 右扳机
}

#endregion