CREATE TABLE `acknowledgment` (
	`id` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
	`cid` INT(10) UNSIGNED NOT NULL,
	`sid` INT(10) UNSIGNED NOT NULL,
	`initials` VARCHAR(3) NOT NULL,
	`class` INT(10) UNSIGNED NOT NULL,
	PRIMARY KEY (`id`),
	INDEX `cid` (`cid`),
	INDEX `sid` (`sid`)
)
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
AUTO_INCREMENT=0;
