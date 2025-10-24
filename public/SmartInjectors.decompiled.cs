using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;
using Duckov;
using Duckov.Modding;
using Duckov.Utilities;
using ItemStatsSystem;
using ItemStatsSystem.Items;
using Microsoft.CodeAnalysis;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: TargetFramework(".NETStandard,Version=v2.1", FrameworkDisplayName = ".NET Standard 2.1")]
[assembly: AssemblyCompany("SmartInjectors")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("1.0.0+ecf703333fe8409045024fdd86e1e1f9b7e6701d")]
[assembly: AssemblyProduct("SmartInjectors")]
[assembly: AssemblyTitle("SmartInjectors")]
[assembly: AssemblyVersion("1.0.0.0")]
[module: RefSafetyRules(11)]
namespace Microsoft.CodeAnalysis
{
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	internal sealed class EmbeddedAttribute : Attribute
	{
	}
}
namespace System.Runtime.CompilerServices
{
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	[AttributeUsage(AttributeTargets.Module, AllowMultiple = false, Inherited = false)]
	internal sealed class RefSafetyRulesAttribute : Attribute
	{
		public readonly int Version;

		public RefSafetyRulesAttribute(int P_0)
		{
			Version = P_0;
		}
	}
}
namespace SmartInjectors
{
	public class InjectionCaseUI
	{
		private const string LOG_PREFIX = "[SmartInjectors.UI]";

		private bool isVisible;

		private Item currentInjectionCase;

		private Rect windowRect = new Rect((float)(Screen.width / 2 - 100), (float)(Screen.height - 350), 800f, 170f);

		private GUIStyle windowStyle;

		private GUIStyle buttonStyle;

		private GUIStyle labelStyle;

		private bool stylesInitialized;

		private bool showQuickInjectPrompt;

		private float promptShowTime;

		private const float PROMPT_DURATION = 5f;

		private bool showInjectionResult;

		private string injectionResultText = "";

		private float resultShowTime;

		private const float RESULT_DURATION = 3f;

		private bool showCooldownWarning;

		private string cooldownWarningText = "";

		private float cooldownWarningTime;

		private const float COOLDOWN_WARNING_DURATION = 1.5f;

		private float lastQuickInjectTime = -999f;

		private const float QUICK_INJECT_COOLDOWN = 60f;

		public bool IsVisible => isVisible;

		public void Show(Item injectionCase)
		{
			if ((Object)(object)injectionCase == (Object)null)
			{
				Debug.LogWarning((object)"[SmartInjectors.UI] 尝试显示UI但注射器收纳包为null");
				return;
			}
			if (injectionCase.TypeID != 882)
			{
				Debug.LogWarning((object)string.Format("{0} 物品TypeID不是注射器收纳包: {1}", "[SmartInjectors.UI]", injectionCase.TypeID));
				return;
			}
			currentInjectionCase = injectionCase;
			isVisible = true;
			Debug.Log((object)"[SmartInjectors.UI] 显示注射器收纳包UI");
		}

		public void Hide()
		{
			isVisible = false;
			currentInjectionCase = null;
			Debug.Log((object)"[SmartInjectors.UI] 隐藏注射器收纳包UI");
		}

		public void Toggle(Item injectionCase)
		{
			if (isVisible && (Object)(object)currentInjectionCase == (Object)(object)injectionCase)
			{
				Hide();
			}
			else
			{
				Show(injectionCase);
			}
		}

		public void DrawGUI()
		{
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Expected O, but got Unknown
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			DrawQuickInjectPrompts();
			if (isVisible && !((Object)(object)currentInjectionCase == (Object)null))
			{
				HandleKeyboardEventInGUI();
				if (!stylesInitialized)
				{
					InitializeStyles();
				}
				windowRect = GUI.Window(12345, windowRect, new WindowFunction(DrawWindow), "注射器收纳包", windowStyle);
			}
		}

