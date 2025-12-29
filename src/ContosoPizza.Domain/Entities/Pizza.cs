namespace ContosoPizza.Domain.Entities;

public class Pizza
{
    // Construtor privado para EF Core
    private Pizza() { }

    // Construtor público para criação de entidade
    public Pizza(string nome, string descricao, decimal preco, string tamanho)
    {

        // Validações Explicitas
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome da pizza não pode ser vazio.", nameof(nome));

        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição da pizza é obrigatória.", nameof(descricao));

        if (preco <= 0)
            throw new ArgumentException("O preço da pizza deve ser maior que zero.", nameof(preco));

        if (string.IsNullOrWhiteSpace(tamanho))
            throw new ArgumentException("O tamanho da pizza é obrigatório.", nameof(tamanho));

        // validaçnao avançado de tamanho
        var tamanhosValidos = new List<string> { "Pequena", "Média", "Grande", "Família" };
        if (!tamanhosValidos.Contains(tamanho))
            throw new ArgumentException($"Tamanho inválido. Valores aceitos: {string.Join(", ", tamanhosValidos)}", nameof(tamanho));


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

    public string Nome {get; private set;} = string.Empty;
    public string Descricao {get; private set;} = string.Empty;

    public decimal Preco {get; private set;}
    public string Tamanho {get; private set;} = string.Empty;
    public bool Ativa {get; private set;}

    public DateTime DataCriacao {get; private set;}
    public DateTime DataAtualizacao {get; private set;}



    // Métodos de negócio (encapsulamento)
    public void Atualizar(string nome, string descricao, decimal preco, string tamanho)
    {

        // Reutilizar mesmas validações do construtor
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome da pizza não pode ser vazio.", nameof(nome));

        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição da pizza é obrigatória.", nameof(descricao));

        if (preco <= 0)
            throw new ArgumentException("O preço da pizza deve ser maior que zero.", nameof(preco));
        
        if (string.IsNullOrWhiteSpace(tamanho))
            throw new ArgumentException("O tamanho da pizza é obrigatório.", nameof(tamanho));

        var tamanhosValidos = new List<string> { "Pequena", "Média", "Grande", "Família" };
        if (!tamanhosValidos.Contains(tamanho))
            throw new ArgumentException($"Tamanho inválido. Valores aceitos: {string.Join(", ", tamanhosValidos)}", nameof(tamanho));


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


// commit para test seguindo o git flow