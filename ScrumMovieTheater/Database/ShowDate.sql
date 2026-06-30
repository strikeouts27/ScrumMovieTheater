USE scrummovietheater;
UPDATE showtimes
SET ShowDate = '2026-06-20'
WHERE MovieId IN (1,2);
UPDATE showtimes SET ShowDate = '2026-06-21' WHERE MovieId IN (3,4);
UPDATE showtimes SET ShowDate = '2026-06-22' WHERE MovieId IN (5,6);
UPDATE showtimes SET ShowDate = '2026-06-23' WHERE MovieId IN (7,8);
UPDATE showtimes SET ShowDate = '2026-06-24' WHERE MovieId IN (9,10);

SELECT * FROM showtimes;