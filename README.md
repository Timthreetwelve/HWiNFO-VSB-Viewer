# HWiNFO VSB Viewer

[![GitHub](https://img.shields.io/github/license/Timthreetwelve/HWiNFO-VSB-Viewer?style=plastic)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/blob/main/LICENSE)
[![NET6win](https://img.shields.io/badge/.NET-6.0--Windows-blueviolet?style=plastic)](https://dotnet.microsoft.com/en-us/download) 
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/Timthreetwelve/HWiNFO-VSB-Viewer?style=plastic)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases/latest) 
[![GitHub Release Date](https://img.shields.io/github/release-date/timthreetwelve/HWiNFO-VSB-Viewer?style=plastic&color=orange)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases/latest) 
[![GitHub all releases](https://img.shields.io/github/downloads/Timthreetwelve/HWiNFO-VSB-Viewer/total?style=plastic)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases)
[![GitHub release (by tag)](https://img.shields.io/github/downloads/timthreetwelve/HWiNFO-VSB-Viewer/latest/total?style=plastic&color=2196F3&label=downloads%20latest%20version)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases/latest)
[![GitHub Issues](https://img.shields.io/github/issues/timthreetwelve/HWiNFO-VSB-Viewer?style=plastic&color=orangered)](https://github.com/TimthreetwelveHWiNFO-VSB-Viewer/issues)
[![GitHub Issues](https://img.shields.io/github/issues-closed/timthreetwelve/HWiNFO-VSB-Viewer?style=plastic&color=slateblue)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/issues)

HWiNFO VSB Viewer is an application that will read the HWiNFO VSB registry values and display them in a grid. The Index values shown in the grid can then be used in Rainmeter skins.

This app gets the same information from the registry as the `reg query HKEY_CURRENT_USER\SOFTWARE\HWiNFO64\VSB` command and then puts it in an easy to use grid. 

HWiNFO VSB Viewer requires .NET 6. [***](#framework)

For more information on using HWiNFO with Rainmeter, read this [https://docs.rainmeter.net/tips/hwinfo/](https://docs.rainmeter.net/tips/hwinfo/)

Get Rainmeter here [www.rainmeter.net](https://www.rainmeter.net/) 

Get HWiNFO here [www.hwinfo.com](https://www.hwinfo.com/)

## Features
* It checks to make sure that HWiNFO (either 32 or 64 bit) is running.
* Displays the HWiNFO/VSB registry key values in a scrollable, sortable grid.
* Initially sorted by Index number.
* Filter to find text quickly.
* Data can be refreshed (F5).
* The entire grid can be exported as CSV or HTML.
* Individual grid cells can copied to the clipboard.

## Screenshot
![Screenshot of 0.3.0](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/blob/main/Images/HWiNFOVSBViewer3.png)

## Download
Download from [here](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases) 

### <a name="framework" /> .NET Framework Version
[Version 0.1.4](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases/tag/v0.1.4), which runs on .NET Framework 4.8, is still available on the releases page.
