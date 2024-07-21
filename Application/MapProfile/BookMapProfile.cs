using Abstraction.Command.Book.CreateBook;
using AutoMapper;
using Domain.Entity;

namespace Application.MapProfile;

public class BookMapProfile : Profile
{
    public BookMapProfile()
    {
        CreateMap<CreateBookCommand, Book>();
        CreateMap<Book, CreateBookCommandResult>();
    }
}

