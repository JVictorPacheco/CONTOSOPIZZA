using Microsoft.EntityFrameworkCore;
using ContosoPizza.Domain.Interfaces;
using ContosoPizza.Domain.Entities;
using ContosoPizza.Infrastructure.Data;


namespace ContosoPizza.Infrastructure.Repositories;


public class PizzaRepository : IPizzaRepository
{
    private readonly AppDbContext _context; // Aqui estamos criando uma variável de contexto
    private readonly Dbset<Pizza> _dbSet; // Aqui estamos criando uma variável do tipo DbSet para a entidade Pizza

    //Constructor, no construtor estamos injetando o ApplicationDbContext
    public PizzaRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context)); // Aqui estamos atribuindo o contexto injetado à variável _context
        _dbSet = _context.Set<Pizza>(); // Aqui estamos inicializando a variável _dbSet com o DbSet da entidade Pizza
    }

    // ==================== COMANDOS (CUD) ====================

    public async Task<Pizza> AdicionarBeneficiarioAsync(Pizza pizza, CancellationToken cancellationToken = default)
    {
        if (pizza == null)
            throw new ArgumentNullException(nameof(pizza));

            await _dbSet.addAsync(pizza, cancellationToken);
            return pizza;
    }

    public async Task<Pizza> AtualizarBeneficiarioAsync(Pizza pizza, CancellationToken cancellationToken = default)
    {
        if (pizza == null)
            throw new ArgumentNullExeception(nameof(pizza));  // Se for nulo, lança uma exceção. nameof retorna o nome do parâmetro

            await _dbSet.Update(pizza); // Atualiza a entidade pizza no DbSet
            return Task.CompletedTask; // Retorna uma tarefa concluída
    }

    public async Task<Pizza> RemoverBeneficiarioAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var pizza = await ObterPizzaPorIdAsync(id, cancellationToken); // Obtém a pizza pelo id
        
        if (pizza == null)
            throw new InvalidOperationException($"Pizza com ID {id} não encontrada.");
    
        //soft delete - apenas desativa
        pizza.Desativar();
        await AtualizarAsync(pizza, cancellationToken);

        // Hard Delete - remove fisicamente (descomente se preferir)
        // _dbSet.Remove(pizza);
    
    }

        // ==================== CONSULTAS (READ) ====================

    public async Task<Pizza?> ObterPizzaPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking() // Não rastreia a entidade para melhorar o desempenho
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken); // Retorna a primeira pizza que corresponde ao id ou nulo se não encontrar
    }

    public async Task<IEnumerable<Pizza>> ObterTodasPizzasAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking() // Não rastreia as entidades para melhorar o desempenho, porque são apenas para leitura
            .OrderBy(p => p.Nome)
            .ToListAsync(cancellationToken);
    }


    public async Task<IEnumerable<Pizza>> ObterPizzasAtivasAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(p => p.Ativa) // Filtra apenas as pizzas ativas
            .OrderBy(p => p.Nome)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Pizza>> ObterPizzasPorTamanhoAsync(string tamanho, CancellationToken cancellationToken = default)
    {

        if (string.IsNullOrWhiteSpace(tamanho))
            throw new ArgumentException("O tamanho não pode ser nulo ou vazio.", nameof(tamanho)); // Valida o parâmetro tamanho

        return await _dbSet
            .AsNoTracking()
            .Where(p => p.Tamanho == tamanho && p.Ativa) // Filtra pelo tamanho e apenas pizzas ativas
            .OrderBy(p => p.Nome)
            .ToListAsync(cancellationToken);
    }

    public async Task<Pizza?> ObterPizzaPorNomeAsync(string nome, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(nome)); // Valida o parâmetro nome

        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Nome == nome, cancellationToken); // Retorna a primeira pizza que corresponde ao nome ou nulo se não encontrar
    }

        // ==================== VERIFICAÇÕES ====================

    public async Task<bool> PizzaExisteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .AnyAsync(p => p.Id == id, cancellationToken); // Verifica se existe alguma pizza com o id fornecido
    }
    

       // ==================== PERSISTENCIA ====================

       public async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default) // Método para salvar as alterações no banco de dados
     {
            return await _context.SaveChangesAsync(cancellationToken);  // Salva as alterações no contexto. Nessa linha estamos chamando o método SaveChangesAsync do DbContext para persistir as alterações no banco de dados.
     }

}

