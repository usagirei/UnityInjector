@echo off

set MSBUILD=C:\Program Files (x86)\MSBuild\14.0\Bin
set BUILD=%CD%\.build

set PATH=%MSBUILD%;%PATH%

if not exist "%BUILD%" (
	echo Place a copy of the MSBuildTasks Binaries in the .build directory
	echo Source code can be found at https://github.com/loresoft/msbuildtasks
	echo You may need to build the binaries from source
	goto end
)
if not exist "%MSBUILD%\msbuild.exe" (
	echo MSBuild Not Found, Aborting
	goto end
)

echo ---------- Cleaning Solution
del Build\* /q /s
echo ---------- Building Solution
msbuild.exe UnityInjector\.build /p:Platform=x86
msbuild.exe UnityInjector_Patcher\.build /p:Platform=x86
:end
pause