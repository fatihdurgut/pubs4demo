namespace PubsModern.Application.Authors.Queries;

using MediatR;
using PubsModern.Application.Authors.DTOs;
using PubsModern.Application.Common.Interfaces;
using AutoMapper;

public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllAuthorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDto>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _unitOfWork.Authors.GetAuthorsWithBooksAsync(cancellationToken);
        return _mapper.Map<IEnumerable<AuthorDto>>(authors);
    }
}

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorDto?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(request.Id, cancellationToken);
        return author == null ? null : _mapper.Map<AuthorDto>(author);
    }
}

public class GetAuthorByEmailQueryHandler : IRequestHandler<GetAuthorByEmailQuery, AuthorDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAuthorByEmailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorDto?> Handle(GetAuthorByEmailQuery request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByEmailAsync(request.Email, cancellationToken);
        return author == null ? null : _mapper.Map<AuthorDto>(author);
    }
}
