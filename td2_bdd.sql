DROP DATABASE IF EXISTS velomax;
CREATE DATABASE velomax;
USE velomax;

DROP TABLE IF EXISTS velo;

CREATE TABLE `velomax`.`velo`(
	`numProduit` VARCHAR(4) NOT NULL,
    `nom` VARCHAR(20) NOT NULL,
    `grandeur` VARCHAR(20) NOT NULL,
    `prix` INT,
    `type` VARCHAR(20),
    `dateIntro` DATE NOT NULL,
    `dateDiscont` DATE NULL,
    PRIMARY KEY (`numProduit`) );
    
CREATE TABLE `velomax`.`assemblage`(
	`nomVelo` VARCHAR(20) NOT NULL,
    `grandeurVelo` VARCHAR(20) NOT NULL,
    `cadre` VARCHAR(5) NOT NULL,
    `guidon` VARCHAR(5) NOT NULL,
    `frein` VARCHAR(5) NULL,
    `selle` VARCHAR(5) NOT NULL,
    `derailleurAvant` VARCHAR(5) NULL,
    `derailleurArriere` VARCHAR(5) NULL,
    `roueAvant` VARCHAR(5) NOT NULL,
    `roueArriere` VARCHAR(5) NOT NULL,
    `reflecteurs` VARCHAR(5) NULL,
    `pedalier` VARCHAR(5) NOT NULL,
    `ordinateur` VARCHAR(5) NULL,
    `panier` VARCHAR(5) NULL,
    PRIMARY KEY (`nomVelo`, `grandeurVelo`) );

INSERT INTO velo VALUES ('101', 'Kilimandjaro', 'Adultes', '569', 'VTT', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('102', 'NorthPole', 'Adultes', '329', 'VTT', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('103', 'MontBlanc', 'Jeunes', '399', 'VTT', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('104', 'Hooligan', 'Jeunes', '199', 'VTT', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('105', 'Orléans', 'Hommes', '229', 'Vélo de course', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('106', 'Orléans', 'Dames', '229', 'Vélo de course', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('107', 'BlueJay', 'Hommes', '349', 'Vélo de course', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('108', 'BlueJay', 'Dames', '349', 'Vélo de course', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('109', 'Trail Explorer', 'Filles', '129', 'Classique', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('110', 'Trail Explorer', 'Garçons', '129', 'Classique', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('111', 'Night Hawk', 'Jeunes', '189', 'Classique', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('112', 'Tierra Verde', 'Hommes', '199', 'Classique', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('113', 'Tierra Verde', 'Dames', '199', 'Classique', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('114', 'Mud Zinger I', 'Jeunes', '279', 'BMX', '2000-07-31', NULL); 
INSERT INTO velo VALUES ('115', 'Mud Zinger II', 'Adultes', '359', 'BMX', '2000-07-31', NULL); 