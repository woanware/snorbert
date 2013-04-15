-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.6.10 - MySQL Community Server (GPL)
-- Server OS:                    Win64
-- HeidiSQL version:             7.0.0.4053
-- Date/time:                    2013-03-28 14:08:14
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET FOREIGN_KEY_CHECKS=0 */;
-- Dumping data for table snort.acknowledgment_class: ~0 rows (approximately)
/*!40000 ALTER TABLE `acknowledgment_class` DISABLE KEYS */;
INSERT INTO `acknowledgment_class` (`id`, `desc`) VALUES
	(1, 'Taken'),
	(2, 'Unauthorized Root Access'),
	(3, 'Unauthorized User Access'),
	(4, 'Attempted Unauthorized Access'),
	(5, 'Denial of Service Attack'),
	(6, 'Policy Violation'),
	(7, 'Reconnaissance'),
	(8, 'Virus Infection'),
	(9, 'False Positive'),
	(10, 'Email Spam'),
	(11, 'DNS Lookup');
/*!40000 ALTER TABLE `acknowledgment_class` ENABLE KEYS */;
/*!40014 SET FOREIGN_KEY_CHECKS=1 */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
