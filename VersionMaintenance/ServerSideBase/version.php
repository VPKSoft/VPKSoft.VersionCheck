﻿<?php	
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

	// Return values:
	// 0: No error
	// 1: A general error
	// 2: Invalid-size array was given
	// 3: Access denied

	// the version query for a software name..
	if (isset($_POST["Query_Version"]))
	{
		try 
		{
			$return_value = array(
				"ID" => "-1",
				"SoftwareName" => "unknown",
				"SoftwareVersion" => "0.0.0.0",
				"DownloadLink" => "",
				"ReleaseDate" => "0000-00-00 00:00:00",
				"IsDirectDownload" => "False",
				"MetaData" => ""
				);
					
			$software_name = $_POST["Query_Version"];
			
			// create a database connection..
			$version_db = new PDO("sqlite:version.sqlite");		
			
			$select = "SELECT * FROM VERSIONS WHERE SOFTWARENAME = :name ";
			
			$stmt = $version_db->prepare($select);
			
			$stmt->execute(array(":name" => $software_name));
			$r = $stmt->fetchAll();
			
			foreach ($r as $row => $entry)
			{
				$return_value["SoftwareName"] = $software_name;
				$return_value["ID"] = $entry["ID"];
				$return_value["SoftwareVersion"] = $entry["VERSIONSTRING"];
				$return_value["DownloadLink"] = $entry["DOWNLOADLINK"];
				$return_value["ReleaseDate"] = $entry["RELEASEDATE"];
				$return_value["IsDirectDownload"] = $entry["ISDIRECT_DOWNLOAD"];			
				$return_value["MetaData"] = $entry["METADATA"];			
				break; // only one entry with the same name should exist..
			}
				
			$version_db = null; // release the database connection..
			$stmt = null;			
			echo json_encode($return_value);
		}
		catch (Exception $e) // just exit with an error..
		{
			exit(1);
		}
		exit(0); // no error..
	}
	// update or add of a software is requested..
	elseif (isset($_POST["UpdateInsert_Version"]))
	{	
		try 
		{
			// the format is SOFTWARENAME;VERSIONSTRING;DOWNLOADLINK;RELEASEDATE;ISDIRECT_DOWNLOAD(True / False);METADATA;APIKEY
			$software_data = explode(";", $_POST["UpdateInsert_Version"]);
			
			if (sizeof($software_data) != 7)
			{
				// an invalid POST value was given..
				exit(2);
			}		

			// get the data..
			$name = $software_data[0];
			$version = $software_data[1];
			$link = $software_data[2];
			$release_date = $software_data[3];
			$direct = $software_data[4];
			$meta = $software_data[5];
			$secret_file = $software_data[6];

					
			// validate the right to manipulate the version database..
			if (!file_exists($secret_file))
			{
				// ..no rights..
				exit(3);
			}
							
			// create a database connection..		
			$version_db = new PDO("sqlite:version.sqlite");		
			
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
		}
		catch (Exception $e) // just exit with an error..
		{
			exit(1);
		}
		exit(0); // no error..
	}
	// a request was made to delete a version from the database..
	elseif (isset($_POST["Delete_Version"]))
	{
		try 
		{
			// the format is SOFTWARENAME;APIKEY
			$software_data = explode(";", $_POST["Delete_Version"]);		

			if (sizeof($software_data) != 2)
			{
				// an invalid POST value was given..
				exit(2);
			}	
			
			// get the data..
			$software_name = $software_data[0];
			$secret_file = $software_data[1];		
			
			// validate the right to manipulate the version database..
			if (!file_exists($secret_file))
			{
				// ..no rights..
				exit(3);
			}
					
			// create a database connection..
			$version_db = new PDO("sqlite:version.sqlite");		
			
			$sentence = "DELETE FROM VERSIONS WHERE SOFTWARENAME = :name ";
			
			$stmt = $version_db->prepare($sentence);
			
			$stmt->execute(array(":name" => $software_name));
			$sentence = null;
			$stmt = null;
			$version_db = null; // release the database connection..
		}
		catch (Exception $e) // just exit with an error..
		{
			exit(1);
		}
		exit(0); // no error.
	}
	// get the list of software entries in the database..
	elseif (isset($_POST["GetSoftwareList"]))
	{
		try 
		{
			// create a database connection..
			$version_db = new PDO("sqlite:version.sqlite");		
			
			// initialize an empty array..
			$result = array();
			
			$select = "SELECT * FROM VERSIONS";
			
			$stmt = $version_db->prepare($select);
			
			$stmt->execute();
			$r = $stmt->fetchAll();
			
			foreach ($r as $row => $entry)
			{
				$array_result = array(
					"ID" => "-1",
					"SoftwareName" => "unknown",
					"SoftwareVersion" => "0.0.0.0",
					"DownloadLink" => "",
					"ReleaseDate" => "0000-00-00 00:00:00",
					"IsDirectDownload" => "False",
					"MetaData" => ""
					);			
				
				$array_result["ID"] = $entry["ID"];
				$array_result["SoftwareName"] = $entry["SOFTWARENAME"];
				$array_result["SoftwareVersion"] = $entry["VERSIONSTRING"];
				$array_result["DownloadLink"] = $entry["DOWNLOADLINK"];
				$array_result["ReleaseDate"] = $entry["RELEASEDATE"];
				$array_result["IsDirectDownload"] = $entry["ISDIRECT_DOWNLOAD"];		
				$array_result["MetaData"] = $entry["METADATA"];			
				
				array_push($result, $array_result);
			}
			$version_db = null; // release the database connection..
			$stmt = null;
			$version_db = null; // release the database connection..			
			echo json_encode($result);
		}
		catch (Exception $e) // just exit with an error..
		{
			exit(1);
		}
		exit(0); // no error.
	}
?>