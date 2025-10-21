# Duckov Modding 示例 (Duckov Modding Example)

## 文件夹结构
- decompile (反编译后的代码文件夹 / Decompiled code folder)
- full_decompile (完整解包后的代码文件夹 / Fully decompiled code folder)
- history (历史版本文件夹 / History folder)
- test (测试项目用文件夹 / Test folder)

## DLL解包方法 / DLL Decompile Method

### 使用ILSpy命令行工具一次性解包所有代码

1. 安装ILSpy命令行工具（只需安装一次）：
   ```
   dotnet tool install --global ilspycmd
   ```

2. 创建解包目录：
   ```
   mkdir full_decompile
   ```

3. 使用ILSpy解包DLL文件：
   ```
   ilspycmd "E:\Steam\steamapps\common\Escape from Duckov\Duckov_Data\Managed\TeamSoda.Duckov.Core.dll" -o decompile -p
   ```

   参数说明：
   - `-o decompile`：指定输出目录
   - `-p`：生成项目文件，便于在IDE中打开

4. 解包完成后，可以在`decompile`目录中查看所有解包后的源代码，并使用IDE打开`TeamSoda.Duckov.Core.csproj`项目文件。


### 解包其他DLL文件

如需解包其他DLL文件，使用相同的命令格式：
```
ilspycmd "DLL文件路径" -o 目标目录 -p
```

## 工作原理概述
[official](official/README.md)

## 使用dotnet编译Debug和Release版本

### 编译命令

使用dotnet命令行工具可以轻松区分编译Debug和Release版本：

1. **编译Debug版本**（默认配置）：
   ```
   dotnet build
   ```
   或明确指定：
   ```
   dotnet build -c Debug
   ```

2. **编译Release版本**：
   ```
   dotnet build -c Release
   ```

3. **发布Release版本**（优化后的可发布版本）：
   ```
   dotnet publish -c Release
   ```

### Debug和Release版本的区别

- **Debug版本**：
  - 包含调试信息
  - 未进行代码优化
  - 文件大小较大
  - 适合开发和调试阶段

- **Release版本**：
  - 不包含调试信息（默认情况下）
  - 进行了代码优化
  - 文件大小较小
  - 运行性能更好
  - 适合发布和分发

### 输出位置

编译后的DLL文件通常位于以下位置：
- Debug版本：`bin/Debug/netstandard2.1/YourModName.dll`
- Release版本：`bin/Release/netstandard2.1/YourModName.dll`

### 推荐做法

1. 开发阶段使用Debug版本，便于调试
2. 发布Mod前使用Release版本，确保性能最优
3. 发布时只需将Release版本的DLL文件、info.ini和preview.png打包

### 示例：完整编译流程

```bash
# 清理之前的构建
dotnet clean

# 编译Release版本
dotnet build -c Release

# 或者直接发布到指定目录
dotnet publish -c Release -o ./publish
```

# Duckov 游戏反编译代码分析

本文档分析了Duckov游戏反编译代码中各个文件和文件夹的作用。

## 目录结构

### decompile/
Duckov游戏的主要反编译代码目录，包含了游戏的核心功能实现。

## 核心系统文件

### ADSAimMarker.cs
处理ADS（瞄准镜）状态下的瞄准标记功能，控制玩家在瞄准时的视觉反馈。

### AICharacterController.cs
AI角色控制器，负责管理非玩家角色(NPC)的基本移动和行为逻辑。

### AIMainBrain.cs
AI主大脑组件，控制NPC的核心决策逻辑和行为模式。

### AISound.cs
AI声音系统，处理NPC产生的声音事件，用于吸引其他AI的注意或触发特定行为。

### AISpecialAttachmentBase.cs
AI特殊附件基类，定义了NPC可以装备的特殊物品的基本行为和属性。

### AISpecialAttachment_Shop.cs
商店NPC的特殊附件实现，处理商店相关的特殊功能和交互。

### AISpecialAttachment_SpawnItemOnCritKill.cs
暴击击杀时生成物品的特殊附件，实现特定NPC在被暴击击杀时掉落额外物品的功能。

