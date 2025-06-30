-- https://leetcode.com/problems/find-followers-count/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Followers(user_id int, follower_id int)
Truncate table Followers
insert into Followers (user_id, follower_id) values ('0', '1')
insert into Followers (user_id, follower_id) values ('1', '0')
insert into Followers (user_id, follower_id) values ('2', '0')
insert into Followers (user_id, follower_id) values ('2', '1')


SELECT
    f1.user_id,
   COUNT(followers.*) as followers_count
FROM Followers f1
CROSS JOIN LATERAL (
    SELECT *
    FROM Followers f2
    WHERE f1.follower_id = f2.user_id
) followers
GROUP BY f1.user_id
ORDER BY f1.user_id