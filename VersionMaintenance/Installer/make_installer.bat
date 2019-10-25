:: VPKSoft.VersionCheck
:: 
:: A version checker for VPKSoft products.
:: Copyright © 2019 VPKSoft, Petteri Kautonen
:: 
:: Contact: vpksoft@vpksoft.net
:: 
:: This file is part of VPKSoft.VersionCheck.
:: 
:: VPKSoft.VersionCheck is free software: you can redistribute it and/or modify
:: it under the terms of the GNU Lesser General Public License as published by
:: the Free Software Foundation, either version 3 of the License, or
:: (at your option) any later version.
:: 
:: VPKSoft.VersionCheck is distributed in the hope that it will be useful,
:: but WITHOUT ANY WARRANTY; without even the implied warranty of
:: MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
:: GNU Lesser General Public License for more details.
:: 
:: You should have received a copy of the GNU Lesser General Public License
:: along with VPKSoft.VersionCheck.  If not, see <http://www.gnu.org/licenses/>.
 
 del ..\bin\Release\*.pdb
 del ..\bin\Release\*.xml
 del ..\bin\Release\*.config
 "c:\Program Files (x86)\NSIS\makensisw.exe" .\setup_version_maintenance.nsi
pause

