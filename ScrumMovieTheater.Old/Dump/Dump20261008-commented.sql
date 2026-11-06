CREATE DATABASE  IF NOT EXISTS `scrummovietheater` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `scrummovietheater`;
-- MySQL dump 10.13  Distrib 8.0.46, for Win64 (x86_64)
--
-- Host: localhost    Database: scrummovietheater
-- ------------------------------------------------------
-- Server version	9.7.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
-- SET @MYSQLDUMP_TEMP_LOG_BIN = @@SESSION.SQL_LOG_BIN;
-- SET @@SESSION.SQL_LOG_BIN= 0;

--
-- GTID state at the beginning of the backup 
--

-- SET @@GLOBAL.GTID_PURGED=/*!80000 '+'*/ 'c267dea0-6052-11f1-8708-9c6b00683b4b:1-349';

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20260829013239_AttemptingNewMigrationHistory','9.0.0'),('20260917205536_TheaterSeedData','9.0.0');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;

--
-- Table structure for table `auditorium`
--

DROP TABLE IF EXISTS `auditorium`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `auditorium` (
  `AuditoriumId` int NOT NULL AUTO_INCREMENT,
  `TheaterId` int NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Capacity` int NOT NULL,
  `Active` tinyint(1) NOT NULL,
  PRIMARY KEY (`AuditoriumId`),
  KEY `IX_auditorium_TheaterId` (`TheaterId`),
  CONSTRAINT `FK_auditorium_theater_TheaterId` FOREIGN KEY (`TheaterId`) REFERENCES `theater` (`TheaterId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `auditorium`
--

/*!40000 ALTER TABLE `auditorium` DISABLE KEYS */;
INSERT INTO `auditorium` VALUES (1,1,'Showroom1',200,1),(2,2,'Showroom2',200,1),(3,3,'Showroom3',200,1),(4,4,'Showroom4',200,1),(5,5,'Showroom5',200,1);
/*!40000 ALTER TABLE `auditorium` ENABLE KEYS */;

--
-- Table structure for table `bookings`
--

DROP TABLE IF EXISTS `bookings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bookings` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ShowtimeId` int NOT NULL,
  `CustomerName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Adults` int NOT NULL,
  `Kids` int NOT NULL,
  `TotalPrice` decimal(65,30) NOT NULL,
  `BookedAt` datetime(6) NOT NULL,
  `ConfirmationCode` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_bookings_ShowtimeId` (`ShowtimeId`),
  CONSTRAINT `FK_bookings_showtimes_ShowtimeId` FOREIGN KEY (`ShowtimeId`) REFERENCES `showtimes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bookings`
--

/*!40000 ALTER TABLE `bookings` DISABLE KEYS */;
/*!40000 ALTER TABLE `bookings` ENABLE KEYS */;

--
-- Table structure for table `concessioninventory`
--

DROP TABLE IF EXISTS `concessioninventory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `concessioninventory` (
  `InventoryId` int NOT NULL AUTO_INCREMENT,
  `ConcessionItemId` int NOT NULL,
  `TheaterId` int NOT NULL,
  `QuantityOnHand` int NOT NULL,
  PRIMARY KEY (`InventoryId`),
  KEY `IX_concessioninventory_ConcessionItemId` (`ConcessionItemId`),
  KEY `IX_concessioninventory_TheaterId` (`TheaterId`),
  CONSTRAINT `FK_concessioninventory_concessionitems_ConcessionItemId` FOREIGN KEY (`ConcessionItemId`) REFERENCES `concessionitems` (`ConcessionItemId`) ON DELETE CASCADE,
  CONSTRAINT `FK_concessioninventory_theater_TheaterId` FOREIGN KEY (`TheaterId`) REFERENCES `theater` (`TheaterId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `concessioninventory`
--

/*!40000 ALTER TABLE `concessioninventory` DISABLE KEYS */;
/*!40000 ALTER TABLE `concessioninventory` ENABLE KEYS */;

--
-- Table structure for table `concessionitems`
--

DROP TABLE IF EXISTS `concessionitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `concessionitems` (
  `ConcessionItemId` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` decimal(65,30) NOT NULL,
  `Category` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Active` tinyint(1) NOT NULL,
  `LowStockThreshold` int NOT NULL,
  PRIMARY KEY (`ConcessionItemId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `concessionitems`
--

/*!40000 ALTER TABLE `concessionitems` DISABLE KEYS */;
/*!40000 ALTER TABLE `concessionitems` ENABLE KEYS */;

--
-- Table structure for table `inventorytransactions`
--

DROP TABLE IF EXISTS `inventorytransactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inventorytransactions` (
  `TransactionId` int NOT NULL AUTO_INCREMENT,
  `InventoryId` int NOT NULL,
  `QuantityChange` int NOT NULL,
  `Reason` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Date` datetime(6) NOT NULL,
  `EmployeeId` int DEFAULT NULL,
  PRIMARY KEY (`TransactionId`),
  KEY `IX_inventorytransactions_InventoryId` (`InventoryId`),
  CONSTRAINT `FK_inventorytransactions_concessioninventory_InventoryId` FOREIGN KEY (`InventoryId`) REFERENCES `concessioninventory` (`InventoryId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventorytransactions`
--

/*!40000 ALTER TABLE `inventorytransactions` DISABLE KEYS */;
/*!40000 ALTER TABLE `inventorytransactions` ENABLE KEYS */;

--
-- Table structure for table `movie`
--

DROP TABLE IF EXISTS `movie`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `movie` (
  `MovieId` int NOT NULL AUTO_INCREMENT,
  `Title` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ReleaseDate` datetime(6) NOT NULL,
  `ImageUrl` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Rating` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Genre` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RuntimeMinutes` int NOT NULL,
  PRIMARY KEY (`MovieId`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `movie`
--

/*!40000 ALTER TABLE `movie` DISABLE KEYS */;
INSERT INTO `movie` VALUES (1,'Pokemon The First Movie','Scientists genetically create a new Pokémon, Mewtwo, but the results are horrific and disastrous.','1998-11-10 00:00:00.000000','/images/pokemon.jpg','G','Action',96),(2,'John Wick','John Wick is a former hitman.','2014-10-24 00:00:00.000000','/images/johnwick.jpg','R','Action-Thriller',101),(3,'Ted','John Bennett brings his teddy bear Ted to life.','2012-06-29 00:00:00.000000','/images/ted.jpg','R','Comedy',106),(4,'The Godfather','The aging patriarch of an organized crime dynasty transfers control to his son.','1972-03-24 00:00:00.000000','/images/godfather.jpg','R','Drama',175),(5,'Interstellar','Explorers travel through a wormhole to save humanity.','2014-11-07 00:00:00.000000','/images/interstellar.jpg','PG-13','Science Fiction',169),(6,'Harry Potter and the Sorcerer\'s Stone','A boy discovers he is a wizard.','2001-11-16 00:00:00.000000','/images/harrypotter1.jpg','PG','Fantasy',152),(7,'Knives Out','A detective investigates a wealthy family death.','2019-11-27 00:00:00.000000','/images/knivesout.jpg','PG-13','Thriller',130),(8,'Titanic','A love story aboard the ill-fated Titanic.','1997-12-19 00:00:00.000000','/images/titanic.jpg','PG-13','Romance',194),(9,'True Grit','A young girl hires a U.S. Marshal to find her father’s killer.','2010-12-22 00:00:00.000000','/images/truegrit.jpg','PG-13','Western',110),(10,'The Last Dance','Michael Jordan documentary series.','2020-04-19 00:00:00.000000','/images/lastdance.jpg','TV-MA','Documentary',500),(11,'Pokémon The First Movie','Mewtwo challenges the world.','1999-11-10 00:00:00.000000','/images/pokemon.jpg','G','Animation',75);
/*!40000 ALTER TABLE `movie` ENABLE KEYS */;

--
-- Table structure for table `orderitems`
--

DROP TABLE IF EXISTS `orderitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderitems` (
  `OrderItemId` int NOT NULL AUTO_INCREMENT,
  `OrderId` int NOT NULL,
  `ConcessionItemId` int NOT NULL,
  `Quantity` int NOT NULL,
  `Price` decimal(65,30) NOT NULL,
  PRIMARY KEY (`OrderItemId`),
  KEY `IX_orderitems_ConcessionItemId` (`ConcessionItemId`),
  KEY `IX_orderitems_OrderId` (`OrderId`),
  CONSTRAINT `FK_orderitems_concessionitems_ConcessionItemId` FOREIGN KEY (`ConcessionItemId`) REFERENCES `concessionitems` (`ConcessionItemId`) ON DELETE CASCADE,
  CONSTRAINT `FK_orderitems_orders_OrderId` FOREIGN KEY (`OrderId`) REFERENCES `orders` (`OrderId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderitems`
--

/*!40000 ALTER TABLE `orderitems` DISABLE KEYS */;
/*!40000 ALTER TABLE `orderitems` ENABLE KEYS */;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `OrderId` int NOT NULL AUTO_INCREMENT,
  `BookingId` int NOT NULL,
  `OrderDate` datetime(6) NOT NULL,
  PRIMARY KEY (`OrderId`),
  KEY `IX_orders_BookingId` (`BookingId`),
  CONSTRAINT `FK_orders_bookings_BookingId` FOREIGN KEY (`BookingId`) REFERENCES `bookings` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;

--
-- Table structure for table `showtimes`
--

DROP TABLE IF EXISTS `showtimes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `showtimes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `MovieId` int NOT NULL,
  `TheaterId` int NOT NULL,
  `AuditoriumId` int NOT NULL,
  `ShowDate` datetime(6) NOT NULL,
  `TimeSlot` time(6) NOT NULL,
  `Price` decimal(65,30) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_showtimes_AuditoriumId` (`AuditoriumId`),
  KEY `IX_showtimes_MovieId` (`MovieId`),
  KEY `IX_showtimes_TheaterId` (`TheaterId`),
  CONSTRAINT `FK_showtimes_auditorium_AuditoriumId` FOREIGN KEY (`AuditoriumId`) REFERENCES `auditorium` (`AuditoriumId`) ON DELETE CASCADE,
  CONSTRAINT `FK_showtimes_movie_MovieId` FOREIGN KEY (`MovieId`) REFERENCES `movie` (`MovieId`) ON DELETE CASCADE,
  CONSTRAINT `FK_showtimes_theater_TheaterId` FOREIGN KEY (`TheaterId`) REFERENCES `theater` (`TheaterId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `showtimes`
--

/*!40000 ALTER TABLE `showtimes` DISABLE KEYS */;
INSERT INTO `showtimes` VALUES (1,1,1,1,'2026-10-07 00:00:00.000000','10:00:00.000000',10.000000000000000000000000000000),(2,1,1,1,'2026-10-07 00:00:00.000000','13:00:00.000000',10.000000000000000000000000000000),(3,1,1,1,'2026-10-07 00:00:00.000000','18:00:00.000000',10.000000000000000000000000000000),(4,2,1,1,'2026-10-07 00:00:00.000000','10:00:00.000000',10.000000000000000000000000000000),(5,2,1,1,'2026-10-07 00:00:00.000000','13:00:00.000000',10.000000000000000000000000000000),(6,2,1,1,'2026-10-07 00:00:00.000000','18:00:00.000000',10.000000000000000000000000000000),(7,3,1,1,'2026-10-07 00:00:00.000000','10:00:00.000000',10.000000000000000000000000000000),(8,3,1,1,'2026-10-07 00:00:00.000000','13:00:00.000000',10.000000000000000000000000000000),(9,3,1,1,'2026-10-07 00:00:00.000000','18:00:00.000000',10.000000000000000000000000000000),(10,4,2,2,'2026-10-07 00:00:00.000000','10:00:00.000000',12.000000000000000000000000000000),(11,4,2,2,'2026-10-07 00:00:00.000000','13:00:00.000000',12.000000000000000000000000000000),(12,4,2,2,'2026-10-07 00:00:00.000000','18:00:00.000000',12.000000000000000000000000000000),(13,5,2,2,'2026-10-07 00:00:00.000000','10:00:00.000000',12.000000000000000000000000000000),(14,5,2,2,'2026-10-07 00:00:00.000000','13:00:00.000000',12.000000000000000000000000000000),(15,5,2,2,'2026-10-07 00:00:00.000000','18:00:00.000000',12.000000000000000000000000000000),(16,6,2,2,'2026-10-07 00:00:00.000000','10:00:00.000000',12.000000000000000000000000000000),(17,6,2,2,'2026-10-07 00:00:00.000000','13:00:00.000000',12.000000000000000000000000000000),(18,6,2,2,'2026-10-07 00:00:00.000000','18:00:00.000000',12.000000000000000000000000000000),(19,7,3,3,'2026-10-07 00:00:00.000000','10:00:00.000000',12.000000000000000000000000000000),(20,7,3,3,'2026-10-07 00:00:00.000000','13:00:00.000000',12.000000000000000000000000000000),(21,7,3,3,'2026-10-07 00:00:00.000000','18:00:00.000000',12.000000000000000000000000000000),(22,8,3,3,'2026-10-07 00:00:00.000000','10:00:00.000000',12.000000000000000000000000000000),(23,8,3,3,'2026-10-07 00:00:00.000000','13:00:00.000000',12.000000000000000000000000000000),(24,8,3,3,'2026-10-07 00:00:00.000000','18:00:00.000000',12.000000000000000000000000000000),(25,9,3,3,'2026-10-07 00:00:00.000000','10:00:00.000000',12.000000000000000000000000000000),(26,9,3,3,'2026-10-07 00:00:00.000000','13:00:00.000000',12.000000000000000000000000000000),(27,9,3,3,'2026-10-07 00:00:00.000000','18:00:00.000000',12.000000000000000000000000000000),(28,10,1,1,'2026-10-07 00:00:00.000000','10:00:00.000000',12.000000000000000000000000000000),(29,10,2,2,'2026-10-07 00:00:00.000000','13:00:00.000000',12.000000000000000000000000000000),(30,10,3,3,'2026-10-07 00:00:00.000000','18:00:00.000000',12.000000000000000000000000000000);
/*!40000 ALTER TABLE `showtimes` ENABLE KEYS */;

--
-- Table structure for table `theater`
--

DROP TABLE IF EXISTS `theater`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `theater` (
  `TheaterId` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Address` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Active` tinyint(1) NOT NULL,
  PRIMARY KEY (`TheaterId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `theater`
--

/*!40000 ALTER TABLE `theater` DISABLE KEYS */;
INSERT INTO `theater` VALUES (1,'Dallas','8687 N Central Expy #3000','Dallas main theater',1),(2,'Houston','11801 S Sam Houston Pkwy E','Houston cinema',1),(3,'San Antonio','849 E Commerce St Suite 800','San Antonio theater',1),(4,'Austin','2901 S Capital of Texas Hwy','Austin cinema',1),(5,'Fort Worth','5015 Trailhead Bnd Wy','Fort Worth theater',1);
/*!40000 ALTER TABLE `theater` ENABLE KEYS */;
-- SET @@SESSION.SQL_LOG_BIN = @MYSQLDUMP_TEMP_LOG_BIN;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-10-08 22:59:51
