CREATE TABLE LoginTable(
   ID   INT              NOT NULL,
   AccountName CHAR (200)     NOT NULL,
   Password  CHAR (200) NOT NULL,   
   PRIMARY KEY (ID)
);

INSERT INTO LoginTable (ID, AccountName, Password)
VALUES (1, '123@gmail.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3');

INSERT INTO LoginTable (ID, AccountName, Password)
VALUES (2, 'testAdminUser@gmail.com', '45961da9ce13da68788eac0836edf79c1a0b510746b26bb471acf8c53a9dd63e');