using Application.Books.Commands.UpdateBook;
using AutoMapper;
using Domain.Books;
using Domain.Common;
using Domain.FileManager;
using Exceptions;
using Extensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IMapper _mapper;
        private readonly IFileStorage _imageStorage;

        public UpdateBookCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
            _imageStorage = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IFileStorage>("Image");
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            
            var book = await Book.GetAsync(request.Id, cancellationToken)??throw new UserFriendlyException("Item not exist");
            _mapper.Map(request, book);
            if(request.Image is not null)
            {
                var newPath = await _imageStorage.StoreAsync(request.Image, cancellationToken, "BookImages");
                _imageStorage.Remove(book.ImageUrl);
                book.ImageUrl = newPath;
            }
            await book.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
