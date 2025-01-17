namespace ImageService.Api.Features.SaveImage;

public class SaveImageValidator : AbstractValidator<SaveImageCommandRequest>
{
    public SaveImageValidator()
    {
        RuleFor(x => x.Requester)
            .IsInEnum()
            .WithMessage("Invalid Entity Type");

        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name is required")
            .Matches(@"^[^\/:*?""<>|\\]+$")
            .WithMessage("File name contains invalid characters.")
            .Matches(@"^.*\.(jpg|jpeg|png|gif)$")
            .WithMessage("Invalid file format. Allowed formats: jpg, jpeg, png, gif.");
        ;
        ;

        RuleFor(x => x.ImageStream)
            .NotNull()
            .WithMessage("Image stream must not be null")
            .Must(BeAValidImage)
            .WithMessage("Invalid Image format")
            .Must(ContainsData)
            .WithMessage("Image Stream must not be empty");
    }

    private bool BeAValidImage(Stream stream)
    {
        if (stream is null || stream.Length == 0)
            return false;

        try
        {
            using var image = Image.Load(stream);

            return image is not null;
        }
        catch
        {
            return false;
        }
    }

    private bool ContainsData(Stream stream)
    {
        return stream is not null && stream.Length > 0;
    }
}
