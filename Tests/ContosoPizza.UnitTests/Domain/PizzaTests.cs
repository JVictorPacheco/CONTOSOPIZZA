using ContosoPizza.Domain.Entities;



namespace ContosoPizza.UnitTests.Domain;


/// <summary>
/// Testes unitários para a entidade Pizza.
/// Seguindo o padrão AAA (Arrange, Act, Assert) e nomenclatura:
/// Metodo_Cenario_ResultadoEsperado
/// </summary>
public class PizzaTests
{
    // # region Construtor - Testes de sucesso


    [Fact]
    public void Construtor_DadosValidos_DeveCriarPizzaCorretamente()
    {
        //Arrange
        var nome = "Margherita";
        var descricao = "Pizza clássica com molho de tomate, mussarela e menjericão";
        var preco = 55.90m;
        var tamanho = "Grande";


        // Act
        var pizza = new Pizza(nome, descricao, preco, tamanho);


        // Assert
        Assert.Equal(nome, pizza.Nome);
        Assert.Equal(descricao, pizza.Descricao);
        Assert.Equal(preco, pizza.Preco);
        Assert.Equal(tamanho, pizza.Tamanho);
        Assert.True(pizza.Ativa);
        Assert.NotEqual(Guid.Empty, pizza.Id);

    }

    [Theory]
    [InlineData("Pequena")]
    [InlineData("Média")]
    [InlineData("Grande")]
    [InlineData("Família")]
    public void Construtor_TamanhoValido_DeveCriarPizza(string tamanhoValido)
    {
        //Arrange & Act
        var pizza = new Pizza("Banana Nevada", "Banana com canela e acuça caramelizada", 45.00m, tamanhoValido);


        // Assert
        Assert.Equal(tamanhoValido, pizza.Tamanho);
    }


    //#endregion

    //#region Construtor - Testes de Validação (Falha Esperada)



    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Construtor_NomeInvalido_DeveLancarArgumentException(string? nomeInvalido)
    {
        // Arrange & Act
        var exception = Assert.Throws<ArgumentException>(() =>
                new Pizza(nomeInvalido!, "Descrição valida", 50.0m, "Grande"));

        Assert.Equal("nome", exception.ParamName);
        Assert.Contains("O nome da pizza não pode ser vazio", exception.Message);
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Construtor_DescricaoInvalida_DeveLancarArgumentException(string? descricaoInvalida)
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
               new Pizza("Margheria", descricaoInvalida!, 50.0m, "Família"));


        Assert.Equal("descricao", exception.ParamName);
        Assert.Contains("A descrição da pizza é obrigatória", exception.Message);
    }



    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100.50)]
    public void Construtor_PrecoInvalido_DeveLancarArgumentException(decimal precoInvalido)
    {
        var exception = Assert.Throws<ArgumentException>(() =>
                new Pizza("Quatro Queijos", "Pizza com gorgonzola, queijo prato, queijo provolone e quejo mussarela", precoInvalido, "Grande"));

        
        Assert.Equal("preco", exception.ParamName);
        Assert.Contains("O preço da pizza deve ser maior que zero", exception.Message);
    }

    [Theory]
    [InlineData("Enorme")]
    [InlineData("Mini")]
    [InlineData("GG")]
    [InlineData("grande")] // Testando o case sensitive
    public void Construtor_TamanhoInvalido_DeveLancarArgumentException(string? tamanhoInvalido)
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
                new Pizza("Frango com cheddar", "Descrição valida", 70.0m, tamanhoInvalido));

        
        Assert.Equal("tamanho", exception.ParamName);
        Assert.Contains("Tamanho inválido. Valores aceitos: Pequena, Média, Grande, Família", exception.Message);
 
    }


    //#endregion


    //#region Método Desativar


    [Fact]
    public void Desativar_PizzaAtiva_DeveDesativarPizza()
    {
        // Arrange
        var pizza = new Pizza("Calabresa", "Pizza de calabresa", 42.00m, "Grande");

        // Act
        pizza.Desativar();

        //Assert
        Assert.False(pizza.Ativa);
    }


    //#endregion

    //#region Método Ativar


    [Fact]
    public void Ativar_PizzaDesativada_DeveAtivarPizza()
    {
        // Arrange
        var pizza = new Pizza("Calabresa", "Pizza de calabresa", 42.00m, "Grande");

        // Act
        pizza.Ativar();

        //Assert
        Assert.True(pizza.Ativa);
    }


    //#endregion

    //region Método Atualizar


    [Fact]
    public void Atualizar_DadosValidos_DeveAtualizarPizza()
    {
        //Arrange 
        var pizza = new Pizza("Pizza Original", "Descrição original", 40.00m, "Pequena");
        var novoNome = "Pizza Atualizado";
        var novaDescricao = "Descrição atualizada";
        var novoPreco = 55.0m;
        var novoTamanho = "Grande";



        // Act
        pizza.Atualizar(novoNome, novaDescricao, novoPreco, novoTamanho);


        // Assert
        Assert.Equal(novoNome, pizza.Nome);
        Assert.Equal(novaDescricao, pizza.Descricao);
        Assert.Equal(novoPreco, pizza.Preco);
        Assert.Equal(novoTamanho, pizza.Tamanho);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Atualizar_NomeInvalido_DeveLancarArgumentException(string? nomeInvalido)
    {
        //Arrange
        var pizza = new Pizza("Pizza Original", "Descrição original", 40.00m, "Pequena");


        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
                pizza.Atualizar(nomeInvalido!, "Nova descrição", 50.00m, "Média"));


        Assert.Equal("nome", exception.ParamName);
        Assert.Contains("O nome da pizza não pode ser vazio", exception.Message);
    }


    //#endregion

    //#region Método AtualizarPreco

    [Fact]
    public void AtualizarPreco_PrecoValido_DeveAtualizarPreco()
    {
        // Arrange
        var pizza = new Pizza("Pizza Teste", "Descrição", 40.00m, "Média");
        var novoPreco = 65.00m;

        // Act
        pizza.AtualizarPreco(novoPreco);

        // Assert
        Assert.Equal(novoPreco, pizza.Preco);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    [InlineData(-100.50)]
    public void AtualizarPreco_PrecoInvalido_DeveLancarArgumentException(decimal precoInvalido)
    {
        // Arrange
        var pizza = new Pizza("Pizza Teste", "Descrição", 40.00m, "Média");

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            pizza.AtualizarPreco(precoInvalido));

        Assert.Equal("novoPreco", exception.ParamName);
    }

    //#endregion

}

