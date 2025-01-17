using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using ImageService.Api.Domain.Enums;
using ImageService.Api.Features.SaveImage;

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
        var handler = new SaveImageCommandHandler(mockFileSystem);

        var request = new SaveImageCommandRequest(
            EntityType.Product,
            "test.png",
            "C:/temp/images",
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

