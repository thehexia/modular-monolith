# server dev cert
dotnet dev-certs https -ep server-cert.pfx --trust

# ca cert
openssl genrsa -out ca-key.pem 4096
openssl req -new -x509 -nodes -days 365000 -key ca-key.pem -out ca-cert.pem
openssl pkcs12 -export -out ca-cert.pfx -inkey ca-key.pem -in ca-cert.pem

# there should probably be an intermediate cert here.
#client
openssl req -newkey rsa:4096 -nodes -days 365000 -keyout client-key.pem -out client-req.pem -config openssl.conf
openssl x509 -req -days 365000 -in client-req.pem -out client-cert.pem -CA ca-cert.pem -CAkey ca-key.pem
openssl pkcs12 -export -out client-cert.pfx -inkey client-key.pem -in client-cert.pem  -passout pass:password123