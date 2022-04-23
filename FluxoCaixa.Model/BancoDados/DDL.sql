--CREATE DATABASE dbFluxoCaixa
--GO

USE dbFluxoCaixa
GO

IF OBJECT_ID('Usuario') IS NOT NULL 
  DROP TABLE Usuario; 

IF OBJECT_ID('Lancamento') IS NOT NULL 
  DROP TABLE Lancamento; 

IF OBJECT_ID('TipoLancamento') IS NOT NULL 
  DROP TABLE TipoLancamento; 


CREATE TABLE TipoLancamento(
	IdTipoLancamento INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Nome VARCHAR(20) NOT NULL,
	Ativo BIT DEFAULT 1 NOT NULL
)
GO

CREATE TABLE Lancamento(
	IdLancamento INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	IdTipoLancamento INT NOT NULL,
	Valor MONEY NOT NULL,
	DataLancamento DATE NOT NULL,

	FOREIGN KEY (IdTipoLancamento) REFERENCES TipoLancamento (IdTipoLancamento)
)
GO

CREATE TABLE Usuario(
	IdUsuario INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Nome VARCHAR(200) NOT NULL,
	Email VARCHAR(250) NOT NULL UNIQUE,
	Senha VARCHAR(150) NOT NULL,
	Ativo BIT NOT NULL
)
GO

INSERT INTO TipoLancamento VALUES ('Crédito', 1) 
GO

INSERT INTO TipoLancamento VALUES ('Débito', 1) 
GO

INSERT INTO Usuario VALUES ('Roberto', 'roberto@roberto.inf.br', 'ec278a38901287b2771a13739520384d43e4b078f78affe702def108774cce24', 1)
GO