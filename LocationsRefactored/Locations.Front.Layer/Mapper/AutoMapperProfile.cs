﻿using AutoMapper;
using LACP.Models;
using LACP.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.Front.Layer.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //to display location's chargepoints without the accuring recursion between ChargePoints(List in Location) and Location
            //location--locationVM
            //used in LocationController->GetLocation
            CreateMap<Location, LocationViewModel>()
                .ForMember(dest => dest.ChargePoints, act => act.Ignore());

            //locationVM--location
            CreateMap<LocationViewModel, Location>();

            //chargepoint--chargepointsVM
            //used in LocationController->GetLocation
            CreateMap<ChargePoint, ChargePointViewModel>();

            //chargepointVM--chargepoint
            CreateMap<ChargePointViewModel, ChargePoint>();

            //chargepointsRequestVM--chargepointsRequest
            CreateMap<ChargePointRequestViewModel, ChargePointRequest>();

            //to map the request and patch models to location model that is saved in the db
            //used in LocationService->CreateLocation
            CreateMap<LocationRequest, Location>();

            //patchlocreq--location
            //used in LocationService->EditLocation
            CreateMap<PatchLocationRequest, Location>();

            CreateMap<Location, Location>();

            //cp--cp
            //used in LocationService->InsertChargePoints
            CreateMap<ChargePoint, ChargePoint>();
        }
    }
}
