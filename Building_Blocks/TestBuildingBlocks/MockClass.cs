using MediatR;

namespace TestBuildingBlocks
{
    public class MockRequestClass : IRequest<MockResponseClass>
    {
    }

    public class MockResponseClass : FluentResults.Result
    {
    }
}
