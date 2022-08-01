using Moq;
using ServiceHub.Interfaces.Services;
using ServiceHub.Services;
using ServiceHub.Interfaces.ServicesConfig;

namespace ServiceHub.Tests.Services
{
    public class Test_JsonFileMonitoringService
    {
        //TODO: I was unable to do it in single test, had problem to synchronize Setup sequence to trigering before and after change file
        //I would need more time to figure this one out.
        [Fact]
        public async Task CheckThatFileChangedEventWasRised()
        {
            // Arrange
            DateTime date = new DateTime();
            var mockFileService = new Mock<IFileService>();
            mockFileService.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
            mockFileService.Setup(m => m.GetLastWriteTime(It.IsAny<string>())).Returns(date);

            var mockPeriodicTimerService = new Mock<IPeriodicTimer>();
            mockPeriodicTimerService.SetupSequence(m => m.WaitForNextTickAsync(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(true) 
                 .ReturnsAsync(true)
                 .ReturnsAsync(false);

            var timerInstance = mockPeriodicTimerService.Object;

            var mockConfig = new Mock<IFileMonitoringServiceConfig>();
            mockConfig.SetupGet(m => m.FilePath).Returns("testFile.json");
            mockConfig.SetupGet(m => m.PeriodicTimer).Returns(timerInstance);
            mockConfig.SetupGet(m => m.CancellationTokenSource).Returns(new CancellationTokenSource());
            mockConfig.SetupGet(m => m.FileService).Returns(mockFileService.Object);

            var jsonFileMonitoringService = new JsonFileMonitoringService(mockConfig.Object);

            bool fileChangedEventRaised = false;
            jsonFileMonitoringService.FileChanged += (sender, args) => fileChangedEventRaised = true;

            // Act
            await jsonFileMonitoringService.Start();
            await Task.Delay(50);
            await jsonFileMonitoringService.Stop();
            timerInstance.Dispose();

            // Assert
            Assert.False(fileChangedEventRaised);
        }

        [Fact]
        public async Task CheckThatFileChangedEventWasNotRised()
        {
            // Arrange
            DateTime date = new DateTime();
            var mockFileService = new Mock<IFileService>();
            mockFileService.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
            mockFileService.Setup(m => m.GetLastWriteTime(It.IsAny<string>())).Returns(date.AddSeconds(2));

            var mockPeriodicTimerService = new Mock<IPeriodicTimer>();
            mockPeriodicTimerService.SetupSequence(m => m.WaitForNextTickAsync(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(true)
                 .ReturnsAsync(true)
                 .ReturnsAsync(false);

            var timerInstance = mockPeriodicTimerService.Object;

            var mockConfig = new Mock<IFileMonitoringServiceConfig>();
            mockConfig.SetupGet(m => m.FilePath).Returns("testFile.json");
            mockConfig.SetupGet(m => m.PeriodicTimer).Returns(timerInstance);
            mockConfig.SetupGet(m => m.CancellationTokenSource).Returns(new CancellationTokenSource());
            mockConfig.SetupGet(m => m.FileService).Returns(mockFileService.Object);

            var jsonFileMonitoringService = new JsonFileMonitoringService(mockConfig.Object);

            bool fileChangedEventRaised = false;
            jsonFileMonitoringService.FileChanged += (sender, args) => fileChangedEventRaised = true;

            // Act
            await jsonFileMonitoringService.Start();
            await Task.Delay(50);
            await jsonFileMonitoringService.Stop();
            timerInstance.Dispose();

            // Assert
            Assert.True(fileChangedEventRaised);
        }
    }
}