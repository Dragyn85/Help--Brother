#define VerFile FileOpen("..\Builds\Version.txt")
#define ApplicationVersion FileRead(VerFile)
#expr FileClose(VerFile)
#undef VerFile

[Setup]
AppId={{FCDD5D67-0614-4868-B76C-C49FE8C27584}
AppName=Help!Brother
AppVersion={#ApplicationVersion}
AppVerName=Help!Brother
AppPublisher=DragynGames
DefaultDirName={pf}\Dragyn Games\Help!Brother
DisableProgramGroupPage=yes
OutputDir=.\
OutputBaseFilename=Help!BrotherSetup-{#ApplicationVersion}
SetupIconFile=Resources\SetupIcon.ico
Compression=lzma
SolidCompression=yes
UninstallDisplayIcon={app}\Help!Brother.exe
WizardSmallImageFile=Resources\banner.bmp
WizardImageFile=Resources\welcome.bmp
DirExistsWarning=no
DisableWelcomePage=no

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\Builds\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{commonprograms}\Help! Brother"; Filename: "{app}\Help! Brother.exe"
Name: "{commondesktop}\Help! Brother"; Filename: "{app}\Help! Brother.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\Help! Brother.exe"; Description:"{cm:LaunchProgram,Help! Brother}"; Flags: nowait postinstall skipifsilent

[Messages]
SetupWindowTitle=Install - %1 - {#ApplicationVersion}
WelcomeLabel2=This will install [name/ver] on your computer. %n%n
