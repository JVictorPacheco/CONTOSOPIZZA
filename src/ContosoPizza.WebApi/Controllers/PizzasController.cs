using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Domain.Entities;
using ContosoPizza.Domain.Interfaces;
using ContosoPizza.Application.DTOs.Pizza;




namespace ContosoPizza.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]

public class PizzasController : ControllerBase
{
    private readonly IPizzaRepository _repository; // Injeção de dependência do repositório. O readonly garante que a variável só possa ser atribuída no construtor.
    private readonly ILogger<PizzasController> _logger; // Injeção de dependência do logger


    // Constructor
    public PizzasController(IPizzaRepository repository, ILogger<PizzasController> logger)
    {
        _repository = repository;
        _logger = logger;
    }



    /// <summary>
    /// Retorna todas as pizzas ativas
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de pizzas</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PizzaResponse>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando todas as pizzas.");
        var pizzas = await _repository.ObterTodasAsync(cancellationToken);
        return Ok(pizzas.Select(p => MapToResponse(p)));

    }



    /// <summary>
    /// Retorna uma pizza específica por ID
    /// </summary>
    /// <param name="id">ID da pizza</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Pizza encontrada</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PizzaResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando pizza com ID: {PizzaId}", id);

        var pizza = await  _repository.ObterPorIdAsync(id, cancellationToken);


        if (pizza == null)
        {
            _logger.LogWarning("Pizza com ID: {PizzaId} não encontrada.", id);
            return NotFound(new { Message = "Pizza não encontrada." });
        }

        return Ok(MapToResponse(pizza));
    }




    /// <summary>
    /// Cria uma nova pizza
    /// </summary>
    /// <param name="request">Dados da pizza</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Pizza criada</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PizzaResponse>> Create ([FromBody] CreatePizzaRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Criando uma nova pizza: {Nome}", request.Nome);


        try
        {
            var pizza = new Pizza(request.Nome, request.Descricao, request.Preco, request.Tamanho);


            await _repository.AdicionarAsync(pizza, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Pizza criada com sucesso. ID: {PizzaId}", pizza.Id);

            var response = MapToResponse(pizza);
            return CreatedAtAction(nameof(GetById), new {Id = pizza.Id}, response);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Erro de validação ao criar pizza: {Message}", ex.Message);
            return BadRequest(new { Message = ex.Message });
        } 
    }


    /// <summary>
    /// Atualiza uma pizza existente
    /// </summary>
    /// <param name="id">ID da pizza</param>
    /// <param name="request">Dados atualizados</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>No content</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update (Guid id, [FromBody] UpdatePizzaRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Atualizando pizza com ID: {PizzaId}", id);

        var pizza = await _repository.ObterPorIdAsync(id, cancellationToken);

        if (pizza == null)
        {
            _logger.LogWarning("Pizza com ID: {PizzaId} não encontrada para atualização", id);
            return NotFound(new { Message = "Pizza não encontrada." });
        }

        try
        {
            pizza.Atualizar(request.Nome, request.Descricao, request.Preco, request.Tamanho);

            await _repository.AtualizarAsync(pizza, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);


            _logger.LogInformation("Pizza {PizzaId} atualizado com sucesso.", id);


            return NoContent();
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Erro de validação ao atualizar pizza {PizzaId}: {Message}", id, ex.Message); // Log de aviso com o ID da pizza e a mensagem de erro
            return BadRequest(new { Message = ex.Message });
        }
    }


    /// <summary>
    /// Desativa uma pizza (soft delete)
    /// </summary>
    /// <param name="id">ID da pizza</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>No content</returns>
    [HttpDelete("{id:guid}")] // Endpoint para desativar uma pizza
    [ProducesResponseType(StatusCodes.Status204NoContent)] // Resposta 204 No Content em caso de sucesso
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Resposta 404 Not Found
    public async Task<IActionResult> Delete (Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Desativando pizza com ID: {Pizzaid}", id);

        var pizza = await _repository.ObterPorIdAsync(id, cancellationToken);

        if (pizza == null)
        {
            _logger.LogWarning("Pizza com ID: {PizzaId} não encontrada para desativação.", id);
            return NotFound(new { Message = "Pizza não encontrada." });
        }

        try
        {
            pizza.Desativar();
            await _repository.AtualizarAsync(pizza, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Pizza {PizzaId} desativada com sucesso", id);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Erro ao desativar pizza com ID: {PizzaId}. Mensagem: {Message}", id, ex.Message);
            return BadRequest(new { Message = ex.Message });
        }
    }

    /// ============================================
    // Método privado de mapeamento
    // ============================================
    
    /// <summary>
    /// Mapeia entidade Pizza para DTO PizzaResponse
    /// </summary>
    private static PizzaResponse MapToResponse(Pizza pizza)
    {
        return new PizzaResponse(
            pizza.Id,
            pizza.Nome,
            pizza.Descricao,
            pizza.Preco,
            pizza.Tamanho,
            pizza.Ativa,
            pizza.DataCriacao,
            pizza.DataAtualizacao
        );
    }
}

