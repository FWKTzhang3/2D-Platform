using UnityEngine;

public class SystemManager : MonoBehaviour
{
     private void Start()
     {
          Application.targetFrameRate = 60;  // 将帧率锁定为60帧/秒
     }
}
