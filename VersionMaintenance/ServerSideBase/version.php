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
				"MetaData" => "",
				"DownloadCount" => "0",
				"MetaDataLocalized" => ""
				);

			$array_result = explode(";", $_POST["Query_Version"]);
					
			$software_name = $array_result[0];
			$sofware_lang = "en-US"; // default to English (United States)..

			if (sizeof($software_data) == 2) // to support the previous version, do a range check..
			{
				// an invalid POST value was given..
				$sofware_lang = $array_result[1];
			}

			// create a database connection..
			$version_db = new PDO("sqlite:version.sqlite");		

			// to support previous version check if a table exists..
			$select =
				"SELECT CASE WHEN EXISTS (SELECT * FROM SQLITE_MASTER WHERE TYPE = 'table' AND NAME = :table) THEN 1 ELSE 0 END AS TABLE_EXISTS;";

			$stmt = $version_db->prepare($select);			
			$stmt->execute(array(":table" => "CHANGEHISTORY"));

			$r = $stmt->fetchAll();
			$table_exists = false;
			foreach ($r as $row => $entry)
			{
				$table_exists = $entry["TABLE_EXISTS"] == 1;
				break;
			}
			$stmt = null;

			$lang_split = explode("-", $sofware_lang);
					
			$select = 
				"SELECT V.*, IFNULL(D.DLCOUNT, 0) AS DLCOUNT,\r\n" .
				"--localization via some SQL magic..\r\n" .
				"IFNULL(IFNULL(H1.METADATA, H2.METADATA), V.METADATA) AS METADATANEW\r\n" .
				"FROM\r\n" .
				"VERSIONS V\r\n" .
				"  LEFT OUTER JOIN DLCOUNT D ON (D.ID = V.ID)\r\n" .
				"  LEFT OUTER JOIN CHANGEHISTORY H1 ON (H1.ID = V.ID AND H1.CULTUREMAIN = :en AND\r\n" .
				"    IFNULL(H1.CULTURESPECIFIC, :us) = :us AND V.VERSIONSTRING = H1.VERSIONSTRING)\r\n" .
				"  LEFT OUTER JOIN CHANGEHISTORY H2 ON (H2.ID = V.ID AND H2.CULTUREMAIN = :lang1 AND\r\n" .
				"    IFNULL(H2.CULTURESPECIFIC, :lang2) = :lang2 AND V.VERSIONSTRING = H2.VERSIONSTRING)\r\n" .
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
			
			foreach ($r as $row => $entry)
			{
				$return_value["SoftwareName"] = $software_name;
				$return_value["ID"] = $entry["ID"];
				$return_value["SoftwareVersion"] = $entry["VERSIONSTRING"];
				$return_value["DownloadLink"] = $entry["DOWNLOADLINK"];
				$return_value["ReleaseDate"] = $entry["RELEASEDATE"];
				$return_value["IsDirectDownload"] = $entry["ISDIRECT_DOWNLOAD"];			
				$return_value["MetaData"] = $entry["METADATA"];			
				$return_value["DownloadCount"] = $entry["DLCOUNT"];			
				if ($table_exists)
				{
					$return_value["MetaDataLocalized"] = $entry["METADATANEW"];			
				}
				
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
	// update the download count for a given software name..
	else if (isset($_POST["Increase_DownloadCount"]))
	{
		try
		{
			$name = $_POST["Increase_DownloadCount"];

			// create a database connection..
			$version_db = new PDO("sqlite:version.sqlite");		

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
		}
		catch (Exception $e) // just exit with an error..
		{
			exit(1);
		}
		exit(0); // no error..
	}
	// updates the database to the latest version..
	elseif (isset($_POST["Update_Database"])) 
	{
		try
		{
			$secret_file = $_POST["Update_Database"];

			// validate the right to manipulate the version database..
			if (!file_exists($secret_file))
			{
				// ..no rights..
				exit(3);
			}

			// create a database connection..
			$version_db = new PDO("sqlite:version.sqlite");		

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
				"ID INTEGER PRIMARY KEY NOT NULL,\r\n" .
				"SOFTWARENAME TEXT NOT NULL,\r\n" .
				"VERSIONSTRING TEXT NOT NULL,\r\n" .
				"CULTUREMAIN TEXT NOT NULL,\r\n" .
				"CULTURESPECIFIC TEXT NULL,\r\n" .
				"METADATA TEXT NOT NULL);\r\n";

			$version_db->exec($sentence);

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
	else if (isset($_POST["AddVersionChanges"]))
	{
		try
		{
			// the format is SOFTWARENAME;APIKEY
			$software_data = explode(";", $_POST["AddVersionChanges"]);		

			if (sizeof($software_data) != 6)
			{
				// an invalid POST value was given..
				exit(2);
			}	

			// get the data..
			$id = $software_data[0];
			$software_name = $software_data[1];
			$secret_file = $software_data[2];		

			// validate the right to manipulate the version database..
			if (!file_exists($secret_file))
			{
				// ..no rights..
				exit(3);
			}

			$culture = $software_data[3];			
			$version = $software_data[4];
			$meta = $software_data[5];

			$culture_parts = explode("-", $culture);
			$culture_main = $culture_parts[0];
			$culture_specific = "''";

			if (sizeof($culture_parts) == 2)
			{
				$culture_specific = $culture_parts[0];
			}

			// create a database connection..		
			$version_db = new PDO("sqlite:version.sqlite");		
			
			$sentence = // conditional insert..
				"INSERT INTO CHANGEHISTORY\r\n" .
				"(ID, SOFTWARENAME, VERSIONSTRING, CULTUREMAIN, CULTURESPECIFIC, METADATA)\r\n" .
				"SELECT :id, :softwarename, :versionstring, :culturemain, :culturespecific, :meta\r\n" .
				"WHERE NOT EXISTS(SELECT * FROM CHANGEHISTORY WHERE ID = :id AND SOFTWARENAME = :softwarename\r\n AND" . 
				"CULTUREMAIN = :culturemain AND ISNULL(CULTURESPECIFIC, :culturespecific) = :culturespecific);\r\n";

			$stmt = $version_db->prepare($sentence);
			$stmt->execute(array(":id" => $name, ":softwarename" => $software_name, ":versionstring" => $version,
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
				"ID = :id AND\r\n" .
				"ISNULL(CULTURESPECIFIC, :culturespecific) = :culturespecific);\r\n";

			$stmt = $version_db->prepare($sentence);
			$stmt->execute(array(":id" => $name, ":softwarename" => $software_name, ":versionstring" => $version,
				":meta" => $meta, ":culturemain" => $culture_main, ":culturespecific" => $culture_specific));

			$stmp = null;
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
			
			$select = 
				"SELECT V.*, IFNULL(D.DLCOUNT, 0) AS DLCOUNT\r\n" .
				"FROM\r\n" .
				"VERSIONS V\r\n" .
				"  LEFT OUTER JOIN DLCOUNT D ON (D.ID = V.ID)";
				
			$stmt = $version_db->prepare($select);
			
			$stmt->execute();
			$r = $stmt->fetchAll(\PDO::FETCH_ASSOC);
			
			foreach ($r as $row => $entry)
			{
				$array_result = array(
					"ID" => "-1",
					"SoftwareName" => "unknown",
					"SoftwareVersion" => "0.0.0.0",
					"DownloadLink" => "",
					"ReleaseDate" => "0000-00-00 00:00:00",
					"IsDirectDownload" => "False",
					"MetaData" => "",
					"DownloadCount" => "0"
					);			
				
				$array_result["ID"] = $entry["ID"];
				$array_result["SoftwareName"] = $entry["SOFTWARENAME"];
				$array_result["SoftwareVersion"] = $entry["VERSIONSTRING"];
				$array_result["DownloadLink"] = $entry["DOWNLOADLINK"];
				$array_result["ReleaseDate"] = $entry["RELEASEDATE"];
				$array_result["IsDirectDownload"] = $entry["ISDIRECT_DOWNLOAD"];		
				$array_result["MetaData"] = $entry["METADATA"];			
				$array_result["DownloadCount"] = $entry["DLCOUNT"];							
				array_push($result, $array_result);
			}
			$version_db = null; // release the database connection..
			$stmt = null;
			echo json_encode($result);
		}
		catch (Exception $e) // just exit with an error..
		{
			exit(1);
		}
		exit(0); // no error.
	}
?>