using BuildingBlocks.Messaging.Enums;

namespace TestImageService;

public class SaveImageValidationTest
{
    private readonly SaveImageValidator _sut;

    public SaveImageValidationTest()
    {
        _sut = new SaveImageValidator();
    }


    [Fact]
    public async Task ShouldNotHaveErrorValidationError_WhenRequesterIsAValidEntityType()
    {
        var request = new SaveImageCommandRequest(EntityType.Product, 1, "Test.jpg", null!);

        var result = await _sut.TestValidateAsync(request);

        result.ShouldNotHaveValidationErrorFor(x => x.Requester);
    }

    [Fact]
    public async Task ShouldHaveError_WhenRequesterIsNotAValidEntityType()
    {
        var request = new SaveImageCommandRequest((EntityType)999, 1, "Test.jpg", null!);

        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(x => x.Requester);
    }

    [Fact]
    public async Task ShouldHaveError_WhenImageStreamIsNull()
    {
        var request = new SaveImageCommandRequest(EntityType.Product, 1, "Test.jpg", null!);

        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(x => x.ImageStream);
    }

    [Fact]
    public async Task ShouldHaveError_WhenImageStreamIsEmpty()
    {
        var request = new SaveImageCommandRequest(EntityType.Product, 1, "Test.jpg", new MemoryStream());

        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(x => x.ImageStream);
    }

    //[Fact]
    //public async Task ShouldNotHaveError_WhenImageStreamIsValid()
    //{



    //    using (var ms = new MemoryStream())
    //    {
    //        image.Save(ms, image);
    //        return ms.ToArray();
    //    }

    //    var request = new SaveImageCommandRequest(EntityType.Product, "Test.jpg", memoryStream);

    //    var result = await _sut.TestValidateAsync(request);

    //    result.ShouldNotHaveValidationErrorFor(x => x.ImageStream);
    //}

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task ShouldHaveError_WhenFileNameIsNullOrEmpty(string? fileName)
    {
        var request = new SaveImageCommandRequest(EntityType.Product, 1, fileName!, new MemoryStream());

        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(x => x.FileName);
    }

    [Fact]
    public void ShouldHaveError_WhenFileNameHasInvalidExtension()
    {
        var request = new SaveImageCommandRequest(EntityType.Product, 1, "file.txt", null!);

        var result = _sut.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.FileName);
    }

    [Fact]
    public void ShouldHaveError_WhenFileNameHasInvalidCharacters()
    {
        var request = new SaveImageCommandRequest(EntityType.Product, 1, "file?.txt", null!);

        var result = _sut.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.FileName);
    }
}