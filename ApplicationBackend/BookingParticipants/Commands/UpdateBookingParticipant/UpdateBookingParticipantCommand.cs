using Application.BookingParticipants.Commands.UpdateBookingParticipant;
using AutoMapper;
using Domain.BookingParticipants;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingParticipants.Commands.UpdateBookingParticipant
{
    public class UpdateBookingParticipantCommandHandler : IRequestHandler<UpdateBookingParticipantCommand>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;


        public UpdateBookingParticipantCommandHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateBookingParticipantCommand request, CancellationToken cancellationToken)
        {
            var item = await BookingParticipant.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            _mapper.Map(request, item);
            await item.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
