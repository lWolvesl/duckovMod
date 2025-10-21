using HarmonyLib;
using UnityEngine;

namespace test
{
    /// <summary>
    /// 基础模板
    /// 继承自Duckov.Modding.ModBehaviour
    /// </summary>
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private Harmony harmony;
        
        /// <summary>
        /// 模组初始化时调用，类似于构造函数
        /// </summary>
        void Awake()
        {
            // 初始化Harmony实例
            harmony = new Harmony("com.test.itemdetailsdisplay");
            
            // 应用所有Harmony补丁
            harmony.PatchAll();
            
            Debug.Log("Test模组已加载，ItemDetailsDisplay补丁已应用");
        }
        
        /// <summary>
        /// 模组销毁时调用，用于清理资源
        /// </summary>
        void OnDestroy()
        {
            // 卸载所有Harmony补丁
            harmony?.UnpatchAll();
        }
        
        void OnEnable()
        {
            // 模组启用时重新应用补丁
            if (harmony == null)
            {
                harmony = new Harmony("com.test.itemdetailsdisplay");
                harmony.PatchAll();
            }
        }
        
        /// <summary>
        /// 模组禁用时调用，取消事件监听器
        /// 取消订阅事件，防止在模组禁用后仍然响应事件
        /// </summary>
        void OnDisable()
        {
            // 模组禁用时卸载补丁
            harmony?.UnpatchAll();
            harmony = null;
        }
    }
}