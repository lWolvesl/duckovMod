
namespace test
{
    /// <summary>
    /// test模组的主要行为类
    /// 继承自Duckov.Modding.ModBehaviour，用于在游戏UI中显示物品价值
    /// </summary>
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {

        
        /// <summary>
        /// 模组初始化时调用，类似于构造函数
        /// </summary>
        void Awake()
        {

        }
        
        /// <summary>
        /// 模组销毁时调用，用于清理资源
        /// </summary>
        void OnDestroy()
        {

        }
        
        void OnEnable()
        {

        }
        
        /// <summary>
        /// 模组禁用时调用，取消事件监听器
        /// 取消订阅事件，防止在模组禁用后仍然响应事件
        /// </summary>
        void OnDisable()
        {

        }
    }
}