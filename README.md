# modular-monolith
A boilerplate template of modular monolith using mediator pattern and fast endpoints. Example is a trivial application that pretends to bill patients.

This project has 2 modules.

1) Patient module that handles recording patients and their bills
2) Item module which acts as a catalog for possible items a patient can be billed for.

Modules communicate through internal endpoints handled by MediatR.

FastEndpoints is used for external API endpoints.

Sqlite is used for data persistence for simplicity.

## WIP
The API uses certificate based authentication.

Before running the project, go into PayYourChart\PayYourChart\certs and use one of the scripts to generate the certs.
You will have to trust the CA cert and the client cert on your computer. There's a readme file in that folder to follow.

There's a Postman collection for most of the API endpoints. Don't forget to install the client-cert.pfx into Postman.