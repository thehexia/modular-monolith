In order to generate certs, run the generate-certs.sh script. (Remember to chmod +x the script and run as sudo for trust.)

This will generate several development certs.
1. server-cert.pfx is used for the local kestrel development server and is automatically trusted by the machine if run as superuser.
2. ca-cert.pfx is the root CA cert.
3. client-cert.pfx is the client certificate signed by the root CA. The web server will only allow certs signed by the ca-cert to be used when using certificate based authentication.

Your computer will have to trust the CA cert public key and the client cert to use certificate based authentication. The script automatically trusts the server cert but you'll have to manually install the CA cert and client cert.