### AI_PathControl.cs
AI路径控制组件，管理NPC的导航和寻路行为。

### ATMPanel.cs
ATM（自动取款机）面板UI组件，处理玩家与ATM的交互界面。

### ATMPanel_DrawPanel.cs
ATM取款面板子组件，专门处理取款功能的UI逻辑。

### ATMPanel_SavePanel.cs
ATM存款面板子组件，专门处理存款功能的UI逻辑。

### ATMView.cs
ATM视图组件，管理ATM的整体视觉表现和用户界面。

### AT_InteractWithMainCharacter.cs
与主角交互的动作任务，处理NPC与玩家角色的交互逻辑。

### AT_SetBlackScreen.cs
设置黑屏的动作任务，用于场景转换或过场动画中的黑屏效果。

### AT_SetVirtualCamera.cs
设置虚拟摄像机的动作任务，用于控制游戏中的摄像机切换和视角变化。

### AccessoryBase.cs
配件基类，定义了游戏内配件物品的基本行为和属性，处理配件与武器/角色的连接和初始化。

### Accessory_Lazer.cs
激光瞄准器配件实现，提供武器瞄准时的激光线显示功能，包括障碍物检测和命中点标记。

### ActionProgressHUD.cs
动作进度HUD组件，显示角色执行各种动作（如钓鱼、开锁等）的进度条，并提供取消操作的指示。

### Action_Fishing.cs
钓鱼动作实现，处理完整的钓鱼流程，包括选择鱼饵、等待鱼咬钩、收鱼等阶段的状态管理。

### AddBuffAction.cs
添加增益效果动作，用于给角色添加临时增益效果（如提升属性、特殊能力等）。

### AddToWishListButton.cs
添加到愿望清单按钮UI组件，允许玩家将想要的物品添加到愿望清单中。

### AimMarker.cs
瞄准标记组件，控制屏幕上的瞄准准星显示，包括准星散布、距离显示和击杀标记等功能。

### AimTargetFinder.cs
瞄准目标查找器，用于自动寻找和锁定瞄准目标。

### AimTypes.cs
瞄准类型定义，包含游戏中不同瞄准模式的枚举和配置。

### AudioEventProxy.cs
音频事件代理，用于处理游戏中的音频播放事件。

### BaseBGMSelector.cs
背景音乐选择器基类，管理游戏中的背景音乐播放逻辑。

### BaseSceneUtilities.cs
场景工具基类，提供场景加载和管理的通用功能。

### BezierSpline.cs
贝塞尔曲线实现，用于游戏中的路径规划和曲线运动。

### BitcoinMinerView.cs
比特币矿机视图组件，处理游戏内比特币挖矿功能的UI显示。

### BlueNoiseSetter.cs
蓝噪点设置器，用于图形渲染中的噪点效果。

### BoundaryGenerator.cs
边界生成器，用于创建游戏世界的边界和限制区域。

### BowAnimation.cs
弓箭动画控制器，处理弓类武器的动画效果。

### Breakable.cs
可破坏物体组件，定义可以被玩家破坏的物体行为。

### BuffVFX.cs
增益效果视觉特效，处理增益状态的视觉表现。

### BuilderViewInvoker.cs
建造视图调用器，用于激活和显示建造界面。

### BulletCountHUD.cs
子弹数量HUD显示，显示当前武器的剩余弹药数量。

### BulletPool.cs
子弹对象池，管理子弹实例的创建和回收，优化性能。

### BulletTypeDisplay.cs
子弹类型显示组件，用于UI中展示不同子弹类型。

### BulletTypeHUD.cs
子弹类型HUD，显示当前使用的子弹类型信息。

### BulletTypeInfo.cs
子弹类型信息，定义各种子弹的属性和特性。

### BulletTypeSelectButton.cs
子弹类型选择按钮，允许玩家切换不同类型的子弹。

## 重要文件夹分析

### CameraSystems/
摄像机系统文件夹，包含游戏摄像机的控制和管理功能。
- CameraPropertiesControl.cs：摄像机属性控制器，管理摄像机的各种参数设置。

