using ContosoPizza.Application.DTOs.Pizza;


namespace ContosoPizza.Application.Services.Interfaces;


public interface IPizzaService
{
    // Comandos (Create, Update, Delete)
Task<PizzaResponse> CriarPizzaAsync(CreatePizzaRequest requestDto, CancellationToken cancellationToken = default);

Task<PizzaResponse?> AtualizarPizzaAsync (Guid id, UpdatePizzaRequest requestDto, CancellationToken cancellationToken = default);

Task<bool> RemoverPizzaAsync (Guid Id, CancellationToken cancellationToken = default);

Task<bool> AtivarAsync (Guid Id, CancellationToken cancellationToken = default);



  // Consultas (Read)
Task<PizzaResponse?> ObterPizzaPorIdAsync (Guid id, CancellationToken cancellationToken = default);

Task<IEnumerable<PizzaResponse>> ObterTodasPizzaAsync (CancellationToken cancellationToken = default);

Task<IEnumerable<PizzaResponse>> ObterPizzaAtivasAsync (CancellationToken cancellationToken = default);

Task<IEnumerable<PizzaResponse>> ObterPizzaPorTamanhoAsync (string tamanho, CancellationToken cancellationToken = default);

Task<PizzaResponse?> ObterPizzaPorNomeAsync (string nome, CancellationToken cancellationToken = default);


}