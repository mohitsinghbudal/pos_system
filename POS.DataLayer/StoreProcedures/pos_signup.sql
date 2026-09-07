create procedure dbo.pos_signup
@email varchar(150),
@passwordHash varchar(150),
@phoneNo varchar(20),
@roleId int,
@isActive bit,
@createdat datetime

as
begin 
 SET NOCOUNT ON;

 insert into Users (Email, PasswordHash, PhoneNo, RoleId, IsActive, CreatedAt) 
 values (@email, @passwordHash, @phoneNo, @roleId, @isActive, @createdat);
end

