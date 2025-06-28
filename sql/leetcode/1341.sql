-- https://leetcode.com/problems/movie-rating/description/?envType=study-plan-v2&envId=top-sql-50

DROP TABLE Users;
Create table If Not Exists Movies
(
    movie_id int,
    title    varchar(30)
)
Create table If Not Exists Users
(
    user_id int,
    name    varchar(30)
)
Create table If Not Exists MovieRating
(
    movie_id   int,
    user_id    int,
    rating     int,
    created_at date
)
Truncate table Movies
Truncate table Users
Truncate table MovieRating
-- insert into Movies (movie_id, title) values ('1', 'Avengers')
-- insert into Movies (movie_id, title) values ('2', 'Frozen 2')
-- insert into Movies (movie_id, title) values ('3', 'Joker')
-- insert into Users (user_id, name) values ('1', 'Daniel')
-- insert into Users (user_id, name) values ('2', 'Monica')
-- insert into Users (user_id, name) values ('3', 'Maria')
-- insert into Users (user_id, name) values ('4', 'James')
-- insert into MovieRating (movie_id, user_id, rating, created_at) values ('1', '1', '3', '2020-01-12')
-- insert into MovieRating (movie_id, user_id, rating, created_at) values ('1', '2', '4', '2020-02-11')
-- insert into MovieRating (movie_id, user_id, rating, created_at) values ('1', '3', '2', '2020-02-12')
-- insert into MovieRating (movie_id, user_id, rating, created_at) values ('1', '4', '1', '2020-01-01')
-- insert into MovieRating (movie_id, user_id, rating, created_at) values ('2', '1', '5', '2020-02-17')
-- insert into MovieRating (movie_id, user_id, rating, created_at) values ('2', '2', '2', '2020-02-01')
-- insert into MovieRating (movie_id, user_id, rating, created_at) values ('2', '3', '2', '2020-03-01')
-- insert into MovieRating (movie_id, user_id, rating, created_at) values ('3', '1', '3', '2020-02-22')
-- insert into MovieRating (movie_id, user_id, rating, created_at) values ('3', '2', '4', '2020-02-25')

INSERT INTO MovieRating (movie_id, user_id, rating, created_at)
VALUES (1, 1, 3, '2020-02-18');
INSERT INTO MovieRating (movie_id, user_id, rating, created_at)
VALUES (1, 2, 4, '2020-02-23');
INSERT INTO MovieRating (movie_id, user_id, rating, created_at)
VALUES (1, 3, 1, '2020-02-01');
INSERT INTO MovieRating (movie_id, user_id, rating, created_at)
VALUES (1, 4, 3, '2020-02-19');
INSERT INTO MovieRating (movie_id, user_id, rating, created_at)
VALUES (1, 5, 2, '2020-02-19');
INSERT INTO MovieRating (movie_id, user_id, rating, created_at)
VALUES (2, 1, 2, '2020-02-21');
INSERT INTO MovieRating (movie_id, user_id, rating, created_at)
VALUES (2, 3, 1, '2020-02-23');
INSERT INTO MovieRating (movie_id, user_id, rating, created_at)
VALUES (2, 4, 4, '2020-02-04');
INSERT INTO MovieRating (movie_id, user_id, rating, created_at)
VALUES (2, 5, 1, '2020-01-26');
INSERT INTO MovieRating (movie_id, user_id, rating, created_at)
VALUES (3, 2, 5, '2020-02-03');
INSERT INTO MovieRating (movie_id, user_id, rating, created_at)
VALUES (3, 3, 2, '2020-02-22');
INSERT INTO MovieRating (movie_id, user_id, rating, created_at)
VALUES (3, 4, 2, '2020-02-24');
INSERT INTO MovieRating (movie_id, user_id, rating, created_at)
VALUES (3, 5, 1, '2020-01-29');

INSERT INTO Users (user_id, name)
VALUES (1, 'Maria');
INSERT INTO Users (user_id, name)
VALUES (2, 'Jade');
INSERT INTO Users (user_id, name)
VALUES (3, 'Claire');
INSERT INTO Users (user_id, name)
VALUES (4, 'Will');
INSERT INTO Users (user_id, name)
VALUES (5, 'Anna');

INSERT INTO Movies (movie_id, title)
VALUES (1, 'Five feets apart');
INSERT INTO Movies (movie_id, title)
VALUES (2, 'Back to the Future');
INSERT INTO Movies (movie_id, title)
VALUES (3, 'Shrek');

-- 6/28, beats 34.58%
WITH baseUsers as (SELECT Users.user_id,
                          Users.name,
                          COUNT(ratings.rating) as count
                   FROM Users
                            JOIN LATERAL (
                       SELECT *
                       FROM MovieRating
                       WHERE MovieRating.user_id = Users.user_id
                       ) ratings ON True
                   GROUP BY Users.user_id, Users.name
                   ORDER BY count DESC, Users.name
                   LIMIT 1),
     baseMovies as (SELECT Movies.movie_id,
                           Movies.title,
                           avg(ratings.rating) as rating
                    FROM Movies
                             JOIN LATERAL (
                        SELECT *
                        FROM MovieRating
                        WHERE Movies.movie_id = MovieRating.movie_id
                          AND MovieRating.created_at >= '2020-02-01'
                          AND MovieRating.created_at < '2020-03-01'
                        ) ratings ON true
                    GROUP BY Movies.movie_id, Movies.title
                    ORDER BY rating DESC, Movies.title ASC
                    LIMIT 1)
SELECT name as results
from baseUsers
UNION ALL
SELECT title as results
FROM baseMovies
