Obviously in a real project you would use some configuration or key provider such as key vault to store your certs. 

Instructions to generate these certs comes from https://mariadb.com/docs/server/security/data-in-transit-encryption/create-self-signed-certificates-keys-openssl/

To generate these certs, first we generate a private key for the CA

`openssl genrsa 2048 > ca-key.pem`

Then we generate the X509 Certificate for the CA

`openssl req -new -x509 -nodes -days 365000 -key ca-key.pem -out ca-cert.pem`

Then we make the servers' certificate request and keys.

`openssl req -newkey rsa:2048 -nodes -days 365000 -keyout server-key.pem -out server-req.pem`

Then we generate the X509 certificate for the server.

`openssl x509 -req -days 365000 -in server-req.pem -out server-cert.pem -CA ca-cert.pem -CAkey ca-key.pem`

Then to generate the client's certificate request and private key.

`openssl req -newkey rsa:2048 -nodes -days 365000 -keyout client-key.pem -out client-req.pem`

And to generate the X509 certificate for the client.

`openssl x509 -req -days 365000 -in client-req.pem -out client-cert.pem -CA ca-cert.pem -CAkey ca-key.pem`