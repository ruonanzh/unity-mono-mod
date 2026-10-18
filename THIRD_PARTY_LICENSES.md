# 第三方组件与许可

本仓入库（git 跟踪）的第三方二进制。**全部为 MIT**。

游戏自己的 DLL 不在本表：它们是编译期引用（各 mod 的 csproj 用 `<Private>false</Private>`），既不随仓也不随 mod 分发。

## 清单一览

| 文件 | 组件 | 版本 | 许可 | 用途 |
|---|---|---|---|---|
| `tools/data-probe/lib/AssetsTools.NET.dll` | [AssetsTools.NET](https://github.com/nesrak1/AssetsTools.NET) | **3.0.0.0** | MIT | 离线读 Unity `.assets`：类、字段、序列化对象 |
| `tools/data-probe/lib/AssetsTools.NET.MonoCecil.dll` | AssetsTools.NET.MonoCecil | **1.0.0.0** | MIT | 上面的 Mono 脚本支持 |
| `tools/data-probe/lib/Mono.Cecil.dll` | [Mono.Cecil](https://github.com/jbevain/cecil) | **0.11.4.0** | MIT | IL / 程序集解析 |
| `tools/data-probe/lib/Mono.Cecil.Rocks.dll` | Mono.Cecil.Rocks | **0.11.4.0** | MIT | 同上（Rocks 扩展） |
| `tools/data-probe/lib/classdata.tpk` | AssetsTools 类型数据库 | 与 AssetsTools 3.0 配套 | 随组件 | 解析 Unity 类型所需的 `.tpk` |
| `tools/api-probe/lib/ICSharpCode.Decompiler.dll` | [ICSharpCode.Decompiler](https://github.com/icsharpcode/ILSpy) | **11.1.0.9782** | MIT | 反编译 C#（查入口基类、字段含义） |
| `tools/api-probe/lib/System.Reflection.Metadata.dll` | System.Reflection.Metadata | **9.0.0.0** | MIT | 上者的依赖 |
| `tools/api-probe/lib/System.Collections.Immutable.dll` | System.Collections.Immutable | **9.0.0.0** | MIT | 上者的依赖 |

## 编辑器模板

`editors/_template/`（若你按它建新的编辑器工具）**默认不带** `three.js`：要画 3D 就自己把 `three.bundle.js` 放进去，
并按该目录的 `THIRD_PARTY_LICENSES.md` 记一行（含 `/*! … */` banner 与 MIT 全文指引）。

## 升级与核对

- 升级任何一个 DLL 时**连版本一起改本表**。版本读法（不执行被读的代码）：

```sh
# 临时 console 工程里：
dotnet run -c Release -- tools/api-probe/lib/ICSharpCode.Decompiler.dll
# Program.cs 用 AssemblyName.GetAssemblyName(path) + FileVersionInfo.GetVersionInfo(path)
```

- `classdata.tpk` 跟 AssetsTools 大版本绑定：换 `AssetsTools.NET` 大版本时要一起换，否则新类型读不出来。
- `AssetsTools.NET.MonoCecil` 的版本号是上游自己写的 `1.0.0.0`，不代表与 `AssetsTools.NET` 同版本。

## 已知注意

- 本仓**没有自己的 LICENSE 文件**（默认保留所有权利）。第三方许可只覆盖上表那些文件本身。
- 各 mod 会把 `libs/core/*.cs`、`libs/modules/*.cs` **编译进自己的 DLL** ⇒ 不得把 LGPL 这类传染性许可的库放进 `libs/`，
  否则每个 mod 的 DLL 都要按该许可分发。

## MIT 许可全文

以下适用于上表中所有 MIT 组件（各自版权行见组件仓库；仓内二进制里可读到的是
`Mono.Cecil` = `Copyright © 2008 - 2018 Jb Evain`、`ICSharpCode.Decompiler` = `Copyright 2011-2026 AlphaSierraPapa`、
`AssetsTools.NET` = `Written by nes`、`System.*` = `© Microsoft Corporation`）。

```
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
```
