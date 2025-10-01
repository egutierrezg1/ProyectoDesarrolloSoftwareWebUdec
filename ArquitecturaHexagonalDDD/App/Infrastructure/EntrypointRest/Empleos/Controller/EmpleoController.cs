using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Port.In;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.CommonErrorHandler;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Empleos.Request;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Empleos.Response;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Empleos.Controller;

[ApiController]
[Route("api/[controller]")]
public class EmpleoController : ControllerBase
{
    private readonly ICreateEmpleoUseCase _createUseCase;
    private readonly IUpdateEmpleoUseCase _updateUseCase;
    private readonly IDeleteEmpleoUseCase _deleteUseCase;
    private readonly IGetEmpleoByIdUseCase _getByIdUseCase;
    private readonly IListEmpleosUseCase _listUseCase;
    private readonly IMapper _mapper;

    public EmpleoController(
        ICreateEmpleoUseCase createUseCase,
        IUpdateEmpleoUseCase updateUseCase,
        IDeleteEmpleoUseCase deleteUseCase,
        IGetEmpleoByIdUseCase getByIdUseCase,
        IListEmpleosUseCase listUseCase,
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
    public async Task<IActionResult> Create([FromBody] CreateEmpleoHttpRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = _mapper.Map<CreateEmpleoCommand>(request);
            var result = await _createUseCase.ExecuteAsync(command);
            var response = _mapper.Map<EmpleoHttpResponse>(result);

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
            var query = new GetEmpleoByIdQuery { Id = id };
            var result = await _getByIdUseCase.ExecuteAsync(query);
            if (result == null)
                return NotFound(new { message = "Empleo no encontrado" });

            var response = _mapper.Map<EmpleoHttpResponse>(result);
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
        [FromQuery] string? searchTerm = null,
        [FromQuery] string? categoria = null,
        [FromQuery] string? empresa = null,
        [FromQuery] string? nivel = null,
        [FromQuery] string? areaTrabajo = null,
        [FromQuery] decimal? minSueldo = null,
        [FromQuery] decimal? maxSueldo = null)
    {
        try
        {
            var query = new ListEmpleosQuery
            {
                Page = page,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                Categoria = categoria,
                Empresa = empresa,
                Nivel = nivel,
                AreaTrabajo = areaTrabajo,
                MinSueldo = minSueldo,
                MaxSueldo = maxSueldo
            };

            var result = await _listUseCase.ExecuteAsync(query);
            var response = _mapper.Map<EmpleoListHttpResponse>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmpleoHttpRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = _mapper.Map<UpdateEmpleoCommand>(request);
            command.Id = id;
            var result = await _updateUseCase.ExecuteAsync(command);
            var response = _mapper.Map<EmpleoHttpResponse>(result);
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
            var command = new DeleteEmpleoCommand { Id = id };
            await _deleteUseCase.ExecuteAsync(command);
            return NoContent();
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }
}

