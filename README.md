<p align="center">
    <img src="Icon.png" width="200" height="200" alt="go-cqhttp">
</p>
<div align="center">
    <h1 align="center">AuroraNative</h2>
    <h3 align="center"><b>基于 <a href="https://github.com/Mrs4s/go-cqhttp">go-cqhttp</a> 以及 <a href="https://github.com/howmanybots/onebot/blob/master/README.md">OneBot</a> 的 C# 机器人开发框架 </b></h3>


<h4 align="center">
<a href="https://www.nuget.org/packages/AuroraNative/">
    <img src="https://img.shields.io/nuget/vpre/AuroraNative?style=flat-square">
</a>
<a href="https://github.com/howmanybots/onebot">
    <img src="https://img.shields.io/badge/OneBot-v11-blue?style=flat-square">
</a>
<a href="https://github.com/Mrs4s/go-cqhttp/releases">
    <img src="https://img.shields.io/badge/go--cqhttp-v0.9.40--fix4-blueviolet?style=flat-square">
</a>
<img src="https://img.shields.io/github/license/timi137137/AuroraNative?style=flat-square">
<img src="https://img.shields.io/github/workflow/status/timi137137/AuroraNative/BuildPackages/master?style=flat-square">
</h4>
</div>

---

## 介绍

这是一个基于 go-cqhttp 实现的C#机器人开发框架

目前框架采用了 [.NET Standard2.0](https://docs.microsoft.com/zh-cn/dotnet/standard/net-standard) 编译，能够兼容目前主流.NET版本。

## 开发注意事项

本框架处于快速迭代开发状态，框架将会随着 [go-cqhttp](https://github.com/Mrs4s/go-cqhttp) 的版本更新而迭代。
> 请时刻留意所下载的框架包所依赖的 [go-cqhttp](https://github.com/Mrs4s/go-cqhttp) 版本，不一致可能导致错误！

可能会因为以下状况更新版本号
  - 所依赖的 [go-cqhttp](https://github.com/Mrs4s/go-cqhttp) 版本更新 - vX.X+1.X
  - 优化内部算法或修改类型（如将返回的JObject类型抽象为新自定义类型） - vX.X.X+1
  - 重命名/删除/新增 文件/命名空间/API - vX.X+1.X

## 文档

开发文档:[点我查看](https://auroranative.mikuy.cn)

> 开发文档是与框架一起更新的，因此文档也处于快速迭代状态。

## 兼容性

### 通讯方式

- [ ] HTTP API
- [ ] 反向 HTTP POST
- [x] 正向 Websocket
- [x] 反向 Websocket

## 关于 ISSUE

如果没有大问题请到 [Discussions](https://github.com/timi137137/AuroraNative/discussions) 处提问

以下 ISSUE 会被直接关闭

- 询问已知问题
- 提问找不到重点
- 重复提问

> 请注意, 开发者并没有义务回复您的问题. 您应该具备基本的提问技巧。  
> 有关如何提问，请阅读[《提问的智慧》](https://github.com/ryanhanwu/How-To-Ask-Questions-The-Smart-Way/blob/main/README-zh_CN.md)

## 鸣谢

感谢以下大佬对本框架开发的帮助

[Jie2GG](https://github.com/Jie2GG) | [Yukari316](https://github.com/Yukari316) | [bleatingsheep](https://github.com/b11p)

### 使用到的开源库

[Newtonsoft.Json](https://www.newtonsoft.com/json) | [Microsoft.Extensions.Caching.Memory](https://www.nuget.org/packages/Microsoft.Extensions.Caching.Memory/5.0.0)
