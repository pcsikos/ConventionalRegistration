set buildNumber=0 
set version=3.2.0
rem set version_Core=%version%
set numeric_version_Core=%version%.%buildNumber%
set numeric_version_SimpleInjector=%version%.%buildNumber%
set numeric_version_Unity=%version%.%buildNumber%

call "%PROGRAMFILES%\Microsoft Visual Studio 14.0\Common7\Tools\vsvars32.bat"

set msbuild="%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe"
set targetPath="..\binaries"
set buildPath = ..\%targetPath%\tmp

set configuration=Release

set net40ClientProfile=TargetFrameworkVersion=v4.0;TargetFrameworkProfile=Client;Configuration=%configuration%
set net40FullProfile=TargetFrameworkVersion=v4.0;TargetFrameworkProfile=;Configuration=%configuration%
set net45Profile=TargetFrameworkVersion=v4.5;TargetFrameworkProfile=;Configuration=%configuration%




rem %msbuild% "ConventionalRegistration\ConventionalRegistration.csproj" /nologo /p:%net40ClientProfile% /p:VersionNumber=%numeric_version_Core%
rem %msbuild% "ConventionalRegistration.SimpleInjector\ConventionalRegistration.SimpleInjector.csproj" /nologo /p:%net40ClientProfile% /p:VersionNumber=%numeric_version_SimpleInjector%
rem %msbuild% "ConventionalRegistration.Unity\ConventionalRegistration.Unity.csproj" /nologo /p:%net40ClientProfile% /p:VersionNumber=%numeric_version_Unity%

%msbuild% "ConventionalRegistration\ConventionalRegistration.csproj" /nologo /p:%net45Profile% /p:VersionNumber=%numeric_version_Core% /p:OutputPath=%buildPath%
%msbuild% "ConventionalRegistration.SimpleInjector\ConventionalRegistration.SimpleInjector.csproj" /nologo /p:%net45Profile% /p:VersionNumber=%numeric_version_SimpleInjector% /p:OutputPath=%buildPath%
%msbuild% "ConventionalRegistration.Unity\ConventionalRegistration.Unity.csproj" /nologo /p:%net45Profile% /p:VersionNumber=%numeric_version_Unity% /p:OutputPath=%buildPath%

rem md "..\binaries\tmp"

packages\ILMerge.2.14.1208\tools\ILMerge.exe /target:library /out:..\binaries\ConventionalRegistration.SimpleInjector.dll ..\binaries\tmp\ConventionalRegistration.SimpleInjector.dll ..\binaries\tmp\ConventionalRegistration.dll 
copy ..\binaries\tmp\ConventionalRegistration.dll ..\binaries
