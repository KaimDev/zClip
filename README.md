﻿# zClip for Windows

Is a synchronization tool and open source for Windows that allows you to copy and paste text and images between your Windows PC and your Android device.

![image](https://github.com/KaimDev/zClip-Desktop/assets/88113215/6647c757-f91b-441b-850c-b1b67769bba8)


## Technologies for this project

* C#
* .NET Framework 4.8
* WPF

## How does it work?

* Using the LAN network, an Ipv4 address is assigned to the Windows PC and the Android device by the router. 


* zClip detects the Ipv4 address of them and establishes a connection.


* Both devices create a local server and a client to communicate with each other.

## Expected Features

1. End-to-end encryption.
2. Optionality to choose whether it starts with the system or not.
3. Fast and lightweight.
4. Send text, images and files.


## Benchmarks

[**ClipboardService**](https://github.com/KaimDev/zClip-Desktop/blob/main/zClip-Desktop/Services/ClipboardService.cs)

```shell
// * Summary *

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3296/23H2/2023Update/SunValley3)
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9181.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8.1 (4.8.9181.0), X86 LegacyJIT


| Method                     | Mean     | Error     | StdDev    |
|--------------------------- |---------:|----------:|----------:|
| GetALargeTextFromClipboard | 1.231 ms | 0.1142 ms | 0.3278 ms |

// ***** BenchmarkRunner: End *****
Run time: 00:01:22 (82.37 sec), executed benchmarks: 1

Global total time: 00:01:29 (89.77 sec), executed benchmarks: 1
// * Artifacts cleanup *
Artifacts cleanup is finished
```

**Explanation**: The method `GetALargeTextFromClipboard` is responsible for executing the ClipboardService multiple times to determine the average time it takes to retrieve a 500-word text from the clipboard.

[By KaimDev](https://github.com/KaimDev)