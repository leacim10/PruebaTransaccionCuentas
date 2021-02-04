CREATE DATABASE DB_TRANSACCIONES_CCMR
GO
USE DB_TRANSACCIONES_CCMR
GO
CREATE TABLE CUENTA
(
	nro_cuenta NVARCHAR(14) UNIQUE not null,
	tipo char(3) not null,
	moneda char(3) not null,
	nombre NVARCHAR(40) not null,
	saldo DECIMAL(12,2) not null,
)
GO
CREATE TABLE MOVIMIENTO
(
	fecha DATETIME not null,
	nro_cuenta NVARCHAR(14) not null,
	tipo CHAR(1) not null,
	importe DECIMAL(12,2) not null,
)
GO
ALTER TABLE MOVIMIENTO
ADD CONSTRAINT FK_Movimiento_Cuenta
FOREIGN KEY (nro_cuenta) REFERENCES CUENTA(nro_cuenta)
	ON UPDATE CASCADE
	ON DELETE CASCADE

