using System;

namespace BLL
{
    public class Queries
    {
        public string GetQuery(eQuery query)
        {
            var objType = typeof(Queries);
            var method = objType.GetMethod(query.ToString());
            return method.Invoke(new Queries(), null).ToString();
        }

        #region Common Method

        public static string GetINQuery(string ParaIn)
        {
            #region Default Query
            //        create table #IDs
            //        (
            //Id   int
            //        )

            //        Declare @delimiter varchar
            //        Set @delimiter = ','

            //        DECLARE @index int
            //        SET @index = -1

            //        WHILE(LEN(@PriorityIn) > 0)
            //          BEGIN
            //            SET @index = CHARINDEX(@delimiter, @PriorityIn)
            //            IF(@index = 0) AND(LEN(@PriorityIn) > 0)
            //              BEGIN
            //                INSERT INTO #IDs VALUES (@PriorityIn)
            //                  BREAK
            //              END
            //            IF(@index > 1)
            //              BEGIN
            //                INSERT INTO #IDs VALUES (LEFT(@PriorityIn, @index - 1))  
            //                SET @PriorityIn = RIGHT(@PriorityIn, (LEN(@PriorityIn) - @index))
            //              END
            //            ELSE
            //              SET @PriorityIn = RIGHT(@PriorityIn, (LEN(@PriorityIn) - @index))
            //            END
            //        --------------------------------------------------------------------------------------
            //        WHERE(@TaskIdIn      IS NULL OR Task.TaskId IN(SELECT #TaskIdIn.Id FROM #TaskIdIn))
            #endregion Default Query

            return @" create table #" + ParaIn + @"
(
	Id   int
)

Declare @" + ParaIn + @"Delimiter varchar
Set @" + ParaIn + @"Delimiter = ',' 

DECLARE @" + ParaIn + @"index int
SET @" + ParaIn + @"index = -1

WHILE (LEN(@" + ParaIn + @") > 0)
  BEGIN 
    SET @" + ParaIn + @"index = CHARINDEX(@" + ParaIn + @"Delimiter , @" + ParaIn + @") 
    IF (@" + ParaIn + @"index = 0) AND (LEN(@" + ParaIn + @") > 0) 
      BEGIN  
        INSERT INTO #" + ParaIn + @" VALUES (@" + ParaIn + @")
          BREAK 
      END 
    IF (@" + ParaIn + @"index > 1) 
      BEGIN  
        INSERT INTO #" + ParaIn + @" VALUES (LEFT(@" + ParaIn + @", @" + ParaIn + @"index - 1))  
        SET @" + ParaIn + @" = RIGHT(@" + ParaIn + @", (LEN(@" + ParaIn + @") - @" + ParaIn + @"index)) 
      END 
    ELSE
      SET @" + ParaIn + @" = RIGHT(@" + ParaIn + @", (LEN(@" + ParaIn + @") - @" + ParaIn + @"index))
    END ";
        }

