using FluentValidation;

namespace BiodataManagement.Service.BiodataService;

public class BiodataUpdateValidator : AbstractValidator<BiodataUpdateRequest>
{
    private readonly IBiodataRepository _biodataRepository;
    public BiodataUpdateValidator(IBiodataRepository biodataRepository)
    {
        _biodataRepository = biodataRepository;

        RuleFor(x => x).MustAsync(async (bio, ct) => await _biodataRepository.IsBiodataExist(bio.UserId!, bio.Email))
            .WithMessage(x => $"Biodata is not exists");

    }
}

