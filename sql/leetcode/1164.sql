-- https://leetcode.com/problems/product-price-at-a-given-date/description/?envType=study-plan-v2&envId=top-sql-50

DROP TABLE Products
Create table If Not Exists Products (product_id int, new_price int, change_date date)
Truncate table Products
insert into Products (product_id, new_price, change_date) values ('1', '20', '2019-08-14')
insert into Products (product_id, new_price, change_date) values ('2', '50', '2019-08-14')
insert into Products (product_id, new_price, change_date) values ('1', '30', '2019-08-15')
insert into Products (product_id, new_price, change_date) values ('1', '35', '2019-08-16')
insert into Products (product_id, new_price, change_date) values ('2', '65', '2019-08-17')
insert into Products (product_id, new_price, change_date) values ('3', '20', '2019-08-18')

-- 6/27, beats 8.58%
-- SELECT
--     p.product_id,
--     COALESCE(a.new_price, 10) as price
-- FROM Products p
-- LEFT JOIN LATERAL (
--     SELECT *
--     FROM Products p2
--     WHERE
--         p2.product_id = p.product_id
--         AND p2.change_date <= '2019-08-16'
--     ORDER BY p2.change_date DESC
--     LIMIT 1
-- ) a ON true
-- GROUP BY p.product_id, COALESCE(a.new_price, 10)


-- 7/5, beats 7.68%
-- WITH uniq_products AS (SELECT product_id
--               FROM Products
--               GROUP BY product_id)
-- SELECT
--     up.product_id,
--     COALESCE(asd.new_price, 10) as price
-- FROM uniq_products up
-- LEFT JOIN LATERAL (
--     SELECT *
--     FROM Products p
--     WHERE
--         p.product_id = up.product_id
--         AND p.change_date <= '2019-08-16'
--     ORDER BY change_date DESC
--     LIMIT 1
-- ) asd ON true;

-- SELECT
--     p.product_id,
--     10
-- FROM Products p
-- WHERE NOT EXISTS (
--     SELECT 1
--     FROM Products p
--     WHERE p.change_date <= '2019-08-16'
-- )