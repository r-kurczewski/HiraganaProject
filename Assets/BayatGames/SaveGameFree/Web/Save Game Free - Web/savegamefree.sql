CREATE DATABASE IF NOT EXISTS `savegamefree`;

USE savegamefree;

CREATE TABLE IF NOT EXISTS savegames (
  id VARCHAR(255) CHARACTER SET utf8 NOT NULL PRIMARY KEY,
  data LONGTEXT CHARACTER SET utf8 NOT NULL
);
