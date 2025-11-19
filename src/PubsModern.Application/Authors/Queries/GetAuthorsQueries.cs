namespace PubsModern.Application.Authors.Queries;

using MediatR;
using PubsModern.Application.Authors.DTOs;

/// <summary>
/// Query to get all authors
/// </summary>
public record GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDto>>;

/// <summary>
/// Query to get author by ID
/// </summary>
public record GetAuthorByIdQuery(Guid Id) : IRequest<AuthorDto?>;

/// <summary>
/// Query to get author by email
/// </summary>
public record GetAuthorByEmailQuery(string Email) : IRequest<AuthorDto?>;
