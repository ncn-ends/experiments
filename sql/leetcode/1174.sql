-- https://leetcode.com/problems/immediate-food-delivery-ii/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Delivery (delivery_id int, customer_id int, order_date date, customer_pref_delivery_date date)
Truncate table Delivery
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('1', '1', '2019-08-01', '2019-08-02')
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('2', '2', '2019-08-02', '2019-08-02')
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('3', '1', '2019-08-11', '2019-08-12')
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('4', '3', '2019-08-24', '2019-08-24')
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('5', '3', '2019-08-21', '2019-08-22')
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('6', '2', '2019-08-11', '2019-08-13')
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('7', '4', '2019-08-09', '2019-08-09')

-- 6/26, beats 5.03%
SELECT
    ROUND(AVG(ratio) * 100, 2) as immediate_percentage
FROM (SELECT customer_id,
             COUNT(CASE WHEN d.order_date = d.customer_pref_delivery_date THEN 1 END)::numeric(10, 2) /
             COUNT(*) as ratio
      FROM Delivery d
      WHERE order_date = (SELECT d2.order_date
                          FROM Delivery d2
                          WHERE d.customer_id = d2.customer_id
                          ORDER BY order_date ASC
                          LIMIT 1)
      GROUP BY customer_id)

/* TODO: can be optimized a lot */