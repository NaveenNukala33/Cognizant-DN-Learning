USE employe;
GO
DROP PROCEDURE IF EXISTS sp_GetEmployeeNamesByDept;
GO

CREATE PROCEDURE sp_GetEmployeeNamesByDept
  @DeptID INT
AS
BEGIN
  SELECT
    FirstName,
    LastName
  FROM Employees
  WHERE DepartmentID = @DeptID
  ORDER BY LastName, FirstName;
END;
GO

--example1:

EXEC sp_GetStaffByDept @DeptID = 1;

--example2:

EXEC sp_GetStaffByDept @DeptID = 2;

