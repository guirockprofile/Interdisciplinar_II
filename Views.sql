USE VendasGeek
GO

CREATE VIEW VProdutos
AS
SELECT 
    P.IdProduto, P.NomeProd, P.Descricao, P.Preco, P.Quantidade, P.Status, CA.NomeCategoria, P.Imagem
FROM 
    Produtos P
INNER JOIN 
    Categorias CA ON P.CategoriaId = CA.IdCategoria;
GO

CREATE VIEW VEndereco
AS
SELECT 
    E.Rua, E.Numero, E.Bairro, E.Cidade, E.Estado, E.CEP, E.UserId
FROM 
    Endereco E
INNER JOIN
	Users U ON E.UserId = U.IdUser
GO

CREATE VIEW VPedidosProdutos
AS
SELECT 
   PP.IdPedido, PP.IdProduto, Ped.UserId, P.NomeProd, P.Descricao, C.NomeCategoria, PP.Preco, PP.Qtd, PP.TotalPedido, 
   Ped.DataPedido, Ped.StatusPedido
FROM 
    PedidosProdutos PP
INNER JOIN 
    Pedidos Ped ON PP.IdPedido = Ped.IdPedido
INNER JOIN 
    Produtos P ON PP.IdProduto = P.IdProduto
INNER JOIN 
    Categorias C ON P.CategoriaId = C.IdCategoria;
GO

CREATE VIEW VUsersClientes
AS
SELECT
	U.IdUser, Cli.Nome, Cli.Sobrenome, Cli.CPF, Cli.Telefone
FROM
	Users U
INNER JOIN
	Clientes Cli ON U.idUser = Cli.IdCliente
GO

CREATE VIEW VProdutosHome
AS
SELECT
	 P.NomeProd, P.Descricao, P.Preco, P.Quantidade, P.Status, CA.NomeCategoria
FROM
	Produtos P
INNER JOIN 
	Categorias CA ON IdProduto = CA.IdCategoria
GO

ALTER VIEW VCarrinho
AS
SELECT 
  Ped.UserId, PP.IdPedido, P.NomeProd, P.Descricao, PP.Preco, PP.Qtd, PP.TotalPedido, Ped.DataPedido, Ped.StatusPedido
FROM 
    PedidosProdutos PP, Pedidos Ped, Produtos P
WHERE 
    Ped.StatusPedido = 'A Pagar';
GO

SELECT * FROM VProdutos
GO

SELECT * FROM VEndereco
GO

SELECT * FROM VPedidosProdutos 
GO

SELECT * FROM VUsersClientes
go

select * from Users
go

SELECT * FROM VProdutosHome
GO

SELECT * FROM VCarrinho 
go