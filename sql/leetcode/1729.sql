-- https://leetcode.com/problems/find-followers-count/description/?envType=study-plan-v2&envId=top-sql-50

Create table If Not Exists Followers(user_id int, follower_id int)
Truncate table Followers
-- insert into Followers (user_id, follower_id) values ('0', '1')
-- insert into Followers (user_id, follower_id) values ('1', '0')
-- insert into Followers (user_id, follower_id) values ('2', '0')
-- insert into Followers (user_id, follower_id) values ('2', '1')

INSERT INTO Followers (user_id, follower_id) VALUES (39, 13);
INSERT INTO Followers (user_id, follower_id) VALUES (20, 76);
INSERT INTO Followers (user_id, follower_id) VALUES (54, 86);
INSERT INTO Followers (user_id, follower_id) VALUES (17, 41);
INSERT INTO Followers (user_id, follower_id) VALUES (78, 27);
INSERT INTO Followers (user_id, follower_id) VALUES (56, 76);
INSERT INTO Followers (user_id, follower_id) VALUES (98, 27);
INSERT INTO Followers (user_id, follower_id) VALUES (77, 53);
INSERT INTO Followers (user_id, follower_id) VALUES (78, 44);
INSERT INTO Followers (user_id, follower_id) VALUES (82, 27);


-- SELECT
--     f1.user_id,
--    COUNT(followers.*) as followers_count
-- FROM Followers f1
-- CROSS JOIN LATERAL (
--     SELECT *
--     FROM Followers f2
--     WHERE f1.follower_id = f2.user_id
-- ) followers
-- GROUP BY f1.user_id
-- ORDER BY f1.user_id

-- 6/31, beats 54.83%
WITH base AS (SELECT *,
                     COUNT(follower_id) OVER (partition by user_id) as count
              FROM Followers f)
SELECT
    base.user_id,
    base.count as followers_count
FROM base
GROUP BY
    base.user_id,
    -- at this point the count will be the same for each user_id due to the window function, so it's ok to group by it
    base.count
ORDER BY base.user_id