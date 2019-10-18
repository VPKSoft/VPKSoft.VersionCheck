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

function IncreaseDownloadCount($postdata)
{
    try
    {
        // the format is SOFTWARENAME
        $name = $postdata;

        // create a database connection..
        $version_db = CreateDBConnection();

        $sentence = // conditional insert..
            "INSERT INTO DLCOUNT\r\n" .
            "(ID, SOFTWARENAME)\r\n" .
            "SELECT (SELECT ID FROM VERSIONS WHERE SOFTWARENAME = :name), :name\r\n" .
            "WHERE NOT EXISTS(SELECT * FROM DLCOUNT WHERE SOFTWARENAME = :name)\r\n";

        $stmt = $version_db->prepare($sentence);
        $stmt->execute(array(":name" => $name));
        $stmp = null;

        $sentence = // the update goes after with no conditions..
            "UPDATE DLCOUNT\r\n" .
            "SET\r\n" .
            "DLCOUNT = (SELECT DLCOUNT + 1 FROM DLCOUNT WHERE SOFTWARENAME = :name)\r\n" .
            "WHERE SOFTWARENAME = :name\r\n";

        $stmt = $version_db->prepare($sentence);
        $stmt->execute(array(":name" => $name));
        $stmp = null;

        $sentence = null;
        $stmt = null;
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

function UpdateInsertVersion($postdata)
{
    try 
    {
        // the format is SOFTWARENAME;VERSIONSTRING;DOWNLOADLINK;RELEASEDATE;ISDIRECT_DOWNLOAD(True / False);METADATA;APIKEY
        $software_data = explode(";", $postdata);

        if (sizeof($software_data) != 7)
        {
            // an invalid POST value was given..
            echo CreateGeneralResult("Fail: Invalid POST value, required 7 values, got: " . sizeof($software_data) . "!", "4", "True");
            return;
        }		

        $secret_file = $software_data[6];

        // validate the right to manipulate the version database..
        if (!APIKeyCorrect($secret_file))
        {
            echo CreateGeneralResult("Fail: Invalid API key!", "3", "True");
            return;
        }

        // get the data..
        $name = $software_data[0];
        $version = $software_data[1];
        $link = $software_data[2];
        $release_date = $software_data[3];
        $direct = $software_data[4];
        $meta = $software_data[5];

        // create a database connection..		
        $version_db = CreateDBConnection();

        $sentence = // conditional insert..
            "INSERT INTO VERSIONS\r\n" .
            "(SOFTWARENAME, VERSIONSTRING, DOWNLOADLINK, RELEASEDATE, ISDIRECT_DOWNLOAD, METADATA)\r\n" .
            "SELECT :name, :version, :link, :rdate, :dlink, :meta\r\n" .
            "WHERE NOT EXISTS(SELECT * FROM VERSIONS WHERE SOFTWARENAME = :name)\r\n";

        $stmt = $version_db->prepare($sentence);

        $stmt->execute(array(":name" => $name, ":version" => $version, ":link" => $link, ":rdate" => $release_date, ":dlink" => $direct, ":meta" => $meta ));
        $stmp = null;
        $sentence = null;

        $sentence = // the update goes after with no conditions..
            "UPDATE VERSIONS\r\n" .
            "SET\r\n" .
            "VERSIONSTRING = :version,\r\n" .
            "DOWNLOADLINK = :link,\r\n" .
            "RELEASEDATE = :rdate,\r\n" .
            "ISDIRECT_DOWNLOAD = :dlink,\r\n" .
            "METADATA = :meta\r\n" .
            "WHERE SOFTWARENAME = :name\r\n";


        $stmt = $version_db->prepare($sentence);
        $stmt->execute(array(":name" => $name, ":version" => $version, ":link" => $link, ":rdate" => $release_date, ":dlink" => $direct, ":meta" => $meta ));		
        $sentence = null;
        $stmt = null;
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

function AddVersionChanges($postdata)
{
    try
    {
        // the format is APIKEY;ID;SOFTWARENAME;VERSIONSTRING;METADATA;CULTURE
        $software_data = explode(";", $postdata);		

        if (sizeof($software_data) != 6)
        {
            echo CreateGeneralResult("Fail: Invalid POST value, required 6 values, got: " . sizeof($software_data) . "!", "4", "True");            
            return;
        }	

        // get the data..
        $secret_file = $software_data[0];		
        $id = $software_data[1];
        $software_name = $software_data[2];

        // validate the right to manipulate the version database..
        if (!APIKeyCorrect($secret_file))
        {
            echo CreateGeneralResult("Fail: Invalid API key!", "3", "True");
            return;
        }

        $version = $software_data[3];
        $meta = $software_data[4];
        $culture = $software_data[5];			

        $culture_parts = explode("-", $culture);
        $culture_main = $culture_parts[0];
        $culture_specific = null;

        if (sizeof($culture_parts) == 2)
        {
            $culture_specific = $culture_parts[1];
        }

        // create a database connection..		
        $version_db = CreateDBConnection();

        $sentence = // conditional insert..
            "INSERT INTO CHANGEHISTORY\r\n" .
            "(APP_ID, SOFTWARENAME, VERSIONSTRING, CULTUREMAIN, CULTURESPECIFIC, METADATA)\r\n" .
            "SELECT :id, :softwarename, :versionstring, :culturemain, :culturespecific, :meta\r\n" .
            "WHERE NOT EXISTS(SELECT * FROM CHANGEHISTORY WHERE APP_ID = :id AND SOFTWARENAME = :softwarename AND\r\n" . 
            "CULTUREMAIN = :culturemain AND IFNULL(CULTURESPECIFIC, :culturespecific) = :culturespecific AND VERSIONSTRING = :versionstring) AND\r\n" .
            "EXISTS(SELECT * FROM VERSIONS WHERE ID = :id);\r\n";

        $stmt = $version_db->prepare($sentence);
        $stmt->execute(array(":id" => $id, ":softwarename" => $software_name, ":versionstring" => $version,
            ":meta" => $meta, ":culturemain" => $culture_main, ":culturespecific" => $culture_specific));

        $stmp = null;

        $sentence = // the update goes after with no conditions..
            "UPDATE CHANGEHISTORY\r\n" .
            "SET\r\n" .
            "SOFTWARENAME = :softwarename,\r\n" .
            "METADATA = :meta\r\n" .
            "WHERE\r\n" .
            "CULTUREMAIN = :culturemain AND\r\n" .
            "VERSIONSTRING = :versionstring AND\r\n" .
            "APP_ID = :id AND\r\n" .
            "IFNULL(CULTURESPECIFIC, :culturespecific) = :culturespecific;\r\n";

        $stmt = $version_db->prepare($sentence);
        $stmt->execute(array(":id" => $id, ":softwarename" => $software_name, ":versionstring" => $version,
            ":meta" => $meta, ":culturemain" => $culture_main, ":culturespecific" => $culture_specific));

        $stmp = null;
        $sentence = null;
        $stmt = null;
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