CREATE TYPE action_enum AS ENUM ('timeout', 'confirmed');

Create table If Not Exists Signups (user_id int, time_stamp timestamp);
Create table If Not Exists Confirmations (user_id int, time_stamp timestamp, action action_enum);
Truncate table Signups;
insert into Signups (user_id, time_stamp) values ('3', '2020-03-21 10:16:13');
insert into Signups (user_id, time_stamp) values ('7', '2020-01-04 13:57:59');
insert into Signups (user_id, time_stamp) values ('2', '2020-07-29 23:09:44');
insert into Signups (user_id, time_stamp) values ('6', '2020-12-09 10:39:37');
Truncate table Confirmations;
insert into Confirmations (user_id, time_stamp, action) values ('3', '2021-01-06 03:30:46', 'timeout');
insert into Confirmations (user_id, time_stamp, action) values ('3', '2021-07-14 14:00:00', 'timeout');
insert into Confirmations (user_id, time_stamp, action) values ('7', '2021-06-12 11:57:29', 'confirmed');
insert into Confirmations (user_id, time_stamp, action) values ('7', '2021-06-13 12:58:28', 'confirmed');
insert into Confirmations (user_id, time_stamp, action) values ('7', '2021-06-14 13:59:27', 'confirmed');
insert into Confirmations (user_id, time_stamp, action) values ('2', '2021-01-22 00:00:00', 'confirmed');
insert into Confirmations (user_id, time_stamp, action) values ('2', '2021-02-28 23:59:59', 'timeout');

-- SELECT c.user_id, COUNT(c2) as "confirms", COUNT(*) as "total", ROUND(COUNT(c2)::numeric / COUNT(c)::numeric, 2) as "ratio"
-- FROM Confirmations c
-- LEFT JOIN public.confirmations c2 on c.user_id = c2.user_id AND c2.action = 'confirmed'
-- GROUP BY c.user_id
--
-- WITH
-- SELECT
--     su.user_id,
--     COUNT(all_c.*)
-- FROM Signups su
-- JOIN Confirmations all_c on su.user_id = all_c.user_id
-- JOIN Confirmations confirmed_c on su.user_id = all_c.user_id AND confirmed_c.action = 'confirmed'
-- GROUP BY su.user_id

-- SELECT (
--     SELECT 1.0
--            ) / (SELECT 2)

-- 6/21, beats 76.40%
SELECT su.user_id, (COALESCE(c.count, 0)::numeric(10, 2) / COALESCE(c2.count, 1))::numeric(10, 2) as "confirmation_rate"
FROM Signups su
LEFT JOIN (SELECT user_id, COUNT(*) FROM Confirmations WHERE Confirmations.action = 'confirmed' GROUP BY user_id) c on c.user_id = su.user_id
LEFT JOIN (SELECT user_id, COUNT(*) FROM Confirmations GROUP BY user_id) c2 on c2.user_id = su.user_id

/* TODO: interesting one. come back to and check out other solutions */
