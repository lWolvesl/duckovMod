# Duckov Modding 示例 (Duckov Modding Example)

_This is an example project for modding Escape From Duckov._

## 工作原理概述 / Overview

《逃离鸭科夫》的 Mod 模块会扫描并读取 Duckov_Data/Mods 文件夹中的各个子文件夹，以及 Steam 创意工坊已订阅物品的各个文件夹。通过文件夹中包含的 dll 文件，info.ini 和 preview.png 在游戏中展示、加载 mod。

The modding system of Escape From Duckov scans and reads the subfolders in the Duckov_Data/Mods folder, as well as the folders of subscribed items in the Steam Workshop. Mods are displayed and loaded in the game through the `dll` files, `info.ini`, and `preview.png` contained in these folders.

《逃离鸭科夫》会读取 info.ini 中的 name 参数，并以此作为 namespace 尝试加载名为 ModBehaviour 的类。例如，info.ini 中如果记载`name=MyMod`,则会加载`MyMod.dll`文件中的`MyMod.ModBehaviour`。

Escape From Duckov reads the name parameter in info.ini and uses it as a namespace to load a class named ModBehaviour. For example, if info.ini contains `name=MyMod`, it will load `MyMod.ModBehaviour` from the `MyMod.dll` file.

ModBehaviour 应继承自 Duckov.Modding.ModBehaviour。Duckov.Modding.ModBehaviour 是一个继承自 MonoBehaivour 的类。其中还包含了一些 mod 系统中需要使用的额外功能。在加载 mod 时,《逃离鸭科夫》会创建一个该 mod 的 GameObject 并通过调用 GameObject.AddComponent(Type) 的方式创建一个 ModBehaviour 的实例。Mod 作者可以通过编写 ModBehaviour 的 Start\Update 等 Unity 事件实现功能，也可以通过注册《逃离鸭科夫》中的其他事件实现功能。

ModBehaviour should inherit from `Duckov.Modding.ModBehaviour`. `Duckov.Modding.ModBehaviour` is a class that inherits from MonoBehaviour and includes some additional features needed in the mod system. When loading a mod, Escape From Duckov creates a GameObject for that mod and creates an instance of ModBehaviour by calling GameObject.AddComponent(Type). Mod authors can implement functionality by writing Unity events such as Start\Update in ModBehaviour, or by registering other events in Escape From Duckov.

## Mod 文件结构 / File Structure

将准备好的 Mod 文件夹放到《逃离鸭科夫》本体的 Duckov_Data/Mods 中，即可在游戏主界面的 Mods 界面加载该 Mod。
假设 Mod 的名字为"MyMod"。发布的文件结构应该如下：

Place the prepared Mod folder in `Duckov_Data/Mods` within the Escape From Duckov installation directory, and the Mod can be loaded in the Mods interface on the game's main menu.
Assuming the Mod's name is "MyMod", the published file structure should be as follows:

- MyMod (文件夹 / Folder)
  - MyMod.dll
  - info.ini
  - preview.png (正方形的预览图，建议使用 256*256 分辨率 / Square preview image, recommended resolution 256*256)

[Mod 文件夹示例 / Mod Folder Example](DisplayItemValue/ReleaseExample/DisplayItemValue/)

### info.ini

info.ini 应包含以下参数:

- name (mod 名称，主要用于加载 dll 文件)
- displayName (显示的名称)
- description（显示的描述）

info.ini should contain the following parameters:

- name (mod name, primarily used for loading the dll file)
- displayName (display name)
- description (display description)

info.ini 还可能包含以下参数:

- publishedFileId （记录本 Mod 在 steam 创意工坊的 id）

info.ini may also contain the following parameters:

