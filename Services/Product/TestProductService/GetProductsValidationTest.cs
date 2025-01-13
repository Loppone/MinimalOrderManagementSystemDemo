namespace TestProductService;

public class GetProductsValidationTest
{
    private readonly GetProductsValidator _sut;

    public GetProductsValidationTest()
    {
        _sut = new GetProductsValidator();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void GetProductsValidation_ShouldHaveError_WhenPageNumberIsLessOrEqualThanZero(int pageNumber)
    {
        var product = new GetProductsQueryRequest(pageNumber, 1);

        var result = _sut.TestValidate(product);

        result.ShouldHaveValidationErrorFor(x => x.PageNumber);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void GetProductsValidation_ShouldHaveError_WhenPageSizeIsLessOrEqualThanZero(int pageSize)
    {
        var product = new GetProductsQueryRequest(1, pageSize);

        var result = _sut.TestValidate(product);

        result.ShouldHaveValidationErrorFor(x => x.PageSize);
    }

    [Fact]
    public void GetProductsValidation_ShouldNotHaveError_WhenParametersAreValid()
    {
        var product = new GetProductsQueryRequest(1, 1);

        var result = _sut.TestValidate(product);
        
        result.ShouldNotHaveAnyValidationErrors();
    }
}