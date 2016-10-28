DROP DATABASE IF EXISTS XtrmCoach;

CREATE DATABASE XtrmCoach;

USE XtrmCoach;

CREATE TABLE user (
	id			INT				PRIMARY KEY   AUTO_INCREMENT,
	first_name	VARCHAR(255)	NOT NULL,
	last_name	VARCHAR(255)	NOT NULL,
	email_id	VARCHAR(255)	NOT NULL,
	password	VARCHAR(255)	NOT NULL,
	is_admin	BOOLEAN			NOT NULL
);

INSERT INTO User (first_name, last_name, email_id, password, is_admin) VALUES ('Atul', 'Banwar', 'a@a.a', 'a', false);

SELECT * FROM User;