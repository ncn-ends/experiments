-- https://leetcode.com/problems/employees-whose-manager-left-the-company/description/?envType=study-plan-v2&envId=top-sql-50

DROP Table Employees
Create table If Not Exists Employees (employee_id int, name varchar(20), manager_id int, salary int)
Truncate table Employees
-- insert into Employees (employee_id, name, manager_id, salary) values ('3', 'Mila', '9', '60301')
-- insert into Employees (employee_id, name, manager_id, salary) values ('12', 'Antonella', NULL, '31000')
-- insert into Employees (employee_id, name, manager_id, salary) values ('13', 'Emery', NULL, '67084')
-- insert into Employees (employee_id, name, manager_id, salary) values ('1', 'Kalel', '11', '21241')
-- insert into Employees (employee_id, name, manager_id, salary) values ('9', 'Mikaela', NULL, '50937')
-- insert into Employees (employee_id, name, manager_id, salary) values ('11', 'Joziah', '6', '28485')

-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (18, 'Drew', NULL, 41568);
-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (20, 'Ronan', 3, 65209);
-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (10, 'Jaxton', 15, 96667);
-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (13, 'Louie', 16, 6801);
-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (17, 'Mylah', 20, 26540);
-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (21, 'Kenia', 15, 98690);
-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (7, 'Hadley', 6, 23590);
-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (9, 'Hayden', 4, 90798);
-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (2, 'Nixon', NULL, 25560);
-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (8, 'Arthur', 11, 67027);
-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (11, 'Brycen', NULL, 42570);
-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (3, 'Noemi', NULL, 87321);
-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (14, 'Hayden', NULL, 4123);
-- INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES (19, 'Astrid', 20, 37680);

INSERT INTO Employees (employee_id, name, manager_id, salary) VALUES
(2, 'Forrest', NULL, 3417),
(3, 'Kieran', 15, 40130),
(14, 'Amiyah', 7, 80916),
(8, 'Marcos', 22, 58585),
(10, 'Jayden', 13, 19569),
(9, 'Skye', 19, 65929),
(12, 'Ensley', 18, 19575),
(7, 'Margot', NULL, 49379),
(11, 'Sadie', 20, 89210),
(22, 'Kamari', 3, 75274),
(21, 'Maison', 3, 68485),
(15, 'Fabian', 21, 68118),
(18, 'Kimora', NULL, 71537),
(5, 'Mya', 19, 70623),
(13, 'Cullen', 12, 55491),
(4, 'Jovanni', 11, 16973);



-- 6/27, beats 94.92%

SELECT e1.employee_id
FROM Employees e1
LEFT JOIN Employees e2 ON e1.manager_id = e2.employee_id
WHERE
    e1.salary < 30000
    AND e1.manager_id is not null
    AND e2.employee_id IS NULL
ORDER BY e1.employee_id