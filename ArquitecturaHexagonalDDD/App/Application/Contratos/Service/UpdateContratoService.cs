using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Response;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Contratos.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Contratos.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Application.Contratos.Service;

public class UpdateContratoService : IUpdateContratoUseCase
{
    private readonly IContratoRepository _repository;
    private readonly IMapper _mapper;

    public UpdateContratoService(IContratoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ContratoResponse> ExecuteAsync(UpdateContratoCommand command)
    {
        var contrato = await _repository.GetByIdAsync(new ContratoId(command.Id));
        if (contrato == null)
            throw new KeyNotFoundException("Contrato no encontrado");

        contrato.Update(
            command.FechaFirma,
            command.FechaInicio,
            command.FechaFin,
            new Empresa(command.Empresa),
            new EmpleadoId(command.EmpleadoId),
            new Funciones(command.Funciones),
            new Monto(command.Monto),
            new FrecuenciaPago(command.FrecuenciaPago));

        await _repository.UpdateAsync(contrato);
        return _mapper.Map<ContratoResponse>(contrato);
    }
}

