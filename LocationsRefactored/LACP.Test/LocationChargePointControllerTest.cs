using AutoMapper;
using LACP.Models;
using LACP.Services.Services.Services.Interfaces;
using Locations.Front.Layer.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACP.Test
{
    public class LocationChargePointControllerTest
    {
        private LocationChargePointController controller;
        private Mock<ILocationChargePointService> locationChargePointService;
        private Mock<IMapper> mapper;
        private Mock<LocationRequest> locReqMock;


        [SetUp]
        public void Setup()
        {
            locationChargePointService = new Mock<ILocationChargePointService>();
            locReqMock = new Mock<LocationRequest>();
            mapper = new Mock<IMapper>();
            controller = new LocationChargePointController(locationChargePointService.Object,mapper.Object);
        }

        //post location test
        [Test]
        public void PostReturnTaskCompleted()
        {  
            //arrange
            locationChargePointService.Setup(l => l.CreateLocation(locReqMock.Object)).Returns(Task.CompletedTask);
            //act
            var result = controller.PostLocation(locReqMock.Object);
            //assert
            locationChargePointService.Verify(s => s.CreateLocation(locReqMock.Object), Times.Once());
            Assert.AreEqual(Task.CompletedTask, result);
        }
    }
}
