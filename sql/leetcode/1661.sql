-- https://leetcode.com/problems/average-time-of-process-per-machine/solutions/3722056/sql-join-or-subquery-easy-to-understand/?envType=study-plan-v2&envId=top-sql-50

CREATE TYPE activity_type_enum AS ENUM('start', 'end');
Create table If Not Exists Activity (machine_id int, process_id int, activity_type activity_type_enum, timestamp float)
Truncate table Activity
insert into Activity (machine_id, process_id, activity_type, timestamp) values ('0', '0', 'start', '0.712')
insert into Activity (machine_id, process_id, activity_type, timestamp) values ('0', '0', 'end', '1.52')
insert into Activity (machine_id, process_id, activity_type, timestamp) values ('0', '1', 'start', '3.14')
insert into Activity (machine_id, process_id, activity_type, timestamp) values ('0', '1', 'end', '4.12')
insert into Activity (machine_id, process_id, activity_type, timestamp) values ('1', '0', 'start', '0.55')
insert into Activity (machine_id, process_id, activity_type, timestamp) values ('1', '0', 'end', '1.55')
insert into Activity (machine_id, process_id, activity_type, timestamp) values ('1', '1', 'start', '0.43')
insert into Activity (machine_id, process_id, activity_type, timestamp) values ('1', '1', 'end', '1.42')
insert into Activity (machine_id, process_id, activity_type, timestamp) values ('2', '0', 'start', '4.1')
insert into Activity (machine_id, process_id, activity_type, timestamp) values ('2', '0', 'end', '4.512')
insert into Activity (machine_id, process_id, activity_type, timestamp) values ('2', '1', 'start', '2.5')
insert into Activity (machine_id, process_id, activity_type, timestamp) values ('2', '1', 'end', '5')

SELECT machine_id
FROM Activity a
JOIN (
    SELECT machine_id,
    FROM public.activity asd
    JOIN (SELECT *) asd2 ON asd2.process_id = asd.process_id
) a2 ON a2.machine_id = a.machine_id
GROUP BY machine_id

-- this is a monstrosity
-- SELECT *
-- FROM Activity process_activity
-- JOIN LATERAL (
--     WITH a as (
--         SELECT *
--         FROM activity
--         WHERE
--             Activity.machine_id = 0
--             AND Activity.process_id = process_activity.process_id
--     )
--     SELECT a.process_id, ((SELECT timestamp from a where activity_type = 'end') - (SELECT timestamp FROM a WHERE activity_type = 'start')) FROM a) asd ON asd.process_id = process_activity.process_id

-- SELECT *
-- FROM Activity a1
-- JOIN public.activity a2 on a1.machine_id = a2.machine_id AND a1.process_id=a2.process_id and a1.activity_type = 'start' and a2.activity_type = 'end'

-- 6/21, beats 64.28%
SELECT a1.machine_id,
       avg(a2.timestamp - a1.timestamp)::numeric(10, 3)
       as processing_time
FROM Activity a1
JOIN activity a2 ON a2.machine_id = a1.machine_id AND a1.process_id = a2.process_id AND a1.activity_type = 'start' AND a2.activity_type = 'end'
GROUP BY a1.machine_id

/* TODO: had trouble on this one, although it was late at night. come back to this and make sure you can do it easily */