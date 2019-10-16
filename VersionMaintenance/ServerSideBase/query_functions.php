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

function QueryVersion($postdata)
{
    try 
    {
        // the format is SOFTWARENAME;OPTIONAL_LANG_STRING(i.e. fi-FI or fi)
        $array_result = explode(";", $postdata);

        $software_name = $array_result[0];
        $sofware_lang = "en-US"; // default to English (United States)..

        if (sizeof($array_result) == 2) // to support the previous version, do a range check..
        {
            // an invalid POST value was given..
            $sofware_lang = $array_result[1];
        }

        // create a database connection..
        $version_db = CreateDBConnection();

        // the version of the database might have changed, so do check if a new table exists..
        $table_exists = TableExists($version_db, "CHANGEHISTORY");

        $lang_split = explode("-", $sofware_lang);

        $select = 
            "SELECT V.*, IFNULL(D.DLCOUNT, 0) AS DLCOUNT,\r\n" .
            "--localization via some SQL magic..\r\n" .
            "IFNULL(IFNULL(H1.METADATA, H2.METADATA), V.METADATA) AS METADATANEW\r\n" .
            "FROM\r\n" .
            "VERSIONS V\r\n" .
            "  LEFT OUTER JOIN DLCOUNT D ON (D.ID = V.ID)\r\n" .
            "  LEFT OUTER JOIN CHANGEHISTORY H1 ON (H1.APP_ID = V.ID AND H1.CULTUREMAIN = :en AND\r\n" .
            "    IFNULL(H1.CULTURESPECIFIC, :us) = :us AND V.VERSIONSTRING = H1.VERSIONSTRING)\r\n" .
            "  LEFT OUTER JOIN CHANGEHISTORY H2 ON (H2.APP_ID = V.ID AND H2.CULTUREMAIN = :lang1 AND\r\n" .
            "    (IFNULL(H2.CULTURESPECIFIC, :lang2) = :lang2 OR :lang2 IS NULL) AND V.VERSIONSTRING = H2.VERSIONSTRING)\r\n" .
            "WHERE V.SOFTWARENAME = :name";

        if (!$table_exists)
        {
            $select = 
                "SELECT V.*, IFNULL(D.DLCOUNT, 0) AS DLCOUNT,\r\n" .
                "V.METADATA AS METADATANEW\r\n" .
                "FROM\r\n" .
                "VERSIONS V\r\n" .
                "  LEFT OUTER JOIN DLCOUNT D ON (D.ID = V.ID)\r\n" .
                "WHERE V.SOFTWARENAME = :name";			
        }

        $stmt = $version_db->prepare($select);

        if ($table_exists)
        {
            // the default culture is en-US..
            $stmt->execute(array(":name" => $software_name, ":lang1" => $lang_split[0], ":lang2" => $lang_split[1], ":en" => "en", ":us" => "US"));
        }
        else 
        {
            $stmt->execute(array(":name" => $software_name));
        }

        $r = $stmt->fetchAll();

        $return_value = CreateSoftwareEntryResult(true);

        foreach ($r as $row => $entry)
        {
            $return_value = 
                CreateSoftwareEntryResult
                    (
                        true,
                        $entry["ID"],
                        $software_name,
                        $entry["VERSIONSTRING"],
                        $entry["DOWNLOADLINK"],
                        $entry["RELEASEDATE"],
                        $entry["ISDIRECT_DOWNLOAD"],
                        $entry["METADATA"],
                        $entry["DLCOUNT"],
                        $table_exists ? $entry["METADATANEW"] : "");

            break; // only one entry with the same name should exist..
        }

        $version_db = null; // release the database connection..
        $stmt = null;			
        echo $return_value;
        return;
    }
    catch (Exception $e) // just exit with an error..
    {
        echo CreateSoftwareEntryResult(true, "2", $e->getMessage());
        return;
    }    
}

