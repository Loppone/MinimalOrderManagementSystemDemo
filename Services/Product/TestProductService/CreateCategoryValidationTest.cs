namespace TestProductService;

public class CreateCategoryValidationTest
{
    private readonly CreateCategoryValidation _sut;

    public CreateCategoryValidationTest()
    {
        _sut = new CreateCategoryValidation();
    }

    [Fact]
    public void CreateCategoryValidation_ShouldHaveError_WhenNameIsEmpty()
    {
        var request = new CreateCategoryCommandRequest(string.Empty);
        
        var result = _sut.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void CreateCategoryValidation_ShouldHaveError_WhenNameIsGreaterThan30Characters()
    {
        var request = new CreateCategoryCommandRequest("This is a name that is greater than 30 characters");

        var result = _sut.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void CreateCategoryValidation_ShouldNotHaveError_WhenNameIsValid()
    {
        var request = new CreateCategoryCommandRequest("Valid Name");

        var result = _sut.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
}