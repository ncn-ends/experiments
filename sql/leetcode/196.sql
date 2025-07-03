-- https://leetcode.com/problems/delete-duplicate-emails/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Person (Id int, Email varchar(255))
Truncate table Person
insert into Person (id, email) values ('1', 'john@example.com')
insert into Person (id, email) values ('2', 'bob@example.com')
insert into Person (id, email) values ('3', 'john@example.com')

-- 7/3, beats 37.52%
DELETE FROM Person p
WHERE EXISTS (
    SELECT 1
    FROM Person p2
    WHERE p.Email = p2.Email AND p2.Id < p.Id
)

-- TODO: can be optimized
-- NOTE: good example of using exists for a delete statement