        public static string GetINstringQuery(string ParaIn)
        {
            #region Default Query
            //            create table #IDs
            //(
            //    Id   int
            //)

            //Declare @delimiter varchar
            //Set @delimiter = ',' 

            //DECLARE @index int
            //SET @index = -1

            //WHILE (LEN(@PriorityIn) > 0)
            //  BEGIN 
            //    SET @index = CHARINDEX(@delimiter , @PriorityIn) 
            //    IF (@index = 0) AND (LEN(@PriorityIn) > 0) 
            //      BEGIN  
            //        INSERT INTO #IDs VALUES (@PriorityIn)
            //          BREAK 
            //      END 
            //    IF (@index > 1) 
            //      BEGIN  
            //        INSERT INTO #IDs VALUES (LEFT(@PriorityIn, @index - 1))  
            //        SET @PriorityIn = RIGHT(@PriorityIn, (LEN(@PriorityIn) - @index)) 
            //      END 
            //    ELSE
            //      SET @PriorityIn = RIGHT(@PriorityIn, (LEN(@PriorityIn) - @index))
            //    END
            //--------------------------------------------------------------------------------------
            //WHERE	(@TaskIdIn		IS NULL OR Task.TaskId IN(SELECT #TaskIdIn.Id FROM #TaskIdIn))
            #endregion Default Query

            return @" create table #" + ParaIn + @"
(
	Id   nvarchar(MAX) COLLATE Latin1_General_CI_AI
)

Declare @" + ParaIn + @"Delimiter varchar(50)
Set @" + ParaIn + @"Delimiter = ',' 

DECLARE @" + ParaIn + @"index varchar(50)
SET @" + ParaIn + @"index = -1

WHILE (LEN(@" + ParaIn + @") > 0)
  BEGIN 
    SET @" + ParaIn + @"index = CHARINDEX(@" + ParaIn + @"Delimiter , @" + ParaIn + @") 
    IF (@" + ParaIn + @"index = 0) AND (LEN(@" + ParaIn + @") > 0) 
      BEGIN  
        INSERT INTO #" + ParaIn + @" VALUES (@" + ParaIn + @")
          BREAK 
      END 
    IF (@" + ParaIn + @"index > 1) 
      BEGIN  
        INSERT INTO #" + ParaIn + @" VALUES (LEFT(@" + ParaIn + @", @" + ParaIn + @"index - 1))  
        SET @" + ParaIn + @" = RIGHT(@" + ParaIn + @", (LEN(@" + ParaIn + @") - @" + ParaIn + @"index)) 
      END 
    ELSE
      SET @" + ParaIn + @" = RIGHT(@" + ParaIn + @", (LEN(@" + ParaIn + @") - @" + ParaIn + @"index))
    END ";
        }

        #endregion


        public static string Get_All_ProjectRole_UserId_Wise()
        {
            return @"
SELECT 
	ProjectId,
    eMemberRole AS Role
FROM ProjectMember
WHERE   
    (UserId	            =   @UserId)
AND	(eMemberStatus		=   @eMemberStatus)	
";
        }

        public static string Get_All_Person_Detail()
        {
            return @"
SELECT 
	PersonId,
    FirstName,
    LastName,
    Mobile,
    Email,
    CASE WHEN Gender IS NULL THEN '' 
	     WHEN Gender = 1 THEN 'Male'
         WHEN Gender = 2 THEN 'Female' 
    END AS Gender
FROM Person
WHERE
    (eStatus            <> @eStatus)
AND (@MasterSearch  IS NULL OR (FirstName       LIKE '%' + @MasterSearch +'%')
							OR (LastName	    LIKE '%' + @MasterSearch +'%'))
";
        }

        public static string Get_Person_Email_For_Unique_Validation()
        {
            return @"
SELECT
    PersonId,
    Email,
    eStatus
FROM Person
WHERE
    (Email      = @Email)
AND (eStatus    <> @eStatus)
";
        }



        public static string Get_All_Student_Detail()
        {
            return @"
            SELECT 
	        StudentId,
            StudentName,
            FatherName,
            CityName,
            CASE WHEN Gender IS NULL THEN '' 
	                 WHEN Gender = 1 THEN 'Male'
                     WHEN Gender = 2 THEN 'Female' 
            END AS Gender,
            Class,
            RoleNo,
            Mobile,
            EmailAddress,
            SchoolFees,
            BusFees,
            Address,
            eStatus
            FROM Student
            WHERE(eStatus = @eStatus)
            AND (@MasterSearch IS NULL OR (StudentName LIKE '%' + @MasterSearch +'%'))";
        }
    }
    public enum eQuery
    {
        Get_All_ProjectRole_UserId_Wise,
        Get_All_Person_Detail,
        Get_Person_Email_For_Unique_Validation,
        Get_All_Student_Detail,
    }
}
