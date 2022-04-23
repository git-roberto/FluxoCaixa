--CREATE DATABASE dbFluxoCaixa
--GO

USE dbFluxoCaixa
GO

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

INSERT INTO TipoLancamento VALUES ('Crédito', 1) 
GO

INSERT INTO TipoLancamento VALUES ('Débito', 1) 
GO