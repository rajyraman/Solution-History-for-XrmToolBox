# Project Description

This is a tool for XRMToolBox. The purpose of the tool is to view a historical list of solutions that have been imported. This provides sort of audit functionality for solutions, as the OOB solution view doesn't really display how many times a solution has been imported, version number that was updated, error/warning messages that were encountered during solution import. This tool uses _importjob_ entity to get list of solution imports and _RetrieveFormattedImportJobResultsRequest_ message to save the import solution file details.

# Install Instructions
Download the most recent version of XrmToolBox. Click on the "Plugin Store" button in the menu and install this tool from the store.

[image:SolutionHistory.png]

# Code that I didn't write
CommonDelegates, ListViewItemComparer and ListViewDelegates have been taken from ViewLayout Replicator tool. Credits to tanguy.
