param(
    [Parameter(Mandatory)]
    [String]$BaseCommitSha,
    [Parameter(Mandatory)]
    [String]$ChangeCommitSha
)

Write-Host 'Hello world'
Write-Host "Base: $($BaseCommitSha); Change: $($ChangeCommitSha)"
$sha = git log -n 1 main --pretty=format:"%H"
Write-Host $sha