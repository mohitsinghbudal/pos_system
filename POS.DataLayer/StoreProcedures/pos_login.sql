create or alter procedure dbo.pos_login 
@email varchar(150)
as
begin 
 SET NOCOUNT ON;
select
u.Id as userId,
u.Email,
u.PasswordHash,
u.IsActive,
u.PhoneNo,
u.RoleUpdatedAt,
u.RoleUpdatedBy,
r.Id As roleId
from Users u
inner join
Roles r 
ON
u.RoleId = r.Id
where email = @email 
end;