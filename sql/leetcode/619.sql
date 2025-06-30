-- https://leetcode.com/problems/biggest-single-number/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists MyNumbers
(
    num int
)
Truncate table MyNumbers
insert into MyNumbers (num)
values ('8')
insert into MyNumbers (num)
values ('8')
insert into MyNumbers (num)
values ('3')
insert into MyNumbers (num)
values ('3')
insert into MyNumbers (num)
values ('1')
-- insert into MyNumbers (num)
-- values ('1')
insert into MyNumbers (num)
values ('4')
-- insert into MyNumbers (num)
-- values ('4')
insert into MyNumbers (num)
values ('5')
-- insert into MyNumbers (num)
-- values ('5')
insert into MyNumbers (num)
values ('6')
-- insert into MyNumbers (num)
-- values ('6')

-- 6/30, beats 66.53%
WITH base as (SELECT MAX(n1.num) as num
FROM MyNumbers n1
    LEFT JOIN LATERAL (
        SELECT num
        FROM MyNumbers n2
        WHERE n2.num = n1.num
        ) uniq ON true
    GROUP BY n1.num
    HAVING COUNT(uniq) = 1
    ORDER BY n1.num DESC)
SELECT MAX(base.num) as num
FROM base
;
