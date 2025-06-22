Create table If Not Exists Students (student_id int, student_name varchar(20))
Create table If Not Exists Subjects (subject_name varchar(20))
Create table If Not Exists Examinations (student_id int, subject_name varchar(20))
Truncate table Students
insert into Students (student_id, student_name) values ('1', 'Alice')
insert into Students (student_id, student_name) values ('2', 'Bob')
insert into Students (student_id, student_name) values ('13', 'John')
insert into Students (student_id, student_name) values ('6', 'Alex')
Truncate table Subjects
insert into Subjects (subject_name) values ('Math')
insert into Subjects (subject_name) values ('Physics')
insert into Subjects (subject_name) values ('Programming')
Truncate table Examinations
insert into Examinations (student_id, subject_name) values ('1', 'Math')
insert into Examinations (student_id, subject_name) values ('1', 'Physics')
insert into Examinations (student_id, subject_name) values ('1', 'Programming')
insert into Examinations (student_id, subject_name) values ('2', 'Programming')
insert into Examinations (student_id, subject_name) values ('1', 'Physics')
insert into Examinations (student_id, subject_name) values ('1', 'Math')
insert into Examinations (student_id, subject_name) values ('13', 'Math')
insert into Examinations (student_id, subject_name) values ('13', 'Programming')
insert into Examinations (student_id, subject_name) values ('13', 'Physics')
insert into Examinations (student_id, subject_name) values ('2', 'Math')
insert into Examinations (student_id, subject_name) values ('1', 'Math')

-- students
SELECT *
FROM Students

-- subjects
SELECT *
FROM Subjects

-- examinations
SELECT *
FROM Examinations

-- SELECT s.student_name, COUNT(e.subject_name), e.subject_name
-- FROM Students s
-- JOIN Examinations e on s.student_id = e.student_id
-- GROUP BY s.student_name, s.student_id, e.subject_name

-- SELECT e.student_id, s.student_name, sub.subject_name, COUNT(*)
-- FROM Subjects sub
-- JOIN Examinations E on sub.subject_name = E.subject_name
-- JOIN public.students s on E.student_id = s.student_id
-- GROUP BY sub.subject_name, e.student_id, s.student_name
-- ORDER BY e.student_id, sub.subject_name

-- 6/21, beats 96.31%
SELECT s.student_id, s.student_name, sub.subject_name, COUNT(E.student_id) as attended_exams
FROM Subjects sub
CROSS JOIN Students s
LEFT JOIN Examinations E on s.student_id = E.student_id AND E.subject_name = sub.subject_name
GROUP BY s.student_id, s.student_name, sub.subject_name
ORDER BY s.student_id, sub.subject_name

/* TODO: make sure you remmeber cross joins */