using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace ContosoPizza.Domain.Entities;

public class Pizza
{
    // Construtor privado para EF Core
    private Pizza() { }

    // Construtor público para criação de entidade
    public Pizza(string nome, string descricao, decimal preco, string tamanho)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
        Preco  = preco;
        Tamanho = tamanho;
        Ativa   = true;
        DataCriacao = DateTime.UtcNow;
        DataAtualizacao = DateTime.UtcNow;
    }


    // Propriedades
    public Guid Id {get; private set;}

    [Required]
    public string Nome {get; private set;}
    public string Descricao {get; private set;}

    public decimal Preco {get; private set;}
    public string Tamanho {get; private set;}
    public bool Ativa {get; private set;}

    public DateTime DataCriacao {get; private set;}
    public DateTime DataAtualizacao {get; private set;}


    // Métodos de negócio (encapsulamento)
    public void Atualizar(string nome, string descricao, decimal preco, string tamanho)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Tamanho = tamanho;
        DataAtualizacao = DateTime.UtcNow;
    }


    public void Ativar()
    {
        Ativa = true;
        DataAtualizacao = DateTime.UtcNow;
    }


    public void Desativar()
    {
        Ativa = false;
        DataAtualizacao = DateTime.UtcNow;
    }

    public void AtualizarPreco(decimal novoPreco)
    {
        if (novoPreco <= 0)
        {
            throw new ArgumentException("O preço deve ser maior que zero.", nameof(novoPreco));
        }

        Preco = novoPreco;
        DataAtualizacao = DateTime.UtcNow;
    }

}