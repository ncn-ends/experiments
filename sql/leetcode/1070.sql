-- https://leetcode.com/problems/product-sales-analysis-iii/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Sales (sale_id int, product_id int, year int, quantity int, price int)
Truncate table Sales
insert into Sales (sale_id, product_id, year, quantity, price) values ('1', '100', '2008', '10', '5000')
insert into Sales (sale_id, product_id, year, quantity, price) values ('2', '100', '2009', '12', '5000')
insert into Sales (sale_id, product_id, year, quantity, price) values ('7', '200', '2011', '15', '9000')

-- 6/30, beats 78.61%
WITH first_year_by_product AS (
    SELECT
        s.product_id,
        MIN(s.year) as first_year
    FROM Sales s
    GROUP BY s.product_id
)
SELECT
    y.product_id,
    y.first_year,
    ok.quantity,
    ok.price
FROM first_year_by_product y
CROSS JOIN LATERAL (
    SELECT *
    FROM Sales s
    WHERE
        s.product_id = y.product_id
        AND y.first_year = s.year
) ok