namespace TestProductService;

public class ProductTestValidation
{
    private readonly CreateProductValidation _sut;

    public ProductTestValidation()
    {
        _sut = new CreateProductValidation();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ShouldHaveError_WhenCategoryIdIsNotValid(int categoryId)
    {
        var product = new CreateProductCommandRequest(categoryId, "Product 1", "Description", 100);

        var result = _sut.TestValidate(product);

        result.ShouldHaveValidationErrorFor(x => x.CategoryId);
    }

    [Fact]
    public void ShouldHaveError_WhenNameIsEmpty()
    {
        var product = new CreateProductCommandRequest(1, "", "Description", 100);

        var result = _sut.TestValidate(product);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void ShouldHaveError_WhenDescriptionIsTooShort()
    {
        var product = new CreateProductCommandRequest(1, "Product 1", "Desc", 100);

        var result = _sut.TestValidate(product);

        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ShouldHaveError_WhenPriceIsNotvalid(decimal price)
    {
        var product = new CreateProductCommandRequest(1, "Product 1", "Description", price);

        var result = _sut.TestValidate(product);

        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Fact]
    public void ShouldNotHaveError_WhenModelIsValid()
    {
        var product = new CreateProductCommandRequest(1, "Product 1", "Description", 100);

        var result = _sut.TestValidate(product);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
