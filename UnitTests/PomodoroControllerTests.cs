using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyPomodoroServer.Controllers;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class PomodoroControllerTests
    {
        [TestMethod]
        public void GetAllPomodoros_TestMethod()
        {
            var testPomorodo = new Pomodoro() {
                Id = Guid.NewGuid(),
                StartTime = "TestStartTime",
                FinishTime = "TestFinishTime",
                Description = "TestDescription"
            };

            var repositoryMock = new Mock<IPomodoroRepository>();
            repositoryMock.Setup(pr => pr.GetAllPomodoros()).ReturnsAsync(new List<Pomodoro>{ testPomorodo });

            var repositoryWrapperMock = new Mock<IRepositoryWrapper>();

            repositoryWrapperMock.Setup(rw => rw.Pomodoro).Returns(repositoryMock.Object);

            var mapperMock = new Mock<IMapper>();

            var loggerMock = new Mock<ILoggerManager>();
            loggerMock.Setup(pr => pr.LogInfo("Test"));

            var pc = new PomodoroController(repositoryWrapperMock.Object, mapperMock.Object, loggerMock.Object);
            var result = pc.GetAllPomodoros().Result;
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
