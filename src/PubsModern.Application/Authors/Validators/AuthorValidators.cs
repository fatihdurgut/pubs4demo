namespace PubsModern.Application.Authors.Validators;

using FluentValidation;
using PubsModern.Application.Authors.DTOs;

public class CreateAuthorDtoValidator : AbstractValidator<CreateAuthorDto>
{
    public CreateAuthorDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters");

        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("Phone must not exceed 20 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.Phone));

        RuleFor(x => x.Biography)
            .MaximumLength(1000).WithMessage("Biography must not exceed 1000 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.Biography));

        RuleFor(x => x.Street)
            .MaximumLength(100).WithMessage("Street must not exceed 100 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.Street));

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required when address is provided")
            .When(x => !string.IsNullOrWhiteSpace(x.Street));

        RuleFor(x => x.PostalCode)
            .MaximumLength(20).WithMessage("Postal code must not exceed 20 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.PostalCode));
    }
}

public class UpdateAuthorDtoValidator : AbstractValidator<UpdateAuthorDto>
{
    public UpdateAuthorDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Author ID is required");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters");

        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("Phone must not exceed 20 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.Phone));

        RuleFor(x => x.Biography)
            .MaximumLength(1000).WithMessage("Biography must not exceed 1000 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.Biography));
    }
}
