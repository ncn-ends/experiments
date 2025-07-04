--https://leetcode.com/problems/recyclable-and-low-fat-products/?envType=study-plan-v2&envId=top-sql-50


-- 7/4, beats 82.28%
CREATE TYPE yes_or_no_enum AS ENUM('Y', 'N')
DROP TABLE Products
Create table If Not Exists Products (product_id int, low_fats yes_or_no_enum, recyclable yes_or_no_enum)
Truncate table Products
insert into Products (product_id, low_fats, recyclable) values ('0', 'Y', 'N')
insert into Products (product_id, low_fats, recyclable) values ('1', 'Y', 'Y')
insert into Products (product_id, low_fats, recyclable) values ('2', 'N', 'Y')
insert into Products (product_id, low_fats, recyclable) values ('3', 'Y', 'Y')
insert into Products (product_id, low_fats, recyclable) values ('4', 'N', 'N')

SELECT
    p.product_id
FROM Products p
WHERE p.low_fats = 'Y' AND p.recyclable = 'Y'