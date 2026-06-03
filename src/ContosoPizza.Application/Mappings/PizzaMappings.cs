using ContosoPizza.Application.Features.Pizzas.DTOs;
using ContosoPizza.Domain.Entities;

namespace ContosoPizza.Application.Mappings;

public static class PizzaMappings
{
    public static PizzaResponse ToResponse(this Pizza pizza) =>
        new(
            pizza.Id,
            pizza.Nome,
            pizza.Descricao,
            pizza.Preco,
            pizza.Tamanho,
            pizza.Ativa,
            pizza.DataCriacao,
            pizza.DataAtualizacao
        );
}