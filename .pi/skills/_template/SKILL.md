---
name: replace-model
description: 玩家想**换某个对象的外观**时用（例如「把 X 换成这个模型」）。不适用：新增对象、只换图标。
---

# 换外观（<这个游戏>）

**目标**：一句话说清这个 SKILL 让 agent 做什么、不做什么。

## 1. 需要的输入（只有这几项，问清即可）

| 问 | 默认 |
|---|---|
| 要换哪个对象 | 不得默认，必须问清（写清怎么用 `data-probe` 查） |
| 用哪个模型 | 玩家给的文件；没有就先做预览图（见 `generate_image`） |

## 2. 做

1. 先看清目标对象的结构：`inspect_game_data action=transform --pattern <名字> --depth 3`（场景里的对象加 `file=levelN`）。
2. 写 `config.json`（形状见 `mods/_template/config.json`）。
3. 先 `validate_mod` 校验，再 `install_mod` 安装。

## 3. 验收

- 游戏里能看到新外观；
- 日志里出现本文指定的那一行（写清 grep 什么）。

## 4. 常见错误

| 现象 | 原因 | 修法 |
|---|---|---|
| 什么都没变 | 规则没命中目标 | 核对 `targets` / 标识字段 |
