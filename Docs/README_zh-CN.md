
<div align="center"><a href="https://discord.gg/fbsRYc7bBA"><img alt="Discord - Server for Lesser Konwn Anime Games" src="https://i.imgtg.com/2023/06/08/O5Lt2S.jpg"></a></div>


[EN](../README.md) | [简中](Docs/README_zh-CN.md) | [繁中](README_zh-TW.md)
# PemukulPaku  
A private server implementation for a third impact game but made in see sharp  
## 快速开始  
**前提条件*  
* 阅读文章的能力  
* GitHub 账户   
* [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)   
在命令行运行如下命令   
```
dotnet dev-certs https --trust
```
* [MongoDB](https://www.mongodb.com/try/download/community)  
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
**在运行前请确保MongoDB服务已经正确启动**  
**连接至服务器*    
选择你想要的代理工具 (mitmproxy/Fiddler reccomended)     
使用以下工具之一进行代理配置：:    
[mitmproxy](https://mitmproxy.org/)  
[Fiddler](https://github.com/rafi1212122/PemukulPaku/wiki/Starting#connecting-to-the-server)     
若出现 ``为了账号安全，请重新输入密码``，可尝试使用mitmproxy    
## 使用Fiddler    
1.运行 Fiddler Classic,  开启解密http通信 ``Tools-options-Https-勾选Decrypt HTTPS traffic``并将端口设为任意未占用端口``Tools-options-Https-Connections``  
2.点击Fiddler Script，并填入以下内容   
```
import System;
import System.Windows.Forms;
import Fiddler;
import System.Text.RegularExpressions;
class Handlers
{
static function OnBeforeRequest(oS: Session) {
    if(oS.host.EndsWith(".yuanshen.com") || oS.host.EndsWith(".hoyoverse.com") || oS.host.EndsWith(".starrails.com") || oS.host.EndsWith(".bhsr.com") || oS.host.EndsWith(".kurogame.com") || oS.host.EndsWith(".zenlesszonezero.com") || oS.host.EndsWith(".g3.proletariat.com") || oS.host.EndsWith("west.honkaiimpact3.com") || oS.host.EndsWith("westglobal01.honkaiimpact3.com") || oS.host.EndsWith(".os.honkaiimpact3.com") || oS.host.EndsWith("overseas01-appsflyer-report.honkaiimpact3.com") || oS.host.EndsWith(".mihoyo.com") || (oS.host.EndsWith("bh3.com") && !oS.host.Contains("bundle"))) {
        oS.host = "localhost";
    }
}
}
```
##  使用mitmproxy  

1.从[这里](https://gist.github.com/rafi1212122/5cc76297d6cf6396de5fc572d1e55812#file-proxy-py)获取proxy.py  
2.在proxy.py所在目录运行 `mitmdump -s proxy.py -k`命令  
3.设置系统代理为你所设置的端口，默认为 ``127.0.0.1:8080``  
4.信任CA证书   
 * mitmproxy 的 CA 证书通常存放在 `%USERPROFILE%\ .mitmproxy` 或者浏览器访问 ``http://mitm.it``下载证书,如果你访问该地址后看到了 ``If you can see this, traffic is not passing through mitmproxy.``说明mitmproxy配置有误，请重新配置
 * 双击安装证书，或者运行 ``certutil -addstore root %USERPROFILE%\.mitmproxy\mitmproxy-ca-cert.p12``命令  

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
加入[Discord server](https://discord.gg/fbsRYc7bBA) or 提交GitHub issue  
