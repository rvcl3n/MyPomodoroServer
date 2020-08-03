using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class PomodoroRepositoryTests
    {
        [TestMethod]
        public void GetAllPomodoros_TestMethod()
        {         
            var options = new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(databaseName: "MyPomodoroDatabase")
            .Options;

            using (var context = new RepositoryContext(options))
            {
                context.Pomodoros.Add(new Pomodoro()
                {
                    Id = Guid.NewGuid(),
                    StartTime = "TestStartTime1",
                    FinishTime = "TestFinishTime1",
                    Description = "TestDescription1"
                });
                context.Pomodoros.Add(new Pomodoro()
                {
                    Id = Guid.NewGuid(),
                    StartTime = "TestStartTime2",
                    FinishTime = "TestFinishTime2",
                    Description = "TestDescription2"
                }); 
                context.Pomodoros.Add(new Pomodoro()
                {
                    Id = Guid.NewGuid(),
                    StartTime = "TestStartTime2",
                    FinishTime = "TestFinishTime2",
                    Description = "TestDescription2"
                });
                context.SaveChangesAsync();
            }

            using (var context = new RepositoryContext(options))
            {
                var pomodoroRepository = new PomodoroRepository(context);
                List<Pomodoro> pomodoros = pomodoroRepository.GetAllPomodoros().Result.ToList();

                Assert.IsNotNull(pomodoros);
                Assert.AreEqual(3, pomodoros.Count);
            }
        }
    }
}
