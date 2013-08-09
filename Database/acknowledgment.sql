CREATE TABLE `acknowledgment` (
	`id` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
	`cid` INT(10) UNSIGNED NOT NULL,
	`sid` INT(10) UNSIGNED NOT NULL,
	`initials` VARCHAR(3) NOT NULL,
	`class` INT(10) UNSIGNED NOT NULL,
	`notes` VARCHAR(100) NULL DEFAULT NULL,
	`timestamp` DATETIME NOT NULL,
	PRIMARY KEY (`id`),
	UNIQUE INDEX `unique` (`cid`, `sid`),
	INDEX `cid` (`cid`),
	INDEX `sid` (`sid`),
	INDEX `timestamp` (`timestamp`)
)
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
AUTO_INCREMENT=23;