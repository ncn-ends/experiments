-- DROP TYPE primary_flag_enum
CREATE TYPE primary_flag_enum AS enum('Y', 'N')
DROP TABLE Employee
Create table If Not Exists Employee (employee_id int, department_id int, primary_flag primary_flag_enum)
Truncate table Employee
insert into Employee (employee_id, department_id, primary_flag) values ('1', '1', 'N')
insert into Employee (employee_id, department_id, primary_flag) values ('2', '1', 'Y')
insert into Employee (employee_id, department_id, primary_flag) values ('2', '2', 'N')
insert into Employee (employee_id, department_id, primary_flag) values ('3', '3', 'N')
insert into Employee (employee_id, department_id, primary_flag) values ('4', '2', 'N')
insert into Employee (employee_id, department_id, primary_flag) values ('4', '3', 'Y')
insert into Employee (employee_id, department_id, primary_flag) values ('4', '4', 'N')

-- 7/1, beats 5.06%
WITH asd as (SELECT
    e.Employee_id,
    CASE
        WHEN asd.department_id is null
            THEN (e.department_id)
            ELSE asd.department_id
    END as qwe
FROM Employee e
LEFT JOIN LATERAL (
   SELECT *
    FROM Employee e2
    WHERE
        primary_flag = 'Y'
        AND e2.employee_id = e.employee_id
) asd ON True)
SELECT
    DISTINCT asd.employee_id,
    asd.qwe as department_id
FROM asd

-- TODO: can be optimized significantly