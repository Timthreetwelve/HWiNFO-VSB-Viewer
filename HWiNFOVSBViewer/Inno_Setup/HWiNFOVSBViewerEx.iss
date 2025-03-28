﻿; ---------------------------------------------------------------------
;  Inno Setup Script for HWiNFO VSB Viewer (HWiNFOVSBViewer)
;----------------------------------------------------------------------
; The following #include file is created by the PubSetupEx.ps1 script.
;
; It contains #define statements for:
;
;             InstallType:      String denoting installer type.
;                               Inserted into the installer file name.
;
;             PublishFolder:    The output folder from MS Build.
;                               Varies depending on the type of build.
;----------------------------------------------------------------------
#include "D:\Temp\PubSetup.Temp.iss"

#define BaseDir              "V:\Source\Repos\HWiNFOVSBViewer\HWiNFOVSBViewer"
#define MySourceDir          BaseDir + PublishFolder
#define MySetupIcon          BaseDir + "\Images\H-in-blue-cloud.ico"
#define MyOutputDir          "D:\InnoSetup\Output"
#define MyLargeImage         "D:\InnoSetup\Images\WizardImageHWiNFO.bmp"
#define MySmallImage         "D:\InnoSetup\Images\WizardSmallImage.bmp"

#define MyAppID              "{D9C48FED-F5A8-468F-A907-E612266C2830}"
#define MyAppName            "HWiNFO VSB Viewer"
#define MyAppNameNoSpaces    StringChange(MyAppName, " ", "")
#define MyAppExeName         "HWiNFOVSBViewer.exe"
#define MyAppVersion         GetVersionNumbersString(MySourceDir + "\" + MyAppExeName)
#define MyInstallerFilename  MyAppNameNoSpaces + "_" + MyAppVersion + "_" + InstallType + "_Setup"
#define MyCompanyName        "T_K"
#define MyPublisherName      "Tim Kennedy"
#define StartCopyrightYear   "2021"
#define CurrentYear          GetDateTimeString('yyyy', '/', ':')
#define MyCopyright          "(c) " + StartCopyrightYear + "-" + CurrentYear + " Tim Kennedy"
#define MyDateTimeString     GetDateTimeString('yyyy/mm/dd hh:nn:ss', '/', ':')
#define MyAppSupportURL      "https://github.com/Timthreetwelve/HWiNFO-VSB-Viewer"
;#define RunRegKey            "Software\Microsoft\Windows\CurrentVersion\Run"


