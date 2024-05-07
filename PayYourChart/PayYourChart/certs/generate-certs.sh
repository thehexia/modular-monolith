#!/bin/bash

# ca cert
# openssl genrsa 2048 > ca-key.pem
# openssl req -new -x509 -nodes -days 365000 -key ca-key.pem -out ca-cert.pem
# openssl pkcs12 -export -out ca-cert.pfx -inkey ca-key.pem -in ca-cert.pem

# server cert
# openssl req -newkey rsa:2048 -nodes -days 365000 -key server-key.pem -out server-req.pem
# openssl x509 -req -days 365000 -in server-req.pem -out server-cert.pem -CA ca-cert.pem -CAkey ca-key.pem
# openssl pkcs12 -export -out server-cert.pfx -inkey server-key.pem -in server-cert.pem

#client
openssl req -newkey rsa:2048 -nodes -days 365000 -keyout client-key.pem -out client-req.pem -config openssl.conf
openssl x509 -req -days 365000 -in client-req.pem -out client-cert.pem -CA ca-cert.pem -CAkey ca-key.pem
openssl pkcs12 -export -out client-cert.pfx -inkey client-key.pem -in client-cert.pem

#insomnia prefers the private and public key merged together into a pem
cat client-cert.pem > client-cert-insomnia.pfx
cat client-key.pem >> client-cert-insomnia.pfx