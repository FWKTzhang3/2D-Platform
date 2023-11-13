/// <summary>
/// 这是接口
/// </summary>
/// <remarks>负责定义一些方法，不负责实现（约等于公用的协议，所有继承它的都要遵守）</remarks>
public interface IState
{
     /// <summary>
     /// 进入
     /// </summary>
     /// <remarks>进入时需要执行一次的操作</remarks>
     void Enter();

     /// <summary>
     /// 退出
     /// </summary>
     /// <remarks>退出时需要执行一次的操作</remarks>
     void Exit();

     /// <summary>
     /// 逻辑更新
     /// </summary>
     /// <remarks>在状态中需要执行的逻辑更新操作（通常每帧都运行，很TM快，Debug可能一秒几千回）</remarks>
     void LogicUpdate();

     /// <summary>
     /// 物理更新
     /// </summary>
     /// <remarks>在状态中需要执行的物理更新操作（每个物理更新周期调用一次,就是不管是天灾水灾还是埃博拉病毒，它都是固定周期运行）</remarks>
     void PhysicUpdate();
}
