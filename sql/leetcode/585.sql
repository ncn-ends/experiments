-- https://leetcode.com/problems/investments-in-2016/?envType=study-plan-v2&envId=top-sql-50

Create Table If Not Exists Insurance (pid int, tiv_2015 float, tiv_2016 float, lat float, lon float)
Truncate table Insurance
insert into Insurance (pid, tiv_2015, tiv_2016, lat, lon) values ('1', '10', '5', '10', '10')
insert into Insurance (pid, tiv_2015, tiv_2016, lat, lon) values ('2', '20', '20', '20', '20')
insert into Insurance (pid, tiv_2015, tiv_2016, lat, lon) values ('3', '10', '30', '20', '20')
insert into Insurance (pid, tiv_2015, tiv_2016, lat, lon) values ('4', '10', '40', '40', '40')

-- 7/3, beats 67.11%
SELECT
    ROUND(SUM(i.tiv_2016)::numeric(10, 2), 2) as tiv_2016
FROM Insurance i
WHERE EXISTS (
    SELECT 1
    FROM Insurance i2
    WHERE
        i2.pid != i.pid
        AND i.tiv_2015 = i2.tiv_2015
) AND NOT EXISTS (
    SELECT *
    FROM Insurance i2
    WHERE
        i2.pid != i.pid
        AND i2.lat = i.lat
        AND i2.lon = i.lon
)