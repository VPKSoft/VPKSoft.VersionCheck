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
    
    include "functions.php";

	// the version query for a software name..
	if (isset($_POST["Query_Version"]))
	{
		try 
		{
			$array_result = explode(";", $_POST["Query_Version"]);
					
			$software_name = $array_result[0];
			$sofware_lang = "en-US"; // default to English (United States)..

			if (sizeof($software_data) == 2) // to support the previous version, do a range check..
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
	// update the download count for a given software name..
	else if (isset($_POST["Increase_DownloadCount"]))
	{
		try
		{
			$name = $_POST["Increase_DownloadCount"];

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
	// updates the database to the latest version..
	elseif (isset($_POST["Update_Database"])) 
	{
		try
		{
			// validate the right to manipulate the version database..
            if (!APIKeyCorrect($_POST["Update_Database"]))
            {
                echo CreateGeneralResult("Fail: Invalid API key!", "3", "True");
                return;
            }
            
			if (!file_exists("version.sqlite"))
			{
                try
                {
                    if (!rename("empty.sqlite", "version.sqlite"))
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
                echo CreateGeneralResult("Fail: Invalid POST value!", "4", "True");
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
	else if (isset($_POST["AddVersionChanges"]))
	{
		try
		{
			// the format is APIKEY;ID;SOFTWARENAME;VERSIONSTRING;METADATA;CULTURE
			$software_data = explode(";", $_POST["AddVersionChanges"]);		

			if (sizeof($software_data) != 6)
			{
                echo CreateGeneralResult("Fail: Invalid POST value!", "4", "True");
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
			$culture_specific = "''";

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
	// a request was made to delete a version from the database..
	elseif (isset($_POST["Delete_Version"]))
	{
		try 
		{
			// the format is SOFTWARENAME;APIKEY
			$software_data = explode(";", $_POST["Delete_Version"]);		

			if (sizeof($software_data) != 2)
			{
                echo CreateGeneralResult("Fail: Invalid POST value!", "4", "True");
                return;
			}	
			
			// get the data..
			$software_name = $software_data[0];
			$secret_file = $software_data[1];		
			
			// validate the right to manipulate the version database..
            if (!APIKeyCorrect($secret_file))
            {
                echo CreateGeneralResult("Fail: Invalid API key!", "3", "True");
                return;
            }
					
			// create a database connection..
			$version_db = CreateDBConnection();
			
			$sentence = "DELETE FROM VERSIONS WHERE SOFTWARENAME = :name ";
			
			$stmt = $version_db->prepare($sentence);
			
			$stmt->execute(array(":name" => $software_name));
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
	// get the list of software entries in the database..
	elseif (isset($_POST["GetSoftwareList"]))
	{
		try 
		{		
			// initialize an empty array..
			$result = array();
            
			// the format is APIKEY;language-region; i.e: fi-FI..
			$software_data = explode(";", $_POST["GetSoftwareList"]);                
         
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
				"    IFNULL(H2.CULTURESPECIFIC, :lang2) = :lang2 AND V.VERSIONSTRING = H2.VERSIONSTRING)\r\n" .
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
    else
    {
        echo CreateGeneralResult("Fail: Unknown method: '" . $_POST[0] . "'.", "4", "True");
    }
?>