using Application.TimeSlots.Commands.UpdateTimeSlot;
using AutoMapper;
using Domain.TimeSlots;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.TimeSlots.Commands.UpdateTimeSlot
{
    public class UpdateTimeSlotCommandHandler : IRequestHandler<UpdateTimeSlotCommand>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;


        public UpdateTimeSlotCommandHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateTimeSlotCommand request, CancellationToken cancellationToken)
        {
            var tariff = await TimeSlot.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            _mapper.Map(request, tariff);
            await tariff.DomainService.SetSpace(request.SpaceId, cancellationToken);
            await tariff.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
