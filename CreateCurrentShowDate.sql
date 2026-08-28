USE scrummovietheater;

INSERT INTO `scrummovietheater`.`showtimes`
(
`MovieId`,
`TheaterId`,
`TimeSlot`,
`Price`,
`ShowDate`,
`AuditoriumId`)
VALUES
(
50,
1,
'13:00:00',
12.00,
NOW(),
1
);
