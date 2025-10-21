using System;
using HarmonyLib;
using ItemStatsSystem;
using ItemStatsSystem.Items;
using UnityEngine;
using System.Reflection;

namespace test
{
    /// <summary>
    /// 注射器收纳包槽位修改补丁
    /// 将注射器收纳包(ID 882)的槽位数量从6个修改为12个
    /// </summary>
    [HarmonyPatch(typeof(Item))]
    [HarmonyPatch("Initialize")]
    public class InjectionBagSlotPatch
    {
        private const int InjectionBagTypeID = 882;
        /// <summary>
        /// Initialize方法的后缀补丁
        /// 在物品初始化后检查是否是注射器收纳包
        /// 槽位默认6个，变为12个
        /// </summary>
        /// <param name="__instance">Item实例</param>
        [HarmonyPostfix]
        static void Postfix(Item __instance)
        {
            Debug.Log($"[注射器收纳包修改] 被调用");

            // 检查是否是注射器收纳包
            if (__instance == null || __instance.TypeID != InjectionBagTypeID || __instance.Slots == null)
                return;

            // 获取槽位集合
            SlotCollection slots = __instance.Slots;
            
            if (slots.Count == 12)
                return;

            // 获取原始槽位数量
            int originalSlotCount = slots.Count;

            // 添加6个新的槽位
            for (int i = 0; i < 6; i++)
            {
                // 创建新的Slot对象
                Slot newSlot = (Slot)typeof(Slot).GetConstructor(new Type[0])?.Invoke(null);

                // 设置Slot的Key属性
                typeof(Slot).GetField("Key", BindingFlags.Public | BindingFlags.Instance)?.SetValue(newSlot, $"Slot_{originalSlotCount + i}");

                // 添加到槽位列表
                slots.list.Add(newSlot);

                // 初始化Slot
                typeof(Slot).GetMethod("Initialize", BindingFlags.Public | BindingFlags.Instance)?.Invoke(newSlot, new object[] { slots });
            }

            // 重建字典缓存
            typeof(SlotCollection).GetMethod("BuildDictionary", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(slots, null);

            Debug.Log($"[注射器收纳包修改] 已将物品ID {__instance.TypeID} 的槽位数量从{originalSlotCount}修改为{slots.Count}");
        }
    }
}
