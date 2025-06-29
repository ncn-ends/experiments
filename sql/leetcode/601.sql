-- https://leetcode.com/problems/human-traffic-of-stadium/

Create table If Not Exists Stadium (id int, visit_date DATE NULL, people int)
Truncate table Stadium
-- insert into Stadium (id, visit_date, people) values ('1', '2017-01-01', '10')
-- insert into Stadium (id, visit_date, people) values ('2', '2017-01-02', '109')
-- insert into Stadium (id, visit_date, people) values ('3', '2017-01-03', '150')
-- insert into Stadium (id, visit_date, people) values ('4', '2017-01-04', '99')
-- insert into Stadium (id, visit_date, people) values ('5', '2017-01-05', '145')
-- insert into Stadium (id, visit_date, people) values ('6', '2017-01-06', '1455')
-- insert into Stadium (id, visit_date, people) values ('7', '2017-01-07', '199')
-- insert into Stadium (id, visit_date, people) values ('8', '2017-01-09', '188')
--
-- INSERT INTO Stadium (id, visit_date, people) VALUES (1, '2017-01-01', 10);
-- INSERT INTO Stadium (id, visit_date, people) VALUES (2, '2017-01-02', 109);
-- INSERT INTO Stadium (id, visit_date, people) VALUES (3, '2017-01-03', 150);
-- INSERT INTO Stadium (id, visit_date, people) VALUES (4, '2017-01-04', 99);
-- INSERT INTO Stadium (id, visit_date, people) VALUES (5, '2017-01-05', 145);
-- INSERT INTO Stadium (id, visit_date, people) VALUES (6, '2017-01-06', 1455);
-- INSERT INTO Stadium (id, visit_date, people) VALUES (7, '2017-01-07', 99);
-- INSERT INTO Stadium (id, visit_date, people) VALUES (8, '2017-01-08', 188);
-- INSERT INTO Stadium (id, visit_date, people) VALUES (9, '2017-01-09', 200);

-- INSERT INTO Stadium (id, visit_date, people) VALUES (1, '2017-01-01', 10);
-- INSERT INTO Stadium (id, visit_date, people) VALUES (2, '2017-01-02', 109);
-- INSERT INTO Stadium (id, visit_date, people) VALUES (3, '2017-01-03', 150);
-- INSERT INTO Stadium (id, visit_date, people) VALUES (4, '2017-01-04', 100);

INSERT INTO Stadium (id, visit_date, people) VALUES (1, '2017-01-01', 10);
INSERT INTO Stadium (id, visit_date, people) VALUES (2, '2017-01-02', 109);
INSERT INTO Stadium (id, visit_date, people) VALUES (3, '2017-01-03', 150);
INSERT INTO Stadium (id, visit_date, people) VALUES (4, '2017-01-04', 99);
INSERT INTO Stadium (id, visit_date, people) VALUES (5, '2017-01-05', 145);
INSERT INTO Stadium (id, visit_date, people) VALUES (6, '2017-01-06', 1455);
INSERT INTO Stadium (id, visit_date, people) VALUES (7, '2017-01-07', 199);




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
--     s.visit_date,
--     s.people
--     COUNT(related)
*

FROM Stadium s
JOIN LATERAL (
    SELECT
        *,
        Abs(s.id - s2.id) as diff,
        s2.id - LAG(s2.id) OVER (partition by s.id ORDER BY s2.id) as diff2
    FROM Stadium s2
    WHERE
        s2.people >= 100
) related ON
    -- ensure there's enough items in the sequence to satisfy the requirements
    related.diff <= 2
    -- original row needs to be above 100 people too
    AND s.people >= 100
--     AND (related.diff2 <= 1 OR related.diff2 is null)
-- GROUP BY s.id, s.visit_date, s.people
-- HAVING
--     COUNT(related) >= 2
ORDER BY s.visit_date ASC