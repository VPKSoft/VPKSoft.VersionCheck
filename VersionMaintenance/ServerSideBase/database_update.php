<?php
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

include_once "functions.php";

function UpdateDatabase($postdata)
{
    try
    {
        // the format is APIKEY
        // validate the right to manipulate the version database..
        if (!APIKeyCorrect($postdata))
        {
            echo CreateGeneralResult("Fail: Invalid API key!", "3", "True");
            return;
        }

        if (!file_exists(SQLiteDatabase()))
        {
            try
            {
                if (!rename("empty.sqlite", SQLiteDatabase()))
                {
                    echo CreateGeneralResult("Fail: File rename!", "1", "True");
                    return;
                }
            }
            catch(Exception $e)
            {
                echo CreateGeneralResult("Fail: " . $e->getMessage(), "2", "True");
                return;
            }
        }

        // create a database connection..
        $version_db = CreateDBConnection();

        $sentence = 
            "CREATE TABLE IF NOT EXISTS VERSIONS(\r\n" .
            "ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n" .
            "SOFTWARENAME TEXT NOT NULL,\r\n" .
            "VERSIONSTRING TEXT NOT NULL,\r\n" .
            "DOWNLOADLINK TEXT NOT NULL,\r\n" .
            "RELEASEDATE TEXT NOT NULL,\r\n" .
            "ISDIRECT_DOWNLOAD TEXT NULL, -- True / False\r\n" .
            "METADATA TEXT NULL);\r\n";

        $version_db->exec($sentence);

        $sentence =
            "CREATE TABLE IF NOT EXISTS DLCOUNT(\r\n" .
            "ID INTEGER PRIMARY KEY NOT NULL,\r\n" .
            "SOFTWARENAME TEXT NOT NULL,\r\n" .
            "DLCOUNT INTEGER NOT NULL DEFAULT 0);\r\n";

        $version_db->exec($sentence);

        $sentence =
            "CREATE TABLE IF NOT EXISTS CHANGEHISTORY(\r\n" .
            "ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n" .
            "APP_ID INTEGER NOT NULL,\r\n" .
            "SOFTWARENAME TEXT NOT NULL,\r\n" .
            "VERSIONSTRING TEXT NOT NULL,\r\n" .
            "CULTUREMAIN TEXT NOT NULL,\r\n" .
            "CULTURESPECIFIC TEXT NULL,\r\n" .
            "METADATA TEXT NOT NULL);\r\n";
        
        $version_db->exec($sentence);

        $sentence = 
            "CREATE TABLE IF NOT EXISTS VERSIONS_ARCHIVE(\r\n" .
            "ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n" .
            "SOFTWARENAME TEXT NOT NULL,\r\n" .
            "VERSIONSTRING TEXT NOT NULL,\r\n" .
            "DOWNLOADLINK TEXT NOT NULL,\r\n" .
            "RELEASEDATE TEXT NOT NULL,\r\n" .
            "ISDIRECT_DOWNLOAD TEXT NULL, -- True / False\r\n" .
            "METADATA TEXT NULL);\r\n";

        $version_db->exec($sentence);
        
        $sentence =
            "CREATE TABLE IF NOT EXISTS CHANGEHISTORY_ARCHIVE(\r\n" .
            "ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n" .
            "APP_ID INTEGER NOT NULL,\r\n" .
            "SOFTWARENAME TEXT NOT NULL,\r\n" .
            "VERSIONSTRING TEXT NOT NULL,\r\n" .
            "CULTUREMAIN TEXT NOT NULL,\r\n" .
            "CULTURESPECIFIC TEXT NULL,\r\n" .
            "METADATA TEXT NOT NULL);\r\n";
        
        $version_db->exec($sentence);
        

        $sentence = null;

        $version_db = null; // release the database connection..
        echo CreateGeneralResult();
        return;
    }
    catch (Exception $e) // just exit with an error..
    {
        echo CreateGeneralResult("Fail: " . $e->getMessage(), "2", "True");
        return;
    }    
}
