-- https://leetcode.com/problems/department-top-three-salaries/?envType=study-plan-v2&envId=top-sql-50

DROP TABLE Employee
Create table If Not Exists Employee
(
    id           int,
    name         varchar(255),
    salary       int,
    departmentId int
)
Create table If Not Exists Department
(
    id   int,
    name varchar(255)
)
Truncate table Employee
insert into Employee (id, name, salary, departmentId)
values ('1', 'Joe', '85000', '1')
insert into Employee (id, name, salary, departmentId)
values ('2', 'Henry', '80000', '2')
insert into Employee (id, name, salary, departmentId)
values ('3', 'Sam', '60000', '2')
insert into Employee (id, name, salary, departmentId)
values ('4', 'Max', '90000', '1')
insert into Employee (id, name, salary, departmentId)
values ('5', 'Janet', '69000', '1')
insert into Employee (id, name, salary, departmentId)
values ('6', 'Randy', '85000', '1')
insert into Employee (id, name, salary, departmentId)
values ('7', 'Will', '70000', '1')
Truncate table Department
insert into Department (id, name)
values ('1', 'IT')
insert into Department (id, name)
values ('2', 'Sales')


-- 6/28, beats 97.43%
SELECT
    d.name as "Department",
    o.name as "Employee",
    o.salary as "Salary"
FROM Department d
JOIN LATERAL (
    SELECT
        *,
        dense_rank() OVER (PARTITION BY e.departmentId ORDER BY e.salary DESC) as rank
    FROM Employee e
    WHERE
        e.departmentId = d.id
    ORDER BY salary DESC
) o ON rank <= 3;

-- wasn't too hard, despite being a hard problem