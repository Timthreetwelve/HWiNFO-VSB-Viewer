<p align="center">
  <a target="_blank" rel="noopener noreferrer">
    <img width="128" src="https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/blob/main/HWiNFOVSBViewer/Images/H-in-blue-cloud.png?raw=true" alt="HWiNFO VSB Viewer logo">
  </a>
</p>
<h1 align="center">
  HWiNFO VSB Viewer
</h1>
<div align="center">

[![GitHub](https://img.shields.io/github/license/Timthreetwelve/HWiNFO-VSB-Viewer?style=plastic)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/blob/main/LICENSE)
[![NET6win](https://img.shields.io/badge/.NET-8.0--Windows-blueviolet?style=plastic)](https://dotnet.microsoft.com/en-us/download) 
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/Timthreetwelve/HWiNFO-VSB-Viewer?style=plastic)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases/latest) 
[![GitHub Release Date](https://img.shields.io/github/release-date/timthreetwelve/HWiNFO-VSB-Viewer?style=plastic&color=orange)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases/latest) 
[![GitHub commits since latest release (by date)](https://img.shields.io/github/commits-since/timthreetwelve/HWiNFO-VSB-Viewer/latest?style=plastic)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/commits/main)
[![GitHub last commit](https://img.shields.io/github/last-commit/timthreetwelve/HWiNFO-VSB-Viewer?style=plastic)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/commits/main)
[![GitHub commits](https://img.shields.io/github/commit-activity/m/timthreetwelve/HWiNFO-VSB-Viewer?style=plastic)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/commits/main)
[![GitHub Stars](https://img.shields.io/github/stars/timthreetwelve/HWiNFO-VSB-Viewer?style=plastic&color=goldenrod&logo=github)](https://docs.github.com/en/get-started/exploring-projects-on-github/saving-repositories-with-stars)
[![GitHub all releases](https://img.shields.io/github/downloads/Timthreetwelve/HWiNFO-VSB-Viewer/total?style=plastic&label=total%20downloads)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases)
[![GitHub release (by tag)](https://img.shields.io/github/downloads/timthreetwelve/HWiNFO-VSB-Viewer/latest/total?style=plastic&color=2196F3&label=downloads%20latest%20version)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases/latest)
[![GitHub Issues](https://img.shields.io/github/issues/timthreetwelve/HWiNFO-VSB-Viewer?style=plastic&color=orangered)](https://github.com/TimthreetwelveHWiNFO-VSB-Viewer/issues)
[![GitHub Issues](https://img.shields.io/github/issues-closed/timthreetwelve/HWiNFO-VSB-Viewer?style=plastic&color=slateblue)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/issues)

</div>

## Notice:

#### I believe that HWiNFO-VSB-Viewer is functionally complete. I am changing the repository to public archive status. My sincere thanks to everyone that expressed an interest in the project and to those that downloaded and use it. I hope that made your Rainmeter/HWiNFO configuration tasks a little easier. My thanks also to everyone that contributed a translation. _You Rock!_ If anyone wants to fork the repository to maintain or improve it, please do. You have my blessing.
---

HWiNFO VSB Viewer is an application that will read the HWiNFO VSB registry values and display them in a grid. The Index values shown in the grid can then be used in Rainmeter skins.

This app gets the same information from the registry as the `reg query HKEY_CURRENT_USER\SOFTWARE\HWiNFO64\VSB` command and then puts it in an easy to use grid. 

HWiNFO VSB Viewer uses .NET 8. Self-contained versions are available if .NET 8 isn't installed. 
- [_see below for .NET Framework version_](#framework) 

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

## Main window
![Screenshot 1](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/blob/main/Images/HWiNFOVSBViewer_6a.png)

## Filtered 
![Screenshot 1](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/blob/main/Images/HWiNFOVSBViewer_6b.png)

## Download
Download from [here](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases) 

### <a name="framework" /> .NET Framework Version
[Version 0.1.4](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases/tag/v0.1.4), which runs on .NET Framework 4.8, is still available on the releases page.