		private void DrawQuickInjectPrompts()
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Expected O, but got Unknown
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_012d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0134: Expected O, but got Unknown
			//IL_0154: Unknown result type (might be due to invalid IL or missing references)
			//IL_0240: Unknown result type (might be due to invalid IL or missing references)
			//IL_0247: Expected O, but got Unknown
			//IL_0267: Unknown result type (might be due to invalid IL or missing references)
			//IL_0190: Unknown result type (might be due to invalid IL or missing references)
			//IL_0197: Expected O, but got Unknown
			//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02aa: Expected O, but got Unknown
			//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0312: Unknown result type (might be due to invalid IL or missing references)
			GUIStyle val = new GUIStyle(GUI.skin.label);
			val.alignment = (TextAnchor)4;
			val.fontSize = 24;
			val.fontStyle = (FontStyle)1;
			val.normal.textColor = Color.white;
			if ((Object)(object)GUI.skin != (Object)null && (Object)(object)GUI.skin.font != (Object)null)
			{
				val.font = GUI.skin.font;
			}
			GUIStyle val2 = new GUIStyle(val);
			val2.normal.textColor = new Color(0f, 0f, 0f, 0.8f);
			if (showQuickInjectPrompt)
			{
				float num = (float)Screen.width / 2f;
				float num2 = (float)Screen.height / 2f;
				string text = "即将全部注射...";
				GUI.Label(new Rect(num - 198f, num2 - 48f, 400f, 100f), text, val2);
				GUI.Label(new Rect(num - 200f, num2 - 50f, 400f, 100f), text, val);
			}
			if (showInjectionResult)
			{
				float num3 = (float)Screen.width / 2f;
				float num4 = (float)Screen.height / 2f;
				GUIStyle val3 = new GUIStyle(GUI.skin.label);
				val3.alignment = (TextAnchor)4;
				val3.fontSize = 20;
				val3.fontStyle = (FontStyle)1;
				val3.normal.textColor = Color.green;
				if ((Object)(object)GUI.skin != (Object)null && (Object)(object)GUI.skin.font != (Object)null)
				{
					val3.font = GUI.skin.font;
				}
				GUIStyle val4 = new GUIStyle(val3);
				val4.normal.textColor = new Color(0f, 0f, 0f, 0.8f);
				GUI.Label(new Rect(num3 - 298f, num4 - 98f, 600f, 200f), injectionResultText, val4);
				GUI.Label(new Rect(num3 - 300f, num4 - 100f, 600f, 200f), injectionResultText, val3);
			}
			if (showCooldownWarning)
			{
				float num5 = (float)Screen.width / 2f;
				float num6 = (float)Screen.height / 2f;
				GUIStyle val5 = new GUIStyle(GUI.skin.label);
				val5.alignment = (TextAnchor)4;
				val5.fontSize = 22;
				val5.fontStyle = (FontStyle)1;
				val5.normal.textColor = Color.red;
				if ((Object)(object)GUI.skin != (Object)null && (Object)(object)GUI.skin.font != (Object)null)
				{
					val5.font = GUI.skin.font;
				}
				GUIStyle val6 = new GUIStyle(val5);
				val6.normal.textColor = new Color(0f, 0f, 0f, 0.8f);
				GUI.Label(new Rect(num5 - 248f, num6 - 48f, 500f, 100f), cooldownWarningText, val6);
				GUI.Label(new Rect(num5 - 250f, num6 - 50f, 500f, 100f), cooldownWarningText, val5);
			}
		}

