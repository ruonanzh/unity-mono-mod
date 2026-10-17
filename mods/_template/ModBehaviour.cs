// TODO: 换成本游戏真实的 mod 基类 —— 用 tools/api-probe 查，不要猜：
//   dotnet tools/api-probe/bin/Release/net8.0/api-probe.dll --managed <Managed> --action search --target Behaviour
// 下面这个占位基类只是为了让你看清结构；接游戏时删掉它、改成真实基类。

namespace TemplateMod
{
    /// <summary>
    /// mod 入口。命名约定（常见形态，以该游戏文档为准）：
    ///   <c>info.ini</c> 的 name = 本文件 namespace = 产物文件名（去掉 .dll）。
    /// 三者不一致时游戏通常**静默不加载**，所以接游戏第一步就是实测一次「能加载」。
    /// </summary>
    public class ModBehaviour : UnityEngine.MonoBehaviour   // TODO: 换成游戏的 mod 基类
    {
        void Start()
        {
            UnityEngine.Debug.Log("[TemplateMod] loaded");
        }
    }
}
