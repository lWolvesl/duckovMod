using Duckov.UI;
using Duckov.Utilities;
using ItemStatsSystem;
using TMPro;
using UnityEngine;

// 物品价值显示类
// 用于在物品悬停UI中显示物品的价值

namespace test
{
    /// <summary>
    /// test模组的主要行为类
    /// 继承自Duckov.Modding.ModBehaviour，用于在游戏UI中显示物品价值
    /// </summary>
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private TextMeshProUGUI? _text = null;
        
        private TextMeshProUGUI Text
        {
            get
            {
                // 如果文本组件尚未创建，则实例化一个新的
                if (_text == null)
                {
                    // 使用游戏UI样式模板创建文本组件
                    _text = Instantiate(GameplayDataSettings.UIStyle.TemplateTextUGUI);
                }
                return _text;
            }
        }
        
        /// <summary>
        /// 模组初始化时调用，类似于构造函数
        /// 在此记录模组加载日志
        /// </summary>
        void Awake()
        {
            // 在控制台输出模组加载成功的日志信息
            Debug.Log("DisplayItemValue Loaded!!!");
        }
        
        /// <summary>
        /// 模组销毁时调用，用于清理资源
        /// 确保创建的文本组件被正确销毁，避免内存泄漏
        /// </summary>
        void OnDestroy()
        {
            // 检查文本组件是否存在
            if (_text != null)
                // 销毁文本组件以释放资源
                Destroy(_text);
        }
        void OnEnable()
        {
            // 注册物品悬停UI设置事件的监听器
            ItemHoveringUI.onSetupItem += OnSetupItemHoveringUI;
        }
        
        /// <summary>
        /// 模组禁用时调用，取消事件监听器
        /// 取消订阅事件，防止在模组禁用后仍然响应事件
        /// </summary>
        void OnDisable()
        {
            // 取消注册物品悬停UI设置事件的监听器
            ItemHoveringUI.onSetupItem -= OnSetupItemHoveringUI;
        }

        private void OnSetupItemHoveringUI(ItemHoveringUI uiInstance, Item item)
        {
            if (item == null)
            {
                Text.gameObject.SetActive(false);
                return;
            }
            
            Text.gameObject.SetActive(true);
            Text.transform.SetParent(uiInstance.LayoutParent);
            Text.transform.localScale = Vector3.one;
            Text.transform.SetSiblingIndex(1);
            Text.text = $"${item.GetTotalRawValue() / 2}";
            Text.fontSize = 20f;
            Text.color = Color.green;
        }
    }
}