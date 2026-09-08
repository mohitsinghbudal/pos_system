CREATE OR ALTER PROCEDURE dbo.pos_signup
    @Email VARCHAR(150),
    @PasswordHash VARCHAR(255),
    @Name varchar(100),
    @PhoneNo VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

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
        LTRIM(RTRIM(@Email)),
        @PasswordHash,
        @Name,
        LTRIM(RTRIM(@PhoneNo)),
        3,
        1,
        GETUTCDATE()
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS UserId;
END;