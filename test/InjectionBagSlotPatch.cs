using System;
using HarmonyLib;
using ItemStatsSystem;
using ItemStatsSystem.Items;
using System.Reflection;
using System.Collections.Generic;
using Duckov.Utilities;

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
        private const int TargetSlotCount = 12;
        private const int AdditionalSlots = 6;
        
        // 记录已经修改过的物品，避免重复修改
        private static readonly HashSet<int> modifiedItems = new HashSet<int>();
        
        // 缓存反射方法，避免重复获取
        private static readonly ConstructorInfo SlotConstructor = typeof(Slot).GetConstructor(new Type[] { typeof(string) });
        private static readonly FieldInfo RequireTagsField = typeof(Slot).GetField("requireTags", BindingFlags.Public | BindingFlags.Instance);
        private static readonly MethodInfo SlotInitializeMethod = typeof(Slot).GetMethod("Initialize", BindingFlags.Public | BindingFlags.Instance);
        private static readonly MethodInfo BuildDictionaryMethod = typeof(SlotCollection).GetMethod("BuildDictionary", BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// Initialize方法的后缀补丁
        /// 在物品初始化后检查是否是注射器收纳包
        /// 槽位默认6个，变为12个
        /// </summary>
        /// <param name="__instance">Item实例</param>
        [HarmonyPostfix]
        static void Postfix(Item __instance)
        {
            // 快速检查：是否是注射器收纳包
            if (__instance?.TypeID != InjectionBagTypeID || __instance.Slots == null)
                return;
                
            // 检查是否已经修改过这个物品实例
            int instanceId = __instance.GetInstanceID();
            if (!modifiedItems.Add(instanceId)) // 使用Add的返回值检查是否已存在
                return;

            // 获取槽位集合
            SlotCollection slots = __instance.Slots;
            
            // 获取原始槽位数量
            int originalSlotCount = slots.Count;
            
            // 如果已经是目标槽位数量，则不需要修改
            if (originalSlotCount == TargetSlotCount)
                return;

            // 获取第一个槽位的标签，用于复制
            List<Tag> firstSlotTags = null;
            if (originalSlotCount > 0 && slots.list.Count > 0)
            {
                var firstSlot = slots.list[0];
                if (firstSlot != null)
                {
                    firstSlotTags = RequireTagsField?.GetValue(firstSlot) as List<Tag>;
                }
            }

            // 预分配槽位列表容量，减少内存重新分配
            if (slots.list.Capacity < originalSlotCount + AdditionalSlots)
            {
                slots.list.Capacity = originalSlotCount + AdditionalSlots;
            }

            // 添加6个新的槽位
            for (int i = 0; i < AdditionalSlots; i++)
            {
                // 设置Slot的Key属性
                string slotKey = $"InjectionSlot_{originalSlotCount + i + 1}";
                
                // 使用缓存的构造函数创建新的Slot对象
                Slot newSlot = SlotConstructor?.Invoke(new object[] { slotKey }) as Slot;

                // 复制第一个槽位的requireTags
                if (newSlot != null && firstSlotTags != null && firstSlotTags.Count > 0)
                {
                    // 创建新的Tag列表并复制第一个槽位的Tag
                    var newTags = new List<Tag>(firstSlotTags.Count);
                    for (int j = 0; j < firstSlotTags.Count; j++)
                    {
                        var tag = firstSlotTags[j];
                        if (tag != null)
                        {
                            newTags.Add(tag);
                        }
                    }
                    RequireTagsField?.SetValue(newSlot, newTags);
                }

                // 初始化Slot
                SlotInitializeMethod?.Invoke(newSlot, new object[] { slots });

                // 添加到槽位列表
                slots.list.Add(newSlot);
            }

            // 重建字典缓存
            BuildDictionaryMethod?.Invoke(slots, null);
        }
    }
}
