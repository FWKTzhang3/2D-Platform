using System;
using System.Numerics;
using System.Runtime.Serialization;

/// <summary>
/// ֥ʿ�㼶ö��
/// </summary>
/// <remarks>������ǰö��һЩ�ַ�������ָ���㼶���֣��������Ը��������</remarks>
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
/// ֥ʿ��ǩö��
/// </summary>
/// <remarks> ��ǰö�ٳ��� </remarks>
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
/// ֥ʿ��ɫö��
/// </summary>
/// <remarks> ���������Űɣ���Ҳ��֪����ʲô�� </remarks>
public enum CharacterType
{
     Player,
     Enemy,
     Boss,
}

/// <summary>
/// ֥ʿ��������ö��
/// </summary>
/// <remarks> ���������Űɣ���Ҳ��֪����ʲô�� </remarks>
public enum AttackType
{
     Normal,         // ��ͨ����
     Skill,          // ���ܹ���
     Combo,          // ��������
     Critical,       // ��������
     Counter,        // ��������
     Aerial,         // ���й���
     Grab,           // ץȡ����
     Projectile,     // �����﹥��
     AoE,            // ��Χ����
     Charge,         // ��̹���
     Finisher        // ��������
}

#region ��������Ϊö��

/// <summary>
/// ҡ�˷���
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
/// ��Ϊ����
/// </summary>
public enum ActionType
{
     Jump,
     Attack,
     Skill,
     Interact,
}

/// <summary>
/// ʮ�ּ�����
/// </summary>
public enum DirectionPadType
{
     Up,
     Down,
     Left,
     Right,
}

/// <summary>
/// �粿��������
/// </summary>
public enum ShoulderButtonType
{
     LeftButton,    // ����
     RightButton,   // �Ҽ��
}

/// <summary>
/// �����������
/// </summary>
public enum TriggerButtonType
{
     LeftTrigger,   // ����
     RightTrigger,  // �Ұ��
}

#endregion