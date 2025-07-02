-- https://leetcode.com/problems/user-activity-for-the-past-30-days-i/?envType=study-plan-v2&envId=top-sql-50

DROP TYPE activity_type;
CREATE TYPE activity_type AS enum('open_session', 'end_session', 'scroll_down', 'send_message');
DROP TABLE Activity
Create table If Not Exists Activity (user_id int, session_id int, activity_date date, activity_type activity_type)
Truncate table Activity
insert into Activity (user_id, session_id, activity_date, activity_type) values ('1', '1', '2019-07-20', 'open_session')
insert into Activity (user_id, session_id, activity_date, activity_type) values ('1', '1', '2019-07-20', 'scroll_down')
insert into Activity (user_id, session_id, activity_date, activity_type) values ('1', '1', '2019-07-20', 'end_session')
insert into Activity (user_id, session_id, activity_date, activity_type) values ('2', '4', '2019-07-20', 'open_session')
insert into Activity (user_id, session_id, activity_date, activity_type) values ('2', '4', '2019-07-21', 'send_message')
insert into Activity (user_id, session_id, activity_date, activity_type) values ('2', '4', '2019-07-21', 'end_session')
insert into Activity (user_id, session_id, activity_date, activity_type) values ('3', '2', '2019-07-21', 'open_session')
insert into Activity (user_id, session_id, activity_date, activity_type) values ('3', '2', '2019-07-21', 'send_message')
insert into Activity (user_id, session_id, activity_date, activity_type) values ('3', '2', '2019-07-21', 'end_session')
insert into Activity (user_id, session_id, activity_date, activity_type) values ('4', '3', '2019-06-25', 'open_session')
insert into Activity (user_id, session_id, activity_date, activity_type) values ('4', '3', '2019-06-25', 'end_session')


-- SELECT
--     activity_date,
--     COUNT(activity_type)
-- FROM Activity
-- WHERE activity_type != 'end_session'
-- GROUP BY activity_date

-- 7/1, beats 70.63%
WITH dates AS (
    SELECT DISTINCT activity_date
    FROM activity
    WHERE
        activity_date BETWEEN ('2019-07-27'::date - 29) AND '2019-07-27'::date
)
SELECT
    d.activity_date as day,
    COUNT(DISTINCT asd.user_id) as active_users
FROM dates d
CROSS JOIN LATERAL (
    SELECT
        *
    FROM activity a
    WHERE
        d.activity_date = a.activity_date
) asd
GROUP BY d.activity_date

-- TODO: originally - 30 days didn't work. need to figure out proper way to handle dates like this
