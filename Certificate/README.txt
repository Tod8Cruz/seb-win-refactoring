In order to create a certificate for delelopment purposes, please open PowerShell here (inside a folder)

1) Create certificate:
$cert = New-SelfSignedCertificate -DnsName localhost -Type CodeSigning -CertStoreLocation Cert:\CurrentUser\My

2) set the password for it:
$CertPassword = ConvertTo-SecureString -String "123" -Force –AsPlainText

3) Export it:
Export-PfxCertificate -Cert "cert:\CurrentUser\My\$($cert.Thumbprint)" -FilePath "monito.pfx" -Password $CertPassword