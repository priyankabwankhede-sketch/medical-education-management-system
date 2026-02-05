CREATE PROCEDURE sp_CreateUser
    @Username NVARCHAR(50),
    @PasswordHash NVARCHAR(255),
    @Role NVARCHAR(20)
AS
BEGIN
    INSERT INTO Users (Username, PasswordHash, Role)
    VALUES (@Username, @PasswordHash, @Role)
END
GO

CREATE PROCEDURE sp_GetTrainees
AS
BEGIN
    SELECT t.TraineeId, u.Username, p.ProgramName
    FROM Trainees t
    JOIN Users u ON t.UserId = u.UserId
    JOIN Programs p ON t.ProgramId = p.ProgramId
END
GO
