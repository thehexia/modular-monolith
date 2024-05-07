using System.Security.Cryptography.X509Certificates;

namespace PayYourChart.CertificateAuth;


internal interface ICertificateValidationService
{
    bool ValidateCertificate(X509Certificate2 clientCertificate);
}


internal class CertificateValidationService : ICertificateValidationService
{
    // Don't hardcode passwords in production code.
    // Use a certificate thumbprint or Azure Key Vault.
    readonly X509Certificate2 expectedCertificate;
    public CertificateValidationService()
    {
        expectedCertificate = new X509Certificate2(Path.Combine("certs/client-cert.pfx"), "password123");
    }

    public bool ValidateCertificate(X509Certificate2 clientCertificate)
    {
        return clientCertificate.Thumbprint == expectedCertificate.Thumbprint;
    }
}
