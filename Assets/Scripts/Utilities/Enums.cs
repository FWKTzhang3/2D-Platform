/// <summary>
/// ֥ʿ�㼶ö��
/// </summary>
/// <remarks>������ǰö��һЩ�ַ�������ָ���㼶���֣��������Ը��������</remarks>
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
}

/// <summary>
/// ֥ʿ��ɫö��
/// </summary>
/// <remarks> ���������Űɣ���Ҳ��֪����ʲô�� </remarks>
public enum CharacterTyper
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
