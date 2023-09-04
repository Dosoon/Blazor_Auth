CREATE DATABASE  IF NOT EXISTS `accountdb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `accountdb`;
-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: accountdb
-- ------------------------------------------------------
-- Server version	8.0.33

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

--
-- Table structure for table `account`
--

DROP TABLE IF EXISTS `account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `account` (
  `AccountId` bigint NOT NULL COMMENT '계정번호',
  `Email` varchar(50) NOT NULL COMMENT '이메일',
  `SaltValue` varchar(100) NOT NULL COMMENT '암호화 값',
  `HashedPassword` varchar(100) NOT NULL COMMENT '해싱된 비밀번호',
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '생성 날짜',
  PRIMARY KEY (`AccountId`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='계정 정보';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account`
--

LOCK TABLES `account` WRITE;
/*!40000 ALTER TABLE `account` DISABLE KEYS */;
INSERT INTO `account` VALUES (1136170162282561536,'dosoon@com2us.com','fwwxnzwzk3qvgiwhllv6pyuur53jfty5lq085um3j7olwgznpvqxhcmixk9nanw2','53bd2fdfe6f6fad16c3047408e4dd56e9f5477c08baad4b4d287dd8ba3e24cde','2023-08-02 14:34:35'),(1136172814928183296,'nana@com2us.com','jgee2iikoyngkchbiig6bk4pi5mj7e9dd02oyx24iwwu1y75zh1aiaiqmdicrtvd','49fdea6483730d8bb55626ce29e74950560b7ef60b37c4ccbf10a30162d40cd7','2023-08-02 14:45:08'),(1136191576284856320,'yrsk@com2us.com','7ih7c9pmxj645c7rzc9vsxbutr2v271e5iki2g1lro1l50z9qy6pq7bhiiqo73bg','e7d84621109aaf6ef3ccc7eaf7ed63c8e03a2cd222f50276f070d4ddbe231c49','2023-08-02 15:59:41'),(1136191680685277184,'central_server@com2us.com','0trz94z93zx0p0p1tiejrvup28x0s3fwwgx77fa5qw8sezc8agdd3x591k9kwhwt','50d8f5f6f7637223132028b16828c98b6c3f9ca687dad42f1bfdc14a5f946ee6','2023-08-02 16:00:06'),(1136191711043649536,'030_636@naver.com','cve0qn06n37qgdnr2s9num3vqw93nrg4khw5o0mjrnacb55qhg4569xn5ijhudfo','a2e429ea4bff42e5b22bb6fad3e9ba309b359fdf9596265e30a8f7883abfd612','2023-08-02 16:00:13'),(1136191754861543424,'cocacola@gmail.com','f7wmjl0dp5d939r692ueh2y44c7czuavxfsyvq8fa2j4zmvhd7gl79a0r9i386or','45af7d0a8ec70aba80c4b60df7734aed385e768be6737c2646e2154912aad39c','2023-08-02 16:00:23'),(1136191794178949120,'com2usdev@gmail.com','cvafytgzcrh6ldinuc8ups413y35lhg32mopypriqo35uunx5ot38dpcfdgsskiy','cd4f8b34b57d924089617a7e8f99778a70c7c7e58605683627af138570f16fd8','2023-08-02 16:00:33'),(1136191822339506176,'minigame@paradise.com','7c5i8xfprigtvke5hxtr75wrudr8hqqxrkke8ee3ylyhen85h0sn594snolgvgqq','67beceb1b7665e9983bfff702475638fca7ddfdee5485905e9989ca1cadc71e6','2023-08-02 16:00:39'),(1136191846091849728,'houserabbit@com2us.com','u51hqi02wy3gab50kwh7gdrpuqrikcynv5og0vukfamgry7pgkpmfbp6eb3nouau','e07a558858c0b558ca40d7fe7c1705e8896ab0e6b442f5d607d0c585e18fc4f3','2023-08-02 16:00:45'),(1136191866178371584,'rabbit@naver.com','hd6mk21j87v1v5juis0ejfcayap3izl4pjp0hm98tuks625hulcyd9i294ar2q9l','587ef8baa323fd81e92b38c589a1a879c0f5b34f0cd2dd6c5d7c27297d1061a1','2023-08-02 16:00:50'),(1136191891243532288,'managing@com2us.com','efh0h99fjr3x9y8uz9p5tbih5qr275nm869bsyk1b790asb3bbvzhgqbe1a5vynq','2fb75c19882f558e8c53428f76157ca685aee5abedab58a96c55391f37568bff','2023-08-02 16:00:56'),(1136191965637902336,'leadingrnd@daum.net','8unm9sxznivfl788aoy9v0uek91pjl9lwvfat921uceafl62r1qz8ef8mdgm55ae','23125729a4f51df2d243d410f046e4eb17a1cedd7f274d7a25d49c2697b74cba','2023-08-02 16:01:14'),(1136191995685896192,'ipad@gmail.com','ckgra99rxg0d9126ox5mw361n504r0qk8174d9hapqgdl92sowbxh0guqrh035lg','b163dd935646de499026bf3d75e8ad56fda68c0426969e37e8de03271871bc51','2023-08-02 16:01:21'),(1136192013083869184,'ipod@gmail.com','k99qrstkoxcdwli469g7anaklit40dh4lprxyk8eaul2q6fs1c7b2mrpe1wcmaz3','0cf0f9378aca43cd2573462bec62a0b96111a8e015c9f45c485aec22cd2d1ecc','2023-08-02 16:01:25'),(1136192040212627456,'iphone@naver.com','d91osxwlg8nltw7q8aa6xj5k2xc98yfy9valn145ushyt9qsvtn2fbqyddkw88it','d7f36172bc01c02cae60324890dab0584038cc80743b78f339b765be2c5ed121','2023-08-02 16:01:31'),(1136192055454728192,'ssafy@ssafy.com','3rdt1b0asd17m8nfdm175srad7dt29evibrtz5ul6qiqbxabtyrr8s20xadjfo2o','96d5db4afae495c639e5089fa0fdf37f1a17cfa33f09bf65c5301ab306b851d5','2023-08-02 16:01:35'),(1136192082214387712,'ssafy9th@ssafy.com','v52memfjfxrbyv30q4dxuh375ltjc0brtmml7vcwy2nz1vfb9qekeaisgv1ikg4n','e9ea2bf71d11ad121eb23f6282123c61d4d19aecc0ab9301b5617faf4601d429','2023-08-02 16:01:41'),(1136192110945370112,'summoners@war.com','n5mhavi2ambtw6t9jayvh2tod0hil78t31vdvs5dxts706bkx33i9w1zc14p6o69','7aa0cfd1041bc2688eb3937f66f0260ba259f5591e17eea6b235c1ac4e82965e','2023-08-02 16:01:48'),(1136192152410259456,'chronicle@naver.com','35j826qo1o1kris4qdkyk68xjbba54t80vthmexl3ivx6yl6188oztzdpph7n8gw','9049ed7274fd327da169d9011c755458b45d08fcdbab8e891065081e6413700d','2023-08-02 16:01:58'),(1136192169887924224,'mmorpg@gmail.com','rdyhrqcj2t44jov4litq8vnpkklh2j1l5rynrf8cbgln3popi1xbr62e858bkeev','9a835dad108b0f8c41ee57f1f50812817816670532b710e5a12d5195ba265bef','2023-08-02 16:02:02'),(1136192259411148800,'cadena@gmail.com','vzw7qcvld34h7oehxi4u1981m9fit7jfckkflgojxeefdafu06afw780jn6m7w0r','ab1d6778c908cb6af441d8700a6569efc1b9d742d87fadb2651180662af079f5','2023-08-02 16:02:24'),(1136192276054147072,'nova@gmail.com','4pdh7e5em5ob81ok23yufivyns7edtunrtvussyla4y72rm1eowr8n6s4adaf0af','c2978cbbaa4a6a139055529c073af476924cb2cdda91462cd505adf65feae120','2023-08-02 16:02:28'),(1136192293536006144,'angelic@buster.com','14yug5rr130tmh0ae3s1ibqs2clhh10h5la2cfnk73a0y4arm0cqyshm5r6crzbr','ce73699f6d0c37a54beeb17a2c3ba2f7619e43b6b20d70290793a914ba4feb8f','2023-08-02 16:02:32'),(1136192330370383872,'asp@dot.net','39pd9pbaukq1ifaj9lj1cio2crcbvwcdmrrfpzoofqbqom7ck2kw5pvbioleyw7n','3e5cc885629a8f21f7be9f9e4340226948f2b2455045876d74bc1938a8ea078d','2023-08-02 16:02:40'),(1136192382392336384,'gasan@digital.org','nh19gvdw7nsg7lp0opz61qpzla6p6hoyfi3e97v83cqqzvgkqfockh7lsapbttvf','5a66362e042b0cb717e3907ba1c541b47a23bc2e33f644486ef8801b8ab3c7d3','2023-08-02 16:02:53');
/*!40000 ALTER TABLE `account` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-09-04 18:57:42
CREATE DATABASE  IF NOT EXISTS `gamedb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `gamedb`;
-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: gamedb
-- ------------------------------------------------------
-- Server version	8.0.33

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

--
-- Table structure for table `inappreceipt`
--

DROP TABLE IF EXISTS `inappreceipt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inappreceipt` (
  `PurchaseId` bigint NOT NULL COMMENT '결제 항목 고유 ID',
  `UserId` bigint NOT NULL COMMENT '계정번호',
  `ProductCode` bigint NOT NULL COMMENT '상품 번호',
  `PurchasedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '결제 일시',
  PRIMARY KEY (`PurchaseId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='인앱 결제 정보';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inappreceipt`
--

LOCK TABLES `inappreceipt` WRITE;
/*!40000 ALTER TABLE `inappreceipt` DISABLE KEYS */;
/*!40000 ALTER TABLE `inappreceipt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mail_data`
--

DROP TABLE IF EXISTS `mail_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mail_data` (
  `MailId` bigint NOT NULL COMMENT '우편 고유 ID',
  `UserId` bigint NOT NULL COMMENT '계정번호',
  `SenderId` bigint NOT NULL COMMENT '발신자 ID',
  `Title` varchar(100) NOT NULL COMMENT '제목',
  `Content` varchar(2000) NOT NULL COMMENT '내용',
  `IsRead` tinyint(1) NOT NULL DEFAULT '0' COMMENT '읽음 여부',
  `hasItem` tinyint(1) NOT NULL COMMENT '아이템 포함 여부',
  `IsDeleted` tinyint(1) NOT NULL DEFAULT '0' COMMENT '메일 삭제 여부',
  `ObtainedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '메일 수신 일시',
  `ExpiredAt` datetime NOT NULL COMMENT '메일 만료 일시',
  PRIMARY KEY (`MailId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='우편 정보';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mail_data`
--

LOCK TABLES `mail_data` WRITE;
/*!40000 ALTER TABLE `mail_data` DISABLE KEYS */;
/*!40000 ALTER TABLE `mail_data` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mail_item`
--

DROP TABLE IF EXISTS `mail_item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mail_item` (
  `ItemId` bigint NOT NULL COMMENT '아이템 고유 ID',
  `MailId` bigint NOT NULL COMMENT '우편 고유 ID',
  `ItemCode` bigint DEFAULT NULL COMMENT '아이템 코드',
  `ItemCount` int DEFAULT NULL COMMENT '아이템 개수',
  `IsReceived` tinyint(1) NOT NULL DEFAULT '0' COMMENT '아이템 수령 여부',
  PRIMARY KEY (`ItemId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='우편 아이템 정보';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mail_item`
--

LOCK TABLES `mail_item` WRITE;
/*!40000 ALTER TABLE `mail_item` DISABLE KEYS */;
/*!40000 ALTER TABLE `mail_item` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_attendance`
--

DROP TABLE IF EXISTS `user_attendance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_attendance` (
  `UserId` bigint NOT NULL COMMENT '유저번호',
  `AttendanceCount` tinyint NOT NULL DEFAULT '0' COMMENT '출석 횟수',
  `LastAttendance` datetime NOT NULL DEFAULT '0001-01-01 00:00:00' COMMENT '마지막 출석 일시',
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='유저 출석 정보';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_attendance`
--

LOCK TABLES `user_attendance` WRITE;
/*!40000 ALTER TABLE `user_attendance` DISABLE KEYS */;
INSERT INTO `user_attendance` VALUES (1000,0,'0001-01-01 00:00:00'),(1001,0,'0001-01-01 00:00:00'),(1002,0,'0001-01-01 00:00:00'),(1003,0,'0001-01-01 00:00:00'),(1004,0,'0001-01-01 00:00:00'),(1005,0,'0001-01-01 00:00:00'),(1006,0,'0001-01-01 00:00:00'),(1007,0,'0001-01-01 00:00:00'),(1008,0,'0001-01-01 00:00:00'),(1009,0,'0001-01-01 00:00:00'),(1010,0,'0001-01-01 00:00:00'),(1011,0,'0001-01-01 00:00:00'),(1012,0,'0001-01-01 00:00:00'),(1013,0,'0001-01-01 00:00:00'),(1014,0,'0001-01-01 00:00:00'),(1015,0,'0001-01-01 00:00:00'),(1016,0,'0001-01-01 00:00:00'),(1017,0,'0001-01-01 00:00:00'),(1018,0,'0001-01-01 00:00:00'),(1019,0,'0001-01-01 00:00:00'),(1020,0,'0001-01-01 00:00:00'),(1021,0,'0001-01-01 00:00:00'),(1022,0,'0001-01-01 00:00:00'),(1023,0,'0001-01-01 00:00:00'),(1024,0,'0001-01-01 00:00:00');
/*!40000 ALTER TABLE `user_attendance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_basicinformation`
--

DROP TABLE IF EXISTS `user_basicinformation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_basicinformation` (
  `AccountId` bigint NOT NULL COMMENT '계정번호',
  `UserId` bigint NOT NULL AUTO_INCREMENT COMMENT '유저번호',
  `Level` smallint NOT NULL DEFAULT '1' COMMENT '레벨',
  `Exp` bigint NOT NULL DEFAULT '0' COMMENT '경험치',
  `Money` bigint NOT NULL DEFAULT '0' COMMENT '보유 재화',
  `BestClearStage` int NOT NULL DEFAULT '0' COMMENT '클리어한 최고 스테이지',
  `LastLogin` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '마지막 로그인 일시',
  PRIMARY KEY (`AccountId`),
  UNIQUE KEY `UserId` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=1025 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='유저 기본 정보';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_basicinformation`
--

LOCK TABLES `user_basicinformation` WRITE;
/*!40000 ALTER TABLE `user_basicinformation` DISABLE KEYS */;
INSERT INTO `user_basicinformation` VALUES (1136170162282561536,1000,1,0,0,0,'2023-08-02 14:34:35'),(1136172814928183296,1001,1,0,0,0,'2023-08-02 14:45:08'),(1136191576284856320,1002,1,0,0,0,'2023-08-02 15:59:41'),(1136191680685277184,1003,1,0,0,0,'2023-08-02 16:00:06'),(1136191711043649536,1004,1,0,0,0,'2023-08-02 16:00:13'),(1136191754861543424,1005,1,0,0,0,'2023-08-02 16:00:23'),(1136191794178949120,1006,1,0,0,0,'2023-08-02 16:00:33'),(1136191822339506176,1007,1,0,0,0,'2023-08-02 16:00:39'),(1136191846091849728,1008,1,0,0,0,'2023-08-02 16:00:45'),(1136191866178371584,1009,1,0,0,0,'2023-08-02 16:00:50'),(1136191891243532288,1010,1,0,0,0,'2023-08-02 16:00:56'),(1136191965637902336,1011,1,0,0,0,'2023-08-02 16:01:14'),(1136191995685896192,1012,1,0,0,0,'2023-08-02 16:01:21'),(1136192013083869184,1013,1,0,0,0,'2023-08-02 16:01:25'),(1136192040212627456,1014,1,0,0,0,'2023-08-02 16:01:31'),(1136192055454728192,1015,2,10,66,1,'2023-08-01 00:00:00'),(1136192082214387712,1016,2,111,0,0,'2023-08-02 16:01:41'),(1136192110945370112,1017,1,0,0,0,'2023-08-02 16:01:48'),(1136192152410259456,1018,1,11,599,2,'2023-08-02 16:01:58'),(1136192169887924224,1019,2,0,0,0,'2023-08-02 16:02:02'),(1136192259411148800,1020,9,136133,559382,14,'2023-07-19 00:00:00'),(1136192276054147072,1021,2,10,443,1,'2023-07-17 00:00:00'),(1136192293536006144,1022,2,777,144,1,'2023-07-25 00:00:00'),(1136192330370383872,1023,255,134123576782,12399634,15,'2023-08-02 16:02:40'),(1136192382392336384,1024,125,6032234,500,52,'2023-07-31 00:00:00');
/*!40000 ALTER TABLE `user_basicinformation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_clearstage`
--

DROP TABLE IF EXISTS `user_clearstage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_clearstage` (
  `UserId` bigint NOT NULL COMMENT '계정번호',
  `StageCode` int NOT NULL COMMENT '스테이지 번호',
  `ClearRank` tinyint NOT NULL COMMENT '클리어 랭크',
  `ClearTime` time(3) NOT NULL COMMENT '클리어타임',
  PRIMARY KEY (`UserId`,`StageCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='클리어한 스테이지 정보';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_clearstage`
--

LOCK TABLES `user_clearstage` WRITE;
/*!40000 ALTER TABLE `user_clearstage` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_clearstage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_item`
--

DROP TABLE IF EXISTS `user_item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_item` (
  `ItemId` bigint NOT NULL COMMENT '아이템 고유 ID',
  `UserId` bigint NOT NULL COMMENT '계정번호',
  `ItemCode` bigint NOT NULL COMMENT '아이템 번호',
  `ItemCount` int NOT NULL COMMENT '아이템 개수',
  `Attack` int NOT NULL COMMENT '공격력',
  `Defence` int NOT NULL COMMENT '방어력',
  `Magic` int NOT NULL COMMENT '마력',
  `EnhanceCount` tinyint NOT NULL DEFAULT '0' COMMENT '강화 수치',
  `IsDestroyed` tinyint(1) NOT NULL DEFAULT '0',
  `ObtainedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '획득 일시',
  PRIMARY KEY (`ItemId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='유저 아이템 정보';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_item`
--

LOCK TABLES `user_item` WRITE;
/*!40000 ALTER TABLE `user_item` DISABLE KEYS */;
INSERT INTO `user_item` VALUES (1136170162429362176,1000,2,1,10,5,1,0,0,'2023-08-02 14:34:35'),(1136170162446139392,1000,4,1,3,10,1,0,0,'2023-08-02 14:34:35'),(1136170162454528000,1000,5,1,1,1,1,0,0,'2023-08-02 14:34:35'),(1136170162467110912,1000,6,100,0,0,0,0,0,'2023-08-02 14:34:35'),(1136172815020457984,1001,2,1,10,5,1,0,0,'2023-08-02 14:45:08'),(1136172815037235200,1001,4,1,3,10,1,0,0,'2023-08-02 14:45:08'),(1136172815058206720,1001,5,1,1,1,1,0,0,'2023-08-02 14:45:08'),(1136172815074983936,1001,6,100,0,0,0,0,0,'2023-08-02 14:45:08'),(1136191576398102528,1002,2,1,10,5,1,0,0,'2023-08-02 15:59:41'),(1136191576406491136,1002,4,1,3,10,1,0,0,'2023-08-02 15:59:41'),(1136191576419074048,1002,5,1,1,1,1,0,0,'2023-08-02 15:59:41'),(1136191576427462656,1002,6,100,0,0,0,0,0,'2023-08-02 15:59:41'),(1136191680748191744,1003,2,1,10,5,1,0,0,'2023-08-02 16:00:06'),(1136191680769163264,1003,4,1,3,10,1,0,0,'2023-08-02 16:00:06'),(1136191680785940480,1003,5,1,1,1,1,0,0,'2023-08-02 16:00:06'),(1136191680802717696,1003,6,100,0,0,0,0,0,'2023-08-02 16:00:06'),(1136191711177867264,1004,2,1,10,5,1,0,0,'2023-08-02 16:00:13'),(1136191711207227392,1004,4,1,3,10,1,0,0,'2023-08-02 16:00:13'),(1136191711211421696,1004,5,1,1,1,1,0,0,'2023-08-02 16:00:13'),(1136191711224004608,1004,6,100,0,0,0,0,0,'2023-08-02 16:00:13'),(1136191754899292160,1005,2,1,10,5,1,0,0,'2023-08-02 16:00:23'),(1136191754911875072,1005,4,1,3,10,1,0,0,'2023-08-02 16:00:23'),(1136191754924457984,1005,5,1,1,1,1,0,0,'2023-08-02 16:00:23'),(1136191754937040896,1005,6,100,0,0,0,0,0,'2023-08-02 16:00:23'),(1136191794246057984,1006,2,1,10,5,1,0,0,'2023-08-02 16:00:33'),(1136191794262835200,1006,4,1,3,10,1,0,0,'2023-08-02 16:00:33'),(1136191794283806720,1006,5,1,1,1,1,0,0,'2023-08-02 16:00:33'),(1136191794304778240,1006,6,100,0,0,0,0,0,'2023-08-02 16:00:33'),(1136191822389837824,1007,2,1,10,5,1,0,0,'2023-08-02 16:00:39'),(1136191822402420736,1007,4,1,3,10,1,0,0,'2023-08-02 16:00:39'),(1136191822419197952,1007,5,1,1,1,1,0,0,'2023-08-02 16:00:39'),(1136191822431780864,1007,6,100,0,0,0,0,0,'2023-08-02 16:00:39'),(1136191846125404160,1008,2,1,10,5,1,0,0,'2023-08-02 16:00:45'),(1136191846137987072,1008,4,1,3,10,1,0,0,'2023-08-02 16:00:45'),(1136191846146375680,1008,5,1,1,1,1,0,0,'2023-08-02 16:00:45'),(1136191846158958592,1008,6,100,0,0,0,0,0,'2023-08-02 16:00:45'),(1136191866237091840,1009,2,1,10,5,1,0,0,'2023-08-02 16:00:50'),(1136191866253869056,1009,4,1,3,10,1,0,0,'2023-08-02 16:00:50'),(1136191866295812096,1009,5,1,1,1,1,0,0,'2023-08-02 16:00:50'),(1136191866329366528,1009,6,100,0,0,0,0,0,'2023-08-02 16:00:50'),(1136191891298058240,1010,2,1,10,5,1,0,0,'2023-08-02 16:00:56'),(1136191891314835456,1010,4,1,3,10,1,0,0,'2023-08-02 16:00:56'),(1136191891327418368,1010,5,1,1,1,1,0,0,'2023-08-02 16:00:56'),(1136191891344195584,1010,6,100,0,0,0,0,0,'2023-08-02 16:00:56'),(1136191965679845376,1011,2,1,10,5,1,0,0,'2023-08-02 16:01:14'),(1136191965692428288,1011,4,1,3,10,1,0,0,'2023-08-02 16:01:14'),(1136191965700816896,1011,5,1,1,1,1,0,0,'2023-08-02 16:01:14'),(1136191965713399808,1011,6,100,0,0,0,0,0,'2023-08-02 16:01:14'),(1136191995715256320,1012,2,1,10,5,1,0,0,'2023-08-02 16:01:21'),(1136191995723644928,1012,4,1,3,10,1,0,0,'2023-08-02 16:01:21'),(1136191995736227840,1012,5,1,1,1,1,0,0,'2023-08-02 16:01:21'),(1136191995744616448,1012,6,100,0,0,0,0,0,'2023-08-02 16:01:21'),(1136192013138395136,1013,2,1,10,5,1,0,0,'2023-08-02 16:01:25'),(1136192013155172352,1013,4,1,3,10,1,0,0,'2023-08-02 16:01:25'),(1136192013167755264,1013,5,1,1,1,1,0,0,'2023-08-02 16:01:25'),(1136192013184532480,1013,6,100,0,0,0,0,0,'2023-08-02 16:01:25'),(1136192040292319232,1014,2,1,10,5,1,0,0,'2023-08-02 16:01:31'),(1136192040304902144,1014,4,1,3,10,1,0,0,'2023-08-02 16:01:31'),(1136192040321679360,1014,5,1,1,1,1,0,0,'2023-08-02 16:01:31'),(1136192040338456576,1014,6,100,0,0,0,0,0,'2023-08-02 16:01:31'),(1136192055496671232,1015,2,1,10,5,1,0,0,'2023-08-02 16:01:35'),(1136192055513448448,1015,4,1,3,10,1,0,0,'2023-08-02 16:01:35'),(1136192055526031360,1015,5,1,1,1,1,0,0,'2023-08-02 16:01:35'),(1136192055538614272,1015,6,100,0,0,0,0,0,'2023-08-02 16:01:35'),(1136192082302468096,1016,2,1,10,5,1,0,0,'2023-08-02 16:01:41'),(1136192082319245312,1016,4,1,3,10,1,0,0,'2023-08-02 16:01:41'),(1136192082340216832,1016,5,1,1,1,1,0,0,'2023-08-02 16:01:41'),(1136192082377965568,1016,6,100,0,0,0,0,0,'2023-08-02 16:01:41'),(1136192111008284672,1017,2,1,10,5,1,0,0,'2023-08-02 16:01:48'),(1136192111025061888,1017,4,1,3,10,1,0,0,'2023-08-02 16:01:48'),(1136192111046033408,1017,5,1,1,1,1,0,0,'2023-08-02 16:01:48'),(1136192111062810624,1017,6,100,0,0,0,0,0,'2023-08-02 16:01:48'),(1136192152448008192,1018,2,1,10,5,1,0,0,'2023-08-02 16:01:58'),(1136192152460591104,1018,4,1,3,10,1,0,0,'2023-08-02 16:01:58'),(1136192152468979712,1018,5,1,1,1,1,0,0,'2023-08-02 16:01:58'),(1136192152481562624,1018,6,100,0,0,0,0,0,'2023-08-02 16:01:58'),(1136192169913090048,1019,2,1,10,5,1,0,0,'2023-08-02 16:02:02'),(1136192169921478656,1019,4,1,3,10,1,0,0,'2023-08-02 16:02:02'),(1136192169929867264,1019,5,1,1,1,1,0,0,'2023-08-02 16:02:02'),(1136192169934061568,1019,6,100,0,0,0,0,0,'2023-08-02 16:02:02'),(1136192259457286144,1020,2,1,10,5,1,0,0,'2023-08-02 16:02:24'),(1136192259474063360,1020,4,1,3,10,1,0,0,'2023-08-02 16:02:24'),(1136192259490840576,1020,5,1,1,1,1,0,0,'2023-08-02 16:02:24'),(1136192259507617792,1020,6,100,0,0,0,0,0,'2023-08-02 16:02:24'),(1136192276091895808,1021,2,1,10,5,1,0,0,'2023-08-02 16:02:28'),(1136192276100284416,1021,4,1,3,10,1,0,0,'2023-08-02 16:02:28'),(1136192276112867328,1021,5,1,1,1,1,0,0,'2023-08-02 16:02:28'),(1136192276121255936,1021,6,100,0,0,0,0,0,'2023-08-02 16:02:28'),(1136192293561171968,1022,2,1,10,5,1,0,0,'2023-08-02 16:02:32'),(1136192293569560576,1022,4,1,3,10,1,0,0,'2023-08-02 16:02:32'),(1136192293577949184,1022,5,1,1,1,1,0,0,'2023-08-02 16:02:32'),(1136192293586337792,1022,6,100,0,0,0,0,0,'2023-08-02 16:02:32'),(1136192330441687040,1023,2,1,10,5,1,0,0,'2023-08-02 16:02:41'),(1136192330462658560,1023,4,1,3,10,1,0,0,'2023-08-02 16:02:41'),(1136192330479435776,1023,5,1,1,1,1,0,0,'2023-08-02 16:02:41'),(1136192330500407296,1023,6,100,0,0,0,0,0,'2023-08-02 16:02:41'),(1136192382434279424,1024,2,1,10,5,1,0,0,'2023-08-02 16:02:53'),(1136192382446862336,1024,4,1,3,10,1,0,0,'2023-08-02 16:02:53'),(1136192382459445248,1024,5,1,1,1,1,0,0,'2023-08-02 16:02:53'),(1136192382472028160,1024,6,100,0,0,0,0,0,'2023-08-02 16:02:53');
/*!40000 ALTER TABLE `user_item` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-09-04 18:57:42
CREATE DATABASE  IF NOT EXISTS `masterdb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `masterdb`;
-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: masterdb
-- ------------------------------------------------------
-- Server version	8.0.33

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

--
-- Table structure for table `attendancereward`
--

DROP TABLE IF EXISTS `attendancereward`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `attendancereward` (
  `Code` tinyint NOT NULL COMMENT '날짜',
  `ItemCode` bigint NOT NULL COMMENT '아이템 번호',
  `Count` int NOT NULL COMMENT '개수',
  PRIMARY KEY (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='출석부 보상';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attendancereward`
--

LOCK TABLES `attendancereward` WRITE;
/*!40000 ALTER TABLE `attendancereward` DISABLE KEYS */;
INSERT INTO `attendancereward` VALUES (1,1,100),(2,1,100),(3,1,100),(4,1,200),(5,1,200),(6,1,200),(7,2,1),(8,1,100),(9,1,100),(10,1,100),(11,6,5),(12,1,150),(13,1,150),(14,1,150),(15,1,150),(16,1,150),(17,1,150),(18,4,1),(19,1,200),(20,1,200),(21,1,200),(22,1,200),(23,1,200),(24,5,1),(25,1,250),(26,1,250),(27,1,250),(28,1,250),(29,1,250),(30,3,1);
/*!40000 ALTER TABLE `attendancereward` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `exptable`
--

DROP TABLE IF EXISTS `exptable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `exptable` (
  `Level` int NOT NULL COMMENT '레벨',
  `RequireExp` bigint NOT NULL COMMENT '해당 레벨에서 다음 레벨까지 필요한 경험치'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='경험치 테이블';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `exptable`
--

LOCK TABLES `exptable` WRITE;
/*!40000 ALTER TABLE `exptable` DISABLE KEYS */;
INSERT INTO `exptable` VALUES (1,10),(2,20),(3,1000),(4,2000),(5,4000);
/*!40000 ALTER TABLE `exptable` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inappproduct`
--

DROP TABLE IF EXISTS `inappproduct`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inappproduct` (
  `Code` int NOT NULL COMMENT '상품번호',
  `ItemCode` bigint NOT NULL COMMENT '아이템 번호',
  `ItemName` varchar(50) NOT NULL COMMENT '아이템 이름',
  `ItemCount` int NOT NULL COMMENT '아이템 개수',
  PRIMARY KEY (`Code`,`ItemCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='인앱 상품';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inappproduct`
--

LOCK TABLES `inappproduct` WRITE;
/*!40000 ALTER TABLE `inappproduct` DISABLE KEYS */;
INSERT INTO `inappproduct` VALUES (1,1,'돈',1000),(1,2,'작은 칼',1),(1,3,'도금 칼',1),(2,4,'나무 방패',1),(2,5,'보통 모자',1),(2,6,'포션',10),(3,1,'돈',2000),(3,2,'작은 칼',1),(3,4,'나무 방패',1),(3,5,'보통 모자',1);
/*!40000 ALTER TABLE `inappproduct` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `item`
--

DROP TABLE IF EXISTS `item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `item` (
  `Code` bigint NOT NULL COMMENT '아이템 번호',
  `Name` varchar(50) NOT NULL COMMENT '아이템 이름',
  `Attribute` int NOT NULL COMMENT '특성',
  `SellPrice` bigint NOT NULL COMMENT '판매 금액',
  `BuyPrice` bigint NOT NULL COMMENT '구입 금액',
  `UseLv` smallint NOT NULL COMMENT '사용가능 레벨',
  `Attack` int NOT NULL COMMENT '공격력',
  `Defence` int NOT NULL COMMENT '방어력',
  `Magic` int NOT NULL COMMENT '마법력',
  `EnhanceMaxCount` tinyint NOT NULL COMMENT '최대 강화 가능 횟수',
  PRIMARY KEY (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='아이템';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `item`
--

LOCK TABLES `item` WRITE;
/*!40000 ALTER TABLE `item` DISABLE KEYS */;
INSERT INTO `item` VALUES (1,'돈',5,0,0,0,0,0,0,0),(2,'작은 칼',1,10,20,1,10,5,1,10),(3,'도금 칼',1,100,200,5,29,12,10,10),(4,'나무 방패',2,7,15,1,3,10,1,10),(5,'보통 모자',3,5,8,1,1,1,1,10),(6,'포션',4,3,6,1,0,0,0,0);
/*!40000 ALTER TABLE `item` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `itemattribute`
--

DROP TABLE IF EXISTS `itemattribute`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `itemattribute` (
  `Name` varchar(50) NOT NULL COMMENT '특성 이름',
  `Code` int NOT NULL COMMENT '코드',
  PRIMARY KEY (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='아이템 특성';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `itemattribute`
--

LOCK TABLES `itemattribute` WRITE;
/*!40000 ALTER TABLE `itemattribute` DISABLE KEYS */;
INSERT INTO `itemattribute` VALUES ('무기',1),('방어구',2),('복장',3),('마법도구',4),('돈',5);
/*!40000 ALTER TABLE `itemattribute` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stageenemy`
--

DROP TABLE IF EXISTS `stageenemy`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stageenemy` (
  `Code` int NOT NULL COMMENT '스테이지 단계',
  `NpcCode` int NOT NULL COMMENT '공격 Npc',
  `Count` int NOT NULL COMMENT 'Npc 수',
  `Exp` int NOT NULL COMMENT '경험치',
  PRIMARY KEY (`Code`,`NpcCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='스테이지 적';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stageenemy`
--

LOCK TABLES `stageenemy` WRITE;
/*!40000 ALTER TABLE `stageenemy` DISABLE KEYS */;
INSERT INTO `stageenemy` VALUES (1,101,1,10),(1,110,1,15),(2,201,1,20),(2,211,1,35),(2,221,1,50);
/*!40000 ALTER TABLE `stageenemy` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stageitem`
--

DROP TABLE IF EXISTS `stageitem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stageitem` (
  `Code` int NOT NULL COMMENT '스테이지 단계',
  `ItemCode` bigint NOT NULL COMMENT '파밍 가능 아이템 번호',
  `Count` bigint NOT NULL COMMENT '파밍 가능 최대 개수',
  PRIMARY KEY (`Code`,`ItemCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='스테이지 아이템';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stageitem`
--

LOCK TABLES `stageitem` WRITE;
/*!40000 ALTER TABLE `stageitem` DISABLE KEYS */;
INSERT INTO `stageitem` VALUES (1,1,1500),(1,2,5),(2,2,10),(2,3,5);
/*!40000 ALTER TABLE `stageitem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `versiondata`
--

DROP TABLE IF EXISTS `versiondata`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `versiondata` (
  `AppVersion` decimal(5,4) NOT NULL COMMENT '앱 버전',
  `MasterVersion` decimal(5,4) NOT NULL COMMENT '마스터 버전'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='게임 버전 데이터';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `versiondata`
--

LOCK TABLES `versiondata` WRITE;
/*!40000 ALTER TABLE `versiondata` DISABLE KEYS */;
INSERT INTO `versiondata` VALUES (1.0001,1.0001);
/*!40000 ALTER TABLE `versiondata` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-09-04 18:57:42
CREATE DATABASE  IF NOT EXISTS `managingdb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `managingdb`;
-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: managingdb
-- ------------------------------------------------------
-- Server version	8.0.33

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

--
-- Table structure for table `managing_account`
--

DROP TABLE IF EXISTS `managing_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `managing_account` (
  `AccountId` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(45) NOT NULL,
  `Name` varchar(10) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `RefreshToken` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`AccountId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `managing_account`
--

LOCK TABLES `managing_account` WRITE;
/*!40000 ALTER TABLE `managing_account` DISABLE KEYS */;
INSERT INTO `managing_account` VALUES (1,'dosoon@com2us.com','황서영','dosoon','eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmJmIjoxNjkzODIwODk0LCJleHAiOjE2OTM5MDcyOTQsImlhdCI6MTY5MzgyMDg5NH0.OZXrSIPaNmzFVb5JA-foeTztNPB8j8u7RApmRP8rDQs');
/*!40000 ALTER TABLE `managing_account` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-09-04 18:57:42
