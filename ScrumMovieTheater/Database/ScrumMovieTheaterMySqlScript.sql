CREATE DATABASE IF NOT EXISTS ScrumMovieTheater;
USE ScrumMovieTheater;

--
-- Table structure for table `movie`
--

CREATE TABLE `movie` (
  `MovieId` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(45) NOT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `Rating` varchar(45) NOT NULL,
  `Genre` varchar(45) DEFAULT NULL,
  `ReleaseDate` datetime DEFAULT NULL,
  `RuntimeMinutes` int DEFAULT NULL,
  `ImageUrl` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`MovieId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


INSERT INTO movie 
(Title, Description, Rating, Genre, ReleaseDate, RuntimeMinutes, ImageUrl)
VALUES
('John Wick', 'John Wick is a former hitman.', 'R', 'Action-Thriller', '2014-10-24 00:00:00', 101, '/images/johnwick.jpg'),

('Ted', 'John Bennett brings his teddy bear Ted to life.', 'R', 'Comedy', '2012-06-29 00:00:00', 106, '/images/ted.jpg'),

('The Godfather', 'The aging patriarch of an organized crime dynasty transfers control to his son.', 'R', 'Drama', '1972-03-24 00:00:00', 175, '/images/godfather.jpg'),

('Interstellar', 'Explorers travel through a wormhole to save humanity.', 'PG-13', 'Science Fiction', '2014-11-07 00:00:00', 169, '/images/interstellar.jpg'),

('Harry Potter and the Sorcerer''s Stone', 'A boy discovers he is a wizard.', 'PG', 'Fantasy', '2001-11-16 00:00:00', 152, '/images/harrypotter1.jpg'),

('Knives Out', 'A detective investigates a wealthy family death.', 'PG-13', 'Thriller', '2019-11-27 00:00:00', 130, '/images/knivesout.jpg'),

('Titanic', 'A love story aboard the ill-fated Titanic.', 'PG-13', 'Romance', '1997-12-19 00:00:00', 194, '/images/titanic.jpg'),

('True Grit', 'A young girl hires a U.S. Marshal to find her father’s killer.', 'PG-13', 'Western', '2010-12-22 00:00:00', 110, '/images/truegrit.jpg'),

('The Last Dance', 'Michael Jordan documentary series.', 'TV-MA', 'Documentary', '2020-04-19 00:00:00', 500, '/images/lastdance.jpg'),

('Pokémon The First Movie', 'Mewtwo challenges the world.', 'G', 'Animation', '1999-11-10 00:00:00', 75, '/images/pokemon.jpg');


CREATE TABLE showtimes (
    Id INT NOT NULL AUTO_INCREMENT,
    MovieId INT NOT NULL,
    TheaterId INT NOT NULL,
    TimeSlot TIME NOT NULL,
    Price INT NOT NULL,

    PRIMARY KEY (Id),

    FOREIGN KEY (`MovieId`) REFERENCES `movie` (`MovieId`),
    FOREIGN KEY (`TheaterId`) REFERENCES `theater` (`TheaterId`)
)ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO Showtimes (MovieId, TheaterId, TimeSlot, Price)
VALUES
-- Movie 1
(1, 1, '10:00:00', 100),
(1, 1, '13:00:00', 100),
(1, 1, '18:00:00', 100),

-- Movie 2
(2, 1, '10:00:00', 100),
(2, 1, '13:00:00', 100),
(2, 1, '18:00:00', 100),

-- Movie 3
(3, 1, '10:00:00', 100),
(3, 1, '13:00:00', 100),
(3, 1, '18:00:00', 100),

-- Movie 4
(4, 2, '10:00:00', 120),
(4, 2, '13:00:00', 120),
(4, 2, '18:00:00', 120),

-- Movie 5
(5, 2, '10:00:00', 120),
(5, 2, '13:00:00', 120),
(5, 2, '18:00:00', 120),

-- Movie 6
(6, 2, '10:00:00', 120),
(6, 2, '13:00:00', 120),
(6, 2, '18:00:00', 120),

-- Movie 7
(7, 3, '10:00:00', 150),
(7, 3, '13:00:00', 150),
(7, 3, '18:00:00', 150),

-- Movie 8
(8, 3, '10:00:00', 150),
(8, 3, '13:00:00', 150),
(8, 3, '18:00:00', 150),

-- Movie 9
(9, 3, '10:00:00', 150),
(9, 3, '13:00:00', 150),
(9, 3, '18:00:00', 150),

-- Movie 10
(10, 1, '10:00:00', 100),
(10, 2, '13:00:00', 120),
(10, 3, '18:00:00', 150);



--
-- Table structure for table `theater`
--

CREATE TABLE `theater` (
  `TheaterId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Address` varchar(45) DEFAULT NULL,
  `Description` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`TheaterId`),
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;



INSERT INTO theater (TheaterId, Name, Address, Description)
VALUES
(1, 'Dallas', '8687 N Central Expy #3000', 'Dallas main theater'),
(2, 'Houston', '11801 S Sam Houston Pkwy E', 'Houston cinema'),
(3, 'San Antonio', '849 E Commerce St Suite 800', 'San Antonio theater'),
(4, 'Austin', '2901 S Capital of Texas Hwy', 'Austin cinema'),
(5, 'Fort Worth', '5015 Trailhead Bnd Wy', 'Fort Worth theater');


