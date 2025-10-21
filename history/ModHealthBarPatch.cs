using HarmonyLib;

namespace test
{
    /// <summary>
    /// test模组的主要行为类
    /// 继承自Duckov.Modding.ModBehaviour，用于在游戏UI中显示物品价值
    /// </summary>
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private static Harmony? harmony;
        private static bool patched = false;
        
        /// <summary>
        /// 模组初始化时调用，类似于构造函数
        /// </summary>
        void Awake()
        {
            // 只在第一次初始化时创建Harmony实例
            if (harmony == null)
            {
                harmony = new Harmony("com.test.healthdisplay");
            }
            
            // 只在未打补丁时应用补丁
            if (!patched)
            {
                harmony.PatchAll();
                patched = true;
            }
        }
        
        /// <summary>
        /// 模组销毁时调用，用于清理资源
        /// </summary>
        void OnDestroy()
        {
            // 只在已打补丁时取消补丁
            if (patched && harmony != null)
            {
                harmony.UnpatchAll("com.test.healthdisplay");
                patched = false;
            }
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