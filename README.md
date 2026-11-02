# My Hospital — Doctor Appointment System
A hospital appointment management system for patient. This project is developed for demo and not for distribution for commercial use.
## Technology
- .NET 8 / C# / ASP.NET Core MVC
- ASP.NET Core Identity for authentication and password hashing
- EF Core with SQLite by default; SQL Server provider configuration included
- Bootstrap 5, HTML5, CSS, Sass source, JavaScript
- Responsive layouts, keyboard focus states, skip navigation, semantic headings and form labels

## Run locally
Prerequisites: .NET 8 SDK.

```bash
cd src/DoctorAppointmentMvc
dotnet restore
dotnet run
```
Open the HTTPS/HTTP URL printed by `dotnet run`. On first run the SQLite database `doctor-appointments.db` is created and sample doctors are seeded.

Demo patient:
- Email: `patient@example.com`
- Password: `Patient@12345`

You can also create a new patient account from the UI.

## Use SQL Server instead
1. Start SQL Server or LocalDB.
2. Edit `appsettings.json`: set `Database:Provider` to `SqlServer`.
3. Update `ConnectionStrings:SqlServer` for your SQL Server instance.
4. For a production project, use EF Core migrations rather than `EnsureCreated`:
   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
   Remove `EnsureCreatedAsync()` in `Program.cs` when switching to migrations. `database/schema.sql` documents the core domain tables; Identity tables are managed by EF Core Identity.

## Folder structure
```
DoctorAppointmentMvc/
├── database/schema.sql
├── docs/ (design and security notes)
└── src/DoctorAppointmentSystem/
    ├── Controllers/
    ├── Data/
    ├── Middleware/
    ├── Models/
    ├── Services/
    ├── Views/{Home,Doctors,Appointments,Account,Shared}/
    └── wwwroot/{css,scss,js,images}/
```

## Main workflows
- Home: featured doctors and navigation
- Doctors: browse doctors, view a profile
- Account: register, sign in, sign out
- Appointments: authenticated patient submits a request and sees their appointment list
- Booking validates future dates, allowed time slots, active doctor, and duplicate doctor/date/time requests

## Architecture
MVC controllers handle HTTP and model binding; view models isolate form input; services contain booking/doctor rules; EF Core context owns persistence; Identity manages users, roles, cookies and password hashes; middleware adds baseline security headers.

## Accessibility checklist (WCAG 2.1 AA target)
This system includes a skip link, semantic landmarks, descriptive image alternatives, table captions and scoped headers, labels, validation summaries, keyboard-visible focus, responsive reflow, and status announcements. Before release, test every flow with keyboard-only navigation, JAWS + Chrome/Edge, axe DevTools, zoom at 200–400%, contrast analysis, and reduced-motion preferences. Automated scans do not establish conformance.

## Security and sensitive data
- Define applicable state/federal requirements, data classification, retention, breach response and audit policies with counsel/security.
- Enforce TLS, secure cookies, secrets management, least-privilege database identities, MFA, rate limiting, monitoring and tested backups.
- Encrypt sensitive fields using an approved envelope-encryption design backed by Azure Key Vault/managed HSM; rotate keys. Do not invent or hard-code encryption keys.
- Add tamper-resistant audit trails, authorization tests, consent workflows, data export/deletion controls, threat modeling and penetration testing.
- Add appointment status transitions, provider availability/calendar rules, time-zone handling, email/SMS notifications, admin workflows, and robust cancellation/rescheduling.
- Use CSP with a reviewed nonce/hash strategy; current Bootstrap CDN references should be pinned/self-hosted with integrity attributes in production.

## Notes
The cancellation action intentionally displays a placeholder message; it does not mutate appointment status. The system is intended as a solid starter skeleton rather than a complete clinical product.
