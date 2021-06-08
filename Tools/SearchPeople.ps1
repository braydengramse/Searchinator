<#
	.Synopsis
	Returns people who match the provided search term.
#>

Param(
	[Parameter()]
	[string]
	$searchTerm = ""
)
$ErrorActionPreference = "Stop"

Import-Module "$PSScriptRoot\Modules\SearchinatorEndpoints.psm1" -Force

Search-People $searchTerm