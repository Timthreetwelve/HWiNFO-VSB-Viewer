; -----------------------------------------------------
; HWiNFOVSBViewer
; -----------------------------------------------------

; Change the following for each project
#define MyAppName "HWiNFO VSB Viewer"
#define MyAppVersion GetStringFileInfo("D:\Visual Studio\Source\Prod\HWiNFOVSBViewer\HWiNFOVSBViewer\bin\Publish\HWiNFOVSBViewer.exe", "FileVersion")
#define MyAppExeName "HWiNFOVSBViewer.exe"
#define MySourceDir "D:\Visual Studio\Source\Prod\HWiNFOVSBViewer\HWiNFOVSBViewer\bin\Publish"
#define MySetupIcon "D:\Visual Studio\Source\Prod\HWiNFOVSBViewer\HWiNFOVSBViewer\Images\H-in-blue-cloud.ico"

; Next items shouldn't need to change
#define MyCompanyName "T_K"
#define MyPublisherName "Tim Kennedy"
#define MyCopyright "Copyright (C) 2022 Tim Kennedy"
#define MyOutputDir "D:\InnoSetup\Output"
#define MyLargeImage "D:\InnoSetup\Images\WizardImage.bmp"
#define MySmallImage "D:\InnoSetup\Images\WizardSmallImage.bmp"
#define MyDateTimeString GetDateTimeString('yyyy/mm/dd hh:nn:ss', '/', ':')
#define MyAppNameNoSpaces StringChange(MyAppName, " ", "")
#define MyInstallerFilename MyAppNameNoSpaces + "_" + MyAppVersion + "_Setup"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
;---------------------------------------------
AppId={{D9C48FED-F5A8-468F-A907-E612266C2830}
;---------------------------------------------
; Uncomment the following line to run in non administrative install mode (install for current user only.)
; Installs in %localappdata%\Programs\ instead of \Program Files(x86)
;---------------------------------------------
PrivilegesRequired=lowest
;---------------------------------------------
AllowNoIcons=yes
AppCopyright={#MyCopyright}
AppName={#MyAppName}
AppPublisher={#MyPublisherName}
AppVerName={#MyAppName} {#MyAppVersion}
AppVersion={#MyAppVersion}
Compression=lzma
DefaultDirName={autopf}\{#MyCompanyName}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableDirPage=yes
DisableProgramGroupPage=yes
DisableReadyMemo=yes
DisableStartupPrompt=yes
DisableWelcomePage=no
OutputBaseFilename={#MyInstallerFilename}
OutputDir={#MyOutputDir}
OutputManifestFile={#MyAppName}_{#MyAppVersion}_FileList.txt
SetupIconFile={#MySetupIcon}
SetupLogging=yes
SolidCompression=yes
SourceDir={#MySourceDir}
UninstallDisplayIcon={app}\{#MyAppExeName}
VersionInfoVersion={#MyAppVersion}
WizardImageFile={#MyLargeImage}
WizardSmallImageFile={#MySmallImage}
WizardStyle=modern
WizardSizePercent=100,100
;InfoBeforeFile="D:\InnoSetup\HWiNFOVSBViewer_Before.rtf"

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[LangOptions]
DialogFontSize=9
DialogFontName="Segoe UI"
WelcomeFontSize=14
WelcomeFontName="Verdana"

[Messages]
WelcomeLabel1=Welcome to [name] Setup
WelcomeLabel2=This will install [name/ver] on your computer.%n%nIt is recommended that you close all other applications before continuing.%n%n%nNote that [name] requires .NET 6.%n
FinishedHeadingLabel=Completing [name] Setup

;[Tasks]
;Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; Change the following for each project
Source: "{#MySourceDir}\HWiNFOVSBViewer.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MySourceDir}\*.dll"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
Source: "{#MySourceDir}\*.json"; Excludes: "usersettings.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MySourceDir}\ReadMe.txt"; DestDir: "{app}"; Flags: ignoreversion isreadme
Source: "{#MySourceDir}\License.txt"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[InstallDelete]
Type: filesandordirs; Name: "{group}"
Type: files; Name: "{app}\Nlog.config"

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
;Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
;Name: "{group}\ReadMe File"; Filename: "{app}\ReadMe.txt"
;Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Registry]
Root: HKCU; Subkey: "Software\{#MyCompanyName}"; Flags: uninsdeletekeyifempty
Root: HKCU; Subkey: "Software\{#MyCompanyName}\{#MyAppName}"; Flags: uninsdeletekey
Root: HKCU; Subkey: "Software\{#MyCompanyName}\{#MyAppName}"; ValueType: string; ValueName: "Copyright"; ValueData: "{#MyCopyright}"; Flags: uninsdeletekey
Root: HKCU; Subkey: "Software\{#MyCompanyName}\{#MyAppName}"; ValueType: string; ValueName: "Install Date"; ValueData: "{#MyDateTimeString}"; Flags: uninsdeletekey
Root: HKCU; Subkey: "Software\{#MyCompanyName}\{#MyAppName}"; ValueType: string; ValueName: "Version"; ValueData: "{#MyAppVersion}"; Flags: uninsdeletekey
Root: HKCU; Subkey: "Software\{#MyCompanyName}\{#MyAppName}"; ValueType: string; ValueName: "Install Folder"; ValueData: "{autopf}\{#MyCompanyName}\{#MyAppName}"; Flags: uninsdeletekey
Root: HKCU; Subkey: "Software\{#MyCompanyName}\{#MyAppName}"; ValueType: string; ValueName: "Edition"; ValueData: "Installer Fix"; Flags: uninsdeletekeyRoot: HKCU; Subkey: "Software\{#MyCompanyName}\{#MyAppName}"; ValueType: String; ValueName: "Installer Language"; ValueData:"{language}" ;Flags: uninsdeletekey
; Delete this key from previous installs
Root: HKCU; Subkey: "Software\{#MyCompanyName}\{#MyAppName}"; ValueType: none; ValueName: "Edition"; Flags: uninsdeletekey deletevalue

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
 procedure CurUninstallStepChanged (CurUninstallStep: TUninstallStep);
 var
     mres : integer;
 begin
    case CurUninstallStep of
      usPostUninstall:
        begin
          mres := MsgBox('Do you want to remove the settings files? '#13#13' Select ''No'' if you plan on reinstalling. ', mbConfirmation, MB_YESNO or MB_DEFBUTTON2)
          if mres = IDYES then
          begin
            DelTree(ExpandConstant('{app}\*.json'), False, True, False);
            DelTree(ExpandConstant('{app}'), True, True, True);
          end;
       end;
   end;
end;

