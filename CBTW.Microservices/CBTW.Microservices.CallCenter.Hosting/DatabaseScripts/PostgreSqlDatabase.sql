/*------------------------------------------------------------------------------------------------------------------*/

-- ==================================================================================================================
-- Author:              Jose Luis Ortiz
-- Create date:         11/10/2021
-- Description:         Creación la base de datos DB_MSCALLCENTER y se agregan nuevas tablas.
-- ==================================================================================================================
-- Aplicación:          Microservicio CallCenter
-- ==================================================================================================================

/*--Creación DB_MSCALLCENTER--*/
CREATE DATABASE DB_MSCALLCENTER;

/*--Creación dbo.CALLCENTER--*/
-- Create nuevo table dbo.CALLCENTER : id, name, createDate, updateDate
CREATE TABLE CALLCENTER(
	id INT GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
	"name" VARCHAR(200) NOT NULL,
	createDate TIMESTAMP(3) NOT NULL,
    updateDate TIMESTAMP(3) NULL,
	CONSTRAINT PK_CALLCENTER PRIMARY KEY (id)
);

-- Insert nuevo table dbo.CALLCENTER
INSERT INTO CALLCENTER ("name", createDate, updateDate) VALUES
('Bogota Norte', '2021-10-11', NULL),
('Bogota Centro', '2021-10-11', NULL),
('Bogota Sur', '2021-10-11', NULL),
('Medellin Norte', '2021-10-11', NULL),
('Medellin Centro', '2021-10-11', NULL),
('Medellin Sur', '2021-10-11', NULL),
('Cali Norte', '2021-10-11', NULL),
('Cali Centro', '2021-10-11', NULL),
('Cali Sur', '2021-10-11', NULL),
('Barranquilla Norte', '2021-10-11', NULL),
('Barranquilla Centro', '2021-10-11', NULL),
('Barranquilla Sur', '2021-10-11', NULL);

/*--Creaci�n dbo.PHONECC--*/
-- Create nuevo table dbo.PHONECC : id, idCallCenter, countryCode, phoneNumber, createDate, updateDate
CREATE TABLE PHONECC(
	id INT GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
	idCallCenter INT NOT NULL,
	countryCode VARCHAR(5) NOT NULL,
	phoneNumber VARCHAR(15) NOT NULL,
	createDate TIMESTAMP(3) NOT NULL,
    updateDate TIMESTAMP(3) NULL,
	CONSTRAINT PK_PHONECC PRIMARY KEY (id),
	CONSTRAINT FK_PHONECC_CALLCENTER FOREIGN KEY (idCallCenter) REFERENCES CALLCENTER(id)
);

-- Insert nuevo table dbo.PHONECC
INSERT INTO PHONECC (idCallCenter, countryCode, phoneNumber, createDate, updateDate) VALUES
(1, '+57', '3113554267', '2021-10-11', NULL),
(2, '+57', '3103464063', '2021-10-11', NULL),
(3, '+57', '3133444160', '2021-10-11', NULL),
(4, '+57', '3143478511', '2021-10-11', NULL),
(5, '+57', '3153423509', '2021-10-11', NULL),
(6, '+57', '3163454522', '2021-10-11', NULL),
(7, '+57', '3173184555', '2021-10-11', NULL),
(8, '+57', '3183704521', '2021-10-11', NULL),
(9, '+57', '3123496543', '2021-10-11', NULL),
(10, '+57', '3203434510', '2021-10-11', NULL),
(11, '+57', '3213414599', '2021-10-11', NULL),
(12, '+57', '3223164572', '2021-10-11', NULL);

/*--Creaci�n dbo.COUNTRY--*/
-- Create nuevo table dbo.COUNTRY : id, name, createDate, updateDate
CREATE TABLE COUNTRY(
	id INT GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
	"name" VARCHAR(200) NOT NULL,
	createDate TIMESTAMP(3) NOT NULL,
    updateDate TIMESTAMP(3) NULL,
	CONSTRAINT PK_COUNTRY PRIMARY KEY (id)
);

-- Insert nuevo table dbo.COUNTRY
INSERT INTO COUNTRY ("name", createDate, updateDate) VALUES
('Colombia', '2021-10-11', NULL);

/*--Creaci�n dbo.CITY--*/
-- Create nuevo table dbo.CITY : id, idCountry, name, createDate, updateDate
CREATE TABLE CITY(
	id INT GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
	idCountry INT NOT NULL,
	"name" VARCHAR(200) NOT NULL,	
	createDate TIMESTAMP(3) NOT NULL,
    updateDate TIMESTAMP(3) NULL,
	CONSTRAINT PK_CITY PRIMARY KEY (id),
	CONSTRAINT FK_CITY_COUNTRY FOREIGN KEY (idCountry) REFERENCES COUNTRY(id)
);

