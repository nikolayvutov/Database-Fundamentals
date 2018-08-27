-- 1.	Records’ Count


SELECT COUNT(Id) AS [Count]
FROM WizzardDeposits;


-- 2.	Longest Magic Wand


SELECT MAX(MagicWandSize) AS [LongestMagicWand]
FROM WizzardDeposits;


-- 3.	Longest Magic Wand per Deposit Groups


SELECT DepositGroup,
       MAX(MagicWandSize) AS [LongestMagicWand]
FROM WizzardDeposits
GROUP BY DepositGroup;


-- 4.	* Smallest Deposit Group per Magic Wand Size


SELECT TOP 1 WITH TIES DepositGroup
FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY AVG(MagicWandSize);


-- 5.	Deposits Sum


SELECT DepositGroup,
       SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
GROUP BY DepositGroup;


-- 6.	Deposits Sum for Ollivander Family


SELECT DepositGroup,
       SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup;


-- 7.	Deposits Filter


SELECT DepositGroup,
       SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
ORDER BY TotalSum DESC;


-- 8.	 Deposit Charge


SELECT DepositGroup,
       MagicWandCreator,
       MIN(DepositCharge) AS MinDepositCharge
FROM WizzardDeposits
GROUP BY DepositGroup,
         MagicWandCreator
ORDER BY MagicWandCreator,
         DepositGroup;


-- 9.	Age Groups


SELECT grouped.AgeGroups,
       COUNT(*) AS WizzardsCount
FROM
(
    SELECT CASE
               WHEN Age BETWEEN 0 AND 10
               THEN '[0-10]'
               WHEN Age BETWEEN 11 AND 20
               THEN '[11-20]'
               WHEN Age BETWEEN 21 AND 30
               THEN '[21-30]'
               WHEN Age BETWEEN 31 AND 40
               THEN '[31-40]'
               WHEN Age BETWEEN 41 AND 50
               THEN '[41-50]'
               WHEN Age BETWEEN 51 AND 60
               THEN '[51-60]'
               WHEN Age >= 61
               THEN '[61+]'
               ELSE 'N\A'
           END AS AgeGroups
    FROM WizzardDeposits
) AS grouped
GROUP BY grouped.AgeGroups;


-- 10.	First Letter


SELECT LEFT(FirstName, 1) AS FirstLetter
FROM WizzardDeposits
WHERE DepositGroup = 'Troll Chest'
GROUP BY LEFT(FirstName, 1)
ORDER BY FirstLetter;


-- 11.	Average Interest


SELECT DepositGroup,
       IsDepositExpired,
       AVG(1.0 * DepositInterest)
FROM WizzardDeposits
WHERE DepositStartDate > '01/01/1985'
GROUP BY DepositGroup,
         IsDepositExpired
ORDER BY DepositGroup DESC,
         IsDepositExpired;


-- 12.	* Rich Wizard, Poor Wizard


SELECT SUM(ws.Difference)
FROM
(
    SELECT DepositAmount -
    (
        SELECT DepositAmount
        FROM WizzardDeposits AS wsd
        WHERE wsd.Id = wd.Id + 1
    ) AS Difference
    FROM WizzardDeposits AS wd
) AS ws;


SELECT SUM(WizardDep.Difference)
FROM
(
    SELECT FirstName,
           DepositAmount,
           LEAD(FirstName) OVER(ORDER BY Id) AS GuestWizard,
           LAG(FirstName) OVER(ORDER BY Id) AS GuestLagWizard,
           LEAD(DepositAmount) OVER(ORDER BY Id) AS GuestDeposit,
           LAG(DepositAmount) OVER(ORDER BY Id) AS GuestLagDeposit,
           DepositAmount - LEAD(DepositAmount) OVER(ORDER BY Id) AS Difference
    FROM WizzardDeposits
) AS WizardDep;


-- Problem 13.	Departments Total Salaries


USE SoftUni;

SELECT DepartmentID,
       SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentID;


-- Problem 14.	Employees Minimum Salaries


SELECT DepartmentID,
       MIN(Salary) AS MinimumSalary
FROM Employees
WHERE DepartmentID LIKE '[2, 5, 7]'
      AND HireDate > '01/01/2000'
GROUP BY DepartmentID;


-- Problem 15.	Employees Average Salaries


SELECT *
INTO NewTable
FROM Employees
WHERE Salary > 30000;

DELETE FROM NewTable
WHERE ManagerID = 42;

UPDATE NewTable
  SET
      Salary += 5000
WHERE DepartmentID = 1;

SELECT DepartmentID,
       AVG(Salary)
FROM NewTable
GROUP BY DepartmentID;


-- Problem 16. Employees Maximum Salaries


SELECT DepartmentID,
       MAX(Salary) AS MaxSalary
FROM Employees
GROUP BY DepartmentID
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000;


-- Problem 17.	Employees Count Salaries


SELECT COUNT(Salary)
FROM Employees
WHERE ManagerID IS NULL;


-- Problem 18.	3rd Highest Salary


SELECT salaries.DepartmentID,
       salaries.Salary
FROM
(
    SELECT DepartmentID,
           Salary,
           DENSE_RANK() OVER(PARTITION BY DepartmentID ORDER BY Salary DESC) AS Rank
    FROM Employees
    GROUP BY DepartmentID,
             Salary
) AS salaries
WHERE Rank = 3
GROUP BY salaries.DepartmentID,
         salaries.Salary;


-- Problem 19. Salary Challenge


SELECT TOP 10 FirstName,
              LastName,
              DepartmentID
FROM Employees AS e
WHERE Salary >
(
    SELECT AVG(Salary)
    FROM Employees AS em
    WHERE e.DepartmentID = em.DepartmentID
);