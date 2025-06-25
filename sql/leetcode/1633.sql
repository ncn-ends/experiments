-- https://leetcode.com/problems/percentage-of-users-attended-a-contest/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Users (user_id int, user_name varchar(20))
Create table If Not Exists Register (contest_id int, user_id int)
Truncate table Users
insert into Users (user_id, user_name) values ('6', 'Alice')
insert into Users (user_id, user_name) values ('2', 'Bob')
insert into Users (user_id, user_name) values ('7', 'Alex')
Truncate table Register
insert into Register (contest_id, user_id) values ('215', '6')
insert into Register (contest_id, user_id) values ('209', '2')
insert into Register (contest_id, user_id) values ('208', '2')
insert into Register (contest_id, user_id) values ('210', '6')
insert into Register (contest_id, user_id) values ('208', '6')
insert into Register (contest_id, user_id) values ('209', '7')
insert into Register (contest_id, user_id) values ('209', '6')
insert into Register (contest_id, user_id) values ('215', '7')
insert into Register (contest_id, user_id) values ('208', '7')
insert into Register (contest_id, user_id) values ('210', '2')
insert into Register (contest_id, user_id) values ('207', '2')
insert into Register (contest_id, user_id) values ('210', '7')

-- WITH asd (
--     SELECT Register.contest_id, count(*)
--     FROM Register
--     GROUP BY Register.contest_id
-- )
--     SELECT * FROM asd
-- SELECT
--     R.contest_id,
--     ROUND(COUNT(U.*)::numeric(10, 2) / (SELECT COUNT(DISTINCT contest_id) FROM Register), 2) as percentage
-- FROM Register R
-- JOIN Users U on R.user_id = U.user_id
-- GROUP BY R.contest_id

/* 6/25, beats 5% */
SELECT
    contest_id,
    ROUND(COUNT(1)::numeric(10, 2) / (SELECT COUNT(*) FROM Users) * 100, 2) as percentage
FROM Register r
GROUP BY contest_id
ORDER BY percentage DESC, contest_id ASC

-- TODO: can significantly optimize this
