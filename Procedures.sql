USE VendasGeek
GO

create procedure sp_AddCarrinho
(
	@PedidoId	int,	@ClienteId	int
)
as
begin
	insert into Carrinho (IdCarrinho, PedidoId, ClienteId)
	values	(@@IDENTITY, @PedidoId, @ClienteId)
end
go

CREATE PROCEDURE AdicionarProdutoAoPedido
    @IdUsuario INT,
    @IdProduto INT,
    @Qtd INT
AS
BEGIN
    DECLARE @PrecoProduto DECIMAL(18, 2);
    DECLARE @IdPedido INT;

    -- Verificar se o usu�rio possui um pedido em aberto
    SELECT @IdPedido = IdPedido
    FROM Pedidos
    WHERE UserId = @IdUsuario AND StatusPedido = 'A pagar';

    -- Se n�o houver um pedido em aberto, criar um novo
    IF @IdPedido IS NULL
    BEGIN
        INSERT INTO Pedidos (UserId, DataPedido, StatusPedido)
        VALUES (@IdUsuario, GETDATE(), 'A pagar');

        SET @IdPedido = SCOPE_IDENTITY();
    END

    -- Obter o pre�o do produto
    SELECT @PrecoProduto = Preco
    FROM Produtos
    WHERE IdProduto = @IdProduto;

    -- Verificar se o produto existe
    IF @PrecoProduto IS NULL
    BEGIN
        PRINT 'Produto n�o encontrado.';
        RETURN;
    END

    -- Adicionar o produto ao pedido na tabela de jun��o PedidosProdutos
    INSERT INTO PedidosProdutos (IdPedido, IdProduto, Qtd, Preco, TotalPedido)
    VALUES (@IdPedido, @IdProduto, @Qtd, @PrecoProduto, @Qtd * @PrecoProduto);

	UPDATE Produtos
    SET Quantidade = Quantidade - @Qtd
    WHERE IdProduto = IdProduto;

    PRINT 'Produto adicionado ao pedido com sucesso.';
END;
GO

CREATE PROCEDURE ExcluirPedidoEProdutos
    @IdPedido INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Excluir produtos associados ao pedido na tabela PedidosProdutos
        DELETE FROM PedidosProdutos WHERE IdPedido = @IdPedido;

        -- Excluir o pedido
        DELETE FROM Pedidos WHERE IdPedido = @IdPedido;

        COMMIT;
        PRINT 'Pedido e produtos exclu�dos com sucesso.';
    END TRY
    BEGIN CATCH
        ROLLBACK;
        PRINT 'Ocorreu um erro ao excluir o pedido e produtos.';
    END CATCH;
END;
GO

CREATE PROCEDURE AtualizarQtdPedido
    @IdPedido INT,
    @IdProduto INT,
    @Quantidade INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Atualizar quantidade na tabela PedidosProdutos
        UPDATE PedidosProdutos
        SET Qtd = @Quantidade,
            TotalPedido = @Quantidade * Preco
        WHERE IdPedido = @IdPedido AND IdProduto = @IdProduto;

        -- Selecionar e retornar o novo valor total
        SELECT TotalPedido AS NovoValorTotal
        FROM PedidosProdutos
        WHERE IdPedido = @IdPedido AND IdProduto = @IdProduto;

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        -- Adicione uma mensagem de depura��o para o erro
        PRINT 'Ocorreu um erro ao atualizar a quantidade e o valor total.';
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
GO

CREATE PROCEDURE AtualizarStatusPagamentoRealizado
    @UserId INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Atualizar status na tabela Pedidos
        UPDATE Pedidos
        SET StatusPedido = 'Pagamento Realizado'
        WHERE UserId = @UserId AND StatusPedido = 'A pagar'

        -- Verificar se algum registro foi atualizado
        IF @@ROWCOUNT > 0
        BEGIN
            COMMIT;
            PRINT 'Status do pedido atualizado para Pagamento Realizado.';
        END
        ELSE
        BEGIN
            ROLLBACK;
            PRINT 'Nenhum pedido encontrado para o usu�rio especificado.';
        END
    END TRY
    BEGIN CATCH
        ROLLBACK;
        -- Adicione uma mensagem de depura��o para o erro
        PRINT 'Ocorreu um erro ao atualizar o status do pedido.';
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
GO


CREATE PROCEDURE CadastrarUsuarioCliente
    @UserName VARCHAR(40),
    @Senha VARCHAR(12),
    @NomeCliente VARCHAR(50),
    @SobrenomeCliente VARCHAR(50),
    @CPFCliente VARCHAR(14),
    @TelefoneCliente VARCHAR(14)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Inserir o usu�rio na tabela Users
        INSERT INTO Users (Name, Senha)
        VALUES (@UserName, @Senha);

        -- Obter o ID do usu�rio rec�m-inserido
        DECLARE @UserId INT;
        SET @UserId = SCOPE_IDENTITY();

        -- Inserir o cliente na tabela Clientes com refer�ncia ao usu�rio
        INSERT INTO Clientes (IdCliente, Nome, Sobrenome, CPF, Telefone)
        VALUES (@UserId, @NomeCliente, @SobrenomeCliente, @CPFCliente, @TelefoneCliente);

        -- Commit da transa��o
        COMMIT;
        
        PRINT 'Cadastro realizado com sucesso.';
    END TRY
    BEGIN CATCH
        ROLLBACK;
        -- Adicione uma mensagem de depura��o para o erro
        PRINT 'Ocorreu um erro ao cadastrar usu�rio e cliente.';
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
GO

CREATE PROCEDURE CadastrarEndereco
    @Rua VARCHAR(50),
    @Numero INT,
    @Bairro VARCHAR(100),
    @Cidade VARCHAR(100),
    @Estado VARCHAR(20),
    @CEP VARCHAR(14),
    @UserId INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Inserir o endere�o na tabela Endereco
        INSERT INTO Endereco (Rua, Numero, Bairro, Cidade, Estado, CEP, UserId)
        VALUES (@Rua, @Numero, @Bairro, @Cidade, @Estado, @CEP, @UserId);

        -- Commit da transa��o
        COMMIT;
        
        PRINT 'Endere�o cadastrado com sucesso.';
    END TRY
    BEGIN CATCH
        ROLLBACK;
        -- Adicione uma mensagem de depura��o para o erro
        PRINT 'Ocorreu um erro ao cadastrar o endere�o.';
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
GO

EXEC AdicionarProdutoAoPedido @IdUsuario = 1, @IdProduto = 4, @Qtd = 30

EXEC ExcluirPedidoEProdutos @IdPedido = 11

EXEC AtualizarQtdPedido @IdPedido = 1, @IdProduto = 4, @Quantidade = 7

EXEC AtualizarStatusPagamentoRealizado @IdPedido = 4

EXEC CadastrarUsuarioCliente  @UserName = 'Jos�Viana@email.com',  @Senha = 'Vianinha123',  @NomeCliente = 'Jos�', @SobrenomeCliente =  'Viana', @CPFCliente = '222.222.222-22', @TelefoneCliente = '179999-9999'

EXEC AdicionarProdutoAoPedido @IdUsuario = 4, @IdProduto = 4, @Qtd = 1

EXEC CadastrarEndereco @Rua = 'Santo Elias', @Numero = 2332, @Bairro = 'Jardim Save Salve',  @Cidade = 'S�o Inpiguari',  @Estado = 'SP',  @CEP = '15053500',  @UserId = 3

select * from VPedidosProdutos WHERE UserId = 3
go

select * from Endereco