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

function ArchiveVersion($postdata)
{
    // the format is APIKEY;VERSION_ENTRY_ID;DELETE(1 / 0)..
    try
    {
        $software_data = explode(";", $postdata);

        if (sizeof($software_data) != 3)
        {
            array_push($result, CreateLocalizedChangeHistoryResult("-1", "-1", "Fail: Invalid POST value!", "4"));
            echo json_encode($result);
            return;
        }	            

        // validate the right to access the version database..
        if (!APIKeyCorrect($software_data[0]))
        {
            echo CreateGeneralResult("Fail: Invalid API key!", "3", "True");
            return;
        }
        
        $id = $software_data[1];
        $delete = $software_data[2];

        // create a database connection..
        $version_db = CreateDBConnection();        
     
        $sentence = 
            "INSERT INTO VERSIONS_ARCHIVE(SOFTWARENAME, VERSIONSTRING, DOWNLOADLINK,\r\n" .
            "RELEASEDATE, ISDIRECT_DOWNLOAD, METADATA)\r\n" .
            "SELECT SOFTWARENAME, VERSIONSTRING, DOWNLOADLINK, RELEASEDATE, ISDIRECT_DOWNLOAD, METADATA\r\n" .
            "FROM\r\n" .
            "VERSIONS WHERE ID = :id;\r\n";
                
        $stmt = $version_db->prepare($sentence);
        $stmt->execute(array(":id" => $id));        
        $stmp = null;
        
        if ($delete == "1")
        {
            $sentence = 
                "DELETE FROM VERSIONS WHERE ID = :id;\r\n";
            $stmt = $version_db->prepare($sentence);
            $stmt->execute(array(":id" => $id));        
            $stmp = null;
        }

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

function ArchiveVersionHistory($postdata)
{
    // the format is APIKEY;VERSION_ENTRY_ID;DELETE(1 / 0)..
    try
    {
        $software_data = explode(";", $postdata);

        if (sizeof($software_data) != 3)
        {
            array_push($result, CreateLocalizedChangeHistoryResult("-1", "-1", "Fail: Invalid POST value!", "4"));
            echo json_encode($result);
            return;
        }	            

        // validate the right to access the version database..
        if (!APIKeyCorrect($software_data[0]))
        {
            echo CreateGeneralResult("Fail: Invalid API key!", "3", "True");
            return;
        }
        
        $id = $software_data[1];
        $delete = $software_data[2];

        // create a database connection..
        $version_db = CreateDBConnection();        
     
        $sentence = 
            "INSERT INTO CHANGEHISTORY_ARCHIVE(APP_ID, SOFTWARENAME, VERSIONSTRING,\r\n" .
            "CULTUREMAIN, CULTURESPECIFIC, METADATA)\r\n" .
            "SELECT APP_ID, SOFTWARENAME, VERSIONSTRING, CULTUREMAIN, CULTURESPECIFIC, METADATA\r\n" .
            "FROM\r\n" .
            "CHANGEHISTORY WHERE ID = :id;\r\n";
                
        $stmt = $version_db->prepare($sentence);
        $stmt->execute(array(":id" => $id));        
        $stmp = null;
        
        if ($delete == "1")
        {
            $sentence = 
                "DELETE FROM CHANGEHISTORY WHERE ID = :id;\r\n";
            $stmt = $version_db->prepare($sentence);
            $stmt->execute(array(":id" => $id));        
            $stmp = null;
        }

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
