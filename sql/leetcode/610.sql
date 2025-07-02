-- https://leetcode.com/problems/triangle-judgement/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Triangle (x int, y int, z int)
Truncate table Triangle
insert into Triangle (x, y, z) values ('13', '15', '30')
insert into Triangle (x, y, z) values ('13', '15', '20')
insert into Triangle (x, y, z) values ('10', '20', '15')

-- 7/1, beats 23.23%
SELECT
    t1.*,
    result.*
FROM Triangle t1
CROSS JOIN LATERAL (
    SELECT
        CASE WHEN
            ((t1.x + t1.y) > t1.z)
            AND
            ((t1.x + t1.z) > t1.y)
            AND
            ((t1.y + t1.z) > t1.x)
        THEN 'Yes'
        ELSE 'No'
    END as triangle
) result

-- TODO: can be optimized. wouldve been able use exists with correlated subquery, but need to produce results "yes/no" in result set
