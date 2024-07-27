using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BiodataManagement.Service.BiodataService;

public class BiodataCreateValidator : AbstractValidator<BiodataCreateRequest>
{
    private readonly IBiodataRepository _biodataRepository;
    public BiodataCreateValidator(IBiodataRepository biodataRepository)
    {
        _biodataRepository = biodataRepository;

        RuleFor(x => x).MustAsync(async (bio, ct) => !await _biodataRepository.IsBiodataExist(bio.UserId!, bio.Email))
            .WithMessage(x => $"Biodata with email {x.Email} already exists");

        RuleFor(x => x.NoKTP).NotEmpty()
            .MustAsync(async (ktp, ct) => !await _biodataRepository.IsKTPExists(ktp))
            .WithMessage("No KTP already used");
    }
}

