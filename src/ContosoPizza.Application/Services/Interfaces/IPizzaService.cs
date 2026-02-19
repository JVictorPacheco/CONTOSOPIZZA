using ContosoPizza.Application.DTOs.Pizza;


namespace ContosoPizza.Application.Services.Interfaces;


public interface IPizzaService
{
    // Comandos (Create, Update, Delete)
Task<PizzaResponse> CriarAsync(CreatePizzaRequest requestDto, CancellationToken cancellationToken = default);

Task<PizzaResponse?> AtualizarAsync (Guid id, UpdatePizzaRequest requestDto, CancellationToken cancellationToken = default);

Task<bool> RemoverAsync (Guid Id, CancellationToken cancellationToken = default);

Task<bool> AtivarAsync (Guid Id, CancellationToken cancellationToken = default);



  // Consultas (Read)
Task<PizzaResponse?> ObterPorIdAsync (Guid id, CancellationToken cancellationToken = default);

Task<IEnumerable<PizzaResponse>> ObterTodasAsync (CancellationToken cancellationToken = default);

Task<IEnumerable<PizzaResponse>> ObterAtivasAsync (CancellationToken cancellationToken = default);

Task<IEnumerable<PizzaResponse>> ObterPorTamanhoAsync (string tamanho, CancellationToken cancellationToken = default);

Task<PizzaResponse?> ObterPorNomeAsync (string nome, CancellationToken cancellationToken = default);


}