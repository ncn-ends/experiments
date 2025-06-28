-- https://leetcode.com/problems/last-person-to-fit-in-the-bus/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Queue (person_id int, person_name varchar(30), weight int, turn int)
Truncate table Queue
-- insert into Queue (person_id, person_name, weight, turn) values ('5', 'Alice', '250', '1')
-- insert into Queue (person_id, person_name, weight, turn) values ('4', 'Bob', '175', '5')
-- insert into Queue (person_id, person_name, weight, turn) values ('3', 'Alex', '350', '2')
-- insert into Queue (person_id, person_name, weight, turn) values ('6', 'John Cena', '400', '3')
-- insert into Queue (person_id, person_name, weight, turn) values ('1', 'Winston', '500', '6')
-- insert into Queue (person_id, person_name, weight, turn) values ('2', 'Marie', '200', '4')

INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (6, 'luffy', 5, 4);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (14, 'ace', 16, 10);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (15, 'sabo', 9, 14);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (7, 'zoro', 11, 3);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (9, 'sanji', 10, 7);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (4, 'nami', 3, 11);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (3, 'ussop', 16, 15);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (5, 'chopper', 2, 2);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (12, 'brooke', 11, 5);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (2, 'robin', 19, 12);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (13, 'franky', 17, 1);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (11, 'shanks', 15, 6);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (1, 'kaido', 22, 8);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (8, 'edward', 2, 9);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (16, 'linlin', 6, 13);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (17, 'teach', 7, 16);
INSERT INTO Queue (person_id, person_name, weight, turn) VALUES (10, 'dragon', 6, 17);


-- SELECT
-- --     q.person_name
--     q.person_id, q.turn, q.person_name, sum(a.weight)
-- FROM Queue q
-- LEFT JOIN LATERAL (
--     SELECT
--         q2.person_id,
--         q2.person_name,
--         q2.weight
--     FROM Queue q2
--     WHERE q.turn <= q2.turn
-- ) a ON true
-- GROUP BY q.person_id, q.turn, q.person_name
-- HAVING sum(a.weight) < 1000
-- ORDER BY q.turn DESC
-- LIMIT 1

-- 6/28, beats 90.49%
SELECT
   person_name
FROM (SELECT
    *,
    SUM(weight) OVER (ORDER BY turn ASC) as total
FROM Queue) asd
WHERE asd.total <= 1000
ORDER BY turn DESC
LIMIT 1