-- https://leetcode.com/problems/patients-with-a-condition/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Patients (patient_id int, patient_name varchar(30), conditions varchar(100))
Truncate table Patients
insert into Patients (patient_id, patient_name, conditions) values ('1', 'Daniel', 'YFEV COUGH')
insert into Patients (patient_id, patient_name, conditions) values ('2', 'Alice', '')
insert into Patients (patient_id, patient_name, conditions) values ('3', 'Bob', 'DIAB100 MYOP')
insert into Patients (patient_id, patient_name, conditions) values ('4', 'George', 'ACNE DIAB100')
insert into Patients (patient_id, patient_name, conditions) values ('5', 'Alain', 'DIAB201')

-- 7/4, beats 39.18%
SELECT *
FROM Patients p
WHERE EXISTS (
    WITH base as (SELECT unnest(string_to_array(p.conditions, ' ')) AS ok)
    SELECT 1 FROM base WHERE ok ILIKE 'DIAB1%'
)

-- TODO: array problem with exists