### Duckov/
Duckov游戏核心系统文件夹，包含游戏的基础功能模块。
- AudioManager.cs：音频管理器，控制游戏中的音效和音乐播放。
- CheatMode.cs：作弊模式，用于开发和测试的特殊功能。
- CursorManager.cs：光标管理器，控制游戏中的光标显示和行为。
- EXPManager.cs：经验值管理器，处理角色升级和经验获取。
- GameMetaData.cs：游戏元数据，存储游戏的基本信息。
- ItemShortcut.cs：物品快捷键，管理物品的快速使用功能。

### Duckov.Buffs/
增益效果系统文件夹，处理角色临时增益状态。
- Buff.cs：增益效果基类，定义增益的基本行为和属性。
- CharacterBuffManager.cs：角色增益管理器，管理角色的所有增益效果。

### Duckov.Crops/
作物系统文件夹，处理游戏中的种植和收获功能。

### Duckov.Economy/
经济系统文件夹，管理游戏内的货币和交易。

### Duckov.UI/
用户界面系统文件夹，包含所有UI组件和交互逻辑。

### Duckov.Buildings/
- **BuildingManager.cs** - 建筑管理器，处理建筑的放置、移除和保存，管理建筑区域数据和建筑令牌
- **Building.cs** - 建筑基类，定义建筑的基本属性和行为
- **BuildingArea.cs** - 建筑区域类，管理特定区域内的建筑布局
- **BuildingBuyAndPlaceResults.cs** - 建筑购买和放置结果处理
- **BuildingDataCollection.cs** - 建筑数据集合，存储所有建筑的信息
- **BuildingEffect.cs** - 建筑效果系统，处理建筑对游戏环境的影响
- **BuildingInfo.cs** - 建筑信息类，包含建筑的详细属性
- **BuildingRotation.cs** - 建筑旋转系统，处理建筑的朝向

### Duckov.Crops/
- **CellDisplay.cs** - 作物单元格显示组件，处理作物在花园中的视觉效果
- **Crop.cs** - 作物系统核心，管理作物生长、浇水、成熟和收获逻辑
- **CropAnimator.cs** - 作物动画控制器，处理作物生长阶段和状态变化的动画效果
- **CropData.cs** - 作物数据结构，定义作物属性、生长时间和产出信息
- **CropDatabase.cs** - 作物数据库，存储所有作物类型的相关数据
- **CropInfo.cs** - 作物信息展示组件，提供作物详细信息的UI显示
- **Garden.cs** - 花园管理系统，包含作物网格、自动浇水和尺寸管理
- **IGardenAutoWaterProvider.cs** - 花园自动浇水提供者接口，定义自动浇水系统的标准
- **IGardenSizeAdder.cs** - 花园尺寸扩展器接口，用于扩展花园容量
- **ProductRanking.cs** - 产品评级系统，根据作物质量进行等级评定
- **SeedInfo.cs** - 种子信息组件，管理种子属性和种植要求

### Duckov.Economy/
- **EconomyManager.cs** - 经济系统管理器，处理金钱、物品解锁和交易逻辑
- **Cost.cs** - 成本结构体，定义物品和金钱的成本计算方式
- **StockShop.cs** - 商店系统，实现商品买卖、库存管理和价格调整

### Duckov.Achievements/
- **AchievementManager.cs** - 成就系统管理器，处理成就解锁条件和进度跟踪
- **AchievementDatabase.cs** - 成就数据库，存储所有成就的定义和描述
- **StatisticsManager.cs** - 统计数据管理器，记录玩家游戏行为和成就进度

### Duckov.Buffs/
- **Buff.cs** - Buff效果系统，管理增益/减益效果的属性、持续时间和特效
- **CharacterBuffManager.cs** - 角色Buff管理器，处理Buff的添加、移除和更新逻辑

### Duckov.Bitcoins/
- **BitcoinMiner.cs** - 比特币挖矿系统，实现基于性能的挖矿机制和产出计算

### Duckov.Effects/
- **DamageAction.cs** - 伤害效果动作，处理Buff造成的伤害计算和特效

