using BuildingBlocks.Behaviors;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;

namespace TestBuildingBlocks
{
    public class ValidationTest
    {
        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhereValidationFails()
        {
            var mockValidator = new Mock<IValidator<MockRequestClass>>();

            mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<MockRequestClass>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure>
                {
                    new("Property", "Error message")
                }));

            var mockRequest = new MockRequestClass();
            var mockNext = new Mock<RequestHandlerDelegate<MockResponseClass>>();

            var validators = new List<IValidator<MockRequestClass>> { mockValidator.Object };
            var behavior = new ValidationBehavior<MockRequestClass, MockResponseClass>(validators);

            await Assert.ThrowsAsync<ValidationException>(async () =>
                         await behavior.Handle(mockRequest, mockNext.Object, CancellationToken.None));

            mockNext.Verify(next => next(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldCallNext_WhenValidationSucceeds()
        {
            var mockValidator = new Mock<IValidator<MockRequestClass>>();

            mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<MockRequestClass>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var mockRequest = new MockRequestClass();
            var mockNext = new Mock<RequestHandlerDelegate<MockResponseClass>>();
            
            var validators = new List<IValidator<MockRequestClass>> { mockValidator.Object };
            var behavior = new ValidationBehavior<MockRequestClass, MockResponseClass>(validators);

            await behavior.Handle(mockRequest, mockNext.Object, CancellationToken.None);

            mockNext.Verify(next => next(), Times.Once);
        }
    }
}