<#
	.Synopsis
	Adds person to database.
#>

Param(
	[Parameter()]
	[string]
    $name,
    [Parameter()]
    [string]
    $address,
    [Parameter()]
    [int]
    $age
)
$ErrorActionPreference = "Stop"

Import-Module "$PSScriptRoot\Modules\SearchinatorEndpoints.psm1" -Force

Add-Person $name $address $age