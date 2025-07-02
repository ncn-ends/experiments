-- https://leetcode.com/problems/second-highest-salary/?envType=study-plan-v2&envId=top-sql-50


DROP TABLE Employee
Create table If Not Exists Employee (id int, salary int)
Truncate table Employee
insert into Employee (id, salary) values ('1', '100')
insert into Employee (id, salary) values ('2', '200')
insert into Employee (id, salary) values ('3', '300')

-- 7/01, beats 5.10%
WITH base as (SELECT *,
                     DENSE_RANK() OVER (ORDER BY salary DESC) as rank
              FROM Employee),
second_rank AS (SELECT salary as SecondHighestSalary
FROM base b
WHERE b.rank = 2)

SELECT sr.SecondHighestSalary
FROM second_rank sr

UNION ALL

SELECT NULL
WHERE NOT EXISTS (
    SELECT 1
    FROM second_rank
)
LIMIT 1;

-- TODO: can be optimized significantly
