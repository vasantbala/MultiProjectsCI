New-SelfSignedCertificate -DnsName "localhost", "localhost" -CertStoreLocation "cert:\LocalMachine\My" -NotAfter (Get-Date).AddYears(10) -FriendlyName "CAlocalhost" -KeyUsageProperty All -KeyUsage CertSign, CRLSign, DigitalSignature

//https://www.c-sharpcorner.com/article/using-certificates-for-api-authentication-in-net-5/