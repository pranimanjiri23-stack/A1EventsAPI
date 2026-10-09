CREATE OR ALTER PROCEDURE dbo.usp_Event_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        Description,
        EventDate,
        StartTime,
        EndTime,
        Location,
        ImageUrl,
        Status,
        CreatedAt
    FROM dbo.Events
    ORDER BY EventDate DESC;
END;
GO


CREATE OR ALTER PROCEDURE dbo.usp_Event_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        Description,
        EventDate,
        StartTime,
        EndTime,
        Location,
        ImageUrl,
        Status,
        CreatedAt
    FROM dbo.Events
    WHERE Id = @Id;
END;
GO


CREATE OR ALTER PROCEDURE dbo.usp_Event_GetUpcoming
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        Description,
        EventDate,
        StartTime,
        EndTime,
        Location,
        ImageUrl,
        Status,
        CreatedAt
    FROM dbo.Events
    WHERE EventDate >= CAST(GETDATE() AS DATE)
      AND Status = 2
    ORDER BY EventDate ASC;
END;
GO


CREATE OR ALTER PROCEDURE dbo.usp_Event_Create
    @Name NVARCHAR(200),
    @Description NVARCHAR(2000),
    @EventDate DATE,
    @StartTime TIME,
    @EndTime TIME,
    @Location NVARCHAR(300),
    @ImageUrl NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Events
    (
        Name,
        Description,
        EventDate,
        StartTime,
        EndTime,
        Location,
        ImageUrl,
        Status,
        CreatedAt
    )
    VALUES
    (
        @Name,
        @Description,
        @EventDate,
        @StartTime,
        @EndTime,
        @Location,
        @ImageUrl,
        1,
        SYSUTCDATETIME()
    );

    DECLARE @NewId INT = SCOPE_IDENTITY();

    SELECT
        Id,
        Name,
        Description,
        EventDate,
        StartTime,
        EndTime,
        Location,
        ImageUrl,
        Status,
        CreatedAt
    FROM dbo.Events
    WHERE Id = @NewId;
END;
GO