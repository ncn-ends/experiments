-- https://leetcode.com/problems/average-selling-price/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Prices (product_id int, start_date date, end_date date, price int)
Create table If Not Exists UnitsSold (product_id int, purchase_date date, units int)
Truncate table Prices
insert into Prices (product_id, start_date, end_date, price) values ('1', '2019-02-17', '2019-02-28', '5')
insert into Prices (product_id, start_date, end_date, price) values ('1', '2019-03-01', '2019-03-22', '20')
insert into Prices (product_id, start_date, end_date, price) values ('2', '2019-02-01', '2019-02-20', '15')
insert into Prices (product_id, start_date, end_date, price) values ('2', '2019-02-21', '2019-03-31', '30')
Truncate table UnitsSold
-- insert into UnitsSold (product_id, purchase_date, units) values ('1', '2019-02-25', '100')
-- insert into UnitsSold (product_id, purchase_date, units) values ('1', '2019-03-01', '15')
-- insert into UnitsSold (product_id, purchase_date, units) values ('2', '2019-02-10', '200')
-- insert into UnitsSold (product_id, purchase_date, units) values ('2', '2019-03-22', '30')

-- SELECT
--     US.product_id,
--     ROUND(SUM(P.price * US.units)::numeric(10, 2) / SUM(US.units), 2) as average_price
-- --     *
-- FROM UnitsSold US
-- LEFT JOIN Prices P on US.product_id = P.product_id AND US.purchase_date <= P.end_date AND US.purchase_date >= P.start_date
-- GROUP BY Us.product_id

/* 6/22, beats 98.61 */
SELECT
    p.product_id,
    COALESCE(ROUND(SUM(p.price * US.units)::numeric(10, 2) / SUM(US.units)::numeric(10, 2), 2), 0)
FROM Prices p
LEFT JOIN UnitsSold US on p.product_id = US.product_id AND US.purchase_date >= P.start_date AND US.purchase_date <= P.end_date
GROUP BY p.product_id

/* TODO: good exercise for aggregates */