### Duckov.MiniMaps/
- **IPointOfInterest.cs** - 兴趣点接口，定义地图标记的基本属性和行为
- **MapMarkerManager.cs** - 地图标记管理器，处理自定义地图标记的创建和管理
- **MapMarkerPOI.cs** - 地图标记兴趣点，实现玩家自定义地图标记功能
- **MiniMapCenter.cs** - 小地图中心点，定义各场景的小地图中心坐标
- **MiniMapCompass.cs** - 小地图指南针，显示玩家朝向和方向指示
- **MiniMapSettings.cs** - 小地图设置，管理小地图的配置和场景映射
- **PointsOfInterests.cs** - 兴趣点系统，管理地图上的各种兴趣点标记
- **SimplePointOfInterest.cs** - 简单兴趣点实现，提供基础的兴趣点功能

### 核心游戏系统
天气系统文件夹，处理游戏中的天气变化和效果。

## 核心游戏系统

### CameraArm.cs
摄像机臂组件，控制第三人称摄像机的位置、角度和距离，支持俯视视角切换。

### CameraMode.cs
摄像机模式控制器，管理游戏中的摄像机模式切换（如正常模式和摄影模式）。

### CameraShaker.cs
摄像机震动效果，处理爆炸、冲击等场景中的摄像机震动。

### Carriable.cs
可携带物品基类，定义可以被角色拾取和携带的物品行为。

### CharacterMainControl.cs
角色主控制器，是游戏中最核心的组件之一，管理角色的所有基本行为，包括移动、攻击、使用物品、装备管理等。

### Duckov.Quests/
- **Quest.cs** - 任务系统核心类，定义任务基本属性和状态
- **QuestManager.cs** - 任务管理器，控制任务的激活、完成和追踪
- **QuestTask_BubblePopper_Level.cs** - 泡泡射击关卡任务
- **QuestTask_GoldMiner_Level.cs** - 金矿挖掘关卡任务
- **QuestTask_Snake_Score.cs** - 贪吃蛇得分任务
- **QuestTask_UnlockBeacon.cs** - 解锁信标任务
- **QuestGiver.cs** - 任务发布者，负责分配任务给玩家
- **Condition.cs** - 任务条件类，定义任务完成条件
- **Reward.cs** - 任务奖励系统
- **Task.cs** - 任务子任务基类

### Duckov.Scenes/
- **MultiSceneCore.cs** - 多场景核心管理器，负责主场景和子场景的加载、卸载和切换，处理场景间传送和场景状态管理
- **MultiSceneLocation.cs** - 多场景位置结构体，定义场景中的特定位置点，用于场景间传送定位
- **MultiSceneTeleporter.cs** - 多场景传送器，实现场景间的传送功能，包含传送冷却时间和传送动画
- **SceneIDAttribute.cs** - 场景ID属性标记，用于标识场景ID字段
- **SceneLocationsProvider.cs** - 场景位置提供者，管理场景中的位置点，提供位置查询和路径解析功能
- **SubSceneEntry.cs** - 子场景条目类，定义子场景的基本信息，包括环境音效、室内/室外标识、缓存位置和传送器信息

### Duckov.Utilities/
- **CommonVariables.cs** - 通用变量管理器，提供全局变量存储和访问功能，支持float、int、bool和string类型的数据持久化
- **GameplayDataSettings.cs** - 游戏数据设置，包含游戏中的各种配置数据，如战利品数据、标签系统、预制体数据等
- **LootSpawner.cs** - 战利品生成器，负责在游戏世界中生成战利品，支持随机生成和固定生成两种模式
- **LootBoxLoader.cs** - 战利品箱加载器，处理战利品箱的加载和初始化
- **FishSpawner.cs** - 鱼类生成器，负责在水中生成鱼类
- **SetActiveByPlayerDistance.cs** - 根据玩家距离激活/停用对象，优化游戏性能

### 核心游戏系统

