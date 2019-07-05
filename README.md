# VPKSoft.VersionCheck
An utility for an application to check if a newer version of the application exists. You can also maintain your own releases within the web site.

## Screenshots

_The maintenance application in its simplicity_
![image](https://user-images.githubusercontent.com/40712699/60735025-7f1d2400-9f5a-11e9-8444-efacb37e9769.png)

_The download dialog within the library_

![image](https://user-images.githubusercontent.com/40712699/60735252-416ccb00-9f5b-11e9-98af-18e553c647a1.png)

### The code to initiate an asynchronous download
```cs
VersionCheck.DownloadFile("https://www.vpksoft.net/phocadownload/installers/setup_vampsharp_1_0_0_4.exe",
  @"F:\Temp");
```