-- Insert nuevo table dbo.CITY
INSERT INTO CITY (idCountry, "name", createDate, updateDate) VALUES
(1, 'Santafé de Bogotá', '2021-10-11', NULL),
(1, 'Cali', '2021-10-11', NULL),
(1, 'Medellín', '2021-10-11', NULL),
(1, 'Barranquilla', '2021-10-11', NULL),
(1, 'Cartagena', '2021-10-11', NULL),
(1, 'Cúcuta', '2021-10-11', NULL),
(1, 'Bucaramanga', '2021-10-11', NULL),
(1, 'Ibagué', '2021-10-11', NULL),
(1, 'Pereira', '2021-10-11', NULL),
(1, 'Santa Marta', '2021-10-11', NULL),
(1, 'Manizales', '2021-10-11', NULL),
(1, 'Bello', '2021-10-11', NULL),
(1, 'Pasto', '2021-10-11', NULL),
(1, 'Neiva', '2021-10-11', NULL),
(1, 'Soledad', '2021-10-11', NULL),
(1, 'Armenia', '2021-10-11', NULL),
(1, 'Villavicencio', '2021-10-11', NULL),
(1, 'Soacha', '2021-10-11', NULL),
(1, 'Valledupar', '2021-10-11', NULL),
(1, 'Montería', '2021-10-11', NULL),
(1, 'Itagüí', '2021-10-11', NULL),
(1, 'Palmira', '2021-10-11', NULL),
(1, 'Buenaventura', '2021-10-11', NULL),
(1, 'Floridablanca', '2021-10-11', NULL),
(1, 'Sincelejo', '2021-10-11', NULL),
(1, 'Popayán', '2021-10-11', NULL),
(1, 'Barrancabermeja', '2021-10-11', NULL),
(1, 'Dos Quebradas', '2021-10-11', NULL),
(1, 'Tuluá', '2021-10-11', NULL),
(1, 'Envigado', '2021-10-11', NULL),
(1, 'Cartago', '2021-10-11', NULL),
(1, 'Girardot', '2021-10-11', NULL),
(1, 'Buga', '2021-10-11', NULL),
(1, 'Tunja', '2021-10-11', NULL),
(1, 'Florencia', '2021-10-11', NULL),
(1, 'Maicao', '2021-10-11', NULL),
(1, 'Sogamoso', '2021-10-11', NULL),
(1, 'Giron', '2021-10-11', NULL),
(1, 'Otra', '2021-10-11', NULL);

/*--Creaci�n dbo.DOCUMENTTYPE--*/
-- Create nuevo table dbo.DOCUMENTTYPE : id, name, description, createDate, updateDate
CREATE TABLE DOCUMENTTYPE(
	id INT GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
	"name" VARCHAR(5) NOT NULL,
	description VARCHAR(200) NOT NULL,
	createDate TIMESTAMP(3) NOT NULL,
    updateDate TIMESTAMP(3) NULL,
	CONSTRAINT PK_DOCUMENTTYPE PRIMARY KEY (id)
);

-- Insert nuevo table dbo.DOCUMENTTYPE
INSERT INTO DOCUMENTTYPE ("name", description, createDate, updateDate) VALUES
('CC', 'Cédula de ciudadanía', '2021-10-11', NULL),
('CE', 'Cédula de extranjería', '2021-10-11', NULL),
('TI', 'Tarjeta de identidad', '2021-10-11', NULL);

/*--Creaci�n dbo.CUSTOMER--*/
-- Create nuevo table dbo.CUSTOMER : id, idDocumentType, document, fullName, countryCode, phoneNumber
--                                   dateOfBirth, idCity, idPhoneCC, createDate, updateDate
CREATE TABLE CUSTOMER(
	id BIGINT GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
	idDocumentType INT NOT NULL,
	document VARCHAR(20) NOT NULL,
	fullName VARCHAR(200) NOT NULL,
	countryCode VARCHAR(5) NOT NULL,
	phoneNumber VARCHAR(15) NOT NULL,
	dateOfBirth DATE NOT NULL,
	idCity INT NOT NULL,
	idPhoneCC INT NOT NULL,
	createDate TIMESTAMP(3) NOT NULL,
    updateDate TIMESTAMP(3) NULL,
	CONSTRAINT PK_CUSTOMER PRIMARY KEY (id),
	CONSTRAINT FK_CUSTOMER_DOCUMENTTYPE FOREIGN KEY (idDocumentType) REFERENCES DOCUMENTTYPE(id),
	CONSTRAINT FK_CUSTOMER_CITY FOREIGN KEY (idCity) REFERENCES CITY(id),
	CONSTRAINT FK_CUSTOMER_PHONECC FOREIGN KEY (idPhoneCC) REFERENCES PHONECC(id)
);

/*--Creaci�n dbo.PQR--*/
-- Create nuevo table dbo.PQR : id, idCustomer, subject, description, createDate, updateDate
CREATE TABLE PQR(
	id BIGINT GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
	idCustomer BIGINT NOT NULL,
	subject VARCHAR(200) NOT NULL,
	description TEXT NOT NULL,
	createDate TIMESTAMP(3) NOT NULL,
    updateDate TIMESTAMP(3) NULL,
	CONSTRAINT PK_PQR PRIMARY KEY (id),
	CONSTRAINT FK_PQR_CUSTOMER FOREIGN KEY (idCustomer) REFERENCES CUSTOMER(id)
);

/*------------------------------------------------------------------------------------------------------------------*/