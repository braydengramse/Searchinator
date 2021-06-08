<#
    .Synopsis
    Functions to interact with Searchinator endpoints.
#>
Set-Variable SearchinatorApiBaseUri -Option Constant -Value "https://localhost:44333";

Function Search-People(
    [string] $searchTerm
)
{
    if ($searchTerm -eq "")
    {
        $queryString = "/"
    }
    else
    {
        $queryString = "?searchInput=$($searchTerm)"
    }
    $personSearchEndpoint = "$($SearchinatorApiBaseUri)/person/search$($queryString)"
    $personResponse = Invoke-RestMethod -Method Get -Uri $personSearchEndpoint | ConvertTo-Json | ConvertFrom-Json
    
    foreach ($personJson in $personResponse.value)
    {
        $interestsForPersonEndpoint = "$($SearchinatorApiBaseUri)/interest/$($personJson.id)"
        $interestResponse = Invoke-RestMethod -Method Get -Uri $interestsForPersonEndpoint | ConvertTo-Json | ConvertFrom-Json
        $interests = [System.Collections.ArrayList]::new()
        foreach ($interestJson in $interestResponse.value)
        {
            [void]$interests.Add($interestJson.description)
        }
        $people += @([Person]::new($personJson.id, $personJson.address, $personJson.age, $personJson.name, $interests))
    }
    Write-Host ($people | Format-Table | Out-String)
}

Function Add-Person(
    [string]$name,
    [string]$address,
    [int]$age
)
{
    if(-not($name)) { Write-Error -Message "You must supply a value for -name" -ErrorAction Stop }
    if(-not($address)) { Write-Error -Message "You must supply a value for -address" -ErrorAction Stop }
    if(-not($age)) { Write-Error -Message "You must supply a value for -age" -ErrorAction Stop }

    $personToAddJson = @{
        Id = 0
        Name = "$($name)"
        Age = $age
        Address = "$($address)"
    } | ConvertTo-Json

    $addPersonEndpoint = "$($SearchinatorApiBaseUri)/person/"
    $personJson = Invoke-RestMethod -Method Post -Uri $addPersonEndpoint -Body $personToAddJson -ContentType "application/json" | ConvertTo-Json | ConvertFrom-Json
    Write-Host ($personJson | Format-Table | Out-String)
}

Export-ModuleMember -Function Search-People;
Export-ModuleMember -Function Add-Person

class Person
{
    Person([int]$personId, [string]$address, [int]$age, [string]$name, [string[]]$interests)
    {
        $this.PersonId = $personId
        $this.Address = $address
        $this.Age = $age
        $this.Name = $name
        $this.Interests = $interests
    }

    [int]$PersonId

    [string]$Name

    [int]$Age

    [string]$Address

    [string[]]$Interests
}