using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

class Program {
    static async Task Main() {
        var cfg = new ProducerConfig { BootstrapServers = "localhost:9092" };
        var ccfg = new ConsumerConfig {
            BootstrapServers = "localhost:9092",
            GroupId = Guid.NewGuid().ToString(),
            AutoOffsetReset = AutoOffsetReset.Latest
        };
        using var consumer = new ConsumerBuilder<Ignore, string>(ccfg).Build();
        consumer.Subscribe("chat-topic");

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        _ = Task.Run(() => {
            Console.WriteLine("Consumer started…");
            while (!cts.IsCancellationRequested) {
                try {
                    var msg = consumer.Consume(cts.Token);
                    Console.WriteLine($"> {msg.Value}");
                } catch (OperationCanceledException) { break; }
            }
            consumer.Close();
        });

        using var producer = new ProducerBuilder<Null, string>(cfg).Build();
        Console.WriteLine("Type message (Ctrl+C to exit):");
        while (!cts.IsCancellationRequested) {
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;
            var res = await producer.ProduceAsync("chat-topic", new Message<Null, string> { Value = line });
            Console.WriteLine($"< Sent at {res.TopicPartitionOffset}");
        }
    }
}
