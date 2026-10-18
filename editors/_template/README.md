# editors/_template

该游戏若需要「在浏览器里调模型」的工具（例如按挂点预览 GLB），照这个结构放一份。

```
editors/<tool-id>/
├─ .editor.tool.json   宿主读的清单：id / name / match（如 "*.glb"）/ entry / mode / readScope
├─ index.html          入口页面
├─ editor.js           编辑器逻辑
├─ three.bundle.js     **vendored** 的 three.js（见许可）
├─ assets/             参照模型（可选）
└─ THIRD_PARTY_LICENSES.md
```

三条约束：

1. **vendored，不走 CDN**：所有脚本用相对路径 `<script src>` 引入。目标机器不需要装 three.js、不需要 npm、不需要联网。
2. **保留许可**：three.js 是 MIT。`three.bundle.js` 顶部保留 `/*! … */` banner，`THIRD_PARTY_LICENSES.md` 写明版本与全文。
3. **升级方式**：从 npm 取该版本 → esbuild 打成 IIFE（保持 `window.THREE` 形态）→ 更新 banner 与许可文件里的版本号。

`.editor.tool.json` 的字段以宿主为准（本模板给的是常见形状）。
