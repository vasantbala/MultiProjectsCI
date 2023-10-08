param(
    [Parameter(Mandatory)]
    [String]$BaseCommitSha,
    [Parameter(Mandatory)]
    [String]$ChangeCommitSha
)

Write-Host 'Hello world'
Write-Host "Base: $($BaseCommitSha); Change: $($ChangeCommitSha)"
$sha = gh api vasantbala/MultiProjectsCI/pulls/3
Write-Host $sha