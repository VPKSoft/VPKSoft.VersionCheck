/*
VPKSoft.VersionCheck

A version checker for VPKSoft products.
Copyright © 2019 VPKSoft, Petteri Kautonen

Contact: vpksoft@vpksoft.net

This file is part of VPKSoft.VersionCheck.

VPKSoft.VersionCheck is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

VPKSoft.VersionCheck is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with VPKSoft.VersionCheck.  If not, see <http://www.gnu.org/licenses/>.
*/

CREATE TABLE IF NOT EXISTS VERSIONS(
ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
SOFTWARENAME TEXT NOT NULL,
VERSIONSTRING TEXT NOT NULL,
DOWNLOADLINK TEXT NOT NULL,
RELEASEDATE TEXT NOT NULL,
ISDIRECT_DOWNLOAD TEXT NULL, -- True / False
METADATA TEXT NULL); 

/*
INSERT INTO VERSIONS 
(SOFTWARENAME, VERSIONSTRING, DOWNLOADLINK, RELEASEDATE, ISDIRECT_DOWNLOAD) 
VALUES(
'VPKSoft.Utils', 
'1.0.0.4', 
'https://www.vpksoft.net/2015-03-31-13-33-28/libraries/vpksoft-utils', 
'2016-06-01 14:48:00',
'False');
*/

CREATE TABLE IF NOT EXISTS DLCOUNT(
ID INTEGER PRIMARY KEY NOT NULL,
SOFTWARENAME TEXT NOT NULL,
DLCOUNT INTEGER NOT NULL DEFAULT 0);

-- Check if a table exists: 
-- SELECT CASE WHEN EXISTS (SELECT * FROM SQLITE_MASTER WHERE TYPE='table' AND NAME = 'CHANGEHISTORY') THEN 1 ELSE 0 END AS TABLE_EXISTS;

CREATE TABLE IF NOT EXISTS CHANGEHISTORY(
ID INTEGER PRIMARY KEY AUTOINCREMENTNOT NULL,
APP_ID INTEGER NOT NULL,
SOFTWARENAME TEXT NOT NULL,
VERSIONSTRING TEXT NOT NULL,
CULTUREMAIN TEXT NOT NULL,
CULTURESPECIFIC TEXT NULL,
METADATA TEXT NOT NULL);