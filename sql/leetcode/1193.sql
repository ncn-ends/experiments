-- https://leetcode.com/problems/monthly-transactions-i/description/?envType=study-plan-v2&envId=top-sql-50

DROP TYPE IF EXISTS approval_state CASCADE;
CREATE TYPE approval_state AS ENUM('approved', 'declined');
DROP TABLE Transactions
Create table If Not Exists Transactions (id int, country varchar(4), state approval_state, amount int, trans_date date);
Truncate table Transactions;
insert into Transactions (id, country, state, amount, trans_date) values ('121', 'US', 'approved', '1000', '2018-12-18');
insert into Transactions (id, country, state, amount, trans_date) values ('122', 'US', 'declined', '2000', '2018-12-19');
insert into Transactions (id, country, state, amount, trans_date) values ('123', 'US', 'approved', '2000', '2019-01-01');
insert into Transactions (id, country, state, amount, trans_date) values ('124', 'DE', 'approved', '2000', '2019-01-07');

/* 6/25, beats 98.50% */
SELECT
       TO_CHAR(trans_date, 'YYYY-MM') as month,
       country,
       COUNT(*) as trans_count,
       COUNT(CASE WHEN state = 'approved' THEN 1 END) as approved_count,
       SUM(amount) as trans_total_amount,
       SUM(CASE WHEN state = 'approved' THEN amount ELSE 0 END) as approved_total_amount
FROM Transactions
GROUP BY country, month