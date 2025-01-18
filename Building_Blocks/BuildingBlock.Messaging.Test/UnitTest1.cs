using MassTransit;
using MassTransit.Testing;

namespace BuildingBlock.Messaging.Test;

public class UnitTest1
{
    private readonly IBusControl _bus;
    private readonly ITestHarness _testHarness;
    public UnitTest1()
    {
        // Configura MassTransit e RabbitMQ direttamente nel test
        _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host("localhost", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            // Configura l'endpoint di consumo
            cfg.ReceiveEndpoint("image-message-queue", e =>
            {
                e.Consumer<MyImageConsumer>(); // Consumer che gestisce il messaggio
            });
        });

     //   _testHarness = _bus.GetTestHarness(); // Ottieni il TestHarness
    }


    [Fact]
    public async Task Test1()
    {
        var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            cfg.ReceiveEndpoint("image-message-queue", e =>
            {
                e.Consumer<MyImageConsumer>();
            });
        });

        await busControl.StartAsync();

        try
        {
            var imageData = "Test Queuing";
            var message = new SaveImageCommandRequest
            {
                FileName = "image.png",
                ImageData = imageData
            };

            // Invia il messaggio (publish)
            await busControl.Publish(message);

            // Assert che il consumatore abbia ricevuto e gestito il messaggio
        }
        finally
        {
            // Arresta il bus
            await busControl.StopAsync();
        }
    }


    //[Fact]
    //public async Task ShouldConsumeMessageWhenSent()
    //{
    //    IBusControl _bus;

    //    // Avvia il bus
    //    await _bus.StartAsync();

    //    try
    //    {
    //        // Invia il messaggio al bus
    //        await _bus.Publish(new SaveImageCommandRequest
    //        {
    //            FileName = "image.png",
    //            // Altri parametri...
    //        });

    //        // Assicurati che il messaggio sia stato consumato
    //        var consumed = await _testHarness.Consumed.Any<SaveImageCommandRequest>();
    //        consumed.Should().BeTrue();

    //        // Verifica se il consumer ha ricevuto il messaggio
    //        var consumedMessage = _testHarness.Sent.Select<SaveImageCommandRequest>().FirstOrDefault();
    //        consumedMessage.Should().NotBeNull();
    //        consumedMessage.Message.FileName.Should().Be("image.png");
    //    }
    //    finally
    //    {
    //        // Ferma il bus dopo il test
    //        await _bus.StopAsync();
    //    }
    //}
}
