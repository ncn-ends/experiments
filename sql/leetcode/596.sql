-- https://leetcode.com/problems/classes-with-at-least-5-students/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Courses (student varchar(255), class varchar(255))
Truncate table Courses
insert into Courses (student, class) values ('A', 'Math')
insert into Courses (student, class) values ('B', 'English')
insert into Courses (student, class) values ('C', 'Math')
insert into Courses (student, class) values ('D', 'Biology')
insert into Courses (student, class) values ('E', 'Math')
insert into Courses (student, class) values ('F', 'Computer')
insert into Courses (student, class) values ('G', 'Math')
insert into Courses (student, class) values ('H', 'Math')
insert into Courses (student, class) values ('I', 'Math')

-- 6/31, beats 43.15%
WITH base AS (
    SELECT
        class,
        COUNT(student) OVER (PARTITION BY class) as count
    FROM Courses
)
SELECT
    distinct class as class
FROM base
WHERE count >= 5

-- TODO: decided to use window functions just to use them more, but group by might be better performance. check later