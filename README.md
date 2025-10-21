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