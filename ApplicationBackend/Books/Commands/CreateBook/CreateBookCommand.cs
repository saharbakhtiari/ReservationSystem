using Application.Books.Commands.CreateBook;
using AutoMapper;
using Domain.Books;
using Domain.Common;
using Domain.FileManager;
using Extensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, long>
    {
        private readonly IMapper _mapper;
        private readonly IFileStorage _imageStorage;

        public CreateBookCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
            _imageStorage = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IFileStorage>("Image");
        }

        public async Task<long> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);
            if (request.Image is not null)
            {
                var imagePath = await _imageStorage.StoreAsync(request.Image, cancellationToken, "BookImages");
                book.ImageUrl = imagePath;
            }
            await book.SaveAsync(cancellationToken);
            return book.Id;
        }
    }
}
