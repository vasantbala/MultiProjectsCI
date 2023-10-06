param(
    [Parameter(Mandatory)]
    [String]$BaseCommitSha
    [Parameter(Mandatory)]
    [String]$ChangeCommitSha
)

Write-Host 'Hello world'
Write-Host "Base: $($BaseCommitSha); Change: $($ChangeCommitSha)"