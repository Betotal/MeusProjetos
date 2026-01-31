use master;
go
DROP DATABASE IF EXISTS SALOON;
GO
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'SALOON')
    CREATE DATABASE SALOON;
GO
USE SALOON;
GO

DROP TABLE IF EXISTS Usuario;
GO 
CREATE TABLE Usuario (
    ID int PRIMARY KEY IDENTITY(1,1),
    Login VARCHAR(15) NOT NULL,
    Senha VARCHAR(20) NOT NULL,
    Situacao INT NOT NULL 
);
GO

DROP TABLE IF EXISTS Cliente ;
GO 
CREATE TABLE Cliente (
    ID int PRIMARY KEY IDENTITY(1,1),
    Nome varchar(30) NOT NULL,
    Telefone VARCHAR(15),
    Aniversario DATE,
    IdUsuario int not null,
    CONSTRAINT FK_CLI_USUARIO FOREIGN KEY(IdUsuario)
        REFERENCES Usuario(ID)
        ON DELETE CASCADE
);
GO

DROP TABLE IF EXISTS ClienteObservacao;
GO 
CREATE TABLE ClienteObservacao (
    ID int PRIMARY KEY IDENTITY(1,1),
	IdCliente INT,
	Observacao VARCHAR(50) NOT NULL
    CONSTRAINT FK_CLI_OBSERVACAO FOREIGN KEY(IdCliente)
        REFERENCES Cliente(ID)
        ON DELETE CASCADE
);
GO

DROP TABLE IF EXISTS Profissional;
GO 
CREATE TABLE Profissional (
    ID int PRIMARY KEY IDENTITY(1,1),
    SalarioFixo DECIMAL(10,2) NOT NULL,
    PorcentagemComissao INT NOT NULL,
    IdCliente int NOT NULL,
    CONSTRAINT FK_PROF_CLIENTE FOREIGN KEY(IdCliente)
        REFERENCES Cliente(ID)
        ON DELETE CASCADE
);
GO

DROP TABLE IF EXISTS Acesso;
GO 
CREATE TABLE Acesso (
    ID int PRIMARY KEY IDENTITY(1,1),
	NivelAcesso INT
);
GO

