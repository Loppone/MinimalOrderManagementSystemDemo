using FluentValidation.TestHelper;

namespace TestProductService
{
    public class ProductTestValidation
    {
        private readonly CreateProductValidation _validator;

        public ProductTestValidation()
        {
            _validator = new CreateProductValidation();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ShouldHaveError_WhenCategoryIdIsNotValid(int categoryId)
        {
            var model = new CreateProductCommandRequest(categoryId, "Product 1", "Description", 100);

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }

        [Fact]
        public void ShouldHaveError_WhenNameIsEmpty()
        {
            var model = new CreateProductCommandRequest(1, "", "Description", 100);

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void ShouldHaveError_WhenDescriptionIsTooShort()
        {
            var model = new CreateProductCommandRequest(1, "Product 1", "Desc", 100);

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ShouldHaveError_WhenPriceIsNotvalid(decimal price)
        {
            var model = new CreateProductCommandRequest(1, "Product 1", "Description", price);

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void ShouldNotHaveError_WhenModelIsValid()
        {
            var model = new CreateProductCommandRequest(1, "Product 1", "Description", 100);

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
