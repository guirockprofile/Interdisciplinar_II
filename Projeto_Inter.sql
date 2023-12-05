CREATE DATABASE VendasGeek
go

USE VendasGeek
go

Create table Users
(
	IdUser	int			not null								primary key identity,
	Name	varchar(40) not null,
	Senha	varchar(12) not null,
)
 go

 Create table Admin
 (
	IdAdm		int						not null				primary key		references	Users	(IdUser)
 )

Create table Clientes
(
	IdCliente	int						not null				primary key		references	Users	(IdUser),
	Nome		varchar(50)				not null,
	Sobrenome	varchar(50)				not null,
	CPF			varchar(14)				not null,
	Telefone	varchar(14)				not null,
)
go

Create table Endereco
(
	IdEndereco	int						not null				primary key		identity,
	Rua			varchar(50)				not null,
	Numero		int						not null,
	Bairro		varchar(100)			not null,
	Cidade		varchar(100)			not null,
	Estado		varchar(20)				not null,
	CEP			varchar(14)				not null,
	UserId	int							not null				references		Users	(IdUser)
)
go

Create table Categorias
(
	IdCategoria		int					not null				primary key		identity,
	NomeCategoria	varchar(200)		not null
)
go

-- Tabela de Pedidos
CREATE TABLE Pedidos
(
    IdPedido        int             not null        primary key     identity,
    UserId          int             not null        references Users(IdUser),
    DataPedido      datetime        not null,
    StatusPedido    varchar(50)     not null
);

-- Tabela de Produtos
CREATE TABLE Produtos
(
    IdProduto       int             not null        primary key     identity,
    NomeProd        varchar(50)     not null,
    Descricao       varchar(200)    not null,
    Preco           decimal(18,2)   not null,
    Quantidade      int             not null,
    Status          varchar(20)     not null,
    CategoriaId     int             not null        references Categorias(IdCategoria),
	Imagem			varchar(max)	null
);

-- Tabela de junção PedidosProdutos para representar a relação N:N
CREATE TABLE PedidosProdutos
(
    IdPedido        int             not null        references Pedidos(IdPedido),
    IdProduto       int             not null        references Produtos(IdProduto),
    Qtd             int             not null,
    Preco           decimal(18,2)   not null,
    TotalPedido     decimal(18,2)   not null,
    primary key (IdPedido, IdProduto)
);


Create table Carrinho
(
	IdCarrinho	int						not null				primary key		identity,
	PedidoId	int						not null				references	Pedidos		(IdPedido),
	ClienteId   int						not null				references	Clientes	(IdCliente)
)
go



--drop table Users

insert into Users values('VendasGeek@email.com','vendas123')
go

insert into Admin values (1)
go

select * from  Users

insert into Categorias values ('Infantil')
insert into Categorias values ('Video Games')
insert into Categorias values ('Eletrônicos')
go

/*
drop database VendasGeek
go
*/

select * from Categorias
go

-- Inserir produtos na tabela Produtos
--INSERT INTO Produtos values ('Estatua do Batman', 'Um  boneco do lendário heóri da DC comics, ela tem 5 cm de altura, e articulada', 12, 50, 'Disponível', 1,null)

--INSERT INTO Produtos (NomeProd, Descricao, Preco, Quantidade, Status, Imagem, CategoriaId)
--VALUES ('Estatua do Superman', 'Um boneco do herói mais poderoso da DC comics, articulado, 5 cm de altura', 15, 30, 'Disponível', 1,null);

--INSERT INTO Produtos (NomeProd, Descricao, Preco, Quantidade, Status, Imagem, CategoriaId)
--VALUES ('Máscara do Homem-Aranha', 'Réplica da máscara usada pelo Homem-Aranha, ideal para cosplay', 10, 20, 'Disponível', 2, null);

--INSERT INTO Produtos (NomeProd, Descricao, Preco, Quantidade, Status, Imagem, CategoriaId)
--VALUES ('Sabre de Luz Jedi', 'Réplica do sabre de luz usado pelos Jedi em Star Wars', 25, 15, 'Disponível', 3, null);

--GO

insert into Users values('ClienteTeste@email.com','Cli456')
insert into Clientes values (2,'José', 'da Silva', '111.111.111-11', '1799777777')
go

select * from Clientes
go