DROP TABLE IF EXISTS UsuarioAcesso;
GO 
CREATE TABLE UsuarioAcesso (
    ID int PRIMARY KEY IDENTITY(1,1),
	IdUsuario int NOT NULL,
	IdAcesso int NOT NULL,
    CONSTRAINT FK_USU_ACESSO_USU FOREIGN KEY(IdUsuario)
        REFERENCES Usuario(ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_PROF_ACESSO_ACE FOREIGN KEY(IdAcesso)
        REFERENCES Acesso(ID)
        ON DELETE CASCADE
);
GO

DROP TABLE IF EXISTS Cargo;
GO 
CREATE TABLE Cargo (
    ID int PRIMARY KEY IDENTITY(1,1),
	Nome VARCHAR (30) NOT NULL
);
GO

DROP TABLE IF EXISTS ProfissionalCargo;
GO 
CREATE TABLE ProfissionalCargo (
    ID int PRIMARY KEY IDENTITY(1,1),
	IdProfissional int NOT NULL,
	IdCargo int NOT NULL,
    CONSTRAINT FK_PROF_CARGO_PROF FOREIGN KEY(IdProfissional)
        REFERENCES Profissional(ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_PROF_CARGO_CARG FOREIGN KEY(IdCargo)
        REFERENCES Cargo(ID)
        ON DELETE CASCADE
);
GO

DROP TABLE IF EXISTS Servico;
GO 
CREATE TABLE Servico (
    ID int PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(30) NOT NULL,
    PrecoInicial DECIMAL(10,2) NOT NULL,
    TempoMedio TIME NOT NULL,
    Situacao INT NOT NULL,
	TempoDeEncaixe TIME NOT NULL
);
GO

DROP TABLE IF EXISTS ServicoConjugado;
GO 
CREATE TABLE ServicoConjugado (
    ID int PRIMARY KEY IDENTITY(1,1),
	IdServicoPrincipal int NOT NULL,	
	IdServicoConjugado1 int NOT NULL,
	IdServicoConjugado2 int NOT NULL,
	PorcentagemDesconto1 INT,
	PorcentagemDesconto2 INT,
    CONSTRAINT FK_SERV_PRIN FOREIGN KEY(IdServicoPrincipal)
        REFERENCES Servico(ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_SERV_CONJ1 FOREIGN KEY(IdServicoConjugado1)
        REFERENCES Servico(ID),
    CONSTRAINT FK_SERV_CONJ2 FOREIGN KEY(IdServicoConjugado2)
        REFERENCES Servico(ID)
);
GO  

DROP TABLE IF EXISTS ProfissionalServico;
GO 
CREATE TABLE ProfissionalServico (
    ID int PRIMARY KEY IDENTITY(1,1),
	IdProfissional int NOT NULL,
	IdServico int NOT NULL,	
    CONSTRAINT FK_PROF_SERV_PROF FOREIGN KEY(IdProfissional)
        REFERENCES Profissional(ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_PROF_SERV_SERV FOREIGN KEY(IdServico)
        REFERENCES Servico(ID)
        ON DELETE CASCADE
);
GO

DROP TABLE IF EXISTS Horario;
GO 
CREATE TABLE Horario (
    ID int PRIMARY KEY IDENTITY(1,1),
    HorarioInicial TIME NOT NULL,
    HorarioFinal   TIME NOT NULL
);
GO 

DROP TABLE IF EXISTS ProfissionalHorario ;
GO 
CREATE TABLE ProfissionalHorario (
    ID int PRIMARY KEY IDENTITY(1,1),
	IdProfissional int NOT NULL,
	IdHorario int NOT NULL,	
    CONSTRAINT FK_PROF_HOR_PROF FOREIGN KEY(IdProfissional)
        REFERENCES Profissional(ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_PROF_HOR_HOR FOREIGN KEY(IdHorario)
        REFERENCES Horario(ID)
        ON DELETE CASCADE
);
GO

DROP TABLE IF EXISTS Promocao ;
GO 
CREATE TABLE Promocao (
    ID int PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(30) NOT NULL,
	PromocaoInicio DATE NOT NULL,
	PromocaoFinal DATE NOT NULL,
	IdServico INT NOT NULL,
	QuantidadeServico INT,
	PorcentagemDesconto INT,
	DiaSemanaInicial INT,
	DiaSemanaFinal  int,
	QuemPaga int,
    Situcao INT NOT NULL,
    CONSTRAINT FK_PROMO_SERVICO FOREIGN KEY(IdServico)
        REFERENCES Servico(ID)
        ON DELETE CASCADE	
);
GO 

DROP TABLE IF EXISTS Desconto ;
GO 
CREATE TABLE Desconto (
    ID int PRIMARY KEY IDENTITY(1,1),
    IdCliente INT,
	PorcentagemDesconto INT,
    CONSTRAINT FK_DESC_CLIENTE FOREIGN KEY(IdCliente)
        REFERENCES Cliente(ID)
        ON DELETE CASCADE	
);
GO 


DROP TABLE IF EXISTS ClientePromocao ;
GO 
CREATE TABLE ClientePromocao (
    ID int PRIMARY KEY IDENTITY(1,1),
    IdCliente INT NOT NULL,
    IdPromocao INT NOT NULL,
	QuantidadeDisponivel INT NOT NULL,
	PromocaoValidade DATE,
    CONSTRAINT FK_CLI_PROMO_CLI FOREIGN KEY(IdCliente)
        REFERENCES Cliente(ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_CLI_PROMO_PROMO FOREIGN KEY(IdPromocao)
        REFERENCES Promocao(ID)
        ON DELETE CASCADE	
);
GO 

DROP TABLE IF EXISTS Agenda;
GO 
CREATE TABLE Agenda (
    ID int PRIMARY KEY IDENTITY(1,1),
	AgendaData DATE NOT NULL,
	AgendaHora TIME NOT NULL,
	DiaSemana INT,
    IdProfissional INT NOT NULL,
	IdCliente INT NOT NULL,
	IdServico INT NOT NULL,
	IdClientePromocao INT, 
	AgendaSituacao INT,
    CONSTRAINT FK_AGE_PROF FOREIGN KEY(IdProfissional)
        REFERENCES Profissional(ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_AGE_CLI FOREIGN KEY(IdCliente)
        REFERENCES Cliente(ID),
    CONSTRAINT FK_AGE_SERVICO FOREIGN KEY(IdServico)
        REFERENCES Servico(ID),
    CONSTRAINT FK_CLI_PROMOCAO FOREIGN KEY(IdClientePromocao)
        REFERENCES ClientePromocao(ID)		
);
GO 

DROP TABLE IF EXISTS Pagamento;
GO  
CREATE TABLE Pagamento (
    ID int PRIMARY KEY IDENTITY(1,1),
    TipoPagamento INT NOT NULL,
    ValorPago DECIMAL(10,2) NOT NULL,
    DataPagamento DATE NOT NULL,
    IdAgenda INT NOT NULL,
    CONSTRAINT FK_PAG_AGE FOREIGN KEY(IdAgenda)
        REFERENCES Agenda(ID)
        ON DELETE CASCADE
);
GO      
DROP TABLE IF EXISTS Caixa;
GO  
CREATE TABLE Caixa (
    ID int PRIMARY KEY IDENTITY(1,1),
    DataAbertura DATE NOT NULL,
    HoraAbertura TIME NOT NULL,
    ValorAbertura DECIMAL(10,2) NOT NULL,
    DataFechamento DATE,
    HoraFechamento TIME,
    ValorFechamento DECIMAL(10,2),
    Situacao INT NOT NULL,
    IdUsuario INT NOT NULL,
    CONSTRAINT FK_CAI_USU FOREIGN KEY(IdUsuario)
        REFERENCES Usuario(ID)
        ON DELETE CASCADE
);
GO
DROP TABLE IF EXISTS CaixaMovimento;
GO  
CREATE TABLE CaixaMovimento (
    ID int PRIMARY KEY IDENTITY(1,1),
    TipoMovimento INT NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    Descricao VARCHAR(50),
    DataMovimento DATE NOT NULL,
    IdCaixa INT NOT NULL,
    CONSTRAINT FK_CAI_MOV_CAI FOREIGN KEY(IdCaixa)
        REFERENCES Caixa(ID)
        ON DELETE CASCADE
);  
GO
DROP TABLE IF EXISTS Fornecedor;
GO  
CREATE TABLE Fornecedor (
    ID int PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(30) NOT NULL,
    Telefone VARCHAR(14) NOT NULL,
    ProdutoServico VARCHAR(50) NOT NULL,
    IdUsuario INT NOT NULL,
    CONSTRAINT FK_FORN_USU FOREIGN KEY(IdUsuario)
        REFERENCES Usuario(ID)
        ON DELETE CASCADE
);  
GO
DROP TABLE IF EXISTS Produto;
GO  
CREATE TABLE Produto (
    ID int PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(30) NOT NULL,
    QuantidadeEstoque INT NOT NULL,
    PrecoCompra DECIMAL(10,2) NOT NULL,
    PrecoVenda DECIMAL(10,2) NOT NULL,
    IdFornecedor INT NOT NULL,
    CONSTRAINT FK_PROD_FORN FOREIGN KEY(IdFornecedor)
        REFERENCES Fornecedor(ID)
        ON DELETE CASCADE
);  
GO
DROP TABLE IF EXISTS ProdutoServico;
GO  
CREATE TABLE ProdutoServico (
    ID int PRIMARY KEY IDENTITY(1,1),
    IdProduto INT NOT NULL,
    IdServico INT NOT NULL,
    Quantidade INT NOT NULL,
    CONSTRAINT FK_PROD_SERV_PROD FOREIGN KEY(IdProduto)
        REFERENCES Produto(ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_PROD_SERV_SERV FOREIGN KEY(IdServico)
        REFERENCES Servico(ID)
        ON DELETE CASCADE
);
GO  


