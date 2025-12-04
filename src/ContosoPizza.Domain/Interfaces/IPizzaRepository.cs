using ContosoPizza.Domain.Entities;


namespace ContosoPizza.Domain.Interfaces;


public interface IPizzaRepository
{
    //Comandos (CUD - Create, Update, Delete
    Task<Pizza> AdicionarPizzaAsync (Pizza pizza, CancellationToken cancellationToken = default);
    Task<Pizza> AtualizarPizzaAsync (Pizza pizza, CancellationToken cancellationToken = default);
    Task<Pizza> ExcluirPizzaSuavementeAsync (Guid id, CancellationToken cancellationToken = default);

    // Consultas (Read)

    Task<Pizza?> ObterPizzaPorIdAsync (Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Pizza>> ObterTodasPizzasAsync (CancellationToken cancellationToken = default);
    Task<IEnumerable<Pizza>> ObterPizzasAtivasAsync (CancellationToken cancellationToken = default);
    Task<IEnumerable<Pizza>> ObterPizzaPorTamanhoAsync (string tamanho, CancellationToken cancellationToken = default);
    Task<Pizza?> ObterPizzaPorNomeAsync (string nome, CancellationToken cancellationToken = default);

    Task<bool> ExistePizzaAsync (Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistePizzaPorNomeAsync (string nome, CancellationToken cancellationToken = default); 


    // Persistencia 
    Task<int> SaveChangesAsync (CancellationToken cancellationToken = default);
}
