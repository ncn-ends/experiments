-- https://leetcode.com/problems/count-salary-categories/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Accounts (account_id int, income int)
Truncate table Accounts
insert into Accounts (account_id, income) values ('3', '108939')
insert into Accounts (account_id, income) values ('2', '12747')
insert into Accounts (account_id, income) values ('8', '87709')
insert into Accounts (account_id, income) values ('6', '91796')

SELECT
    a.account_id,
FROM Accounts a
LEFT JOIN LATERAL (
    SELECT *
    FROM Accounts a2
    WHERE a2.income < 20000
) low_salary ON true
LEFT JOIN LATERAL (
    SELECT *
    FROM Accounts a2
    WHERE a2.income BETWEEN 20000 AND 50000
) average_salary ON true
LEFT JOIN LATERAL (
    SELECT *
    FROM Accounts a2
    WHERE
        a2.income > 50000
) high_salary ON true
GROUP BY a.account_id;

SELECT
    sum(income) over (ORDER BY income) as row_num,
    *
FROM Accounts
