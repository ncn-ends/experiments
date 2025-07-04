-- https://leetcode.com/problems/restaurant-growth/description/?envType=study-plan-v2&envId=top-sql-50

DROP TABLE Customer
Create table If Not Exists Customer (customer_id int, name varchar(20), visited_on date, amount int)
Truncate table Customer
insert into Customer (customer_id, name, visited_on, amount) values ('1', 'Jhon', '2019-01-01', '100')
insert into Customer (customer_id, name, visited_on, amount) values ('2', 'Daniel', '2019-01-02', '110')
insert into Customer (customer_id, name, visited_on, amount) values ('3', 'Jade', '2019-01-03', '120')
insert into Customer (customer_id, name, visited_on, amount) values ('4', 'Khaled', '2019-01-04', '130')
insert into Customer (customer_id, name, visited_on, amount) values ('5', 'Winston', '2019-01-05', '110')
insert into Customer (customer_id, name, visited_on, amount) values ('6', 'Elvis', '2019-01-06', '140')
insert into Customer (customer_id, name, visited_on, amount) values ('7', 'Anna', '2019-01-07', '150')
insert into Customer (customer_id, name, visited_on, amount) values ('8', 'Maria', '2019-01-08', '80')
insert into Customer (customer_id, name, visited_on, amount) values ('9', 'Jaze', '2019-01-09', '110')
insert into Customer (customer_id, name, visited_on, amount) values ('1', 'Jhon', '2019-01-10', '130')
insert into Customer (customer_id, name, visited_on, amount) values ('3', 'Jade', '2019-01-10', '150')

-- 7/4, beats 75.04%
WITH moving_avg_dates AS (
    SELECT
        visited_on
    FROM Customer c
    WHERE EXISTS (SELECT 1
                  FROM Customer c2
                  WHERE c.visited_on - 6 = c2.visited_on)
    GROUP BY visited_on
    ORDER BY visited_on ASC
)
SELECT
    d.visited_on,
    SUM(ok.amount) as amount,
    ROUND(SUM(ok.amount) / 7.0, 2) as average_amount
-- *
FROM moving_avg_dates d
CROSS JOIN LATERAL (
    SELECT *
    FROM Customer c
    WHERE
        c.visited_on BETWEEN d.visited_on - 6 AND d.visited_on
) ok
GROUP BY d.visited_on
-- ORDER BY d.visited_on
