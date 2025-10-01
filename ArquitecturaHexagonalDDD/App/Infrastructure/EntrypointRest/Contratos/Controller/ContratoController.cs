using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Port.In;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.CommonErrorHandler;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Contratos.Request;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Contratos.Response;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Contratos.Controller;

[ApiController]
[Route("api/[controller]")]
public class ContratoController : ControllerBase
{
    private readonly ICreateContratoUseCase _createUseCase;
    private readonly IUpdateContratoUseCase _updateUseCase;
    private readonly IDeleteContratoUseCase _deleteUseCase;
    private readonly IGetContratoByIdUseCase _getByIdUseCase;
    private readonly IListContratosUseCase _listUseCase;
    private readonly IMapper _mapper;

    public ContratoController(
        ICreateContratoUseCase createUseCase,
        IUpdateContratoUseCase updateUseCase,
        IDeleteContratoUseCase deleteUseCase,
        IGetContratoByIdUseCase getByIdUseCase,
        IListContratosUseCase listUseCase,
        IMapper mapper)
    {
        _createUseCase = createUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
        _getByIdUseCase = getByIdUseCase;
        _listUseCase = listUseCase;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContratoHttpRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = _mapper.Map<CreateContratoCommand>(request);
            var result = await _createUseCase.ExecuteAsync(command);
            var response = _mapper.Map<ContratoHttpResponse>(result);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var query = new GetContratoByIdQuery { Id = id };
            var result = await _getByIdUseCase.ExecuteAsync(query);
            if (result == null)
                return NotFound(new { message = "Contrato no encontrado" });

            var response = _mapper.Map<ContratoHttpResponse>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> List(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? empresa = null,
        [FromQuery] Guid? empleadoId = null,
        [FromQuery] DateTime? desde = null,
        [FromQuery] DateTime? hasta = null,
        [FromQuery] string? frecuenciaPago = null,
        [FromQuery] decimal? minMonto = null,
        [FromQuery] decimal? maxMonto = null)
    {
        try
        {
            var query = new ListContratosQuery
            {
                Page = page,
                PageSize = pageSize,
                Empresa = empresa,
                EmpleadoId = empleadoId,
                Desde = desde,
                Hasta = hasta,
                FrecuenciaPago = frecuenciaPago,
                MinMonto = minMonto,
                MaxMonto = maxMonto
            };

            var result = await _listUseCase.ExecuteAsync(query);
            var response = _mapper.Map<ContratoListHttpResponse>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContratoHttpRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = _mapper.Map<UpdateContratoCommand>(request);
            command.Id = id;
            var result = await _updateUseCase.ExecuteAsync(command);
            var response = _mapper.Map<ContratoHttpResponse>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var command = new DeleteContratoCommand { Id = id };
            await _deleteUseCase.ExecuteAsync(command);
            return NoContent();
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }
}