		private void InitializeStyles()
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Expected O, but got Unknown
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			windowStyle = new GUIStyle(GUI.skin.window);
			windowStyle.fontSize = 16;
			windowStyle.fontStyle = (FontStyle)1;
			buttonStyle = new GUIStyle(GUI.skin.button);
			buttonStyle.fontSize = 13;
			buttonStyle.padding = new RectOffset(5, 5, 5, 5);
			buttonStyle.fixedHeight = 30f;
			labelStyle = new GUIStyle(GUI.skin.label);
			labelStyle.fontSize = 12;
			labelStyle.alignment = (TextAnchor)1;
			labelStyle.wordWrap = true;
			stylesInitialized = true;
		}

		private void DrawWindow(int windowID)
		{
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			if ((Object)(object)currentInjectionCase.Slots == (Object)null || currentInjectionCase.Slots.Count == 0)
			{
				GUILayout.Label("注射器收纳包没有槽位!", labelStyle, Array.Empty<GUILayoutOption>());
				GUILayout.EndVertical();
				GUI.DragWindow();
				return;
			}
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			for (int i = 0; i < 6; i++)
			{
				GUILayout.BeginVertical((GUILayoutOption[])(object)new GUILayoutOption[1] { GUILayout.Width(120f) });
				GUILayout.FlexibleSpace();
				if (i < currentInjectionCase.Slots.Count)
				{
					Slot slotByIndex = currentInjectionCase.Slots.GetSlotByIndex(i);
					Item val = ((slotByIndex != null) ? slotByIndex.Content : null);
					if ((Object)(object)val != (Object)null)
					{
						GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
						GUILayout.FlexibleSpace();
						if ((Object)(object)val.Icon != (Object)null)
						{
							Texture2D texture = val.Icon.texture;
							if ((Object)(object)texture != (Object)null)
							{
								GUILayout.Box((Texture)(object)texture, (GUILayoutOption[])(object)new GUILayoutOption[2]
								{
									GUILayout.Width(64f),
									GUILayout.Height(64f)
								});
							}
							else
							{
								GUILayout.Box("", (GUILayoutOption[])(object)new GUILayoutOption[2]
								{
									GUILayout.Width(64f),
									GUILayout.Height(64f)
								});
							}
						}
						else
						{
							GUILayout.Box("", (GUILayoutOption[])(object)new GUILayoutOption[2]
							{
								GUILayout.Width(64f),
								GUILayout.Height(64f)
							});
						}
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						string text = $"{i + 1}. {val.DisplayName}";
						if (val.StackCount > 1)
						{
							text += $" x{val.StackCount}";
						}
						if (val.UseDurability && val.MaxDurability > 0f)
						{
							int num = (int)(val.Durability / val.MaxDurability * 100f);
							text += $" ({num}%)";
						}
						GUILayout.Label(text, labelStyle, Array.Empty<GUILayoutOption>());
						GUILayout.Space(3f);
						if (GUILayout.Button("使用", buttonStyle, Array.Empty<GUILayoutOption>()))
						{
							UseSyringe(val);
						}
					}
					else
					{
						GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
						GUILayout.FlexibleSpace();
						GUILayout.Box("", (GUILayoutOption[])(object)new GUILayoutOption[2]
						{
							GUILayout.Width(64f),
							GUILayout.Height(64f)
						});
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.Label($"{i + 1}. (空)", labelStyle, Array.Empty<GUILayoutOption>());
						GUILayout.Space((buttonStyle.fixedHeight > 0f) ? (buttonStyle.fixedHeight + 3f) : 33f);
					}
				}
				else
				{
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.FlexibleSpace();
					GUILayout.Box("", (GUILayoutOption[])(object)new GUILayoutOption[2]
					{
						GUILayout.Width(64f),
						GUILayout.Height(64f)
					});
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.Label($"{i + 1}. (空)", labelStyle, Array.Empty<GUILayoutOption>());
					GUILayout.Space(33f);
				}
				GUILayout.FlexibleSpace();
				GUILayout.EndVertical();
				if (i < 5)
				{
					GUILayout.Space(8f);
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			GUI.DragWindow();
		}

		private void UseSyringe(Item syringe)
		{
			if ((Object)(object)syringe == (Object)null)
			{
				Debug.LogWarning((object)"[SmartInjectors.UI] 尝试使用null针剂");
				return;
			}
			CharacterMainControl main = CharacterMainControl.Main;
			if ((Object)(object)main == (Object)null)
			{
				Debug.LogError((object)"[SmartInjectors.UI] 无法获取主角色引用");
				return;
			}
			if ((Object)(object)syringe.UsageUtilities == (Object)null)
			{
				Debug.LogWarning((object)("[SmartInjectors.UI] 针剂没有UsageUtilities组件: " + syringe.DisplayName));
				return;
			}
			if (!syringe.UsageUtilities.IsUsable(syringe, (object)main))
			{
				Debug.LogWarning((object)("[SmartInjectors.UI] 针剂当前不可使用: " + syringe.DisplayName));
				return;
			}
			Debug.Log((object)string.Format("{0} 使用针剂: {1} (TypeID: {2})", "[SmartInjectors.UI]", syringe.DisplayName, syringe.TypeID));
			main.UseItem(syringe);
			Debug.Log((object)"[SmartInjectors.UI] 针剂使用完成，UI保持打开状态");
		}

		public void HandleInput()
		{
			UpdatePromptAndResults();
			if (!isVisible)
			{
				return;
			}
			if (Input.GetKeyDown((KeyCode)96))
			{
				HandleQuickInjectKey();
			}
			if (Input.GetMouseButtonDown(1))
			{
				Debug.Log((object)"[SmartInjectors.UI] 鼠标右键按下，关闭UI");
				Hide();
			}
			for (int i = 0; i < 6; i++)
			{
				if (Input.GetKeyDown((KeyCode)(49 + i)))
				{
					Debug.Log((object)string.Format("{0} [Update] 检测到数字键 {1}，直接使用针剂", "[SmartInjectors.UI]", i + 1));
					QuickUseSyringeAtSlot(i);
				}
			}
		}

		private void HandleKeyboardEventInGUI()
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Invalid comparison between Unknown and I4
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (!isVisible)
			{
				return;
			}
			Event current = Event.current;
			if ((int)current.type != 4)
			{
				return;
			}
			for (int i = 0; i < 6; i++)
			{
				KeyCode val = (KeyCode)(49 + i);
				if (current.keyCode == val)
				{
					Debug.Log((object)string.Format("{0} OnGUI捕获数字键 {1} (消费事件防止游戏快捷栏响应)", "[SmartInjectors.UI]", i + 1));
					QuickUseSyringeAtSlot(i);
					current.Use();
					break;
				}
			}
		}

		private void QuickUseSyringeAtSlot(int slotIndex)
		{
			Debug.Log((object)string.Format("{0} QuickUseSyringeAtSlot 被调用，槽位索引: {1}", "[SmartInjectors.UI]", slotIndex));
			if ((Object)(object)currentInjectionCase == (Object)null || (Object)(object)currentInjectionCase.Slots == (Object)null)
			{
				Debug.LogWarning((object)"[SmartInjectors.UI] 收纳包或槽位为null，无法使用");
				return;
			}
			if (slotIndex >= currentInjectionCase.Slots.Count)
			{
				Debug.LogWarning((object)string.Format("{0} 槽位索引超出范围: {1} >= {2}", "[SmartInjectors.UI]", slotIndex, currentInjectionCase.Slots.Count));
				return;
			}
			Slot slotByIndex = currentInjectionCase.Slots.GetSlotByIndex(slotIndex);
			if (slotByIndex == null || (Object)(object)slotByIndex.Content == (Object)null)
			{
				Debug.Log((object)string.Format("{0} 槽位 {1} 为空", "[SmartInjectors.UI]", slotIndex + 1));
				return;
			}
			Debug.Log((object)string.Format("{0} 准备使用槽位 {1} 的针剂: {2}", "[SmartInjectors.UI]", slotIndex + 1, slotByIndex.Content.DisplayName));
			UseSyringe(slotByIndex.Content);
		}

		private void UpdatePromptAndResults()
		{
			if (showQuickInjectPrompt && Time.time - promptShowTime > 5f)
			{
				showQuickInjectPrompt = false;
				Debug.Log((object)"[SmartInjectors.UI] 一键注射提示超时消失");
			}
			if (showInjectionResult && Time.time - resultShowTime > 3f)
			{
				showInjectionResult = false;
			}
			if (showCooldownWarning && Time.time - cooldownWarningTime > 1.5f)
			{
				showCooldownWarning = false;
			}
		}

		private void HandleQuickInjectKey()
		{
			float num = Time.time - lastQuickInjectTime;
			float num2 = 60f - num;
			if (num2 > 0f)
			{
				ShowCooldownWarning(num2);
			}
			else if (!showQuickInjectPrompt)
			{
				showQuickInjectPrompt = true;
				promptShowTime = Time.time;
				Debug.Log((object)"[SmartInjectors.UI] 显示一键注射提示");
			}
			else
			{
				showQuickInjectPrompt = false;
				ExecuteQuickInject();
			}
		}

		private void ShowCooldownWarning(float remainingSeconds)
		{
			showCooldownWarning = true;
			cooldownWarningTime = Time.time;
			cooldownWarningText = $"一键注射冷却中！还有 {Mathf.CeilToInt(remainingSeconds)} 秒";
			Debug.Log((object)("[SmartInjectors.UI] " + cooldownWarningText));
		}

		private void ExecuteQuickInject()
		{
			CharacterMainControl main = CharacterMainControl.Main;
			if ((Object)(object)main == (Object)null)
			{
				Debug.LogError((object)"[SmartInjectors.UI] 无法获取主角色引用");
				return;
			}
			if ((Object)(object)currentInjectionCase == (Object)null || (Object)(object)currentInjectionCase.Slots == (Object)null)
			{
				Debug.LogError((object)"[SmartInjectors.UI] 收纳包无效");
				return;
			}
			Debug.Log((object)"[SmartInjectors.UI] ========== 开始执行一键注射 ==========");
			HashSet<int> hashSet = new HashSet<int>();
			List<string> list = new List<string>();
			for (int i = 0; i < 6 && i < currentInjectionCase.Slots.Count; i++)
			{
				Slot slotByIndex = currentInjectionCase.Slots.GetSlotByIndex(i);
				if (slotByIndex == null || (Object)(object)slotByIndex.Content == (Object)null)
				{
					Debug.Log((object)string.Format("{0} 槽位 {1} 为空", "[SmartInjectors.UI]", i + 1));
					continue;
				}
				Item content = slotByIndex.Content;
				Debug.Log((object)string.Format("{0} 检查槽位 {1}: {2} (TypeID: {3})", "[SmartInjectors.UI]", i + 1, content.DisplayName, content.TypeID));
				if ((Object)(object)content.UsageUtilities == (Object)null)
				{
					Debug.Log((object)string.Format("{0} 跳过 {1} (TypeID: {2}) - 没有UsageUtilities", "[SmartInjectors.UI]", content.DisplayName, content.TypeID));
					continue;
				}
				if (hashSet.Contains(content.TypeID))
				{
					Debug.Log((object)string.Format("{0} 跳过 {1} (TypeID: {2}) - TypeID重复", "[SmartInjectors.UI]", content.DisplayName, content.TypeID));
					continue;
				}
				Debug.Log((object)string.Format("{0} 尝试注射: {1} (TypeID: {2})", "[SmartInjectors.UI]", content.DisplayName, content.TypeID));
				if (TryInjectSyringe(main, content))
				{
					hashSet.Add(content.TypeID);
					list.Add(content.DisplayName);
				}
			}
			if (list.Count > 0)
			{
				ShowInjectionResult(list);
				lastQuickInjectTime = Time.time;
				Debug.Log((object)string.Format("{0} 一键注射完成，共注射 {1} 种针剂", "[SmartInjectors.UI]", list.Count));
			}
			else
			{
				Debug.Log((object)"[SmartInjectors.UI] 没有可用的针剂");
			}
		}

		private bool TryInjectSyringe(CharacterMainControl character, Item syringe)
		{
			try
			{
				if (!syringe.UsageUtilities.IsUsable(syringe, (object)character))
				{
					Debug.LogWarning((object)("[SmartInjectors.UI] " + syringe.DisplayName + " 当前不可用"));
					return false;
				}
				syringe.UsageUtilities.Use(syringe, (object)character);
				if (syringe.Stackable && syringe.StackCount > 1)
				{
					int stackCount = syringe.StackCount;
					syringe.StackCount = stackCount - 1;
				}
				else
				{
					for (int i = 0; i < currentInjectionCase.Slots.Count; i++)
					{
						Slot slotByIndex = currentInjectionCase.Slots.GetSlotByIndex(i);
						if (slotByIndex != null && (Object)(object)slotByIndex.Content == (Object)(object)syringe)
						{
							slotByIndex.Unplug();
							break;
						}
					}
				}
				Debug.Log((object)("[SmartInjectors.UI] 成功注射: " + syringe.DisplayName));
				return true;
			}
			catch (Exception ex)
			{
				Debug.LogError((object)("[SmartInjectors.UI] 注射 " + syringe.DisplayName + " 时出错: " + ex.Message));
				return false;
			}
		}

		private void ShowInjectionResult(List<string> injectedNames)
		{
			showInjectionResult = true;
			resultShowTime = Time.time;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("已注射:");
			foreach (string injectedName in injectedNames)
			{
				stringBuilder.AppendLine("• " + injectedName);
			}
			injectionResultText = stringBuilder.ToString();
		}
	}
	public static class ItemTypeIDs
	{
		public const int INJECTION_CASE = 882;

		public const int SYRINGE_YELLOW = 137;

		public const int SYRINGE_BLACK = 395;

		public const int SYRINGE_WEIGHT = 398;

		public const int SYRINGE_ELECTRIC_RESIST = 408;

		public const int SYRINGE_HOT_BLOOD = 438;

		public const int SYRINGE_HARDENING = 797;

		public const int SYRINGE_ENDURANCE = 798;

		public const int SYRINGE_MELEE = 800;

		public const int SYRINGE_WEAK_STORM_PROTECTION = 856;

		public const int SYRINGE_TEST_STORM_PROTECTION = 857;

		public const int SYRINGE_STRONG_WINGS = 872;

		public const int SYRINGE_RECOVERY = 875;

		public const int SYRINGE_FIRE_RESIST = 1070;

		public const int SYRINGE_POISON_RESIST = 1071;

		public const int SYRINGE_SPACE_RESIST = 1072;

		public const int SYRINGE_HEMOSTATIC = 1247;

		public static bool IsSyringe(int typeID)
		{
			if (typeID != 137 && typeID != 395 && typeID != 398 && typeID != 408 && typeID != 438 && typeID != 797 && typeID != 798 && typeID != 800 && typeID != 856 && typeID != 857 && typeID != 872 && typeID != 875 && typeID != 1070 && typeID != 1071 && typeID != 1072)
			{
				return typeID == 1247;
			}
			return true;
		}
	}
	public class ModBehaviour : ModBehaviour
	{
		private const string LOG_PREFIX = "[SmartInjectors]";

		private bool hasAnalyzed;

		private InjectionCaseUI injectionCaseUI;

		private void Awake()
		{
			Debug.Log((object)"[SmartInjectors] ==========================================");
			Debug.Log((object)"[SmartInjectors] Smart Injectors Mod 正在加载...");
			Debug.Log((object)"[SmartInjectors] Version: 1.0.0");
			Debug.Log((object)"[SmartInjectors] ==========================================");
		}

		private void Start()
		{
			Debug.Log((object)"[SmartInjectors] Mod 启动成功!");
			Debug.Log((object)"[SmartInjectors] 按 F9 键运行物品分析工具");
			Debug.Log((object)"[SmartInjectors] 按 F10 键重新运行分析");
			InitializeMod();
		}

		private void InitializeMod()
		{
			Debug.Log((object)"[SmartInjectors] 开始初始化 Smart Injectors 功能...");
			injectionCaseUI = new InjectionCaseUI();
			Debug.Log((object)"[SmartInjectors] 注射器收纳包UI已创建");
			Debug.Log((object)"[SmartInjectors] 快捷栏监听已启用");
			Debug.Log((object)"[SmartInjectors] 提示: 按数字键1-6使用快捷栏中的注射器收纳包");
			Debug.Log((object)"[SmartInjectors] 初始化完成!");
		}

		private void Update()
		{
			if (injectionCaseUI != null)
			{
				injectionCaseUI.HandleInput();
				if (!injectionCaseUI.IsVisible)
				{
					CheckShortcutKeys();
				}
			}
			if (Input.GetKeyDown((KeyCode)290) && !hasAnalyzed)
			{
				AnalyzeAllItems();
				hasAnalyzed = true;
			}
			if (Input.GetKeyDown((KeyCode)291))
			{
				hasAnalyzed = false;
				AnalyzeAllItems();
			}
		}

		private void CheckShortcutKeys()
		{
			if (injectionCaseUI != null && injectionCaseUI.IsVisible)
			{
				return;
			}
			for (int i = 0; i < 6; i++)
			{
				if (Input.GetKeyDown((KeyCode)(49 + i)))
				{
					OnShortcutKeyPressed(i);
				}
			}
		}

		private void OnShortcutKeyPressed(int slotIndex)
		{
			try
			{
				Item val = ItemShortcut.Get(slotIndex);
				if ((Object)(object)val == (Object)null)
				{
					Debug.Log((object)string.Format("{0} 快捷栏 {1} 为空", "[SmartInjectors]", slotIndex + 1));
					return;
				}
				Debug.Log((object)string.Format("{0} 检测到快捷栏 {1} 使用: {2} (TypeID: {3})", "[SmartInjectors]", slotIndex + 1, val.DisplayName, val.TypeID));
				if (val.TypeID == 882)
				{
					Debug.Log((object)"[SmartInjectors] 检测到注射器收纳包，显示UI");
					injectionCaseUI.Show(val);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError((object)("[SmartInjectors] 处理快捷键时出错: " + ex.Message));
				Debug.LogError((object)("[SmartInjectors] 堆栈: " + ex.StackTrace));
			}
		}

		private void AnalyzeAllItems()
		{
			Debug.Log((object)"[SmartInjectors] ==========================================");
			Debug.Log((object)"[SmartInjectors] 开始分析游戏物品...");
			Debug.Log((object)"[SmartInjectors] ==========================================");
			try
			{
				ItemAssetsCollection instance = ItemAssetsCollection.Instance;
				if ((Object)(object)instance == (Object)null)
				{
					Debug.LogError((object)"[SmartInjectors] 无法获取 ItemAssetsCollection!");
					return;
				}
				List<Entry> entries = instance.entries;
				Debug.Log((object)string.Format("{0} 找到 {1} 个物品", "[SmartInjectors]", entries.Count));
				Debug.Log((object)"[SmartInjectors] ");
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				List<string> list3 = new List<string>();
				foreach (Entry item4 in entries)
				{
					if (item4 != null && !((Object)(object)item4.prefab == (Object)null))
					{
						Item prefab = item4.prefab;
						int typeID = item4.typeID;
						string displayName = prefab.DisplayName;
						TagCollection tags = prefab.Tags;
						bool flag = (Object)(object)prefab.Slots != (Object)null && prefab.Slots.Count > 0;
						int num = (flag ? prefab.Slots.Count : 0);
						bool num2 = tags.Contains("Medical") || displayName.ToLower().Contains("medical") || displayName.ToLower().Contains("syringe") || displayName.ToLower().Contains("injection") || displayName.Contains("药") || displayName.Contains("针");
						if (displayName.ToLower().Contains("injection") || displayName.ToLower().Contains("syringe") || displayName.Contains("注射") || displayName.Contains("针剂"))
						{
							string item = $"TypeID: {typeID}, 名称: {displayName}, 槽位: {num}";
							list3.Add(item);
						}
						if (num2)
						{
							string item2 = $"TypeID: {typeID}, 名称: {displayName}";
							list.Add(item2);
						}
						if (flag && num == 6)
						{
							string item3 = $"TypeID: {typeID}, 名称: {displayName}, 槽位: {num}, 重量: {prefab.UnitSelfWeight}kg";
							list2.Add(item3);
						}
					}
				}
				Debug.Log((object)"[SmartInjectors] ");
				Debug.Log((object)"[SmartInjectors] === 注射器/针剂相关物品 ===");
				if (list3.Count > 0)
				{
					foreach (string item5 in list3)
					{
						Debug.Log((object)("[SmartInjectors]   " + item5));
					}
				}
				else
				{
					Debug.Log((object)"[SmartInjectors]   未找到(可能使用本地化名称)");
				}
				Debug.Log((object)"[SmartInjectors] ");
				Debug.Log((object)"[SmartInjectors] === 6槽位容器物品 (可能是Injection Case) ===");
				if (list2.Count > 0)
				{
					foreach (string item6 in list2)
					{
						Debug.Log((object)("[SmartInjectors]   " + item6));
					}
				}
				else
				{
					Debug.Log((object)"[SmartInjectors]   未找到");
				}
				Debug.Log((object)"[SmartInjectors] ");
				Debug.Log((object)string.Format("{0} === 医疗相关物品 (共{1}个) ===", "[SmartInjectors]", list.Count));
				foreach (string item7 in list)
				{
					Debug.Log((object)("[SmartInjectors]   " + item7));
				}
				Debug.Log((object)"[SmartInjectors] ");
				Debug.Log((object)"[SmartInjectors] ==========================================");
				Debug.Log((object)"[SmartInjectors] 分析完成!");
				Debug.Log((object)"[SmartInjectors] 日志文件位置: %AppData%\\..\\LocalLow\\TeamSoda\\Escape From Duckov\\Player.log");
				Debug.Log((object)"[SmartInjectors] ==========================================");
			}
			catch (Exception ex)
			{
				Debug.LogError((object)("[SmartInjectors] 分析出错: " + ex.Message));
				Debug.LogError((object)("[SmartInjectors] 堆栈: " + ex.StackTrace));
			}
		}

		private void OnGUI()
		{
			if (injectionCaseUI != null)
			{
				injectionCaseUI.DrawGUI();
			}
		}

		private void OnDisable()
		{
			Debug.Log((object)"[SmartInjectors] Mod 被禁用");
			if (injectionCaseUI != null && injectionCaseUI.IsVisible)
			{
				injectionCaseUI.Hide();
			}
		}

		private void OnDestroy()
		{
			Debug.Log((object)"[SmartInjectors] Mod 被卸载,清理资源...");
			if (injectionCaseUI != null && injectionCaseUI.IsVisible)
			{
				injectionCaseUI.Hide();
			}
			injectionCaseUI = null;
		}
	}
}
namespace SmartInjectors.Tools
{
	public class GameItemAnalyzer : ModBehaviour
	{
		private bool hasAnalyzed;

