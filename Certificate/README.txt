1) Please install OpenSSL. Windows version could be found here: http://slproweb.com/products/Win32OpenSSL.html

2) Run OpenSSL Command-Line prompt 

3) Switch to 'Cerfiticate' folder inside a solution directory

4) execute openssl `openssl.exe`

5) paste following command `req -x509 -sha256 -nodes -days 365 -newkey rsa:2048 -keyout monito_private.key -out monito_certificate.crt`

6) `openssl pkcs12 -export -out monito.pfx -inkey monito_private.key -in monito_certificate.crt`

7) When password asked, for development purposes please use '123' as a password

8) PROFIT!! now we have a pfx container with certificate and a private key