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
