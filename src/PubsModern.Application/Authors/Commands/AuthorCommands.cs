namespace PubsModern.Application.Authors.Commands;

using MediatR;
using PubsModern.Application.Authors.DTOs;

/// <summary>
/// Command to create a new author
/// </summary>
public record CreateAuthorCommand(CreateAuthorDto Author) : IRequest<AuthorDto>;

/// <summary>
/// Command to update an existing author
/// </summary>
public record UpdateAuthorCommand(UpdateAuthorDto Author) : IRequest<AuthorDto>;

/// <summary>
/// Command to delete an author
/// </summary>
public record DeleteAuthorCommand(Guid Id) : IRequest<bool>;