#### Duckov.Modding/
- **ModManager.cs** (437行) - 模组管理器，负责游戏模组的加载、激活和卸载。提供模组扫描、排序、优先级设置等功能，支持Steam创意工坊模组集成。使用单例模式管理全局模组状态，支持模组动态加载/卸载，提供模组优先级排序和状态持久化功能。
- **ModInfo.cs** (27行) - 模组信息结构体，存储模组的元数据信息，包括名称、显示名称、描述、预览图、DLL路径等。支持本地和Steam创意工坊模组的统一信息表示。
- **ModBehaviour.cs** (30行) - 模组行为基类，所有模组的主类需要继承此类，提供模组生命周期钩子方法。包含OnAfterSetup和OnBeforeDeactivate虚拟方法，允许模组在加载和卸载时执行自定义逻辑。
- **SteamWorkshopManager.cs** (265行) - Steam创意工坊管理器，处理与Steam创意工坊的交互。实现模组上传、下载、订阅管理功能，处理Steam API调用和模组安装验证。使用Steamworks API进行UGC(用户生成内容)管理，支持异步上传操作，提供上传进度监控和错误处理机制。

### Duckov.Options/
- **OptionsManager.cs** (102行) - 选项管理器，负责管理游戏的各种设置选项。处理鼠标灵敏度、音量、图形设置等配置的保存和加载，提供选项变更事件通知。使用ES3插件进行数据持久化，提供选项变更事件通知，支持自动备份和恢复功能确保数据安全。

#### 游戏管理核心
- **GameManager.cs** (161行) - 游戏管理器，作为游戏的核心管理类，负责协调所有主要系统。使用单例模式确保全局唯一性，管理音频、UI输入、游戏规则、暂停菜单、场景加载、黑屏、事件系统、玩家输入、夜视、模组管理、笔记索引和成就系统等核心组件。
- **LevelManager.cs** (726行) - 关卡管理器，负责关卡初始化、角色创建和场景管理。处理主角色和宠物的创建与初始化，管理战利品箱库存，支持多场景位置系统，提供关卡初始化事件通知和异步初始化流程。使用UniTask进行异步操作，支持关卡初始化状态跟踪和注释系统。
- **SceneLoader.cs** (300行) - 场景加载器，负责场景之间的异步加载和过渡。处理场景加载前的准备工作，管理加载界面和过渡效果，支持多场景位置系统和点击继续功能。使用UniTask进行异步操作，提供加载注释系统和事件通知机制。

#### UI系统
- **UIInputManager.cs** (380行) - UI输入管理器，负责处理所有UI相关的输入事件。管理导航、确认、取消、鼠标点击、物品拾取、物品使用、相机模式切换等多种输入操作，提供键盘修饰键状态检测和射线投射功能。使用Unity新输入系统，支持事件驱动的输入处理机制。
- **PauseMenu.cs** (38行) - 暂停菜单，继承自UIPanel基类，提供游戏暂停界面的显示和隐藏功能。支持静态方法调用，提供显示状态查询和切换功能，是游戏UI系统的基础组件之一。

#### 游戏规则系统
- **GameRulesManager.cs** (136行) - 游戏规则管理器，负责管理游戏难度和规则设置。处理预设规则集和自定义规则集的加载与保存，支持规则索引选择和规则变更通知。使用单例模式，通过SavesSystem进行数据持久化，提供规则集切换和自定义规则创建功能。
- **Ruleset.cs** (80行) - 规则集类，定义游戏的各种规则参数。包含对玩家伤害系数、敌人生命值系数、尸体生成、战争迷雾、高级减益模式、后坐力倍数、敌人反应时间、攻击间隔等参数的设置。使用LocalizationKey进行本地化支持，提供显示名称和描述的多语言功能。
- **RulesetFile.cs** (12行) - 规则集文件类，继承自ScriptableObject，用于在Unity编辑器中创建和存储规则集资源。通过CreateAssetMenu特性支持在Unity菜单中创建新的规则集文件，提供Data属性访问内部的Ruleset数据。
- **DifficultySelection.cs** (297行) - 难度选择界面，负责游戏难度选择的UI逻辑。处理难度选项的显示、解锁状态检查和选择确认，支持自定义难度创建和难度解锁条件验证。使用PrefabPool管理UI元素，提供本地化支持和异步操作流程。
- **RuleEntry_Float.cs** (61行) - 浮点数规则条目，负责处理浮点类型游戏规则的UI交互。使用SliderWithTextField组件提供滑块和文本输入功能，通过反射访问Ruleset字段，实现规则值的实时更新和显示。支持自定义规则模式下的值修改，并在非自定义模式下恢复为预设值。
- **RuleEntry_Int.cs** (61行) - 整数规则条目，负责处理整数类型游戏规则的UI交互。使用SliderWithTextField组件提供滑块和文本输入功能，通过反射访问Ruleset字段，实现规则值的实时更新和显示。支持自定义规则模式下的值修改，并在非自定义模式下恢复为预设值。
- **RuleEntry_Bool.cs** (45行) - 布尔值规则条目，继承自OptionsProviderBase，负责处理布尔类型游戏规则的UI交互。提供开关选项界面，通过反射访问Ruleset字段，使用本地化文本显示开/关状态，支持规则值的切换和保存。
  
  #### 玩家系统
