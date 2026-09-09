create or alter procedure dbo.pos_addRoles
@RoleName varchar(100),
@Description varchar(255),
@CreatedBy int

as
begin 
SET NOCOUNT ON;

insert into Roles
(
    RoleName,
    Description,
    CreatedBy,
    CreatedAt,
    IsActive

)
values
(
    @RoleName,
    @Description,
    @CreatedBy,
    GETUTCDATE(),
    1
);

end;