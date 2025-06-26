
USE employe;
GO


CREATE TABLE Departments (
  DepartmentID   INT PRIMARY KEY,
  DepartmentName VARCHAR(100) NOT NULL
);


CREATE TABLE Employees (
  EmployeeID     INT PRIMARY KEY,
  FirstName      VARCHAR(50) NOT NULL,
  LastName       VARCHAR(50) NOT NULL,
  DepartmentID   INT NOT NULL
    REFERENCES Departments(DepartmentID),
  Salary         DECIMAL(10,2) NOT NULL,
  JoinDate       DATE NOT NULL
);
GO


INSERT INTO Departments (DepartmentID, DepartmentName) VALUES
  (1, 'HR'),
  (2, 'Finance'),
  (3, 'IT'),
  (4, 'Marketing');

INSERT INTO Employees (EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate) VALUES
  (1, 'John',    'Doe',     1, 5000.00, '2020-01-15'),
  (2, 'Jane',    'Smith',   2, 6000.00, '2019-03-22'),
  (3, 'Michael', 'Johnson', 3, 7000.00, '2018-07-30'),
  (4, 'Emily',   'Davis',   4, 5500.00, '2021-11-05');
GO

-- Query to Retrieve employees by department
DROP PROCEDURE IF EXISTS sp_GetStaffByDept;
GO
CREATE PROCEDURE sp_GetStaffByDept
  @DeptID INT
AS
BEGIN
  SELECT
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    d.DepartmentName AS DeptName,
    e.Salary,
    e.JoinDate
  FROM Employees AS e
  JOIN Departments AS d
    ON e.DepartmentID = d.DepartmentID
  WHERE e.DepartmentID = @DeptID
  ORDER BY e.LastName, e.FirstName;
END;
GO

-- Query to Insert a new employee with automatic ID assignment
DROP PROCEDURE IF EXISTS sp_AddNewStaff;
GO
CREATE PROCEDURE sp_AddNewStaff
  @FirstName    VARCHAR(50),
  @LastName     VARCHAR(50),
  @DepartmentID INT,
  @Salary       DECIMAL(10,2),
  @JoinDate     DATE
AS
BEGIN
  DECLARE @NewID INT;
  SELECT @NewID = ISNULL(MAX(EmployeeID), 0) + 1 FROM Employees;

  INSERT INTO Employees (
    EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate
  )
  VALUES (
    @NewID, @FirstName, @LastName, @DepartmentID, @Salary, @JoinDate
  );

  SELECT @NewID AS NewEmployeeID;
END;
GO


-- Example: Retrieve staff in department 2

EXEC sp_GetStaffByDept @DeptID = 2;


