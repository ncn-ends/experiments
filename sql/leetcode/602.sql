-- https://leetcode.com/problems/friend-requests-ii-who-has-the-most-friends/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists RequestAccepted (requester_id int not null, accepter_id int null, accept_date date null)
Truncate table RequestAccepted
insert into RequestAccepted (requester_id, accepter_id, accept_date) values ('1', '2', '2016/06/03')
insert into RequestAccepted (requester_id, accepter_id, accept_date) values ('1', '3', '2016/06/08')
insert into RequestAccepted (requester_id, accepter_id, accept_date) values ('2', '3', '2016/06/08')
insert into RequestAccepted (requester_id, accepter_id, accept_date) values ('3', '4', '2016/06/09')

-- 7/3, beats 23.89%
WITH distinct_ids AS (
    SELECT distinct requester_id as id
    FROM RequestAccepted
    UNION DISTINCT
    SELECT distinct accepter_id as id
    FROM RequestAccepted
)
SELECT
    di.id,
    COUNT(asd.*) as num
FROM distinct_ids di
CROSS JOIN LATERAL (
    SELECT
      *
    FROM RequestAccepted
    WHERE requester_id = di.id OR accepter_id = di.id
) asd
GROUP BY di.id
ORDER BY num DESC
LIMIT 1

-- TODO: can be optimized significantly