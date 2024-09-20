/*------------------------------------------------------------------------------------------------------------------*/

-- ==================================================================================================================
-- Author:              Jose Luis Ortiz
-- Create date:         11/10/2021
-- Description:         Creación la base de datos DB_MSCALLCENTER y se agregan nuevas tablas.
-- ==================================================================================================================
-- Aplicación:          Microservicio CallCenter
-- ==================================================================================================================

/*--Creación CALLCENTER--*/
-- Create nuevo table CALLCENTER : id, name, createDate, updateDate
CREATE TABLE CALLCENTER(
	id NUMBER(10) NOT NULL,
	name VARCHAR2(200 CHAR) NOT NULL,
	createDate TIMESTAMP(0) NOT NULL,
    updateDate TIMESTAMP(0) NULL,
	CONSTRAINT PK_CALLCENTER PRIMARY KEY (id)
);

-- Generate ID using sequence and trigger
CREATE SEQUENCE CALLCENTER_seq START WITH 1 INCREMENT BY 1;

CREATE OR REPLACE TRIGGER CALLCENTER_seq_tr
 BEFORE INSERT ON CALLCENTER FOR EACH ROW
 WHEN (NEW.id IS NULL)
BEGIN
 SELECT CALLCENTER_seq.NEXTVAL INTO :NEW.id FROM DUAL;
END;
/

-- Insert nuevo table CALLCENTER
INSERT INTO CALLCENTER (name, createDate, updateDate)
 SELECT 'Bogota Norte', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 'Bogota Centro', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 'Bogota Sur', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 'Medellin Norte', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 'Medellin Centro', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 'Medellin Sur', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 'Cali Norte', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 'Cali Centro', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 'Cali Sur', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 'Barranquilla Norte', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 'Barranquilla Centro', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 'Barranquilla Sur', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual;

/*--Creaci�n PHONECC--*/
-- Create nuevo table PHONECC : id, idCallCenter, countryCode, phoneNumber, createDate, updateDate
CREATE TABLE PHONECC(
	id NUMBER(10) NOT NULL,
	idCallCenter NUMBER(10) NOT NULL,
	countryCode VARCHAR2(5 CHAR) NOT NULL,
	phoneNumber VARCHAR2(15 CHAR) NOT NULL,
	createDate TIMESTAMP(0) NOT NULL,
    updateDate TIMESTAMP(0) NULL,
	CONSTRAINT PK_PHONECC PRIMARY KEY (id),
	CONSTRAINT FK_PHONECC_CALLCENTER FOREIGN KEY (idCallCenter) REFERENCES CALLCENTER(id)
);

-- Generate ID using sequence and trigger
CREATE SEQUENCE PHONECC_seq START WITH 1 INCREMENT BY 1;

CREATE OR REPLACE TRIGGER PHONECC_seq_tr
 BEFORE INSERT ON PHONECC FOR EACH ROW
 WHEN (NEW.id IS NULL)
BEGIN
 SELECT PHONECC_seq.NEXTVAL INTO :NEW.id FROM DUAL;
END;
/

-- Insert nuevo table PHONECC
INSERT INTO PHONECC (idCallCenter, countryCode, phoneNumber, createDate, updateDate)
 SELECT 1, '+57', '3113554267', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 2, '+57', '3103464063', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 3, '+57', '3133444160', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 4, '+57', '3143478511', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 5, '+57', '3153423509', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 6, '+57', '3163454522', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 7, '+57', '3173184555', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 8, '+57', '3183704521', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 9, '+57', '3123496543', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 10, '+57', '3203434510', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 11, '+57', '3213414599', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 12, '+57', '3223164572', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual;

/*--Creaci�n COUNTRY--*/
-- Create nuevo table COUNTRY : id, name, createDate, updateDate
CREATE TABLE COUNTRY(
	id NUMBER(10) NOT NULL,
	name VARCHAR2(200 CHAR) NOT NULL,
	createDate TIMESTAMP(0) NOT NULL,
    updateDate TIMESTAMP(0) NULL,
	CONSTRAINT PK_COUNTRY PRIMARY KEY (id)
);

-- Generate ID using sequence and trigger
CREATE SEQUENCE COUNTRY_seq START WITH 1 INCREMENT BY 1;

CREATE OR REPLACE TRIGGER COUNTRY_seq_tr
 BEFORE INSERT ON COUNTRY FOR EACH ROW
 WHEN (NEW.id IS NULL)
BEGIN
 SELECT COUNTRY_seq.NEXTVAL INTO :NEW.id FROM DUAL;
END;
/

-- Insert nuevo table COUNTRY
INSERT INTO COUNTRY (name, createDate, updateDate) VALUES
('Colombia', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL);

/*--Creaci�n CITY--*/
-- Create nuevo table CITY : id, idCountry, name, createDate, updateDate
CREATE TABLE CITY(
	id NUMBER(10) NOT NULL,
	idCountry NUMBER(10) NOT NULL,
	name VARCHAR2(200 CHAR) NOT NULL,	
	createDate TIMESTAMP(0) NOT NULL,
    updateDate TIMESTAMP(0) NULL,
	CONSTRAINT PK_CITY PRIMARY KEY (id),
	CONSTRAINT FK_CITY_COUNTRY FOREIGN KEY (idCountry) REFERENCES COUNTRY(id)
);

-- Generate ID using sequence and trigger
CREATE SEQUENCE CITY_seq START WITH 1 INCREMENT BY 1;

CREATE OR REPLACE TRIGGER CITY_seq_tr
 BEFORE INSERT ON CITY FOR EACH ROW
 WHEN (NEW.id IS NULL)
