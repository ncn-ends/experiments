-- https://leetcode.com/problems/count-salary-categories/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Accounts
(
    account_id int,
    income     int
)
Truncate table Accounts
insert into Accounts (account_id, income)
values ('3', '108939')
insert into Accounts (account_id, income)
values ('2', '12747')
insert into Accounts (account_id, income)
values ('8', '87709')
insert into Accounts (account_id, income)
values ('6', '91796')

-- SELECT
--     a.account_id,
-- FROM Accounts a
-- LEFT JOIN LATERAL (
--     SELECT *
--     FROM Accounts a2
--     WHERE a2.income < 20000
-- ) low_salary ON true
-- LEFT JOIN LATERAL (
--     SELECT *
--     FROM Accounts a2
--     WHERE a2.income BETWEEN 20000 AND 50000
-- ) average_salary ON true
-- LEFT JOIN LATERAL (
--     SELECT *
--     FROM Accounts a2
--     WHERE
--         a2.income > 50000
-- ) high_salary ON true
-- GROUP BY a.account_id;
--
-- SELECT
--     sum(income) over (ORDER BY income) as row_num,
--     *
-- FROM Accounts

-- 6/31, beats 51.46%
WITH salary_labels AS (SELECT 'Low Salary' as label
                       UNION
                       SELECT 'Average Salary' as label
                       UNION
                       SELECT 'High Salary' as label),
     salaries AS (SELECT 'Low Salary' as label
                  FROM accounts a2
                  WHERE a2.income < 20000

                  UNION ALL

                  SELECT 'Average Salary' as label
                  FROM accounts a2
                  WHERE a2.income BETWEEN 20000 AND 50000

                  UNION ALL

                  SELECT 'High Salary' as label
                  FROM accounts a2
                  WHERE a2.income > 50000)
SELECT
    salary_labels.label as category,
   COUNT(salaries.*) as accounts_count
FROM salary_labels
LEFT JOIN LATERAL (
    SELECT *
    FROM salaries
    WHERE salaries.label = salary_labels.label
) salaries ON true
GROUP BY salary_labels.label
