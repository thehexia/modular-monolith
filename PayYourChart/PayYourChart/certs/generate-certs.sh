#!/bin/bash

openssl genrsa 2048 > ca-key.pem
openssl req -new -x509 -nodes -days 365000 -key ca-key.pem -out ca-cert.pem
openssl req -newkey rsa:2048 -nodes -days 365000 -keyout server-key.pem -out server-req.pem
openssl x509 -req -days 365000 -in server-req.pem -out server-cert.pem -CA ca-cert.pem -CAkey ca-key.pem
openssl req -newkey rsa:2048 -nodes -days 365000 -keyout client-key.pem -out client-req.pem
openssl x509 -req -days 365000 -in client-req.pem -out client-cert.pem -CA ca-cert.pem -CAkey ca-key.pem