- publishedFileId (records this Mod's ID in the Steam Workshop)

**注意：在上传 Steam Workshop 的时候，会复写 info.ini。info.ini 中原有的信息可能会因此丢失。所以不建议在 info.ini 中存储除以上项目之外的其他信息。**

**Note: When uploading to Steam Workshop, info.ini will be overwritten. Original information in info.ini may be lost as a result. Therefore, it is not recommended to store any information other than the above items in info.ini.**

## 配置 C# 工程 / Configuring C# Project

1. 在电脑上准备好《逃离鸭科夫》本体。
2. 创建一个 .Net Class Library 工程。
3. 配置工程参数。
   1. Target Framework
      - **TargetFramework 建议设置为 netstandard2.1。**
      - 注意删除 TargetFramework 不支持的功能，比如`<ImplicitUsings>`
   2. Reference Include
      - 将《逃离鸭科夫》的`\Duckov_Data\Managed\*.dll`添加到引用中。
      - 例：

      ```
        <ItemGroup>
          <Reference Include="$(DuckovPath)\Duckov_Data\Managed\TeamSoda.*" />
          <Reference Include="$(DuckovPath)\Duckov_Data\Managed\ItemStatsSystem.dll" />
          <Reference Include="$(DuckovPath)\Duckov_Data\Managed\Unity*" />
        </ItemGroup> 
      ```

4. 完成！现在在你 Mod 的 Namespace 中编写一个 ModBehaivour 的类。构建工程，即可得到你的 mod 的主要 dll。

### English Translation: 

1. Have Escape From Duckov installed on your computer.
2. Create a .NET Class Library project.
3. Configure project parameters.
   1. Target Framework
      - **It is recommended to set TargetFramework to netstandard2.1.**
      - Note: Remove features not supported by TargetFramework, such as `<ImplicitUsings>`
   2. Reference Include
      - Add `\Duckov_Data\Managed\*.dll` from Escape From Duckov to the references.
      - Example:

      ```
      <ItemGroup>
        <Reference Include="$(DuckovPath)\Duckov_Data\Managed\TeamSoda.*" />
        <Reference Include="$(DuckovPath)\Duckov_Data\Managed\ItemStatsSystem.dll" />
        <Reference Include="$(DuckovPath)\Duckov_Data\Managed\Unity*" />
      </ItemGroup> 
      ```

4. Done! Now write a ModBehaviour class in your Mod's Namespace. Build the project to get your mod's main dll.

csproj 文件示例 / csproj File Example: [DisplayItemValue.csproj](DisplayItemValue/DisplayItemValue.csproj)

## 其他 / Other

### Unity Package

使用 Unity 进行开发时，可以参考本仓库附带的 [manifest.json 文件](UnityFiles/manifest.json) 来选择 package。

When developing with Unity, you can refer to the [manifest.json file](UnityFiles/manifest.json) included in this repository to select packages.

### 自定义游戏物品 / Custom Game Items

- 可调用 `ItemStatsSystem.ItemAssetsCollection.AddDynamicEntry(Item prefab)` 添加自定义物品
- 可调用`ItemStatsSystem.ItemAssetsCollection.RemoveDynamicEntry(Item prefab)`将该 mod 物品移除
- 自定义物品的 prefab 上需要配置好 TypeID。避免与游戏本体和其他 MOD 冲突。
- 进入游戏时如果未加载对应 MOD，存档中的自定义物品会直接消失。

- Call `ItemStatsSystem.ItemAssetsCollection.AddDynamicEntry(Item prefab)` to add custom items
- Call `ItemStatsSystem.ItemAssetsCollection.RemoveDynamicEntry(Item prefab)` to remove the mod item
- Custom item prefabs need to have TypeID configured properly. Avoid conflicts with the base game and other MODs.
- If the corresponding MOD is not loaded when entering the game, custom items in the save file will disappear directly.

### 本地化 / Localization

- 可调用 `SodaCraft.Localizations.LocalizationManager.SetOverrideText(string key, string value)` 来覆盖显示本地化文本。
- 可借助 `SodaCraft.Localizations.LocalizationManager.OnSetLanguage:System.Action<SystemLanguage>` 事件来处理语言切换时的逻辑

- Call `SodaCraft.Localizations.LocalizationManager.SetOverrideText(string key, string value)` to override displayed localization text.
- Use the `SodaCraft.Localizations.LocalizationManager.OnSetLanguage:System.Action<SystemLanguage>` event to handle logic when switching languages

## 鸭科夫社区准则 / Duckov Community Rules

为了鸭科夫社区的长期健康与和谐发展，我们需要共同维护良好的创作环境。 因此，我们希望大家遵守以下规则：
1.禁止违反开发组以及 Steam 平台所在地区法律的内容，包括但不限于涉及政治、儿童色情、宣扬暴力恐怖等内容。
2.禁止严重侮辱角色或者扭曲剧情、意图在玩家社群内容引起反感和制造对立的内容，或者涉及到热门时事与现实人物等容易引发现实争议的内容。
3.禁止未经授权，使用受版权保护的游戏资源或其他第三方素材的内容。
4.禁止利用 Mod 引导至广告、募捐等商业或非官方性质的外部链接，或引导他人付费的行为。
5.使用AI内容的 Mod 需要标注。
对于在Steam创意工坊发布的 Mod，如果违反上述规则，我们可能会在不事先通知的情况下直接删除，并可能封禁相关创作者的权限。

To aid the long-term development of the Duckov community, we ask everyone to contribute to a positive creative environment. Please adhere to the following rules:
1.Content that violates the laws of the regions where the developer team and the Steam platform operate is strictly prohibited, including but not limited to content involving politics, child sexual exploitation, or the promotion of violence or terrorism.
2.Content that severely insults characters, distorts the story, or aims to cause discomfort, conflict, or controversy in the player community is prohibited. This also includes content related to current events or real-life individuals that may trigger real-world disputes.
3.Unauthorized use of copyrighted game assets or other third-party materials is prohibited.
4.Mods must not be used to direct players to advertisements, fundraising, payment requests, or other commercial or unofficial external links.
5.Mods containing AI-generated content must be clearly labeled.
For mods published on Steam Workshop, any violations of the above rules may result in removal without prior notice and may lead to suspension of the creator’s permissions.