		private void Start()
		{
			Debug.Log((object)"[SmartInjectors.Analyzer] 物品分析工具已加载");
			Debug.Log((object)"[SmartInjectors.Analyzer] 按 F9 键分析游戏物品");
		}

		private void Update()
		{
			if (Input.GetKeyDown((KeyCode)290) && !hasAnalyzed)
			{
				AnalyzeAllItems();
				hasAnalyzed = true;
			}
			if (Input.GetKeyDown((KeyCode)291))
			{
				hasAnalyzed = false;
				AnalyzeAllItems();
			}
		}

		private void AnalyzeAllItems()
		{
			Debug.Log((object)"[SmartInjectors.Analyzer] ==========================================");
			Debug.Log((object)"[SmartInjectors.Analyzer] 开始分析游戏物品...");
			Debug.Log((object)"[SmartInjectors.Analyzer] ==========================================");
			try
			{
				ItemAssetsCollection instance = ItemAssetsCollection.Instance;
				if ((Object)(object)instance == (Object)null)
				{
					Debug.LogError((object)"[SmartInjectors.Analyzer] 无法获取 ItemAssetsCollection!");
					return;
				}
				List<Entry> entries = instance.entries;
				Debug.Log((object)$"[SmartInjectors.Analyzer] 找到 {entries.Count} 个物品");
				Debug.Log((object)"[SmartInjectors.Analyzer] ");
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				List<string> list3 = new List<string>();
				foreach (Entry item4 in entries)
				{
					if (item4 != null && !((Object)(object)item4.prefab == (Object)null))
					{
						Item prefab = item4.prefab;
						int typeID = item4.typeID;
						string displayName = prefab.DisplayName;
						TagCollection tags = prefab.Tags;
						bool flag = (Object)(object)prefab.Slots != (Object)null && prefab.Slots.Count > 0;
						int num = (flag ? prefab.Slots.Count : 0);
						bool num2 = tags.Contains("Medical") || displayName.ToLower().Contains("medical") || displayName.ToLower().Contains("syringe") || displayName.ToLower().Contains("injection") || displayName.ToLower().Contains("药") || displayName.ToLower().Contains("针");
						if (displayName.ToLower().Contains("injection") || displayName.ToLower().Contains("syringe") || displayName.Contains("注射") || displayName.Contains("针剂"))
						{
							string item = $"TypeID: {typeID}, 名称: {displayName}, 槽位: {num}";
							list3.Add(item);
						}
						if (num2)
						{
							string item2 = $"TypeID: {typeID}, 名称: {displayName}";
							list.Add(item2);
						}
						if (flag && num == 6)
						{
							string item3 = $"TypeID: {typeID}, 名称: {displayName}, 槽位: {num}, 重量: {prefab.UnitSelfWeight}kg";
							list2.Add(item3);
						}
					}
				}
				Debug.Log((object)"[SmartInjectors.Analyzer] ");
				Debug.Log((object)"[SmartInjectors.Analyzer] === 注射器/针剂相关物品 ===");
				if (list3.Count > 0)
				{
					foreach (string item5 in list3)
					{
						Debug.Log((object)("[SmartInjectors.Analyzer]   " + item5));
					}
				}
				else
				{
					Debug.Log((object)"[SmartInjectors.Analyzer]   未找到(可能使用本地化名称)");
				}
				Debug.Log((object)"[SmartInjectors.Analyzer] ");
				Debug.Log((object)"[SmartInjectors.Analyzer] === 6槽位容器物品 (可能是Injection Case) ===");
				if (list2.Count > 0)
				{
					foreach (string item6 in list2)
					{
						Debug.Log((object)("[SmartInjectors.Analyzer]   " + item6));
					}
				}
				else
				{
					Debug.Log((object)"[SmartInjectors.Analyzer]   未找到");
				}
				Debug.Log((object)"[SmartInjectors.Analyzer] ");
				Debug.Log((object)$"[SmartInjectors.Analyzer] === 医疗相关物品 (共{list.Count}个) ===");
				foreach (string item7 in list.Take(20))
				{
					Debug.Log((object)("[SmartInjectors.Analyzer]   " + item7));
				}
				if (list.Count > 20)
				{
					Debug.Log((object)$"[SmartInjectors.Analyzer]   ... 还有 {list.Count - 20} 个");
				}
				Debug.Log((object)"[SmartInjectors.Analyzer] ");
				Debug.Log((object)"[SmartInjectors.Analyzer] ==========================================");
				Debug.Log((object)"[SmartInjectors.Analyzer] 分析完成!");
				Debug.Log((object)"[SmartInjectors.Analyzer] 日志文件位置: %AppData%\\..\\LocalLow\\TeamSoda\\Escape From Duckov\\Player.log");
				Debug.Log((object)"[SmartInjectors.Analyzer] ==========================================");
			}
			catch (Exception ex)
			{
				Debug.LogError((object)("[SmartInjectors.Analyzer] 分析出错: " + ex.Message));
				Debug.LogError((object)("[SmartInjectors.Analyzer] 堆栈: " + ex.StackTrace));
			}
		}
	}
}
namespace SmartInjectors.Examples
{
	public class APIExamples : ModBehaviour
	{
		private void Start()
		{
			Debug.Log((object)"[SmartInjectors.Examples] API 使用示例已加载");
			RegisterItemUsageListeners();
			ExampleAccessItemCollection();
		}

