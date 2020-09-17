# Safe Exam Browser, Version 3.x

Refactored version of Safe Exam Browser for Windows with Chromium as integrated browser engine.

### Requirements

SEB 3.x requires the prerequisites listed below in order to work correctly. These are automatically installed with the setup bundle and need only be manually installed when using the MSI packages.

* .NET Framework 4.7.2 Runtime: https://dotnet.microsoft.com/download/dotnet-framework/net472
* Visual C++ 2015 Redistributable: https://www.microsoft.com/en-us/download/details.aspx?id=53840

### Project Status

**_DISCLAIMER_**\
**The builds linked below are for testing purposes only.** They may be unstable and should thus _never_ be used in a production environment! Always use the latest, official release version of SEB.

| Aspect          | Status                                                                                                                | Details                                                         |
| --------------- | --------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------- |
| Release Build   | ![Release Build Status](https://sebdev-let.ethz.ch/api/projects/status/kq78qrjtnpk82ti0?svg=true)                     | https://sebdev-let.ethz.ch/project/appveyor/seb-win-refactoring |
| Test Build      | ![Test Build Status](https://ci.appveyor.com/api/projects/status/a56akt9r174570m7?svg=true)                           | https://ci.appveyor.com/project/dbuechel/seb-win-refactoring    |
| Test Run        | ![AppVeyor Tests](https://img.shields.io/appveyor/tests/dbuechel/seb-win-refactoring?logo=appveyor&logoColor=%23ccc)  | https://ci.appveyor.com/project/dbuechel/seb-win-refactoring    |
| Code Coverage   | ![Code Coverage](https://codecov.io/gh/SafeExamBrowser/seb-win-refactoring/branch/master/graph/badge.svg)             | https://codecov.io/gh/SafeExamBrowser/seb-win-refactoring       |
| Issue Status    | ![GitHub Issues](https://img.shields.io/github/issues/safeexambrowser/seb-win-refactoring?logo=github)                | https://github.com/SafeExamBrowser/seb-win-refactoring/issues   |
| Downloads       | ![GitHub All Releases](https://img.shields.io/github/downloads/safeexambrowser/seb-win-refactoring/total?logo=github) | https://github.com/SafeExamBrowser/seb-win-refactoring/releases |
| Development     | ![GitHub Last Commit](https://img.shields.io/github/last-commit/safeexambrowser/seb-win-refactoring?logo=github)      | n/a                                                             |
| Repository Size | ![GitHub Repo Size](https://img.shields.io/github/repo-size/safeexambrowser/seb-win-refactoring?logo=github)          | n/a                                                             |



# Updated

## Build Instruction

### Prerequisites

1. Make sure that .NET Framework and Visual C++ Redistributable (see links above) installed
2. You will need Visual Studio 2019 (not tested on 2017 version)
3. Install the latest stable version of WIX Toolset https://wixtoolset.org/releases/v3.11.2/stable
4. Install WiX Toolset Visual Studio 2019 Extension https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2019Extension
5. Install Windows 10 SDK (requried to sign assemblies) https://go.microsoft.com/fwlink/?LinkID=698771
6. Inside a folder where to you installed Windows SDK search for "signtool.exe". There should be at least 2 locations, something like:
* x86 -> c:\Program Files (x86)\Windows Kits\10\bin\x86\
* x64 -> c:\Program Files (x86)\Windows Kits\10\bin\x64\
7. Next, depends on your system architecture (I assume you have 64bit windows installed), copy to clipboard a path to "signtool.exe"
8. We need to make that path accessible from Windows command line. Go to Environment System settings:
* Press WIN + PAUSE buttons on a keyboard
* Click the "Advanced System Settings" link on the left. In the next dialog, you will see the Environment Variables... button in the bottom of the Advanced tab.
* Under the lower table (System variables) click "Create" and give a name "Win10SDK", and paste a path you have copied at step 7.
* Scroll a table and search for "PATH", double click on it.
* Create a new record: "%Win10SDK%" or "%Win10SDK%\" (depends on trailing backslash symbol in the path you've added in previous step)
* Click OK and close all system windows (Win 10 doesn't require a reboot here, not sure about other versions)

### Cloning Git sources

1. Since you're reading this I think you have already cloned a repository
2. Git doesn't allow to commit large files. So, you should download additionally two files and place it into ".\SetupBundle\chrominimum" folder
* https://github.com/SafeExamBrowser/Chrominimum/releases/download/1.1.0/Chrominimum_1.1.0.21_x64_Setup.msi
* https://github.com/SafeExamBrowser/Chrominimum/releases/download/1.1.0/Chrominimum_1.1.0.21_x86_Setup.msi

** Now you can open a solution in your Visual Studio and build a solution **
** Do not forget to select RELEASE configuration for solution otherwise you will get a assemblies with debug information included **

* The build process will fail on a step of bulding Installers, because both of project "Setup" and "SetupBundle" preconfigured to sign each module and installation files.
Since we don't have a configured certificate, we should reconfigure it:
- Right-click on Setup/SetupBundle project -> Properties -> Build events
- Check "Pre-Build" events. For each line which starting from "signtool" please check parameter options and modify it for a proper certificate
- Also please check files "Setup.wixproj" and "SetupBundle.wixproj" in editor (like notepad) for a correct signtool parameters.

!!!