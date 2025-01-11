namespace ProductService.Features.CreateProduct;

public class CreateProductCommandResult : Result
{
    public int Id { get; set; }
    
    public CreateProductCommandResult()
    {
        // Necessario per la pipeline della validazione
    }
    
    public CreateProductCommandResult(int id)
    {
        Id = id;
    }
};