		private void OnDestroy()
		{
			UnregisterItemUsageListeners();
		}

		private void RegisterItemUsageListeners()
		{
			UsageUtilities.OnItemUsedStaticEvent += OnAnyItemUsed;
			CharacterMainControl.OnMainCharacterStartUseItem += OnMainCharacterStartUseItem;
			Debug.Log((object)"[SmartInjectors.Examples] 物品使用监听器已注册");
		}

		private void UnregisterItemUsageListeners()
		{
			UsageUtilities.OnItemUsedStaticEvent -= OnAnyItemUsed;
			CharacterMainControl.OnMainCharacterStartUseItem -= OnMainCharacterStartUseItem;
		}

		private void OnAnyItemUsed(Item item)
		{
			Debug.Log((object)"[SmartInjectors.Examples] 物品被使用:");
			Debug.Log((object)("  - 名称: " + item.DisplayName));
			Debug.Log((object)$"  - TypeID: {item.TypeID}");
			Debug.Log((object)$"  - 堆叠数: {item.StackCount}");
		}

		private void OnMainCharacterStartUseItem(Item item)
		{
			Debug.Log((object)("[SmartInjectors.Examples] 主角开始使用: " + item.DisplayName));
		}

		private void ExampleAccessItemCollection()
		{
			ItemAssetsCollection instance = ItemAssetsCollection.Instance;
			if ((Object)(object)instance == (Object)null)
			{
				Debug.LogError((object)"[SmartInjectors.Examples] 无法获取 ItemAssetsCollection");
			}
			else
			{
				Debug.Log((object)$"[SmartInjectors.Examples] 游戏中共有 {instance.entries.Count} 个物品");
			}
		}

		private void ExampleAddCustomItem()
		{
		}

		private void ExampleRemoveCustomItem(Item customItemPrefab)
		{
		}

		private void ExampleAddBuffToCharacter()
		{
		}

		private void ExampleAccessItemSlots(Item containerItem)
		{
			if ((Object)(object)containerItem.Slots == (Object)null || containerItem.Slots.Count == 0)
			{
				Debug.Log((object)"[SmartInjectors] 该物品没有槽位");
				return;
			}
			Debug.Log((object)$"[SmartInjectors] 槽位数量: {containerItem.Slots.Count}");
			for (int i = 0; i < containerItem.Slots.Count; i++)
			{
				Slot val = containerItem.Slots[i];
				Debug.Log((object)$"[SmartInjectors] 槽位 {i}:");
				Debug.Log((object)("  - Key: " + val.Key));
				Debug.Log((object)("  - 内容: " + (((Object)(object)val.Content != (Object)null) ? val.Content.DisplayName : "空")));
			}
		}
	}
}
