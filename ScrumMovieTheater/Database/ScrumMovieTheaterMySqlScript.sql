-- HELLO TEAM, ANDREW HERE, RUN THIS SCRIPT IN MYSQL AND DO NOT MAKE ANY CHANGES TO THE DATABASE WITHOUT US TALKING ABOUT IT. 

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
SET @MYSQLDUMP_TEMP_LOG_BIN = @@SESSION.SQL_LOG_BIN;
SET @@SESSION.SQL_LOG_BIN= 0;

--
-- GTID state at the beginning of the backup 
--

SET @@GLOBAL.GTID_PURGED=/*!80000 '+'*/ 'c267dea0-6052-11f1-8708-9c6b00683b4b:1-145';

--
-- Table structure for table `movie`
--

DROP TABLE IF EXISTS `movie`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `movie` (
  `idMovie` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(45) NOT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `Rating` varchar(45) NOT NULL,
  `Genre` varchar(45) DEFAULT NULL,
  `ReleaseDate` datetime DEFAULT NULL,
  `RuntimeMinutes` int DEFAULT NULL,
  PRIMARY KEY (`idMovie`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `movie`
--

LOCK TABLES `movie` WRITE;
/*!40000 ALTER TABLE `movie` DISABLE KEYS */;
INSERT INTO `movie` VALUES (1,'John Wick','John Wick is a former hitman.','R','Action-Thriller','2014-10-24 00:00:00',101),(2,'Ted','John Bennett, a man whose childhood wish of bringing his teddy bear to life came true, now must decide between keeping the relationship with the bear, Ted or his girlfriend, Lori.','R','Comedy','2012-06-29 00:00:00',106),(3,'The Godfather','The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.','R','Drama','1972-03-24 00:00:00',175),(4,'Interstellar','A team of explorers travel through a wormhole in space in an attempt to ensure humanity\'s survival.','PG-13','Science Fiction','2014-11-07 00:00:00',169),(5,'Harry Potter and the Sorcerer\'s Stone','An orphaned boy enrolls in a school of wizardry, where he learns the truth about himself, his family and the terrible evil that haunts the magical world.','PG','Fantasy','2001-11-16 00:00:00',152),(6,'Knives Out','A detective investigates the death of the patriarch of an eccentric, combative family.','PG-13','Thriller','2019-11-27 00:00:00',130),(7,'Titanic','A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic.','PG-13','Romance','1997-12-19 00:00:00',194),(8,'True Grit','A stubborn teenager enlists the help of a tough U.S. Marshal to track down her father\'s killer.','PG-13','Western','2010-12-22 00:00:00',110),(9,'The Last Dance','A sports documentary miniseries focusing on the career of Michael Jordan, with particular focus on his last season with the Chicago Bulls.','TV-MA','Documentary','2020-04-19 00:00:00',500),(10,'Pokémon The First Movie','Scientists genetically create a powerful clone Pokémon named Mewtwo, who seeks revenge against humanity and challenges elite trainers to a battle.','G','Animation','1999-11-10 00:00:00',75);
/*!40000 ALTER TABLE `movie` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `showroom`
--

DROP TABLE IF EXISTS `showroom`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `showroom` (
  `idShowroom` int NOT NULL AUTO_INCREMENT,
  `TimeSlot1` time DEFAULT NULL,
  `TimeSlot2` time DEFAULT NULL,
  `TimeSlot3` time DEFAULT NULL,
  `TimeSlot4` time DEFAULT NULL,
  `TimeSlot5` time DEFAULT NULL,
  `Capacity` int NOT NULL,
  `FK_Movie` int DEFAULT NULL,
  PRIMARY KEY (`idShowroom`),
  KEY `FK_Movie_idx` (`FK_Movie`),
  CONSTRAINT `FK_Showroom` FOREIGN KEY (`FK_Movie`) REFERENCES `movie` (`idMovie`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `showroom`
--

LOCK TABLES `showroom` WRITE;
/*!40000 ALTER TABLE `showroom` DISABLE KEYS */;
INSERT INTO `showroom` VALUES (1,'10:00:00','13:00:00','16:30:00','19:00:00','22:00:00',100,1),(2,'02:30:00','12:00:00','02:30:00','05:00:00','08:00:00',100,2),(3,'11:00:00','14:00:00','16:00:00','19:00:00','22:00:00',250,3),(4,'10:30:00','13:30:00','13:30:00','17:30:00','19:30:00',250,4),(5,'12:00:00','12:00:00','15:00:00','15:00:00','19:00:00',100,5),(6,'16:00:00','18:10:00','20:20:00','22:30:00','01:00:00',200,1),(7,'13:45:00','15:00:00','19:00:00','21:00:00','02:00:00',200,2),(8,'13:30:00','16:15:00','19:30:00','22:00:00','01:10:00',200,3),(9,'10:00:00','13:00:00','16:00:00','18:45:00','21:30:00',200,4),(10,'13:00:00','15:45:00','18:00:00','22:00:00','01:30:00',200,5),(11,'10:00:00','13:00:00','16:30:00','19:00:00','22:00:00',300,6),(12,'02:30:00','12:00:00','02:30:00','05:00:00','08:00:00',300,7),(13,'11:00:00','14:00:00','16:00:00','19:00:00','22:00:00',300,8),(14,'10:30:00','13:30:00','13:30:00','17:30:00','19:30:00',300,9),(15,'12:00:00','12:00:00','15:00:00','15:00:00','19:00:00',300,10),(16,'10:00:00','13:00:00','16:30:00','19:00:00','22:00:00',100,6),(17,'02:30:00','12:00:00','02:30:00','05:00:00','08:00:00',100,7),(18,'11:00:00','14:00:00','16:00:00','19:00:00','22:00:00',250,8),(19,'10:30:00','13:30:00','13:30:00','17:30:00','19:30:00',250,9),(20,'12:00:00','12:00:00','15:00:00','15:00:00','19:00:00',100,10),(21,'16:00:00','18:10:00','20:20:00','22:30:00','01:00:00',200,1),(22,'13:45:00','15:00:00','19:00:00','21:00:00','02:00:00',200,10),(23,'13:30:00','16:15:00','19:30:00','22:00:00','01:10:00',200,2),(24,'10:00:00','13:00:00','16:00:00','18:45:00','21:30:00',200,9),(25,'13:00:00','15:45:00','18:00:00','22:00:00','01:30:00',200,5);
/*!40000 ALTER TABLE `showroom` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `theater`
--

DROP TABLE IF EXISTS `theater`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `theater` (
  `idTheater` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Address` varchar(45) DEFAULT NULL,
  `Description` varchar(45) DEFAULT NULL,
  `FK_Showroom1` int DEFAULT NULL,
  `FK_Showroom2` int DEFAULT NULL,
  `FK_Showroom3` int DEFAULT NULL,
  `FK_Showroom4` int DEFAULT NULL,
  `FK_Showroom5` int DEFAULT NULL,
  PRIMARY KEY (`idTheater`),
  KEY `FK_Showroom1_Theater_idx` (`FK_Showroom1`),
  KEY `FK_Shoroom_Theater_2_idx` (`FK_Showroom2`),
  KEY `FK_Showroom_Theater_3_idx` (`FK_Showroom3`),
  KEY `FK_Showroom_Theater_5_idx` (`FK_Showroom5`),
  KEY `FK_Showroom_Theater_4_idx` (`FK_Showroom4`),
  CONSTRAINT `FK_Showroom_Theater_1` FOREIGN KEY (`FK_Showroom1`) REFERENCES `showroom` (`idShowroom`),
  CONSTRAINT `FK_Showroom_Theater_2` FOREIGN KEY (`FK_Showroom2`) REFERENCES `showroom` (`idShowroom`),
  CONSTRAINT `FK_Showroom_Theater_3` FOREIGN KEY (`FK_Showroom3`) REFERENCES `showroom` (`idShowroom`),
  CONSTRAINT `FK_Showroom_Theater_4` FOREIGN KEY (`FK_Showroom4`) REFERENCES `showroom` (`idShowroom`),
  CONSTRAINT `FK_Showroom_Theater_5` FOREIGN KEY (`FK_Showroom5`) REFERENCES `showroom` (`idShowroom`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `theater`
--

LOCK TABLES `theater` WRITE;
/*!40000 ALTER TABLE `theater` DISABLE KEYS */;
INSERT INTO `theater` VALUES (1,'Dallas','8687 N Central Expy #3000,','Dallas Theater',1,2,3,4,5),(2,'Houston','11801 S Sam Houston Pkwy E','Houston Theater',6,7,8,9,10),(3,'San Antonio','849 E Commerce St Suite 800','San Antonio',11,12,13,14,15),(4,'Austin','2901 S Capital of Texas Hwy','Austin',16,17,18,19,20),(5,'Fort Worth','5015 Trailhead Bnd Wy','Fort Worth',21,22,23,24,25);
/*!40000 ALTER TABLE `theater` ENABLE KEYS */;
UNLOCK TABLES;
SET @@SESSION.SQL_LOG_BIN = @MYSQLDUMP_TEMP_LOG_BIN;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-06-08 14:19:34
