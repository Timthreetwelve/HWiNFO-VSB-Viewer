# HWiNFO VSB Viewer

[![GitHub](https://img.shields.io/github/license/Timthreetwelve/HWiNFO-VSB-Viewer?style=plastic)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/blob/main/LICENSE)
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/Timthreetwelve/HWiNFO-VSB-Viewer?style=plastic)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases/latest) 
[![GitHub Release Date](https://img.shields.io/github/release-date/timthreetwelve/HWiNFO-VSB-Viewer?style=plastic&color=orange)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases/latest) 
[![GitHub all releases](https://img.shields.io/github/downloads/Timthreetwelve/HWiNFO-VSB-Viewer/total?style=plastic)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases)
[![GitHub commits since latest release (by date)](https://img.shields.io/github/commits-since/timthreetwelve/HWiNFO-VSB-Viewer/latest?style=plastic)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/commits/main)
[![GitHub last commit](https://img.shields.io/github/last-commit/timthreetwelve/HWiNFO-VSB-Viewer?style=plastic)](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/commits/main)

HWiNFO VSB Viewer is an application that will read the HWiNFO VSB registry values and display them in a grid. The Index values shown in the grid can then be used in Rainmeter skins.

This app gets the same information from the registry as the `reg query HKEY_CURRENT_USER\SOFTWARE\HWiNFO64\VSB` command and then puts it in an easy to use grid. 

✨ HWiNFO VSB Viewer requires .NET 6. [*](#framework)

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
![Screenshot](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/blob/main/Images/HWiNFOVSBViewer2.png)

## Download
Download from [here](https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer/releases) 

### <a name="framework" /> .NET Framework Version
If you don't want to install .NET 6, version 0.1.4, which runs on .NET Framework 4.8, is still available on the releases page.
