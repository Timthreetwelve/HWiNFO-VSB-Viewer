The HWiNFO VSB Viewer ReadMe file


Introduction
============
HWiNFO VSB Viewer is an application that will read the HWiNFO VSB registry values and display them
in a grid. The Index values shown in the grid can then be used in Rainmeter skins.

This app gets the same information from the registry as the reg query
HKEY_CURRENT_USER\SOFTWARE\HWiNFO64\VSB command and then puts it in an easy to use grid.


Using HWiNFO VSB Viewer
=======================

Click the three-lined (hamburger) icon at the top left and use the bar that appears on the left for
page navigation.

The first screen displays the registry information found at HKEY_CURRENT_USER\SOFTWARE\HWiNFOxx\VSB.
From here you can filter to find text quickly. In the filter box you can enter one ~ (tilde) as a
shortcut to filter items that have the degree symbol (u00b0). To the right of the filter box there
are three icons that have menu selections for copying the grid to the clipboard, to a CSV file or to
an HTML file. There are also options to change the size of the display and for refreshing the data.
By clicking on the grid column headers you can sort the columns in the grid.

The second screen is the Settings screen. You can select between Light, Dark and System themes. You
can select the accent color and you can choose between five sizes for the app. You can choose the
font weight and row spacing in the grid on the first page. There are also options to have the app
stay on top of other windows and you can control the detail of the log file.

You can choose the language used for the user interface, provided that a translation has been
contributed for that language. Checking the ""Use Windows display language"" check box will tell
the app to use the language specified in the Windows settings, which will be used if a translation
is available; otherwise, English (en-US) will be used. The drop-down allows you to choose a
specific language from the list of defined languages. Changing the language will cause the
application to restart.

The last screen is the About screen. This screen shows information about the app such as the version
number and has a link to the GitHub repository where you can check for updates.

You can view the log file or this ReadMe file by clicking on the three dot icon at the right end of
the banner on either the Settings or About screens.

These keyboard shortcuts are available:

	F1 = Go to the About screen
	F5 = Refresh
	Ctrl + A = Change the accent color
	Ctrl + M = Change the theme
	Ctrl + R = Reset column sorts
	Ctrl + S = Change grid row spacing
	Ctrl + W = Change font weight in grid
	Ctrl + comma = Go to the Settings screen
	Ctrl + Numpad Plus = Increase size
	Ctrl + Numpad Minus = Decrease size
	Ctrl + Numpad 0 = set size to default


Uninstalling HWiNFO VSB Viewer
==============================
To uninstall, use the regular Windows add/remove programs feature.


Notices and License
===================
HWiNFO VSB Viewer was written in C# by Tim Kennedy.

HWiNFO VSB Viewer uses the following icons and applications:

    * Material Design in XAML Toolkit https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit

    * CommunityToolkit.Mvvm https://github.com/CommunityToolkit/dotnet

    * NLog https://nlog-project.org/

    * GitVersion https://github.com/GitTools/GitVersion

    * Octokit https://github.com/octokit/octokit.net

    * GitKraken was used for everything Git related. https://www.gitkraken.com/

    * Inno Setup was used to create the installer. https://jrsoftware.org/isinfo.php

    * Visual Studio Community was used throughout the development of this project. https://visualstudio.microsoft.com/vs/community/

    * XAML Styler is indispensable when working with XAML. https://github.com/Xavalon/XamlStyler

    * And of course, the essential PowerToys https://github.com/microsoft/PowerToys



MIT License
Copyright (c) 2021 - 2023 Tim Kennedy

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
associated documentation files (the "Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject
to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial
portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT
LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.