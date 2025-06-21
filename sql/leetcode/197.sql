-- https://leetcode.com/problems/rising-temperature/description/?envType=study-plan-v2&envId=top-sql-50

-- 6/20
Create table If Not Exists Weather (id int, recordDate date, temperature int)
Truncate table Weather
insert into Weather (id, recordDate, temperature) values ('1', '2015-01-01', '10')
insert into Weather (id, recordDate, temperature) values ('2', '2015-01-02', '25')
insert into Weather (id, recordDate, temperature) values ('3', '2015-01-03', '20')
insert into Weather (id, recordDate, temperature) values ('4', '2015-01-04', '30')

-- 6/20, beats 40.48%
SELECT w.id
FROM Weather w
LEFT JOIN Weather wPre ON w.recordDate - wPre.recordDate = 1
WHERE w.temperature > wPre.temperature

-- 6/20, beats 12.52%
SELECT w.id
FROM Weather w
LEFT JOIN Weather wPre ON wPre.recordDate + 1 = w.recordDate
WHERE w.temperature > wPre.temperature

-- 6/20, copied, beats 74.08%
-- https://leetcode.com/problems/rising-temperature/solutions/5014365/beats-99-well-explaind-performand-postgresql-solution-without-in-and-join/?envType=study-plan-v2&envId=top-sql-50
SELECT current_day.id
FROM weather current_day
WHERE exists(
    SELECT 1
    FROM Weather as yesterday
    WHERE current_day.temperature > yesterday.temperature
        AND current_day.recordDate = yesterday.recordDate + 1
)
-- for each record in weather table (current_day), try to find a record in the same table where the current_day temp is higher then the yesterday_temp, and the record_date is one day after. if any records exist like this, return true, so can return a basic 1 to make it more performant

-- note: LAG function won't work, because it'll compare previous row and won't fail on the first row in the table, when the requirements are that it should fail

