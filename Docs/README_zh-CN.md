
<div align="center"><a href="https://discord.gg/fbsRYc7bBA"><img alt="Discord - Server for Lesser Konwn Anime Games" src="https://i.imgtg.com/2023/06/08/O5Lt2S.jpg"></a></div>


[EN](../README.md) [简中](Docs/README_zh-CN.md) [繁中](README_zh-TW.md)
# PemukulPaku  
A private server implementation for a third impact game but made in see sharp  
## 快速开始  
**先决条件*  
阅读文章的能力  
GitHub 账户   
[.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)   
在命令行运行如下命令   
```
dotnet dev-certs https
```
[MongoDB](https://www.mongodb.com/try/download/community)  
**快速开始*  
克隆存储库  
```
git clone https://github.com/rafi1212122/PemukulPaku.git
```

从[这里](https://github.com/rafi1212122/PemukulPaku/actions)下载最新action.   
进入PemukulPaku文件夹，将下载的action解压到该文件夹下  
下载资源文件 [resources](https://anonfiles.com/bf2cR4u1z7/Resources_7z)  并将其解压放置在 `Common\Resources`目录下   
```
├───Common
│   └───Resources
│       └───ExcelOutputAsset
│       └───Proto.cs


```
**运行服务*   
1.运行 PemukulPaku.exe   
**连结至服务器*   
选择你想要的代理工具 (mitmproxy/Fiddler reccomended)   
使用以下脚本之一进行代理配置：:  
[mitmproxy](https://gist.github.com/rafi1212122/5cc76297d6cf6396de5fc572d1e55812#file-proxy-py)  
[Fiddler](https://github.com/rafi1212122/PemukulPaku/wiki/Starting#connecting-to-the-server)   


了解更多[GitHub wiki](https://github.com/rafi1212122/PemukulPaku/wiki)  

## GM 命令  
* **在哪里输入命令？**\  
在控制台或者游戏内聊天窗口  
以下是可用的命令：:  
1. `level <number>`    
   * 设置玩家等级  
   * 例如:  
      * `level 88`  
2. `avatar <add|modify> <avatarId> <...>`  
   * 将角色添加到用户帐户或修改角色信息  
   * 例如:  
      * `avatar add 713`  
      * `avatar modify 713 Level 80`  
           * 请注意 字母L是大写的  
3. `give <avatars|weapons|stigmata|materials|dress>`.  
    * 获取所有角色，  、武器、徽章、材料或服装  
    * 例如:  
    - `give avatars`   

了解更多[development](https://github.com/rafi1212122/PemukulPaku/wiki/Development)  

## 支持  
[Discord server](https://discord.gg/fbsRYc7bBA) or GitHub issue  
