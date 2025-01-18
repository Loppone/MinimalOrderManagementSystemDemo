namespace BuildingBlocks.Messaging.Event;

public record FileSavedEvent(
    int FileId,
    int EntityId,
    EntityType TypeOfEntity,
    DateTime CreatedOnUtc);
