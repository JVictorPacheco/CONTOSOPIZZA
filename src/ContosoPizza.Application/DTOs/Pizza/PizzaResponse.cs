namespace ContosoPizza.Application.DTOs.Pizza;


/// <summary>
/// DTO de resposta com dados da pizza
/// </summary>
/// <param name="Id">Identificador único da pizza</param>
/// <param name="Nome">Nome da pizza</param>
/// <param name="Descricao">Descrição da pizza</param>
/// <param name="Preco">Preço da pizza</param>
/// <param name="Tamanho">Tamanho da pizza</param>
/// <param name="Ativa">Indica se a pizza está ativa</param>
/// <param name="DataCriacao">Data de criação</param>
/// <param name="DataAtualizacao">Data da última atualização</param>

public record PizzaResponse(
    Guid Id,
    string Nome,
    string Descricao,
    decimal Preco,
    string Tamanho,
    bool Ativa,
    DateTime DataCriacao,
    DateTime? DataAtualizacao
);