- **PlayerStorage.cs** (242行) - 玩家存储系统，负责管理玩家物品存储和缓冲区。处理物品的添加、合并、保存和加载，支持存储容量动态计算和物品缓冲区管理。使用单例模式，通过UniTask处理异步加载操作，提供事件通知机制，与SavesSystem集成实现数据持久化。
- **CharacterMainControl.cs** (2263行) - 角色主控制器，管理玩家的核心行为和状态。处理移动、攻击、交互、装备、技能、生命值、体力、能量、水分等所有玩家属性，支持重量状态系统和多种动作状态管理。包含大量属性哈希值用于性能优化，提供武器切换、瞄准、奔跑、冲刺等动作控制，是游戏中最复杂的系统之一，协调玩家角色的所有行为和属性。
  
  ### Duckov.Weathers/
- **WeatherManager.cs** (154行) - 天气管理器，负责控制游戏中的天气系统。处理天气类型（晴天、多云、雨天、风暴）的生成和切换，支持强制天气设置和天气数据持久化。使用单例模式，通过Storm和Precipitation模块计算天气状态，提供缓存机制优化性能，与SavesSystem集成实现天气数据保存和加载。
- **Weather.cs** (10行) - 天气枚举定义，包含游戏中所有可能的天气类型：晴天(Sunny)、多云(Cloudy)、雨天(Rainy)、一级风暴(Stormy_I)和二级风暴(Stormy_II)。
- **Storm.cs** (74行) - 风暴系统，管理游戏中的风暴周期和强度。计算风暴等级、风暴ETA(预计到达时间)、风暴剩余百分比，提供多级风暴状态转换逻辑。使用时间偏移量和周期计算实现风暴的自然循环，包含休眠期、一级风暴和二级风暴三个阶段。
- **Precipitation.cs** (78行) - 降水系统，负责计算游戏中的降水强度和类型。使用柏林噪声算法生成自然的降水模式，支持多云和雨天阈值设置，提供频率、偏移和对比度参数调整。通过时间坐标和种子值确保降水模式的连续性和一致性。
  
  ### 核心游戏系统
