using System.Security.Cryptography;
using System.Text;

namespace DoctorAppointmentSystem.Services;

public interface IAuditService { string CreateReference(string value); }
public class AuditService : IAuditService
{
    // Demonstration only: use a managed key vault and approved cryptographic design in production.
    public string CreateReference(string value)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(hash);
    }
}
