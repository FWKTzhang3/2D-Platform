/// <summary>
/// ���ǽӿ�
/// </summary>
/// <remarks>������һЩ������������ʵ�֣�Լ���ڹ��õ�Э�飬���м̳����Ķ�Ҫ���أ�</remarks>
public interface IState
{
     /// <summary>
     /// ����
     /// </summary>
     /// <remarks>����ʱ��Ҫִ��һ�εĲ���</remarks>
     void Enter();

     /// <summary>
     /// �˳�
     /// </summary>
     /// <remarks>�˳�ʱ��Ҫִ��һ�εĲ���</remarks>
     void Exit();

     /// <summary>
     /// �߼�����
     /// </summary>
     /// <remarks>��״̬����Ҫִ�е��߼����²�����ͨ��ÿ֡�����У���TM�죬Debug����һ�뼸ǧ�أ�</remarks>
     void LogicUpdate();

     /// <summary>
     /// �������
     /// </summary>
     /// <remarks>��״̬����Ҫִ�е�������²�����ÿ������������ڵ���һ��,���ǲ���������ˮ�ֻ��ǰ����������������ǹ̶��������У�</remarks>
     void PhysicUpdate();
}
