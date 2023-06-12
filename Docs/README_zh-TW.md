<div align="center"><a href="https://discord.gg/fbsRYc7bBA"><img alt="Discord - Server for Lesser Konwn Anime Games" src="https://i.imgtg.com/2023/06/08/O5Lt2S.jpg"></a></div>




[EN](../README.md)|[簡中](README_zh-CN.md)|[繁中](README_zh-TW.md)  


# PemukulPaku

A private server implementation for a third impact game but made in see sharp

##快速開始

**前提條件*

*閱讀文章的能力

* GitHub帳戶

* [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

在命令列運行如下命令

```

dotnet dev-certs https --trust

```

* [MongoDB](https://www.mongodb.com/try/download/community)

**快速開始*

尅隆存儲庫

```

git clone https://github.com/rafi1212122/PemukulPaku.git

```



從[這裡](https://github.com/rafi1212122/PemukulPaku/actions)下載最新action.

進入PemukulPaku資料夾，將下載的action解壓到該資料夾下

下載資源檔[resources](https://anonfiles.com/bf2cR4u1z7/Resources_7z)並將其解壓放置在`Common\Resources`目錄下

```

├———Common

│└———Resources

│└———ExcelOutputAsset

│└———Proto.cs




```

**運行服務*

1.運行PemukulPaku.exe

**在運行前請確保MongoDB服務已經正確啟動**

**連接至服務器*

選擇你想要的代理工具(mitmproxy/Fiddler reccomended)

使用以下工具之一進行代理配寘：：

[mitmproxy](https://mitmproxy.org/)

[Fiddler](https://github.com/rafi1212122/PemukulPaku/wiki/Starting#connecting-to-the-server)

若出現``為了帳號安全，請重新輸入密碼``，可嘗試使用mitmproxy

##使用Fiddler

1.運行Fiddler Classic，開啟解密http通信``Tools-options-Https-勾選Decrypt HTTPS traffic``並將埠設為任意未佔用埠``Tools-options-Https-Connections``

2.點擊Fiddler Script，並填入以下內容

```

import System;

import System.Windows.Forms;

import Fiddler;

import System.Text.RegularExpressions;

class Handlers

{

static function OnBeforeRequest(oS: Session){

if(oS.host.EndsWith(".yuanshen.com")|| oS.host.EndsWith(".hoyoverse.com")|| oS.host.EndsWith(".starrails.com")|| oS.host.EndsWith(".bhsr.com")|| oS.host.EndsWith(".kurogame.com")|| oS.host.EndsWith(".zenlesszonezero.com")|| oS.host.EndsWith(".g3.proletariat.com")|| oS.host.EndsWith("west.honkaiimpact3.com")|| oS.host.EndsWith("westglobal01.honkaiimpact3.com")|| oS.host.EndsWith( ".os.honkaiimpact3.com")|| oS.host.EndsWith("overseas01-appsflyer-report.honkaiimpact3.com")|| oS.host.EndsWith(".mihoyo.com")||(oS.host.EndsWith("bh3.com")&&! oS.host.Contains("bundle"))){

oS.host ="localhost";

}

}

}

```

##使用mitmproxy



1.從[這裡](https://gist.github.com/rafi1212122/5cc76297d6cf6396de5fc572d1e55812#file-proxy-py)獲取proxy.py

2.在proxy.py所在目錄運行`mitmdump -s proxy.py -k`命令

3.設定系統代理為你所設定的埠，默認為``127.0.0.1:8080``

4.信任CA證書

* mitmproxy的CA證書通常存放在`%USERPROFILE%\ .mitmproxy`或者瀏覽器訪問`` http://mitm.it ``下載證書，如果你訪問該地址後看到了``If you can see this，traffic is not passing through mitmproxy.`` 說明mitmproxy配寘有誤，請重新配寘

*按兩下安裝證書，或者運行``certutil -addstore root %USERPROFILE%\.mitmproxy\mitmproxy-ca-cert.p12``命令



瞭解更多[GitHub wiki](https://github.com/rafi1212122/PemukulPaku/wiki)



## GM命令

* **在哪裡輸入命令？**\

在控制台或者遊戲內聊天視窗

以下是可用的命令：：

1. `level <number>`

*設定玩家等級

*例如：

* `level 88`

2. `avatar <add|modify> <avatarId> <…>`

*將角色添加到使用者帳戶或修改角色資訊

*例如：

* `avatar add 713`

* `avatar modify 713 Level 80`

*請注意字母L是大寫的

3. `give <avatars|weapons|stigmata|materials|dress>`.

*獲取所有角色，、武器、徽章、資料或服裝

*例如：

- `give avatars`



瞭解更多[development](https://github.com/rafi1212122/PemukulPaku/wiki/Development)



##支持

加入[Discord server](https://discord.gg/fbsRYc7bBA)or提交GitHub issue
