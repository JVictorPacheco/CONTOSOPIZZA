using Microsoft.EntityFrameworkCore;
using ContosoPizza.Domain.Entities;


namespace ContosoPizza.Infrastructure.Data;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options) // Chama o construtor da classe base DbContext com as opções fornecidas
    {
    }

    public DbSet<Pizza> Pizzas {get; set;} = null!; // Isso serve para representar a tabela de Pizzas no banco de dados

    // Configurações adicionais do modelo podem ser feitas aqui
    protected override void OnModelCreating(ModelBuilder modelBuilder) // protected override void é usado para substituir um método da classe base DbContext
    {
        base.OnModelCreating(modelBuilder); // Chama o método OnModelCreating da classe base para garantir que qualquer configuração padrão seja aplicada

        // Aplicar todas as configurações do assembly atual
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    // Override do SaveChanges para auditoria automática (opcional)
    public override Task<int> SaveChangesAsync (CancellationToken cancellationToken = default)
    {
        // Aqui você pode adicionar lógica antes de salvar
        // Exemplo: atualizar DataAtualizacao automaticamente

        return base.SaveChangesAsync(cancellationToken);
    }


}