function GetVersionChanges($postdata)
{
    // the format is APIKEY;APP_ID
    // initialize an empty array..
    $result = array();
    try
    {
        $software_data = explode(";", $postdata);                 

        if (sizeof($software_data) != 2)
        {
            array_push($result, CreateLocalizedChangeHistoryResult("-1", "-1", "Fail: Invalid POST value!", "4"));
            echo json_encode($result);
            return;
        }	            

        // validate the right to access the version database..
        if (!APIKeyCorrect($software_data[0]))
        {
            array_push($result, CreateLocalizedChangeHistoryResult("-1", "-1", "Fail: Invalid API key!", "3"));
            echo json_encode($result);
            return;
        }

        // create a database connection..
        $version_db = CreateDBConnection();                        

        $select = 
            "SELECT * FROM CHANGEHISTORY WHERE APP_ID = :app_id;";

        $stmt = $version_db->prepare($select);

        // the default culture is en-US..
        $stmt->execute(array(":app_id" => $software_data[1]));

        $r = $stmt->fetchAll(\PDO::FETCH_ASSOC);

        foreach ($r as $row => $entry)
        {
            $array_result = CreateLocalizedChangeHistoryResult(
                $entry["ID"],
                $entry["APP_ID"],
                $entry["SOFTWARENAME"],
                $entry["VERSIONSTRING"],
                $entry["CULTUREMAIN"],
                $entry["CULTURESPECIFIC"],
                $entry["METADATA"]);

            array_push($result, $array_result);
        }
        $version_db = null; // release the database connection..
        $stmt = null;            
        echo json_encode($result);
        return;            
    }
    catch (Exception $e) // just exit with an error..
    {
        array_push($result, CreateLocalizedChangeHistoryResult("-1", "-1", "Fail: " . $e->getMessage(), "2"));
        echo json_encode($result);
        return;
    }    
}

function GetSoftwareList($postdata)
{
    // the format is APIKEY;language-region; i.e: fi-FI or fi
    // initialize an empty array..
    $result = array();

    try 
    {		           
        $software_data = explode(";", $postdata);                

        // validate the right to access the version database..
        if (!APIKeyCorrect($software_data[0]))
        {
            array_push($result, CreateSoftwareEntryResult(false, "-1", "Fail: Invalid API key!", "3"));
            echo json_encode($result);
            return;
        }            

        // create a database connection..
        $version_db = CreateDBConnection();            

        $sofware_lang = "en-US"; // default to English (United States)..


        if (sizeof($software_data) == 2) // to support the previous version, do a range check..
        {
            // an invalid POST value was given..
            $sofware_lang = $array_result[1];
        }            

        $lang_split = explode("-", $sofware_lang);

        // the version of the database might have changed, so do check if a new table exists..
        $table_exists = TableExists($version_db, "CHANGEHISTORY");           

        $select = 
            "SELECT V.*, IFNULL(D.DLCOUNT, 0) AS DLCOUNT,\r\n" .
            "--localization via some SQL magic..\r\n" .
            "IFNULL(IFNULL(H1.METADATA, H2.METADATA), V.METADATA) AS METADATANEW\r\n" .                    
            "FROM\r\n" .
            "VERSIONS V\r\n" .
            "  LEFT OUTER JOIN CHANGEHISTORY H1 ON (H1.ID = V.ID AND H1.CULTUREMAIN = :en AND\r\n" .
            "    IFNULL(H1.CULTURESPECIFIC, :us) = :us AND V.VERSIONSTRING = H1.VERSIONSTRING)\r\n" .
            "  LEFT OUTER JOIN CHANGEHISTORY H2 ON (H2.ID = V.ID AND H2.CULTUREMAIN = :lang1 AND\r\n" .
            "    (IFNULL(H2.CULTURESPECIFIC, :lang2) = :lang2 OR :lang2 IS NULL) AND V.VERSIONSTRING = H2.VERSIONSTRING)\r\n" .
            "  LEFT OUTER JOIN DLCOUNT D ON (D.ID = V.ID)";

        if (!$table_exists)
        {
            $select = 
                "SELECT V.*, IFNULL(D.DLCOUNT, 0) AS DLCOUNT,\r\n" .
                "V.METADATA AS METADATANEW\r\n" .
                "FROM\r\n" .
                "VERSIONS V\r\n" .
                "  LEFT OUTER JOIN DLCOUNT D ON (D.ID = V.ID)\r\n";		
        }

        $stmt = $version_db->prepare($select);

        if ($table_exists)
        {
            // the default culture is en-US..
            $stmt->execute(array(":lang1" => $lang_split[0], ":lang2" => $lang_split[1], ":en" => "en", ":us" => "US"));
        }
        else 
        {                
            $stmt->execute();                
        }


        $r = $stmt->fetchAll(\PDO::FETCH_ASSOC);

        foreach ($r as $row => $entry)
        {
            $array_result = CreateSoftwareEntryResult(
                false,
                $entry["ID"],
                $entry["SOFTWARENAME"],
                $entry["VERSIONSTRING"],
                $entry["DOWNLOADLINK"],
                $entry["RELEASEDATE"],
                $entry["ISDIRECT_DOWNLOAD"],
                $entry["METADATA"],
                $entry["DLCOUNT"],    
                $table_exists ? $entry["METADATANEW"] : "");

            array_push($result, $array_result);
        }
        $version_db = null; // release the database connection..
        $stmt = null;

        echo json_encode($result);
        return;
    }
    catch (Exception $e) // just exit with an error..
    {
        array_push($result, CreateSoftwareEntryResult(false, "-1", "Fail: " . $e->getMessage(), "2"));
        echo json_encode($result);
        return;
    }    
}