-- https://leetcode.com/problems/managers-with-at-least-5-direct-reports/?envType=study-plan-v2&envId=top-sql-50

DROP TABLE Employee
Create table If Not Exists Employee (id int, name varchar(255), department varchar(255), managerId int)
Truncate table Employee
insert into Employee (id, name, department, managerId) values ('101', 'John', 'A', NULL)
insert into Employee (id, name, department, managerId) values ('102', 'Dan', 'A', '101')
insert into Employee (id, name, department, managerId) values ('103', 'James', 'A', '101')
insert into Employee (id, name, department, managerId) values ('104', 'Amy', 'A', '101')
insert into Employee (id, name, department, managerId) values ('105', 'Anne', 'A', '101')
insert into Employee (id, name, department, managerId) values ('106', 'Ron', 'B', '101')

-- 6/21, beats 68.09%
SELECT managers.name
FROM Employee
JOIN Employee managers ON managers.id = Employee.managerId
GROUP BY managers.id, managers.name
HAVING COUNT(Employee.managerId) >= 5