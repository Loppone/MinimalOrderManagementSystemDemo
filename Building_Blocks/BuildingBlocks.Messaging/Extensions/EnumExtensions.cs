namespace BuildingBlocks.Messaging.Extensions;

public static class EnumExtensions
{
    public static string ToRoutingKey(this EntityType typeOfEntity)
    {
        return typeOfEntity switch
        {
            EntityType.Product => "PRODUCT",
            EntityType.User => "USER",
            _ => throw new Exception("Invalid Entity Type")
        };
    }
}