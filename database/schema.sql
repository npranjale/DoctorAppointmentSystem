-- SQL Server reference schema (Identity tables are created by ASP.NET Core Identity migrations).
CREATE TABLE dbo.Doctors (
    Id int IDENTITY(1,1) PRIMARY KEY,
    FullName nvarchar(100) NOT NULL,
    Specialty nvarchar(80) NOT NULL,
    Bio nvarchar(500) NOT NULL,
    Clinic nvarchar(200) NOT NULL,
    ConsultationFee decimal(10,2) NOT NULL,
    ImagePath nvarchar(300) NOT NULL,
    IsActive bit NOT NULL DEFAULT 1
);
CREATE TABLE dbo.Appointments (
    Id int IDENTITY(1,1) PRIMARY KEY,
    DoctorId int NOT NULL REFERENCES dbo.Doctors(Id),
    PatientId nvarchar(450) NOT NULL REFERENCES dbo.AspNetUsers(Id),
    AppointmentDate date NOT NULL,
    TimeSlot nvarchar(20) NOT NULL,
    Reason nvarchar(1000) NULL,
    Status int NOT NULL DEFAULT 0,
    CreatedUtc datetime2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT UQ_Doctor_Date_Time UNIQUE (DoctorId, AppointmentDate, TimeSlot)
);
CREATE INDEX IX_Appointments_Patient_Date ON dbo.Appointments(PatientId, AppointmentDate);
