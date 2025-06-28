-- https://leetcode.com/problems/consecutive-numbers/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Logs (id int, num int)
Truncate table Logs
insert into Logs (id, num) values ('1', '1')
insert into Logs (id, num) values ('2', '1')
insert into Logs (id, num) values ('3', '1')
insert into Logs (id, num) values ('4', '2')
insert into Logs (id, num) values ('5', '1')
insert into Logs (id, num) values ('6', '2')
insert into Logs (id, num) values ('7', '2')

-- 6/27, beats 96%
SELECT
    DISTINCT l.num as ConsecutiveNums
FROM Logs l
JOIN LATERAL (
   SELECT *
    FROM Logs l2
    WHERE l2.id = l.id + 1
        AND l2.num = l.num
) a ON true
JOIN LATERAL (
   SELECT *
    FROM Logs l2
    WHERE l2.id = l.id + 2
        AND l2.num = l.num
) b ON true;

-- super easy with cross apply / join lateral