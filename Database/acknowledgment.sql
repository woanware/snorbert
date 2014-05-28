CREATE TABLE `acknowledgment` (
	`id` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
	`cid` INT(10) UNSIGNED NOT NULL,
	`sid` INT(10) UNSIGNED NOT NULL,
	`initials` VARCHAR(3) NOT NULL,
	`class` INT(10) UNSIGNED NOT NULL,
	`notes` VARCHAR(500) NULL DEFAULT NULL,
	`timestamp` DATETIME NOT NULL,
	`successful` BIT(1) NULL DEFAULT NULL,
	PRIMARY KEY (`id`),
	UNIQUE INDEX `unique` (`cid`, `sid`),
	INDEX `cid` (`cid`),
	INDEX `sid` (`sid`),
	INDEX `timestamp` (`timestamp`)
)
ENGINE=InnoDB
AUTO_INCREMENT=0;