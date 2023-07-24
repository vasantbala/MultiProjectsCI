#https://www.c-sharpcorner.com/article/using-certificates-for-api-authentication-in-net-5


$needcacert = Read-Host "Create CA Root cert? (y/n)"

if ($needcacert -eq "y"){

    $friendlyName = Read-Host "Enter friendly name for CA Root cert? (CALocalhost)"
    
    $key = New-SelfSignedCertificate -DnsName "localhost", "localhost" `
    -CertStoreLocation "cert:\LocalMachine\My" `
    -NotAfter (Get-Date).AddYears(10) `
    -FriendlyName $friendlyName `
    -KeyUsageProperty All `
    -KeyUsage CertSign, CRLSign, DigitalSignature


    $pwd = Read-Host "Enter a Password" -AsSecureString

    $exportFilePath = Read-Host "Enter Export file path for CA Root cert? (c:\temp\ca.pfx)"
    $p = 'cert:\localMachine\my\' + $key.Thumbprint
    $rootcert = Get-ChildItem -Path $p | Export-PfxCertificate -FilePath $exportFilePath -Password $pwd
    $rootCertThumbprint = $key.Thumbprint
}
else{
    $rootCertThumbprint = Read-Host "Enter thumbprint for CA Root cert?"
    
}

$p = "cert:\LocalMachine\My\" + $rootCertThumbprint
$rootcert = (Get-ChildItem -Path $p)

$childFriendlyName = Read-Host "Enter friendly name for CA Root cert? (ChildLocalhost)"
$childDns = Read-Host "Enter DNS for Child cert? (localhost)"


$child = New-SelfSignedCertificate -certstorelocation cert:\localmachine\my `
                            -dnsname $childDns `
                            -Signer $rootcert `
                            -NotAfter (Get-Date).AddYears(10) `
                            -FriendlyName $childFriendlyName

$pwd = Read-Host "Enter a Password" -AsSecureString

$exportFilePath = Read-Host "Enter Export file path for child Root cert? (c:\temp\child.pfx)"
$p = "cert:\localMachine\my\" + $child.Thumbprint

Get-ChildItem -Path $p | `
                Export-PfxCertificate -FilePath $exportFilePath -Password $pwd






