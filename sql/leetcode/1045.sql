-- https://leetcode.com/problems/customers-who-bought-all-products/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Customer (customer_id int, product_key int)
DROP TABLE Product
Create table Product (product_key int)
Truncate table Customer
insert into Customer (customer_id, product_key) values ('1', '5')
insert into Customer (customer_id, product_key) values ('2', '6')
insert into Customer (customer_id, product_key) values ('3', '5')
insert into Customer (customer_id, product_key) values ('3', '6')
insert into Customer (customer_id, product_key) values ('1', '6')
Truncate table Product
insert into Product (product_key) values ('5')
insert into Product (product_key) values ('6')
--
-- INSERT INTO Customer (customer_id, product_key) VALUES (39, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (7, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (21, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (21, 14);
-- INSERT INTO Customer (customer_id, product_key) VALUES (4, 18);
-- INSERT INTO Customer (customer_id, product_key) VALUES (12, 8);
-- INSERT INTO Customer (customer_id, product_key) VALUES (33, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (40, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (12, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (27, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (23, 10);
-- INSERT INTO Customer (customer_id, product_key) VALUES (28, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (20, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (40, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (19, 8);
-- INSERT INTO Customer (customer_id, product_key) VALUES (44, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (5, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (40, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (44, 18);
-- INSERT INTO Customer (customer_id, product_key) VALUES (40, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (44, 20);
-- INSERT INTO Customer (customer_id, product_key) VALUES (37, 10);
-- INSERT INTO Customer (customer_id, product_key) VALUES (31, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (25, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (7, 16);
-- INSERT INTO Customer (customer_id, product_key) VALUES (48, 10);
-- INSERT INTO Customer (customer_id, product_key) VALUES (22, 16);
-- INSERT INTO Customer (customer_id, product_key) VALUES (42, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (30, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (12, 16);
-- INSERT INTO Customer (customer_id, product_key) VALUES (48, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (49, 14);
-- INSERT INTO Customer (customer_id, product_key) VALUES (10, 16);
-- INSERT INTO Customer (customer_id, product_key) VALUES (28, 8);
-- INSERT INTO Customer (customer_id, product_key) VALUES (46, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (37, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (27, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (20, 15);
-- INSERT INTO Customer (customer_id, product_key) VALUES (36, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (40, 14);
-- INSERT INTO Customer (customer_id, product_key) VALUES (33, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (26, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (45, 13);
-- INSERT INTO Customer (customer_id, product_key) VALUES (29, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (13, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (2, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (46, 20);
-- INSERT INTO Customer (customer_id, product_key) VALUES (4, 8);
-- INSERT INTO Customer (customer_id, product_key) VALUES (17, 8);
-- INSERT INTO Customer (customer_id, product_key) VALUES (11, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (46, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (33, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (49, 14);
-- INSERT INTO Customer (customer_id, product_key) VALUES (7, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (42, 20);
-- INSERT INTO Customer (customer_id, product_key) VALUES (11, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (4, 18);
-- INSERT INTO Customer (customer_id, product_key) VALUES (5, 15);
-- INSERT INTO Customer (customer_id, product_key) VALUES (6, 8);
-- INSERT INTO Customer (customer_id, product_key) VALUES (15, 13);
-- INSERT INTO Customer (customer_id, product_key) VALUES (9, 14);
-- INSERT INTO Customer (customer_id, product_key) VALUES (22, 18);
-- INSERT INTO Customer (customer_id, product_key) VALUES (4, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (44, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (10, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (1, 14);
-- INSERT INTO Customer (customer_id, product_key) VALUES (50, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (4, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (24, 16);
-- INSERT INTO Customer (customer_id, product_key) VALUES (15, 18);
-- INSERT INTO Customer (customer_id, product_key) VALUES (22, 14);
-- INSERT INTO Customer (customer_id, product_key) VALUES (46, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (9, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (27, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (33, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (16, 13);
-- INSERT INTO Customer (customer_id, product_key) VALUES (28, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (3, 14);
-- INSERT INTO Customer (customer_id, product_key) VALUES (30, 14);
-- INSERT INTO Customer (customer_id, product_key) VALUES (24, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (17, 14);
-- INSERT INTO Customer (customer_id, product_key) VALUES (28, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (38, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (12, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (16, 10);
-- INSERT INTO Customer (customer_id, product_key) VALUES (10, 15);
-- INSERT INTO Customer (customer_id, product_key) VALUES (10, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (20, 10);
-- INSERT INTO Customer (customer_id, product_key) VALUES (12, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (29, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (20, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (41, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (17, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (11, 20);
-- INSERT INTO Customer (customer_id, product_key) VALUES (34, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (36, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (2, 20);
-- INSERT INTO Customer (customer_id, product_key) VALUES (46, 18);
-- INSERT INTO Customer (customer_id, product_key) VALUES (24, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (50, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (44, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (42, 10);
-- INSERT INTO Customer (customer_id, product_key) VALUES (45, 18);
-- INSERT INTO Customer (customer_id, product_key) VALUES (25, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (30, 13);
-- INSERT INTO Customer (customer_id, product_key) VALUES (47, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (38, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (43, 10);
-- INSERT INTO Customer (customer_id, product_key) VALUES (22, 13);
-- INSERT INTO Customer (customer_id, product_key) VALUES (36, 18);
-- INSERT INTO Customer (customer_id, product_key) VALUES (42, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (14, 15);
-- INSERT INTO Customer (customer_id, product_key) VALUES (48, 14);
-- INSERT INTO Customer (customer_id, product_key) VALUES (42, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (19, 15);
-- INSERT INTO Customer (customer_id, product_key) VALUES (13, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (29, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (16, 20);
-- INSERT INTO Customer (customer_id, product_key) VALUES (5, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (41, 8);
-- INSERT INTO Customer (customer_id, product_key) VALUES (14, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (22, 16);
-- INSERT INTO Customer (customer_id, product_key) VALUES (37, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (5, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (50, 10);
-- INSERT INTO Customer (customer_id, product_key) VALUES (29, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (18, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (17, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (10, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (37, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (44, 8);
-- INSERT INTO Customer (customer_id, product_key) VALUES (41, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (26, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (34, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (47, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (26, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (35, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (13, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (15, 10);
-- INSERT INTO Customer (customer_id, product_key) VALUES (31, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (12, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (6, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (43, 20);
-- INSERT INTO Customer (customer_id, product_key) VALUES (23, 15);
-- INSERT INTO Customer (customer_id, product_key) VALUES (41, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (33, 16);
-- INSERT INTO Customer (customer_id, product_key) VALUES (16, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (27, 16);
-- INSERT INTO Customer (customer_id, product_key) VALUES (46, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (21, 15);
-- INSERT INTO Customer (customer_id, product_key) VALUES (7, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (30, 13);
-- INSERT INTO Customer (customer_id, product_key) VALUES (7, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (29, 8);
-- INSERT INTO Customer (customer_id, product_key) VALUES (4, 10);
-- INSERT INTO Customer (customer_id, product_key) VALUES (29, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (40, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (25, 15);
-- INSERT INTO Customer (customer_id, product_key) VALUES (4, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (18, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (42, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (40, 8);
-- INSERT INTO Customer (customer_id, product_key) VALUES (15, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (44, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (39, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (35, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (47, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (3, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (30, 10);
-- INSERT INTO Customer (customer_id, product_key) VALUES (21, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (5, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (23, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (2, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (28, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (3, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (4, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (19, 8);
-- INSERT INTO Customer (customer_id, product_key) VALUES (13, 10);
-- INSERT INTO Customer (customer_id, product_key) VALUES (16, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (20, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (2, 18);
-- INSERT INTO Customer (customer_id, product_key) VALUES (27, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (18, 14);
-- INSERT INTO Customer (customer_id, product_key) VALUES (35, 8);
-- INSERT INTO Customer (customer_id, product_key) VALUES (30, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (12, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (49, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (36, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (4, 7);
-- INSERT INTO Customer (customer_id, product_key) VALUES (49, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (41, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (11, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (35, 8);
-- INSERT INTO Customer (customer_id, product_key) VALUES (20, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (27, 9);
-- INSERT INTO Customer (customer_id, product_key) VALUES (11, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (31, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (46, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (37, 13);
-- INSERT INTO Customer (customer_id, product_key) VALUES (39, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (42, 15);
-- INSERT INTO Customer (customer_id, product_key) VALUES (25, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (36, 14);
-- INSERT INTO Customer (customer_id, product_key) VALUES (37, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (38, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (30, 11);
-- INSERT INTO Customer (customer_id, product_key) VALUES (9, 10);
-- INSERT INTO Customer (customer_id, product_key) VALUES (49, 1);
-- INSERT INTO Customer (customer_id, product_key) VALUES (35, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (42, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (12, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (27, 13);
-- INSERT INTO Customer (customer_id, product_key) VALUES (17, 3);
-- INSERT INTO Customer (customer_id, product_key) VALUES (39, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (5, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (26, 2);
-- INSERT INTO Customer (customer_id, product_key) VALUES (7, 20);
-- INSERT INTO Customer (customer_id, product_key) VALUES (12, 12);
-- INSERT INTO Customer (customer_id, product_key) VALUES (14, 19);
-- INSERT INTO Customer (customer_id, product_key) VALUES (9, 17);
-- INSERT INTO Customer (customer_id, product_key) VALUES (38, 15);
-- Remaining statements omitted for brevity --




-- 6/31, beats 5.02%
WITH product_count as (
    SELECT count(*) FROM Product
)
SELECT
    customer_id
FROM Customer c
CROSS JOIN LATERAL (
    SELECT
        COUNT(DISTINCT c2.product_key) as count
    FROM Customer c2
    WHERE
        c2.customer_id = c.customer_id
) asd
WHERE asd.count >= (SELECT * FROM product_count)
GROUP BY
    customer_id,
    asd.count

-- TODO: can be optimized a lot





