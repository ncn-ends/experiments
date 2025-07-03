-- https://leetcode.com/problems/fix-names-in-a-table/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Users (user_id int, name varchar(40))
Truncate table Users
insert into Users (user_id, name) values ('1', 'aLice')
insert into Users (user_id, name) values ('2', 'bOB')


-- 7/3, beats 29.94%
SELECT
    u.user_id,
    UPPER(LEFT(u.name, 1)) || LOWER(SUBSTRING(u.name FROM 2)) as name
FROM Users u
ORDER BY u.user_id

--  initcap doesnt work, because the first character needs to be capitalized, nothing else. in the case of multi word names, initcap will make an incorrect result
-- TODO: optimize