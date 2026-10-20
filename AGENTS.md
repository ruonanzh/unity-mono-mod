# AGENTS.md

这个仓库是 **Unity Mono mod 的骨架仓**。接入一款游戏时，先回答下面 8 个问题，答案写进 `docs/`，再动手写 mod。

## 接入前必须回答的 8 个问题（每条都要带实测来源）

| # | 问题 | 怎么得到答案 |
|---|---|---|
| 1 | 游戏怎么加载 mod？（装配目录、入口类命名、有无清单文件） | 游戏官方文档 + 反编译 loader 相关类 |
| 2 | mod 产物放哪？目录名与入口名有什么关系？ | 同上；写错会静默不加载，必须实测一次 |
| 3 | 入口基类/接口叫什么？生命周期方法有哪些？ | `api-probe --managed <Managed> --dll '<程序集>.*' --action search --target Behaviour` 一类 |
| 4 | 资产放在哪种文件？（`.assets` / AssetBundle / 自研格式） | 看 `Data/` 目录结构 + `data-probe --action classes` |
| 5 | 物品/实体用什么**标识**？整数 id、字符串 key，还是资产引用？ | `data-probe --action export` 抽样 |
| 6 | 有哪些「容器」概念（商店、掉落、配方、任务）？各自的类名？ | `data-probe --action list/search` |
| 7 | 本地化怎么存？（CSV / json / 内嵌） | 看 `StreamingAssets` 或资源目录 |
| 8 | 已知坑：什么改动会静默失效？ | 实测 + 该游戏社区文档 |

## 硬规则

- **不猜**：类名、字段名、标识的形态一律现场查（`api-probe` / `data-probe`），并把命令写进 `docs/`。
- **不把判断写进公共代码**：像「哪块是本体、哪些要关、挂点在哪」这类结论，只放在该游戏的 `docs/` 与该游戏的示例代码里。
- **不假设 3D**：2D 游戏只用 `libs/core`，`libs/modules` 整段不启用。
- **一处改动一处验证**：改完必须能编译、能加载，并给出可复现的检查方式。
- **读代码的范围写在 `.pi/lib/decompile.json`**（`dump.assemblies` = 程序集名或通配），再让 `inspect_game_code` 生成并读取可 grep 的树。
- **工具不许写死游戏**：`tools/`、`.pi/lib/`、`.pi/extensions/` 里不得出现具体游戏的类名、目录名、程序集名；这些一律走 `game.json`。

## 目录职责

- `mods/_template/`：新 mod 的起点。复制后改三个名字（csproj `AssemblyName`、`ModBehaviour` namespace、`info.ini` name），三者必须一致。
- `libs/core/`：纯托管 helper。**不得**出现任何游戏类型。
- `libs/modules/`：可选能力（`gltf` 等）。可按游戏删除。
- `tools/`：离线读取与检查。工具里**不得**出现具体游戏的类名（由参数传入）。
- `docs/`：该游戏的数据地图与做法。
