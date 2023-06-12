namespace Zibma.Sample.Person.Api.Domain.Version.UpdateNow.DB
{
    public class V_1
    {
        public static string GetDBTableCreationScript_NameValue()
        {
            return @"
CREATE TABLE [dbo].[NameValue] (
    [NameValueId]                INT               IDENTITY (1, 1) NOT NULL,
    [Name]                       VARCHAR(100)        NULL,
    [Value]                      NVARCHAR(MAX)       NULL,
    PRIMARY KEY CLUSTERED ([NameValueId] ASC)
);"
;
        }

        public static string GetDBTableCreationScript_Person()
        {
            return @"
CREATE TABLE [dbo].[Person] (
        [PersonId]                  INT                     IDENTITY (1, 1) NOT NULL,
        [FirstName]                 VARCHAR(50)             NULL,
        [LastName]                  VARCHAR(50)             NULL,        
        [Mobile]                    VARCHAR(20)             NULL,
        [Email]                     VARCHAR(50)             NULL,
        [Gender]                    INT                     NULL,        
        [eStatus]                   INT                     NULL,        
        [InsertTime]                DATETIME                NULL,
        [LastUpdateTime]            DATETIME                NULL,
        PRIMARY KEY CLUSTERED ([PersonId] ASC)
    );"
;
        }

        public static string GetDBTableCreationScript_Student()
        {
            return @"
            CREATE TABLE [dbo].[Student] (
                [StudentId]                  INT                     IDENTITY (1, 1) NOT NULL,
                [StudentName]                VARCHAR(50)             NULL,
                [FatherName]                 VARCHAR(50)             NULL,        
                [CityName]                   VARCHAR(20)             NULL,
                [Gender]                     INT                     NULL,
                [Class]                      VARCHAR(20)             NULL,
                [RoleNo]                     INT                     NULL,
                [Mobile]                     VARCHAR(50)             NULL,
                [EmailAddress]               VARCHAR(50)             NULL,
                [SchoolFees]                 DECIMAL(18,2)           NULL,
                [BusFees]                    DECIMAL(18,2)           NULL,
                [Address]                    VARCHAR(50)             NULL,
                [eStatus]                    INT                     NULL,        
                [InsertTime]                 DATETIME                NULL,
                [LastUpdateTime]             DATETIME                NULL,
                PRIMARY KEY CLUSTERED ([StudentId] ASC)
            );";
        }
    }
}
