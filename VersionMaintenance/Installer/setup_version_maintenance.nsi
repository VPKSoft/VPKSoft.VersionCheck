# VPKSoft.VersionCheck
# 
# A version checker for VPKSoft products.
# Copyright © 2019 VPKSoft, Petteri Kautonen
# 
# Contact: vpksoft@vpksoft.net
# 
# This file is part of VPKSoft.VersionCheck.
# 
# VPKSoft.VersionCheck is free software: you can redistribute it and/or modify
# it under the terms of the GNU Lesser General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
# 
# VPKSoft.VersionCheck is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU Lesser General Public License for more details.
# 
# You should have received a copy of the GNU Lesser General Public License
# along with VPKSoft.VersionCheck.  If not, see <http://www.gnu.org/licenses/>.


SetCompressor /SOLID /FINAL lzma

Name "VersionMaintenance"

# General Symbol Definitions
!define REGKEY "SOFTWARE\$(^Name)"
!define VERSION 1.0.1.3
!define COMPANY VPKSoft
!define URL http://www.vpksoft.net

# MUI Symbol Definitions
!define MUI_ICON ..\version-icon-10.ico
!define MUI_FINISHPAGE_NOAUTOCLOSE
!define MUI_STARTMENUPAGE_REGISTRY_ROOT HKLM
!define MUI_STARTMENUPAGE_NODISABLE
!define MUI_STARTMENUPAGE_REGISTRY_KEY ${REGKEY}
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME StartMenuGroup
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "VersionMaintenance"
!define MUI_UNICON .\uninstall.ico
!define MUI_UNFINISHPAGE_NOAUTOCLOSE
!define MUI_LANGDLL_REGISTRY_ROOT HKLM
!define MUI_LANGDLL_REGISTRY_KEY ${REGKEY}
!define MUI_LANGDLL_REGISTRY_VALUENAME InstallerLanguage
!define MUI_FINISHPAGE_RUN # this needs to be not-defined for the MUI_FINISHPAGE_RUN_FUNCTION to work..
!define MUI_FINISHPAGE_RUN_FUNCTION "RunAsCurrentUser" # The check box for a query whether to run the installed software as the current user after the installation..
BrandingText "VersionMaintenance"

!include 'LogicLib.nsh'
!include "x64.nsh"
!include "InstallOptions.nsh"
!include "DotNetChecker.nsh"
!include "nsProcess.nsh"


# Included files
!include Sections.nsh
!include MUI2.nsh

# Reserved Files
!insertmacro MUI_RESERVEFILE_LANGDLL

# Variables
Var StartMenuGroup

# Installer pages
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE ..\..\COPYING # the GNU Lesser General Public License still needs the whole license..
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_STARTMENU Application $StartMenuGroup
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

# Installer languages
!insertmacro MUI_LANGUAGE English
!insertmacro MUI_LANGUAGE Finnish

# Installer attributes
OutFile setup_version_maintenance.exe # this is "static" as there is no need to install the software separately..
InstallDir "$PROGRAMFILES64\VersionMaintenance"
CRCCheck on
XPStyle on
ShowInstDetails hide
VIProductVersion 1.0.1.3
VIAddVersionKey /LANG=${LANG_ENGLISH} ProductName "VersionMaintenance installer"
VIAddVersionKey /LANG=${LANG_ENGLISH} ProductVersion "${VERSION}"
VIAddVersionKey /LANG=${LANG_ENGLISH} CompanyName "${COMPANY}"
VIAddVersionKey /LANG=${LANG_ENGLISH} CompanyWebsite "${URL}"
VIAddVersionKey /LANG=${LANG_ENGLISH} FileVersion "${VERSION}"
VIAddVersionKey /LANG=${LANG_ENGLISH} FileDescription "VersionMaintenance"
VIAddVersionKey /LANG=${LANG_ENGLISH} LegalCopyright "Copyright © VPKSoft 2019"
InstallDirRegKey HKLM "${REGKEY}" Path
ShowUninstDetails hide

# Installer sections
Section -Main SEC0000
    SetOutPath $INSTDIR
    SetOverwrite on

    !insertmacro CheckNetFramework 47	
   
    SetOutPath $INSTDIR

	# killing the previous version softly..
    ${nsProcess::FindProcess} "VersionMaintenance.exe" $R0
    ${If} $R0 == 0
		${nsProcess::CloseProcess} "VersionMaintenance.exe" $R0
	${EndIf}
	
	${nsProcess::Unload} # dispose..
	    
    File /r ..\bin\Release\*.* # assume that all the required files are there..
	File ..\version-icon-10.ico # icon (?)..
   
	SetOutPath $SMPROGRAMS\$StartMenuGroup
    CreateShortcut "$SMPROGRAMS\$StartMenuGroup\VersionMaintenance.lnk" $INSTDIR\VersionMaintenance.exe	
	
    WriteRegStr HKLM "${REGKEY}\Components" Main 1	
