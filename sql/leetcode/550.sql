-- https://leetcode.com/problems/game-play-analysis-iv/description/?envType=study-plan-v2&envId=top-sql-50

DROP TABLE Activity
Create table If Not Exists Activity (player_id int, device_id int, event_date date, games_played int)
Truncate table Activity
-- insert into Activity (player_id, device_id, event_date, games_played) values ('1', '2', '2016-03-01', '5')
-- insert into Activity (player_id, device_id, event_date, games_played) values ('1', '2', '2016-03-02', '6')
-- insert into Activity (player_id, device_id, event_date, games_played) values ('2', '3', '2017-06-25', '1')
-- insert into Activity (player_id, device_id, event_date, games_played) values ('3', '1', '2016-03-02', '0')
-- insert into Activity (player_id, device_id, event_date, games_played) values ('3', '4', '2018-07-03', '5')

insert into Activity (player_id, device_id, event_date, games_played) values ('1', '2', '2016-03-01', '5')
insert into Activity (player_id, device_id, event_date, games_played) values ('1', '2', '2016-03-02', '1')
insert into Activity (player_id, device_id, event_date, games_played) values ('3', '1', '2016-01-02', '10')
insert into Activity (player_id, device_id, event_date, games_played) values ('3', '4', '2016-01-03', '15')

-- SELECT *
--              FROM Activity a
--                       JOIN Activity a2 on a.player_id = a2.player_id AND a2.event_date = a.event_date + 1
--              WHERE a.event_date = (SELECT a3.event_date
--                                    FROM Activity a3
--                                    WHERE a.player_id = a3.player_id
--                                    ORDER BY a3.event_date ASC
--                                    LIMIT 1);
--
-- 6/26, beats 5.01%
WITH asd as (SELECT COUNT(*)
             FROM Activity a
                      JOIN Activity a2 on a.player_id = a2.player_id AND a2.event_date = a.event_date + 1
             WHERE a.event_date = (SELECT a3.event_date
                                   FROM Activity a3
                                   WHERE a.player_id = a3.player_id
                                   ORDER BY a3.event_date ASC
                                   LIMIT 1))
SELECT
    ROUND((SELECT * FROM asd)::numeric(10, 2) / COUNT(DISTINCT Activity.player_id) , 2) as fraction
FROM activity

-- TODO: trickier than most, and can be optimized a lot


