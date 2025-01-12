namespace ProductService.Features.CreateCategory;

public class CreateCategoryCommandResult : Result
{
    public int Id { get; set; }

    public CreateCategoryCommandResult()
    {
        
    }

    public CreateCategoryCommandResult(int id)
    {
        Id = id;
    }
}