SectionEnd

Section -post SEC0001
    WriteRegStr HKLM "${REGKEY}" Path $INSTDIR
    SetOutPath $INSTDIR
    WriteUninstaller $INSTDIR\uninstall_version_maintenance.exe
    !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    SetOutPath $SMPROGRAMS\$StartMenuGroup
    CreateShortcut "$SMPROGRAMS\$StartMenuGroup\$(^UninstallLink).lnk" $INSTDIR\uninstall_version_maintenance.exe
    !insertmacro MUI_STARTMENU_WRITE_END
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayName "$(^Name)"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayVersion "${VERSION}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" Publisher "${COMPANY}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" URLInfoAbout "${URL}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayIcon $INSTDIR\uninstall_version_maintenance.exe
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" UninstallString $INSTDIR\uninstall_version_maintenance.exe
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoModify 1
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoRepair 1
SectionEnd

# Macro for selecting uninstaller sections
!macro SELECT_UNSECTION SECTION_NAME UNSECTION_ID
    Push $R0
    ReadRegStr $R0 HKLM "${REGKEY}\Components" "${SECTION_NAME}"
    StrCmp $R0 1 0 next${UNSECTION_ID}
    !insertmacro SelectSection "${UNSECTION_ID}"
    GoTo done${UNSECTION_ID}
next${UNSECTION_ID}:
    !insertmacro UnselectSection "${UNSECTION_ID}"
done${UNSECTION_ID}:
    Pop $R0
!macroend

# Uninstaller sections
Section /o -un.Main UNSEC0000
    Delete /REBOOTOK "$SMPROGRAMS\$StartMenuGroup\VersionMaintenance.lnk"

	RMDir /r /REBOOTOK $INSTDIR
	
	Call un.DeleteLocalData
		
    DeleteRegValue HKLM "${REGKEY}\Components" Main
SectionEnd

Section -un.post UNSEC0001
    DeleteRegKey HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)"
    Delete /REBOOTOK "$SMPROGRAMS\$StartMenuGroup\$(^UninstallLink).lnk"
    Delete /REBOOTOK $INSTDIR\uninstall_version_maintenance.exe
    DeleteRegValue HKLM "${REGKEY}" StartMenuGroup
    DeleteRegValue HKLM "${REGKEY}" Path
    DeleteRegKey /IfEmpty HKLM "${REGKEY}\Components"
    DeleteRegKey /IfEmpty HKLM "${REGKEY}"	
	
    RMDir /REBOOTOK $SMPROGRAMS\$StartMenuGroup
	
    RMDir /REBOOTOK $INSTDIR
SectionEnd

# Installer functions
Function .onInit
    InitPluginsDir
    !insertmacro MUI_LANGDLL_DISPLAY    
FunctionEnd

# Uninstaller functions
Function un.onInit
    ReadRegStr $INSTDIR HKLM "${REGKEY}" Path
    !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuGroup
    !insertmacro MUI_UNGETLANGUAGE
    !insertmacro SELECT_UNSECTION Main ${UNSEC0000}
FunctionEnd

# verify from the user that the actual application stored data should be deleted..
Function un.DeleteLocalData
	MessageBox MB_ICONQUESTION|MB_YESNO "$(DeleteUserData)" IDYES deletelocal IDNO nodeletelocal
	deletelocal:
    RMDir /r /REBOOTOK "$LOCALAPPDATA\VersionMaintenance"
	nodeletelocal:
FunctionEnd

# a function to execute the installed software as non-administrator.. 
Function RunAsCurrentUser	
	ShellExecAsUser::ShellExecAsUser "" "$INSTDIR\VersionMaintenance.exe"
FunctionEnd


# Installer Language Strings
#START: Localization..
LangString ^UninstallLink ${LANG_ENGLISH} "Uninstall $(^Name)"
LangString ^UninstallLink ${LANG_FINNISH} "Poista $(^Name)"

# localization..
LangString DeleteUserData ${LANG_FINNISH} "Poista paikalliset käyttäjätiedot?"
LangString DeleteUserData ${LANG_ENGLISH} "Delete local user data?"
#END: localization..
