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

    function CreateGeneralResult($message = "Success", $error_code = "0", $error = "False")
    {
        $return_value = array(
            "Message" => $message,
            "ErrorCode" => $error_code,
            "Error" => $error
            );          
        
        return json_encode($return_value);
    }

    function CreateLocalizedChangeHistoryResult(
            $id = "-1", 
            $app_id = "-1", 
            $softwareName = "unknown", 
            $softwareVersion = "0.0.0.0", 
            $culture_main = "na",
            $culture_specific = "NA",
			$metaData = "")    
    {
        $return_value = array(
            "ID" => $id,
            "APP_ID" => $app_id,
            "SoftwareName" => $softwareName,
            "SoftwareVersion" => $softwareVersion, 
            "CultureMain" => $culture_main,
            "CultureSpecific" => $culture_specific,
            "MetaData" => $metaData
            );          
        
        return $return_value;
    }    
    
    function CreateSoftwareEntryResult(
            $json_echo = true,
            $id = "-1", 
            $softwareName = "unknown", 
            $softwareVersion = "0.0.0.0", 
            $downloadLink = "",
            $releaseDate = "0000-00-00 00:00:00",
			$isDirectDownload = "False",
			$metaData = "",
            $downloadCount = "0",
			$metaDataLocalized = "")
    {
        $return_value = array(
            "ID" => $id,
            "SoftwareName" => $softwareName,
            "SoftwareVersion" => $softwareVersion,
            "DownloadLink" => $downloadLink,
            "ReleaseDate" => $releaseDate,
            "IsDirectDownload" => $isDirectDownload,
            "MetaData" => $metaData,
            "DownloadCount" => $downloadCount,
            "MetaDataLocalized" => $metaDataLocalized
            );
        
        if ($json_echo)
        {
            return json_encode($return_value);
        }
        return $return_value;
    }
            
    function CreateDBConnection($set_errors = true)
    {
        // create a database connection..
		$version_db = new PDO("sqlite:version.sqlite");
        
        // exceptions are wanted..
        if ($set_errors)
        {
            $version_db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        }
        
        return $version_db;
    }

    
    function TableExists($version_db, $table_name)
    {
        // to support previous version check if a table exists..
        $select =
            "SELECT CASE WHEN EXISTS (SELECT * FROM SQLITE_MASTER WHERE TYPE = 'table' AND NAME = :table) THEN 1 ELSE 0 END AS TABLE_EXISTS;";

        $stmt = $version_db->prepare($select);			
        $stmt->execute(array(":table" => $table_name));

        $r = $stmt->fetchAll();
        $table_exists = false;
        foreach ($r as $row => $entry)
        {
            $table_exists = $entry["TABLE_EXISTS"] == 1;
            break;
        }
        $stmt = null;     
        
        return $table_exists;
    }

    // validate the API key..
    function APIKeyCorrect($api_key)
    {
        // validate the right to manipulate the version database..
		return file_exists($api_key);
    }
?>
