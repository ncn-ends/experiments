-- https://leetcode.com/problems/exchange-seats/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Seat (id int, student varchar(255))
Truncate table Seat
insert into Seat (id, student) values ('1', 'Abbot')
insert into Seat (id, student) values ('2', 'Doris')
insert into Seat (id, student) values ('3', 'Emerson')
insert into Seat (id, student) values ('4', 'Green')
insert into Seat (id, student) values ('5', 'Jeames')

-- 7/3, beats 73.55%
WITH max as (
    SELECT MAX(id) FROM seat
),
base AS (
    SELECT
        s.id % 2 as pairs,
        LAG(s.id) OVER (order by s.id ASC) prev,
        LEAD(s.id) OVER (order by s.id ASC) next,
        *
    FROM Seat s
)
SELECT
    CASE
        WHEN ((SELECT * FROM MAX) = base.id AND base.pairs = 1) THEN id
        WHEN pairs = 0 THEN prev
        WHEN pairs = 1 THEN next
    END as id,
    student
FROM base
ORDER BY id;