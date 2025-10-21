using HarmonyLib;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace test
{
    /// <summary>
    /// Harmony补丁类，用于修改HealthBar的行为
    /// 添加血量数值显示功能
    /// </summary>
    [HarmonyPatch]
    public class HealthBarPatch
    {
        // 使用字典来存储每个HealthBar实例对应的血量文本
        private static readonly Dictionary<Duckov.UI.HealthBar, TextMeshProUGUI> healthTexts = new Dictionary<Duckov.UI.HealthBar, TextMeshProUGUI>();
        
        /// <summary>
        /// 补丁HealthBar的Setup方法，初始化血量文本
        /// </summary>
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Duckov.UI.HealthBar), "Setup")]
        static void SetupPostfix(Duckov.UI.HealthBar __instance)
        {
            // 如果这个HealthBar已经有对应的文本，先清理
            if (healthTexts.ContainsKey(__instance))
            {
                CleanupHealthText(__instance);
            }
            
            // 创建血量文本对象
            GameObject textObj = new GameObject("HealthText");
            textObj.transform.SetParent(__instance.transform);
            
            // 设置文本组件
            TextMeshProUGUI healthText = textObj.AddComponent<TextMeshProUGUI>();
            healthText.fontSize = 12;
            healthText.color = Color.white;
            healthText.alignment = TextAlignmentOptions.Center;
            
            // 设置文本位置
            RectTransform rectTransform = textObj.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);
            
            // 将文本对象存储到字典中
            healthTexts[__instance] = healthText;
            
            // 更新血量文本
            UpdateHealthText(__instance);
        }
        
        /// <summary>
        /// 补丁HealthBar的Refresh方法，更新血量文本
        /// </summary>
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Duckov.UI.HealthBar), "Refresh")]
        static void RefreshPostfix(Duckov.UI.HealthBar __instance)
        {
            UpdateHealthText(__instance);
        }
        
        /// <summary>
        /// 补丁HealthBar的Release方法，清理血量文本
        /// </summary>
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Duckov.UI.HealthBar), "Release")]
        static void ReleasePostfix(Duckov.UI.HealthBar __instance)
        {
            CleanupHealthText(__instance);
        }
        
        /// <summary>
        /// 清理与HealthBar关联的血量文本
        /// </summary>
        static void CleanupHealthText(Duckov.UI.HealthBar healthBar)
        {
            if (healthTexts.TryGetValue(healthBar, out TextMeshProUGUI textComponent))
            {
                if (textComponent != null && textComponent.gameObject != null)
                {
                    Object.DestroyImmediate(textComponent.gameObject);
                }
                healthTexts.Remove(healthBar);
            }
        }
        
        /// <summary>
        /// 更新血量文本显示
        /// </summary>
        static void UpdateHealthText(Duckov.UI.HealthBar healthBar)
        {
            if (!healthTexts.TryGetValue(healthBar, out TextMeshProUGUI textComponent) || 
                textComponent == null || 
                healthBar.target == null)
                return;
                
            float currentHealth = healthBar.target.CurrentHealth;
            float maxHealth = healthBar.target.MaxHealth;
            
            // 显示当前血量和最大血量
            textComponent.text = $"{currentHealth:F0}/{maxHealth:F0}";
        }
    }
}