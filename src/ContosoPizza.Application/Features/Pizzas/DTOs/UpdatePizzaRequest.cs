namespace ContosoPizza.Application.Features.Pizzas.DTOs;


namespace ContosoPizza.Application.DTOs.Pizza;

/// <summary>
/// DTO para atualização de uma pizza existente
/// </summary>
/// <param name="Nome">Nome da pizza (obrigatório, máx 100 caracteres)</param>
/// <param name="Descricao">Descrição da pizza (obrigatório, máx 500 caracteres)</param>
/// <param name="Preco">Preço da pizza (obrigatório, maior que zero)</param>
/// <param name="Tamanho">Tamanho da pizza: Pequena, Média, Grande ou Família</param>
public record UpdatePizzaRequest(
        string Nome,
        string Descricao,
        PizzaStyle Estilo,
        decimal Preco,
        string Tamanho
    );
