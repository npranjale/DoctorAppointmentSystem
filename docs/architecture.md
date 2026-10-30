# Architecture and design notes

## Layers
- Presentation: MVC controllers, Razor views, view models, Bootstrap styling and progressive JavaScript.
- Application: `IAppointmentService` and `IDoctorService` encapsulate business operations.
- Domain: Doctor, Appointment, AppointmentStatus and ApplicationUser.
- Infrastructure: EF Core `ApplicationDbContext`, SQLite/SQL Server providers, ASP.NET Core Identity.
- Cross-cutting: security headers middleware, Identity cookie policy, audit reference hashing sample.

## Core relationships
- One Doctor has many Appointments.
- One Identity patient can have many Appointments.
- Each appointment references exactly one doctor and one patient.
- Unique (DoctorId, AppointmentDate, TimeSlot) index prevents duplicate bookings for a doctor/time.

## Design system vs component library
Bootstrap is the component library/framework used for responsive primitives and common controls. A design system additionally defines brand tokens, typography, spacing, interaction patterns, accessibility rules, content guidance, governance and versioning. `site.scss` is the token starting point; teams should map enterprise design tokens to CSS custom properties and build documented, tested components rather than simply restyling Bootstrap.
