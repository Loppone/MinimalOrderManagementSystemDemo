using BuildingBlocks.Messaging.Enums;

namespace TestImageService;

public class SaveImageCommandHandlerTest
{
    public SaveImageCommandHandlerTest()
    {

    }

    [Fact]
    public async Task Handle_ShouldSaveFileCorrectly()
    {
        var mockFileSystem = new MockFileSystem();
        var mockStorageOptions = new Mock<IOptions<ImageConfiguration>>();

        mockStorageOptions.Setup(x=>x.Value)
            .Returns(new ImageConfiguration() { PathFolder = "c:/temp/images" });

        var handler = new SaveImageCommandHandler(null!, mockFileSystem, mockStorageOptions.Object, null!);

        var request = new SaveImageCommandRequest(
            EntityType.Product,
            1,
            "test.png",
            new MemoryStream(new byte[] { 0x01, 0x02, 0x03 }));

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();

        var savedFiles = mockFileSystem.AllFiles;
        Assert.Single(savedFiles);

        var savedFilePath = savedFiles.First();

        var normalizedSavedPath = Path.GetFullPath(savedFilePath).Replace("\\", "/");
        var expectedPath = Path.GetFullPath("C:/temp/images").Replace("\\", "/");

        Assert.StartsWith(expectedPath, normalizedSavedPath);
        Assert.EndsWith(".png", savedFilePath);
    }
}

