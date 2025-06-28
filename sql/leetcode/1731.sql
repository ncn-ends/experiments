-- https://leetcode.com/problems/the-number-of-employees-which-report-to-each-employee/description/?envType=study-plan-v2&envId=top-sql-50

DROP TABLE Employees
Create table If Not Exists Employees(employee_id int, name varchar(20), reports_to int, age int)
Truncate table Employees
-- insert into Employees (employee_id, name, reports_to, age) values ('9', 'Hercy', NULL, '43')
-- insert into Employees (employee_id, name, reports_to, age) values ('6', 'Alice', '9', '41')
-- insert into Employees (employee_id, name, reports_to, age) values ('4', 'Bob', '9', '36')
-- insert into Employees (employee_id, name, reports_to, age) values ('2', 'Winston', NULL, '37')

INSERT INTO Employees (employee_id, name, reports_to, age) VALUES (1, 'Michael', NULL, 45);
INSERT INTO Employees (employee_id, name, reports_to, age) VALUES (2, 'Alice', 1, 38);
INSERT INTO Employees (employee_id, name, reports_to, age) VALUES (3, 'Bob', 1, 42);
INSERT INTO Employees (employee_id, name, reports_to, age) VALUES (4, 'Charlie', 2, 34);
INSERT INTO Employees (employee_id, name, reports_to, age) VALUES (5, 'David', 2, 40);
INSERT INTO Employees (employee_id, name, reports_to, age) VALUES (6, 'Eve', 3, 37);
INSERT INTO Employees (employee_id, name, reports_to, age) VALUES (7, 'Frank', NULL, 50);
INSERT INTO Employees (employee_id, name, reports_to, age) VALUES (8, 'Grace', NULL, 48);

-- 6/27, beats 66.58%
SELECT
    managers.employee_id,
    managers.name,
    COUNT(e1.reports_to) as reports_count,
    ROUND(AVG(e1.age)) as average_age
FROM Employees e1
JOIN LATERAL (
  SELECT *
  FROM Employees e2
  WHERE
      e1.reports_to = e2.employee_id
) managers ON true
GROUP BY managers.employee_id, managers.name, managers.age
ORDER BY managers.employee_id;