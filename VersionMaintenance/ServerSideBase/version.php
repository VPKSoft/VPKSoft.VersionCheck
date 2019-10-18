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

	// Return values within JSON (also formulated as text describing the error):
    // -1: An error within the client software
	//  0: No error
	//  1: NOT DEFINED YET
	//  2: An internal exception occurred (the server side)
	//  3: Invalid API key
    //  4: Invalid size of a semicolon-delimited array in the API call was given.
    
    // NOT NEEDED IN THIS FILE: include_once "functions.php";
    include_once "delete_functions.php";
    include_once "addupdate_functions.php";
    include_once "query_functions.php";
    include_once "database_update.php";
    include_once "archive_functions.php";

	// the version query for a software name..
	if (isset($_POST["Query_Version"]))
	{
        QueryVersion($_POST["Query_Version"]);
        return;
	}
	// update the download count for a given software name..
	else if (isset($_POST["Increase_DownloadCount"]))
	{
        IncreaseDownloadCount($_POST["Increase_DownloadCount"]);
        return;        
	}
	// updates the database to the latest version..
	elseif (isset($_POST["Update_Database"])) 
	{
        UpdateDatabase($_POST["Update_Database"]);
        return;
	}
	// update or add of a software is requested..
	elseif (isset($_POST["UpdateInsert_Version"]))
	{	
        UpdateInsertVersion($_POST["UpdateInsert_Version"]);
        return;
	}
	else if (isset($_POST["AddVersionChanges"]))
	{
        AddVersionChanges($_POST["AddVersionChanges"]);
        return;
	}
    // a request to get all changes of the version with all localized languages..
	else if (isset($_POST["GetVersionChanges"]))
	{
        GetVersionChanges($_POST["GetVersionChanges"]);
        return;
	}    
    // a request to delete a localized version entry with a given ID..
	else if (isset($_POST["DeleteVersionChange"]))
	{
        DeleteVersionChange($_POST["DeleteVersionChange"]);
        return;
	} 	
    // a request was made to delete a version from the database..
	elseif (isset($_POST["Delete_Version"]))
	{
        DeleteVersion($_POST["Delete_Version"]);
        return;
	}
	// get the list of software entries in the database..
	elseif (isset($_POST["GetSoftwareList"]))
	{
        GetSoftwareList($_POST["GetSoftwareList"]);
        return;
	}
	// archive a version data into the archive database table..
	elseif (isset($_POST["ArchiveVersion"]))
	{
        ArchiveVersion($_POST["ArchiveVersion"]);
        return;
	}    
	// archive a version data into the archive database table..
	elseif (isset($_POST["ArchiveVersionHistory"]))
	{
        ArchiveVersionHistory($_POST["ArchiveVersionHistory"]);
        return;
	}    
	// archive a version data into the archive database table..
	elseif (isset($_POST["ArchiveVersionHistoryByApplicationId"]))
	{
        ArchiveVersionHistoryByApplicationId($_POST["ArchiveVersionHistoryByApplicationId"]);
        return;
	}    
	// archive a version data into the archive database table..
	elseif (isset($_POST["DeleteVersionHistoryByApplicationId"]))
	{
        DeleteVersionHistoryByApplicationId($_POST["DeleteVersionHistoryByApplicationId"]);
        return;
	}           
    else
    {
        // something could done with this but for now silence seems better..
    }