BEGIN
 SELECT CITY_seq.NEXTVAL INTO :NEW.id FROM DUAL;
END;
/

-- Insert nuevo table CITY
INSERT INTO CITY (idCountry, name, createDate, updateDate)
 SELECT 1, 'Santafé de Bo;tá', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Cali', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Medellín', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Barranquilla', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Cartagena', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Cúcuta', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Bucaramanga', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Ibagué', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Pereira', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Santa Marta', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Manizales', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Bello', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Pasto', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Neiva', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Soledad', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Armenia', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Villavicencio', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Soacha', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Valledupar', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Montería', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Itagüí', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Palmira', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Buenaventura', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Floridablanca', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Sincelejo', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Popayán', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Barrancabermeja', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Dos Quebradas', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Tuluá', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Envigado', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Carta;', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Girardot', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Buga', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Tunja', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Florencia', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Maicao', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Sogamoso', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Giron', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 1, 'Otra', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual;

/*--Creaci�n DOCUMENTTYPE--*/
-- Create nuevo table DOCUMENTTYPE : id, name, description, createDate, updateDate
CREATE TABLE DOCUMENTTYPE(
	id NUMBER(10) NOT NULL,
	name VARCHAR2(5 CHAR) NOT NULL,
	description VARCHAR2(200 CHAR) NOT NULL,
	createDate TIMESTAMP(0) NOT NULL,
    updateDate TIMESTAMP(0) NULL,
	CONSTRAINT PK_DOCUMENTTYPE PRIMARY KEY (id)
);

-- Generate ID using sequence and trigger
CREATE SEQUENCE DOCUMENTTYPE_seq START WITH 1 INCREMENT BY 1;

CREATE OR REPLACE TRIGGER DOCUMENTTYPE_seq_tr
 BEFORE INSERT ON DOCUMENTTYPE FOR EACH ROW
 WHEN (NEW.id IS NULL)
BEGIN
 SELECT DOCUMENTTYPE_seq.NEXTVAL INTO :NEW.id FROM DUAL;
END;
/

-- Insert nuevo table DOCUMENTTYPE
INSERT INTO DOCUMENTTYPE (name, description, createDate, updateDate)
 SELECT 'CC', 'Cédula de ciudadanía', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 'CE', 'Cédula de extranjería', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual
 UNION ALL

 SELECT 'TI', 'Tarjeta de identidad', TO_DATE('2021-10-11', 'YYYY-MM-DD'), NULL FROM dual;

/*--Creaci�n CUSTOMER--*/
-- Create nuevo table CUSTOMER : id, idDocumentType, document, fullName, countryCode, phoneNumber
--                                   dateOfBirth, idCity, idPhoneCC, createDate, updateDate
CREATE TABLE CUSTOMER(
	id NUMBER(19) NOT NULL,
	idDocumentType NUMBER(10) NOT NULL,
	document VARCHAR2(20 CHAR) NOT NULL,
	fullName VARCHAR2(200 CHAR) NOT NULL,
	countryCode VARCHAR2(5 CHAR) NOT NULL,
	phoneNumber VARCHAR2(15 CHAR) NOT NULL,
	dateOfBirth DATE NOT NULL,
	idCity NUMBER(10) NOT NULL,
	idPhoneCC NUMBER(10) NOT NULL,
	createDate TIMESTAMP(0) NOT NULL,
    updateDate TIMESTAMP(0) NULL,
	CONSTRAINT PK_CUSTOMER PRIMARY KEY (id),
	CONSTRAINT FK_CUSTOMER_DOCUMENTTYPE FOREIGN KEY (idDocumentType) REFERENCES DOCUMENTTYPE(id),
	CONSTRAINT FK_CUSTOMER_CITY FOREIGN KEY (idCity) REFERENCES CITY(id),
	CONSTRAINT FK_CUSTOMER_PHONECC FOREIGN KEY (idPhoneCC) REFERENCES PHONECC(id)
);

-- Generate ID using sequence and trigger
CREATE SEQUENCE CUSTOMER_seq START WITH 1 INCREMENT BY 1;

CREATE OR REPLACE TRIGGER CUSTOMER_seq_tr
 BEFORE INSERT ON CUSTOMER FOR EACH ROW
 WHEN (NEW.id IS NULL)
BEGIN
 SELECT CUSTOMER_seq.NEXTVAL INTO :NEW.id FROM DUAL;
END;
/

/*--Creaci�n PQR--*/
-- Create nuevo table PQR : id, idCustomer, subject, description, createDate, updateDate
CREATE TABLE PQR(
	id NUMBER(19) NOT NULL,
	idCustomer NUMBER(19) NOT NULL,
	subject VARCHAR2(200 CHAR) NOT NULL,
	description CLOB NOT NULL,
	createDate TIMESTAMP(0) NOT NULL,
    updateDate TIMESTAMP(0) NULL,
	CONSTRAINT PK_PQR PRIMARY KEY (id),
	CONSTRAINT FK_PQR_CUSTOMER FOREIGN KEY (idCustomer) REFERENCES CUSTOMER(id)
);

-- Generate ID using sequence and trigger
CREATE SEQUENCE PQR_seq START WITH 1 INCREMENT BY 1;

CREATE OR REPLACE TRIGGER PQR_seq_tr
 BEFORE INSERT ON PQR FOR EACH ROW
 WHEN (NEW.id IS NULL)
BEGIN
 SELECT PQR_seq.NEXTVAL INTO :NEW.id FROM DUAL;
END;
/

/*------------------------------------------------------------------------------------------------------------------*/