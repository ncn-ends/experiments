-- https://leetcode.com/problems/group-sold-products-by-the-date/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Activities (sell_date date, product varchar(20))
Truncate table Activities
insert into Activities (sell_date, product) values ('2020-05-30', 'Headphone')
insert into Activities (sell_date, product) values ('2020-06-01', 'Pencil')
insert into Activities (sell_date, product) values ('2020-06-02', 'Mask')
insert into Activities (sell_date, product) values ('2020-05-30', 'Basketball')
insert into Activities (sell_date, product) values ('2020-06-01', 'Bible')
insert into Activities (sell_date, product) values ('2020-06-02', 'Mask')
insert into Activities (sell_date, product) values ('2020-05-30', 'T-Shirt')


-- WITH base AS (SELECT 'headphone' as item
--               UNION ALL
--               SELECT 'pencil' as item)
-- SELECT string_agg(item, ', ')
-- FROM base

-- 7/4, beats 75.06%
SELECT
    a.sell_date,
    COUNT(DISTINCT a.product) as num_sold,
    string_agg(DISTINCT a.product, ',') as products
FROM Activities a
GROUP BY a.sell_date
ORDER BY sell_date