; -----------------------------------------------------
; Include the localization file. Thanks bovirus!
; -----------------------------------------------------
#include "HWiNFOVSBViewerLocalization.iss"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
;---------------------------------------------
AppId={{#MyAppID}
;---------------------------------------------
; Uncomment the following line to run in non administrative install mode (install for current user only.)
; Installs in %localappdata%\Programs\ instead of \Program Files(x86)
;---------------------------------------------
PrivilegesRequired=lowest
;---------------------------------------------
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}

AppCopyright={#MyCopyright}

AppPublisherURL={#MyAppSupportURL}
AppSupportURL={#MyAppSupportURL}
AppUpdatesURL={#MyAppSupportURL}

VersionInfoDescription={#MyAppName} installer
VersionInfoProductName={#MyAppName}
VersionInfoVersion={#MyAppVersion}

UninstallDisplayName={#MyAppName}
UninstallDisplayIcon={app}\{#MyAppExeName}
AppPublisher={#MyPublisherName}

ShowLanguageDialog=yes
UsePreviousLanguage=no
WizardStyle=modern
WizardSizePercent=100,100
WizardImageFile={#MyLargeImage}
WizardSmallImageFile={#MySmallImage}

AllowNoIcons=yes
Compression=lzma
DefaultDirName={autopf}\{#MyCompanyName}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableDirPage=yes
DisableProgramGroupPage=yes
DisableReadyMemo=no
DisableStartupPrompt=yes
DisableWelcomePage=no
OutputBaseFilename={#MyInstallerFilename}
OutputDir={#MyOutputDir}
;OutputManifestFile={#MyAppName}_{#MyAppVersion}_FileList.txt
SetupIconFile={#MySetupIcon}
SetupLogging=yes
SolidCompression=no
SourceDir={#MySourceDir}

;[Tasks]
;Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; Change the following for each project
Source: "{#MySourceDir}\HWiNFOVSBViewer.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MySourceDir}\*.dll"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
Source: "{#MySourceDir}\*.json"; Excludes: "usersettings.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MySourceDir}\ReadMe.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MySourceDir}\License.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MySourceDir}\Strings.test.xaml"; DestDir: "{app}"; Flags: ignoreversion
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
Root: HKCU; Subkey: "Software\{#MyCompanyName}\{#MyAppName}"; ValueType: String; ValueName: "Installer Language"; ValueData:"{language}"; Flags: uninsdeletekey
; Delete this key from previous installs
Root: HKCU; Subkey: "Software\{#MyCompanyName}\{#MyAppName}"; ValueType: none; ValueName: "Edition"; Flags: uninsdeletekey deletevalue

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
Filename: "{app}\ReadMe.txt"; Description: "{cm:ViewReadme}"; Flags: nowait postinstall skipifsilent unchecked shellexec

; -----------------------------------------------------------------------------
; Code section follows
; -----------------------------------------------------------------------------
[Code]

procedure InitializeWizard;
var
  Text: String;
begin
  // Change text on welcome page based on installation type
  case ExpandConstant('{#InstallType}') of
    'x64x86': Text := FmtMessage( CustomMessage('NotSelfContained'), [ExpandConstant('{#MyAppName}'), ExpandConstant('{#MyAppVersion}')]); 
    'SC_x86': Text := FmtMessage( CustomMessage('SelfContainedx86'), [ExpandConstant('{#MyAppName}'), ExpandConstant('{#MyAppVersion}')]); 
    'SC_x64': Text := FmtMessage( CustomMessage('SelfContainedx64'), [ExpandConstant('{#MyAppName}'), ExpandConstant('{#MyAppVersion}')]);
  else
      Text := WizardForm.WelcomeLabel2.Caption;
  end;
  WizardForm.WelcomeLabel2.Caption := Text;
end;

// function used to check if app is currently running.
// Returns True if running, otherwise False.
function IsAppRunning(const FileName : string): Boolean;
var
    FSWbemLocator: Variant;
    FWMIService   : Variant;
    FWbemObjectSet: Variant;
begin
    Result := false;
    FSWbemLocator := CreateOleObject('WBEMScripting.SWBEMLocator');
    FWMIService := FSWbemLocator.ConnectServer('', 'root\CIMV2', '', '');
    FWbemObjectSet :=
      FWMIService.ExecQuery(
        Format('SELECT Name FROM Win32_Process Where Name="%s"', [FileName]));
    Result := (FWbemObjectSet.Count > 0);
    FWbemObjectSet := Unassigned;
    FWMIService := Unassigned;
    FSWbemLocator := Unassigned;
end;

// function to check for self-contained vs not-self-contained conflict.
// If mscorlib.dll is present the previous installation was .NET self-contained.
// A non-self-contained version should not be installed over a self-contained version. 
// Returns True mscorlib.dll is present and install type is x64x86, otherwise False.
function CompatibilityCheck(): Boolean;
begin
    Result := False;
    if (FileExists(ExpandConstant('{autopf}\{#MyCompanyName}\{#MyAppName}\mscorlib.dll')) and (ExpandConstant('{#InstallType}') = 'x64x86')) then
      begin
        Result := True;
        Exit;
      end;
end;

function InitializeSetup(): Boolean;
var
  Answer: Integer;
  ThisApp: String;
begin
  Result := true;
  ThisApp := ExpandConstant('{#MyAppExeName}');
  
  // Checks if app is running, if so, displays msgbox asking to close running app
  while IsAppRunning(ThisApp) do
  begin
    Answer := MsgBox(ThisApp + ' ' + CustomMessage('AppIsRunning'), mbError, MB_OKCANCEL);
    If Answer = IDCANCEL then
    begin
      Log('Exiting setup. Application is currently running.');
      Result := false;
      Exit;
    end;
  end;
  
  // Checks for possible self-contained vs not-self-contained conflict.
  If CompatibilityCheck() then
    begin
      MsgBox(CustomMessage('IncompatibleFile'), mbError, MB_OK);
      Log('Exiting setup. Compatibility check failed.');
      Result := false;
      Exit;
    end;
end;

// Copies setup log to app folder
// Overwrites the previous setup log.
procedure CurStepChanged(CurStep: TSetupStep);
var
  logfilepathname, newfilepathname: string;
begin
    if CurStep = ssDone then
    begin
      logfilepathname := ExpandConstant('{log}');
      newfilepathname := ExpandConstant('{app}\') + 'Setup_Log.txt';
      Log('Setup log file copied to: ' + newfilepathname);
      CopyFile(logfilepathname, newfilepathname, False);
   end;
end;

// Uninstall - Delete settings file if user clicks 'Yes' on message box.
procedure CurUninstallStepChanged (CurUninstallStep: TUninstallStep);
var
  mres : integer;
begin
    case CurUninstallStep of
      usPostUninstall:
        begin
          mres := MsgBox(CustomMessage('DeleteConfigFiles'), mbConfirmation, MB_YESNO or MB_DEFBUTTON2)
          if mres = IDYES then
          begin
            DelTree(ExpandConstant('{app}\*.json'), False, True, False);
            DelTree(ExpandConstant('{app}'), True, True, True);
          end;
       end;
   end;
end;
