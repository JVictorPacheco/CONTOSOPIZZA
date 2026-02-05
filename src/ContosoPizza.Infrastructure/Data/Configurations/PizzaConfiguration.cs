using Microsoft.EntityFrameworkCore;
using ContosoPizza.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ContosoPizza.Infrastructure.Data.Configurations;


public class PizzaConfiguration : IEntityTypeConfiguration<Pizza>
{
    public void Configure(EntityTypeBuilder<Pizza> builder)
    {
        // Nome da tabela
        builder.ToTable("Pizzas");

        // Chave Primaria
        builder.HasKey(p => p.Id);

        // Configuração das propriedades
        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedNever(); // GUID gerado pela aplicação, não pelo banco

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.Preco)
            .IsRequired()
            .HasPrecision(10, 2); // Define precisão para valores decimais


        builder.Property(p => p.Tamanho)
            .IsRequired()
            .HasMaxLength(50);


        builder.Property(p => p.Ativa)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.DataCriacao)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP"); // PostgreSQL


        builder.Property(p => p.DataAtualizacao)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP"); // PostgreSQL


        // Índices para melhorar performance em consultas
        builder.HasIndex(p => p.Nome)
             .IsUnique(); // Nome único para evitar duplicatas


        builder.HasIndex(p => p.Ativa);


        builder.HasIndex(p => p.Tamanho);
    }
}