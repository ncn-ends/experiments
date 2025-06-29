-- https://leetcode.com/problems/human-traffic-of-stadium/

Create table If Not Exists Stadium (id int, visit_date DATE NULL, people int)
Truncate table Stadium
insert into Stadium (id, visit_date, people) values ('1', '2017-01-01', '10')
insert into Stadium (id, visit_date, people) values ('2', '2017-01-02', '109')
insert into Stadium (id, visit_date, people) values ('3', '2017-01-03', '150')
insert into Stadium (id, visit_date, people) values ('4', '2017-01-04', '99')
insert into Stadium (id, visit_date, people) values ('5', '2017-01-05', '145')
insert into Stadium (id, visit_date, people) values ('6', '2017-01-06', '1455')
insert into Stadium (id, visit_date, people) values ('7', '2017-01-07', '199')
insert into Stadium (id, visit_date, people) values ('8', '2017-01-09', '188')

-- can't use this approach, because it doesn't look behind, so it'll never return the rows are in the end of the correct sequence
--
-- SELECT *
-- FRom Stadium s
-- CROSS JOIN LATERAL (
--     SELECT *
--     FROM Stadium s2
--     WHERE
--         s2.id = s.id + 1
--         AND s2.people >= 100
-- ) next
-- CROSS JOIN LATERAL (
--     SELECT *
--     FROM Stadium s2
--     WHERE
--         s2.id = s.id + 2
--       AND s2.people >= 100
-- ) next2
-- ;

SELECT
--     s.id,
--     COUNT(related)
*
FROM Stadium s
JOIN LATERAL (
    SELECT
        *,
        Abs(s.id - s2.id) as diff,
        row_number() over (PARTITION BY s.id ORDER BY s2.id)
    FROM Stadium s2
    WHERE
        s2.people >= 100
        AND s2.id != s.id
) related ON related.diff <= 2 ANd s.people >= 100
-- GROUP BY s.id
-- HAVING COUNT(related) >= 2

-- TODO: pretty hard, come back