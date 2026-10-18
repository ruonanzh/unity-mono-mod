# unity-mono-mod

**Unity Mono mod 的骨架仓**（模板）。每接入一款 Unity Mono 游戏，复制这个仓，改 `game.json` 与该游戏的文档。

方案与判据见设计仓 `plans/unity-mono-mod.md`。三条前提：

1. 目标游戏不一定是 3D，也不一定有模型槽位或骨骼。
2. 游戏事实（类名、字段、条目标识的形态、资源文件、加载方式）由**游戏自己给出**，这个仓库不预置。
3. 这个仓库只提供三样东西：**骨架**、**工具**（读资产 / 读 API）、**文档模板**。

## 目录

```
game.json          该游戏的常量（入口、sentinel、程序集、资产文件名、可选模块）
mods/_template/    新 mod 的样板：csproj + 入口类 + info.ini + config.json
libs/core/         纯托管 helper（无 3D、无游戏类型）：Json / PatchRegistry / ModelCache
libs/modules/      可选模块（按 game.json 的 modules 启用）：gltf 等
tools/data-probe   离线读资产（不依赖任何游戏类名）
tools/api-probe    离线读 DLL 的 API
scripts/           门禁：文档引用可达、中文提示词规范
editors/_template/  浏览器内调模型的编辑器模板（vendored three.js，不走 CDN）
docs/              该游戏的数据地图与做法（接入时由 agent 写）
```

## 接入一款新游戏

1. `cp -r mods/_template mods/<你的 mod 名>`，改三处名字（csproj 的 `AssemblyName`、`ModBehaviour` 的 namespace、`info.ini` 的 `name`）。三者必须一致，否则游戏静默不加载（这条是常见约定，具体以该游戏文档为准）。
2. 填 `game.json`。
3. 用 `tools/api-probe` 反编译该游戏 DLL，确认入口基类与数据结构；用 `tools/data-probe` 列类、抽样。
4. 把结论写进 `docs/` 与该游戏的 `AGENTS.md`，每条带实测来源。
5. 编译：`GAME_DIR=<游戏目录> dotnet build mods/<name>`。

## game.json 的字段

| 字段 | 用途 |
|---|---|
| `entry.installDir` / `managedDir` / `assetsDir` | 相对游戏目录的路径；`try_set_game_dir` 与探针都用它 |
| `sentinel` | 该游戏自带的程序集名，用来确认目录真的是这个游戏 |
| `assemblies` | 编译要引用的游戏程序集（`validate_mod` 会转成 `-p:GameAssemblies=…` 传给 csproj） |
| `actions` | 该游戏 config 允许的 action 名。**空数组 = 不校验 action 名**（骨架默认） |
| `checks` | 可选的数据检查（`itemClass` / `tagClass` / `merchantClass` / `assetsFile`…）。缺项时对应检查跳过 |
| `modules` | 启用的可选模块（如 `gltf`） |

## 3D 与 2D

`libs/modules/` 是可选的：2D 游戏把整个目录删掉，`libs/core/` 与工具照常工作。

## 工具

```bash
dotnet build tools/data-probe -c Release && dotnet build tools/api-probe -c Release
dotnet tools/data-probe/bin/Release/net8.0/data-probe.dll --data <Data 目录> --action classes
dotnet tools/api-probe/bin/Release/net8.0/api-probe.dll --managed <Managed 目录> --dll '<程序集名>.*' --action search --target Item
```

两个工具的用法、出口码与边界各自在 `tools/*/README.md`。

## 门禁

```bash
node scripts/check-doc-refs.mjs
node scripts/check-prompt-standard-zh.mjs
```
