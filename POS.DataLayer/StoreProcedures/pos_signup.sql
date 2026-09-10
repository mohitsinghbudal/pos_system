CREATE OR ALTER PROCEDURE dbo.pos_signup
    @Email VARCHAR(150),
    @PasswordHash VARCHAR(255),
    @Name varchar(100),
    @PhoneNo VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.Users WITH (NOLOCK) WHERE Email = @Email)
    BEGIN
        SELECT -1 AS UserId; 
        RETURN;
    END

    INSERT INTO dbo.Users
    (
        Email,
        PasswordHash,
        Name,
        PhoneNo,
        RoleId,
        IsActive,
        CreatedAt
    )
    VALUES
    (
        @Email,
        @PasswordHash,
        @Name,
        @PhoneNo,
        3,
        1,
        GETUTCDATE()
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS UserId;
END;