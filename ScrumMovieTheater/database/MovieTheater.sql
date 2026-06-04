DROP DATABASE IF EXISTS MovieTheaterDB;
CREATE DATABASE MovieTheaterDB;
USE MovieTheaterDB;
CREATE TABLE Movies (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    ImageUrl VARCHAR(255) NOT NULL
);

INSERT INTO Movies (Title, ImageUrl)
VALUES
('Superman', '/Images/titleSuperman.png'),
('Batman', '/Images/titleBatman.png'),
('Avatar 3', '/Images/titleAvatar.png');

CREATE TABLE Schedule (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    TheaterName VARCHAR(100),
    MovieTitle VARCHAR(100),
    Showtime DATETIME
);

INSERT INTO Schedule (TheaterName, MovieTitle, Showtime)
VALUES 
('Dallas', 'Avatar 3', NOW()),
('Houston', 'Superman', DATE_ADD(NOW(), INTERVAL 1 HOUR)),
('Carrollton', 'Batman', DATE_ADD(NOW(), INTERVAL 2 HOUR));


CREATE TABLE Bookings (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ScheduleId INT NOT NULL,
    CustomerName VARCHAR(100),
    Adults INT DEFAULT 0,
    Kids INT DEFAULT 0,
    TotalPrice DECIMAL(10,2),
    BookedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);