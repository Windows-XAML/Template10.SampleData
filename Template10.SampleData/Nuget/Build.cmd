@ECHO OFF
@echo -- NuGet Process Start --

set TARGETDIR=%~1
set TARGETNAME=%~2
set PROJECTDIR=%~3
set WORKINGDIR=%PROJECTDIR%Nuget\
set NUPKG=%TARGETDIR%nupkg

echo.%NUPKG%|findstr /r "Release" >nul
if %errorlevel%==1 (
   echo SKIPPED Debug build - nuget must be Release
   goto end
)

echo.%NUPKG%|findstr /C:"x86" >nul 2>&1
if %errorlevel%==0 (
   echo SKIPPED targets x86 - nuget must target AnyCPU
   goto end
)

echo.%NUPKG%|findstr /C:"x64" >nul 2>&1
if %errorlevel%==0 (
   echo SKIPPED targets x64 - nuget must target AnyCPU
   goto end
)

echo.%NUPKG%|findstr /C:"ARM" >nul 2>&1
if %errorlevel%==0 (
   echo SKIPPED targets ARM - nuget must target AnyCPU
   goto end
)

if "%NuGetCachePath%"=="" (  
	set CACHE=%USERPROFILE%\.nuget\packages\Template10\
) else (  
	set CACHE=%NuGetCachePath%\Template10\
)  

:clear nuget cache
if exist "%CACHE%" (
	echo OK Nuget cache will be cleared: %CACHE%
	rmdir "%CACHE%" /S/Q >nul
) else (
	echo OK No nuget cache to clear: %CACHE%
)

:clear previous build
if exist "%NUPKG%" (
	echo OK Previous build will be cleared: "%NUPKG%"
	rmdir "%NUPKG%" /S/Q >nul
) else (
	echo OK No previous build to clear: "%NUPKG%"
)

:echo Copy \build\%TARGETNAME%.targets
:xcopy.exe "%WORKINGDIR%*.targets" "%NUPKG%\build\" /y >nul

:echo Copy \tools\init.ps1
:xcopy.exe "%WORKINGDIR%init.ps1" "%NUPKG%\Tools\" /y >nul

:echo Copy \lib\*.dll(s)
:echo xcopy.exe "%TARGETDIR%*.dll" "%NUPKG%\lib\" /y 
xcopy.exe "%TARGETDIR%*.dll" "%NUPKG%\lib\" /y 

:echo Copy \lib\*.pri(s)
:echo xcopy.exe "%TARGETDIR%*.pri" "%NUPKG%\lib\" /y 
:xcopy.exe "%TARGETDIR%*.pri" "%NUPKG%\lib\" /y 

:echo Copy \%TARGETNAME%\[Library] (incl. XAML)
:echo xcopy.exe "%TARGETDIR%%TARGETNAME%\*.*" "%NUPKG%\lib\%TARGETNAME%\" /s/e/y 
:xcopy.exe "%TARGETDIR%%TARGETNAME%\*.*" "%NUPKG%\lib\%TARGETNAME%\" /s/e/y 

:echo Copy \%TARGETNAME%.nuspec
:echo xcopy.exe "%WORKINGDIR%%TARGETNAME%.nuspec" "%NUPKG%\" /y 
xcopy.exe "%WORKINGDIR%%TARGETNAME%.nuspec" "%NUPKG%\" /y  

:echo Execute Pack
echo "%WORKINGDIR%NuGet.exe" pack "%NUPKG%\%TARGETNAME%.nuspec" -Verbosity normal -OutputDirectory "%NUPKG%" -NonInteractive 
"%WORKINGDIR%NuGet.exe" pack "%NUPKG%\%TARGETNAME%.nuspec" -Verbosity normal -OutputDirectory "%NUPKG%" -NonInteractive 

:echo Copy %NUPKG%\*.nupkg
xcopy.exe "%NUPKG%\*.nupkg" "c:\nuget-local\" /y > nul

:end
echo -- NuGet Process End --
exit /b 0