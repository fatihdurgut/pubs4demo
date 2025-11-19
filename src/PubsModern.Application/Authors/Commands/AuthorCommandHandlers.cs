namespace PubsModern.Application.Authors.Commands;

using MediatR;
using PubsModern.Application.Authors.DTOs;
using PubsModern.Application.Common.Interfaces;
using PubsModern.Domain.Entities;
using PubsModern.Domain.ValueObjects;
using AutoMapper;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, AuthorDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAuthorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorDto> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Author;
        
        // Create author entity
        var author = new Author(dto.FirstName, dto.LastName, dto.Email, dto.Phone);
        
        // Add address if provided
        if (!string.IsNullOrWhiteSpace(dto.Street) && !string.IsNullOrWhiteSpace(dto.City))
        {
            var address = new Address(dto.Street, dto.City, dto.State ?? "", dto.PostalCode ?? "", dto.Country ?? "USA");
            author.UpdateAddress(address);
        }
        
        // Add biography if provided
        if (!string.IsNullOrWhiteSpace(dto.Biography))
        {
            author.UpdateBiography(dto.Biography);
        }
        
        var createdAuthor = await _unitOfWork.Authors.AddAsync(author, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<AuthorDto>(createdAuthor);
    }
}

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, AuthorDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorDto> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Author;
        var author = await _unitOfWork.Authors.GetByIdAsync(dto.Id, cancellationToken);
        
        if (author == null)
            throw new KeyNotFoundException($"Author with ID {dto.Id} not found");
        
        // Update contact info
        author.UpdateContactInfo(dto.Email, dto.Phone);
        
        // Update address if provided
        if (!string.IsNullOrWhiteSpace(dto.Street) && !string.IsNullOrWhiteSpace(dto.City))
        {
            var address = new Address(dto.Street, dto.City, dto.State ?? "", dto.PostalCode ?? "", dto.Country ?? "USA");
            author.UpdateAddress(address);
        }
        
        // Update biography if provided
        if (!string.IsNullOrWhiteSpace(dto.Biography))
        {
            author.UpdateBiography(dto.Biography);
        }
        
        await _unitOfWork.Authors.UpdateAsync(author, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<AuthorDto>(author);
    }
}

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(request.Id, cancellationToken);
        
        if (author == null)
            return false;
        
        // Soft delete
        author.MarkAsDeleted("system");
        await _unitOfWork.Authors.UpdateAsync(author, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}
