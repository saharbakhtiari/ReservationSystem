using Application.Bookings.Commands.CreateBooking;
using AutoMapper;
using Domain.Bookings;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, long>
    {
        private readonly IMapper _mapper;


        public CreateBookingCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var tariff = _mapper.Map<Booking>(request);
            await tariff.DomainService.SetSpace(request.SpaceId, cancellationToken);
            await tariff.DomainService.SetProfile(cancellationToken);
            await tariff.SaveAsync(cancellationToken);
            return tariff.Id;
        }
    }
}
