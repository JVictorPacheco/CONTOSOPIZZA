using ContosoPizza.Application.DTOs.Pizza;
using ContosoPizza.Application.Services.Interfaces;
using ContosoPizza.Domain.Entities;
using ContosoPizza.Domain.Interfaces;




namespace ContosoPizza.Application.Services;


// PizzaService IMPLEMENTA IPizzaService
// PizzaService USA IPizzaRepository

public class PizzaService : IPizzaService // Implementação da interface IPizzaService
{
    private readonly IPizzaRepository _pizzaRepository; // Dependência do repositório de pizza


    public PizzaService(IPizzaRepository pizzaRepository) // Construtor para injeção de dependência
    {
        _pizzaRepository = pizzaRepository;
    }

    public async Task<PizzaResponse> CriarPizzaAsync (CreatePizzaRequest request, CancellationToken cancellationToken)
    {
        var pizzaExiste = await _pizzaRepository.ExistePizzaPorNomeAsync(request.Nome, cancellationToken);

        if (pizzaExiste)
                throw new ArgumentException("Pizza com o mesmo nome já existe.");
        

        var pizza = new Pizza
        {
            PizzaId = Guid.NewGuid(),
            Nome = request.Nome,
            Descricao = request.Descricao,
            Preco = request.Preco,
            Tamanho = request.Tamanho,
            Ativa = true,
            DataCriacao = DateTime.UtcNow
        };

        await _pizzaRepository.AdicionarAsync(pizza, cancellationToken);

        return MapToPizzaResponse(pizza);
    }
 





}