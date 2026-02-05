CREATE TABLE Users (
    UserId INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Programs (
    ProgramId INT IDENTITY PRIMARY KEY,
    ProgramName NVARCHAR(100) NOT NULL
);

CREATE TABLE Trainees (
    TraineeId INT IDENTITY PRIMARY KEY,
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    ProgramId INT FOREIGN KEY REFERENCES Programs(ProgramId)
);

CREATE TABLE Rotations (
    RotationId INT IDENTITY PRIMARY KEY,
    TraineeId INT FOREIGN KEY REFERENCES Trainees(TraineeId),
    StartDate DATE,
    EndDate DATE
);

CREATE TABLE Evaluations (
    EvaluationId INT IDENTITY PRIMARY KEY,
    TraineeId INT FOREIGN KEY REFERENCES Trainees(TraineeId),
    Comments NVARCHAR(MAX),
    Score INT,
    CreatedAt DATETIME DEFAULT GETDATE()
);
