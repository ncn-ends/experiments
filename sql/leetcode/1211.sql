-- https://leetcode.com/problems/queries-quality-and-percentage/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Queries (query_name varchar(30), result varchar(50), position int, rating int)
Truncate table Queries
insert into Queries (query_name, result, position, rating) values ('Dog', 'Golden Retriever', '1', '5')
insert into Queries (query_name, result, position, rating) values ('Dog', 'German Shepherd', '2', '5')
insert into Queries (query_name, result, position, rating) values ('Dog', 'Mule', '200', '1')
insert into Queries (query_name, result, position, rating) values ('Cat', 'Shirazi', '5', '2')
insert into Queries (query_name, result, position, rating) values ('Cat', 'Siamese', '3', '3')
insert into Queries (query_name, result, position, rating) values ('Cat', 'Sphynx', '7', '4')

-- 6/24, beats 75.44%
SELECT
    query_name,
    ROUND(AVG(rating::numeric(10, 2) / position::numeric(10, 2)), 2) as quality,
    ROUND(COUNT(CASE WHEN rating < 3 THEN 1 END)::numeric(10, 2) / COUNT(*) * 100, 2) as poor_query_percentage
FROM Queries
GROUP BY query_name

/* TODO: good test for conditional count in a group by query */