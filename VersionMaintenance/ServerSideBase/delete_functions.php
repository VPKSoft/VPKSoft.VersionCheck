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

function DeleteVersionChange($postdata)
{
    try
    {
        // the format is APIKEY;ID
        $software_data = explode(";", $postdata);		

        if (sizeof($software_data) != 2)
        {
            echo CreateGeneralResult("Fail: Invalid POST value, required 2 values, got: " . sizeof($software_data) . "!", "4", "True");            
            return;
        }	

        // get the data..
        $secret_file = $software_data[0];		
        $id = $software_data[1];

        // validate the right to manipulate the version database..
        if (!APIKeyCorrect($secret_file))
        {
            echo CreateGeneralResult("Fail: Invalid API key!", "3", "True");
            return;
        }

        // create a database connection..
        $version_db = CreateDBConnection();

        $sentence = "DELETE FROM CHANGEHISTORY WHERE ID = :id ";            
        $stmt = $version_db->prepare($sentence);            

        $stmt->execute(array(":id" => $id));
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

function DeleteVersionHistoryByApplicationId($postdata)
{
    try
    {
        // the format is APIKEY;ID;[VERSIONSTRING]
        $software_data = explode(";", $postdata);		

        if (sizeof($software_data) != 2)
        {
            echo CreateGeneralResult("Fail: Invalid POST value, required 2 values, got: " . sizeof($software_data) . "!", "4", "True");
            return;
        }	

        // get the data..
        $secret_file = $software_data[0];		
        $id = $software_data[1];

        // validate the right to manipulate the version database..
        if (!APIKeyCorrect($secret_file))
        {
            echo CreateGeneralResult("Fail: Invalid API key!", "3", "True");
            return;
        }

        // create a database connection..
        $version_db = CreateDBConnection();

        $sentence = "DELETE FROM CHANGEHISTORY WHERE APP_ID = :id ";            
        $stmt = $version_db->prepare($sentence);            

        $stmt->execute(array(":id" => $id));
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

function DeleteVersion($postdata)
{
    try 
    {
        // the format is SOFTWARENAME;APIKEY
        $software_data = explode(";", $postdata);		

        if (sizeof($software_data) != 2)
        {
            echo CreateGeneralResult("Fail: Invalid POST value, required 2 values, got: " . sizeof($software_data) . "!", "4", "True");
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