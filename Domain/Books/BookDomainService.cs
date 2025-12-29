using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;

namespace Domain.Books
{
    public class BookDomainService : IBookDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public BookDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public Book OwnerEntity { get; set; }


    }
}
