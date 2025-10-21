using HarmonyLib;
using ItemStatsSystem;
using System.Reflection;
using UnityEngine;

namespace test
{
    /// <summary>
    /// ItemDetailsDisplay的Harmony补丁
    /// 为Setup方法添加后缀方法，在控制台输出激活文本和Item对象的ID
    /// </summary>
    [HarmonyPatch(typeof(Duckov.UI.ItemHoveringUI))]
    [HarmonyPatch("SetupRegisteredInfo")]
    public class ItemDetailsCatchPatch
    {
        /// <summary>
        /// Setup方法的后缀补丁，在原始方法执行后调用
        /// </summary>
        /// <param name="__instance">ItemDetailsDisplay实例</param>
        /// <param name="item">设置的Item对象</param>
        [HarmonyPostfix]
        static void Postfix(Duckov.UI.ItemDetailsDisplay __instance, Item item)
        {
            if (item != null)
            {
                // 检查是否是ID为882的物品
                if (item.TypeID == 882)
                {
                    Debug.Log("=== 物品ID 882 的详细信息 ===");
                    
                    // 获取Item类的所有字段和属性
                    BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                    FieldInfo[] fields = typeof(Item).GetFields(flags);
                    PropertyInfo[] properties = typeof(Item).GetProperties(flags);
                    
                    Debug.Log("字段信息:");
                    foreach (FieldInfo field in fields)
                    {
                        try
                        {
                            object value = field.GetValue(item);
                            string valueStr = value != null ? value.ToString() : "null";
                            Debug.Log($"  {field.FieldType} {field.Name}: {valueStr}");
                        }
                        catch (System.Exception e)
                        {
                            Debug.Log($"  {field.FieldType} {field.Name}: [获取失败] {e.Message}");
                        }
                    }
                    
                    Debug.Log("属性信息:");
                    foreach (PropertyInfo property in properties)
                    {
                        try
                        {
                            if (property.GetIndexParameters().Length == 0) // 确保不是索引器属性
                            {
                                object value = property.GetValue(item);
                                string valueStr = value != null ? value.ToString() : "null";
                                Debug.Log($"  {property.PropertyType} {property.Name}: {valueStr}");
                            }
                        }
                        catch (System.Exception e)
                        {
                            Debug.Log($"  {property.PropertyType} {property.Name}: [获取失败] {e.Message}");
                        }
                    }
                    
                    Debug.Log("=== 物品ID 882 信息结束 ===");
                }
                else
                {
                    // 对于其他物品，只显示基本信息
                    string activationText = $"displayPatch激活 Item ID: {item.TypeID}";
                    Debug.Log(activationText);
                }
            }
        }
    }
}