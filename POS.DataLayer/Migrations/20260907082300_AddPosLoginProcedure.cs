using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPosLoginProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            


                var sqlScript = File.ReadAllText(@"..\POS.DataLayer\StoreProcedures\pos_login.sql");
                migrationBuilder.Sql(sqlScript);

         }

            /// <inheritdoc />
            protected override void Down(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.pos_login;");
            }
        }
    }


