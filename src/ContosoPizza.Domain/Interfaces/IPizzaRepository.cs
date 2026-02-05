using ContosoPizza.Domain.Entities;


namespace ContosoPizza.Domain.Interfaces;


public interface IPizzaRepository
{
    //Comandos (CUD - Create, Update, Delete
    Task<Pizza> AdicionarAsync (Pizza pizza, CancellationToken cancellationToken = default);
    Task AtualizarAsync (Pizza pizza, CancellationToken cancellationToken = default);
    Task RemoverAsync (Guid id, CancellationToken cancellationToken = default);

    // Consultas (Read)

    Task<Pizza?> ObterPorIdAsync (Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Pizza>> ObterTodasAsync (CancellationToken cancellationToken = default);
    Task<IEnumerable<Pizza>> ObterAtivasAsync (CancellationToken cancellationToken = default);
    Task<IEnumerable<Pizza>> ObterPorTamanhoAsync (string tamanho, CancellationToken cancellationToken = default);
    Task<Pizza?> ObterPorNomeAsync (string nome, CancellationToken cancellationToken = default);

    Task<bool> ExisteAsync (Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistePorNomeAsync (string nome, CancellationToken cancellationToken = default); 

    // Persistencia 
    Task<int> SaveChangesAsync (CancellationToken cancellationToken = default);
}
