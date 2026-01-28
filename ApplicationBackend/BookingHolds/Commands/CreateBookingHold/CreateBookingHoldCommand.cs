using Application.BookingHolds.Commands.CreateBookingHold;
using AutoMapper;
using Domain.BookingHolds;
using Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingHolds.Commands.CreateBookingHold
{
    public class CreateBookingHoldCommandHandler : IRequestHandler<CreateBookingHoldCommand, long>
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public CreateBookingHoldCommandHandler(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<long> Handle(CreateBookingHoldCommand request, CancellationToken cancellationToken)
        {
            var configHoldTime = _configuration.GetSection("BookingHoldTime").Value.ToInt();
            var holdtime = configHoldTime == 0 ? 20 : configHoldTime;
            var hold = _mapper.Map<BookingHold>(request);
            await hold.DomainService.SetSpace(request.SpaceId, cancellationToken);
            await hold.DomainService.SetProfile(cancellationToken);
            hold.ExpireAt = DateTime.Now.AddMinutes(holdtime);
            await hold.SaveAsync(cancellationToken);
            return hold.Id;
        }
    }
}
