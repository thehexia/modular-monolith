#!/bin/bash

# server dev cert
dotnet dev-certs https -ep server-cert.pfx --trust

# ca cert
openssl genrsa 2048 > ca-key.pem
openssl req -new -x509 -nodes -days 365000 -key ca-key.pem -out ca-cert.pem
openssl pkcs12 -export -out ca-cert.pfx -inkey ca-key.pem -in ca-cert.pem

# there should probably be an intermediate cert here.
#client
openssl req -newkey rsa:2048 -nodes -days 365000 -keyout client-key.pem -out client-req.pem -config openssl.conf
openssl x509 -req -days 365000 -in client-req.pem -out client-cert.pem -CA ca-cert.pem -CAkey ca-key.pem
openssl pkcs12 -export -legacy -out client-cert.pfx -inkey client-key.pem -in client-cert.pem  -passout pass:password123

#insomnia prefers the private and public key merged together into a pem
cat client-cert.pem > client-cert-insomnia.pfx
cat client-key.pem >> client-cert-insomnia.pfx