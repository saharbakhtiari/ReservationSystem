using Application.TimeSlots.Commands.CreateTimeSlot;
using AutoMapper;
using Domain.Contract.Enums;
using Domain.Spaces;
using Domain.Tariffs;
using Domain.TimeSlots;
using Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.TimeSlots.Commands.CreateTimeSlot
{
    public class CreateTimeSlotCommandHandler : IRequestHandler<CreateTimeSlotCommand>
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public CreateTimeSlotCommandHandler(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Unit> Handle(CreateTimeSlotCommand request, CancellationToken cancellationToken)
        {
            List<TimeSlot> timeSlots = new();
            var tariff = await Tariff.GetAsync(request.TariffId, cancellationToken);
            var space = await Space.GetAsync(request.SpaceId, cancellationToken);
            if(request.Type == TimeSlotType.Daily)
            {
                timeSlots =  new TimeSlot().DomainService.GenerateDailyTimeSlot(request.StartDate, request.EndDate,space,tariff);
            }
            else
            {
                timeSlots = new TimeSlot().DomainService.GenerateHourlyTimeSlot(request.StartTime,
                                                                                request.EndTime,
                                                                                request.StartDate,
                                                                                request.EndDate,
                                                                                space,
                                                                                tariff,
                                                                                request.IntervalHours);
            }
            await new TimeSlot().Repository.BulkInsertAsync(timeSlots);
            return Unit.Value;
        }
    }
}