- **GameClock.cs** (150行) - 游戏时钟系统，负责管理游戏内时间和实际游戏时间。处理游戏内时间流逝、天数计算、时间步进，支持时间缩放和存档时间读取。使用单例模式，通过SavesSystem实现时间数据持久化，提供静态属性访问当前时间、天数、小时、分钟等信息，触发游戏时钟步进事件。
- **GameManager.cs** (161行) - 游戏管理器单例，负责全局游戏状态管理。包含音频管理、UI输入管理、难度管理、暂停菜单、场景加载、黑屏过渡、事件系统、玩家输入、夜视系统、模组管理、笔记索引和成就系统等核心组件。使用单例模式，通过静态属性提供各子系统访问接口，处理游戏暂停状态和时间穿越检测。
- **LevelManager.cs** (726行) - 关卡管理器，处理关卡加载、角色创建、出口创建、爆炸效果、角色模型、AI系统、战争迷雾、时间控制、宠物系统和战利品箱等关卡相关功能。使用单例模式，管理关卡初始化流程、角色和宠物的创建与保存、战利品箱库存管理，支持多场景位置和关卡配置。
- **Health.cs** (465行) - 生命值系统，处理角色的生命值计算、伤害处理、护甲系统、元素抗性和死亡事件。支持不同团队和生命值显示，包含最大生命值和当前生命值属性、身体和头部护甲计算、元素因子（物理、火焰、毒、电、空间）处理、角色缓存和隐藏状态检测。使用事件系统（OnHealthChange、OnMaxHealthChange、OnDeadEvent、OnHurtEvent）通知生命值变化，支持无敌状态和死亡后自动销毁功能。
- **Movement.cs** (281行) - 角色移动系统，基于ECM2框架实现角色移动控制。处理行走、奔跑、瞄准转向和强制移动等移动状态，包含移动输入处理、速度计算（walkSpeed、runSpeed）、加速度控制（walkAcc、runAcc）和转向速度管理。支持瞄准方向设置、目标点瞄准、地面约束和物理重力影响，提供位置强制设置和移动状态检测（Moving、Running、StandStill）功能。
- **InputManager.cs** (904行) - 输入管理系统，负责处理玩家输入和设备控制。支持鼠标键盘和触摸设备输入，处理移动、瞄准、奔跑、交互、武器切换、开火、装弹、使用物品等多种输入操作。包含瞄准目标查找、后坐力处理、输入缓冲、输入阻止和输入设备切换等功能。使用Unity新输入系统，提供输入状态检测和输入冷却机制，支持武器槽位哈希值优化和输入事件回调。
- **GameCamera.cs** (278行) - 游戏摄像机系统，基于Cinemachine实现第三人称摄像机控制。处理摄像机跟随、瞄准视角切换、FOV调整和深度对焦效果。支持正常和边界两种瞄准模式，包含摄像机臂控制、瞄准偏移计算和防晕模式。使用Volume系统处理景深效果，提供摄像机位置更新事件和强制同步功能，与InputManager和CharacterMainControl紧密集成实现流畅的摄像机控制。
- **CharacterCreator.cs** (35行) - 角色创建系统，负责处理角色实例化和初始化。基于异步UniTask框架实现角色创建流程，包含角色预制体实例化、模型设置、物品装备和基础Buff添加。支持从ItemAssetsCollection异步加载物品实例，处理非突袭地图的基础Buff添加，提供角色创建失败时的资源清理机制。与GameplayDataSettings紧密集成，使用预制体和Buff配置数据。
- **CharacterMainControl.cs** (2263行) - 角色主控制系统，实现角色的核心功能和状态管理。处理角色移动、攻击、技能、物品使用、装备管理、生命值、体力、能量、水分、食物等全方位角色属性。包含重量状态系统（轻、正常、重、超重、过载）、武器切换、瞄准系统、Buff管理、伤害接收和角色动作控制。使用哈希值优化属性访问，支持团队设置、隐藏状态、角色预设和音频类型配置。与Movement、Health、ItemControl等多个子系统紧密集成，提供完整的角色控制API。
- **Buff.cs** (229行) - Buff效果系统基类，定义增益/减益效果的核心行为和属性。支持多层叠加、持续时间管理、独占标签系统和特效显示。包含多种Buff类型（出血、饥饿、口渴、重量、中毒、疼痛、电击、燃烧、空间、风暴保护、晕眩等），提供优先级机制处理同类型Buff冲突。使用Effect系统实现Buff效果，支持本地化显示和图标配置，提供Buff生命周期管理（Setup、Update、OutOfTime）和层级变化事件。
- **CharacterBuffManager.cs** (158行) - 角色Buff管理器，负责处理角色所有Buff的添加、移除和更新逻辑。使用List存储Buff实例，提供只读集合访问接口。支持按ID添加/移除Buff、按标签批量移除、Buff存在性查询和按标签获取Buff。实现独占Buff优先级处理、多层Buff管理和超时自动移除机制。使用事件系统通知Buff添加和移除，提供每帧更新处理Buff状态变化和生命周期管理。