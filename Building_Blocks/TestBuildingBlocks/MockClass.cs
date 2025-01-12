namespace TestBuildingBlocks;

public class MockRequestClass : IRequest<Result<MockResponseClass>>
{
}

public class MockResponseClass : FluentResults.Result
{
}
