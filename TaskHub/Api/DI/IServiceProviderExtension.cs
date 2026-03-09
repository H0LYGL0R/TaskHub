using Api.Services.DisposedServices;

namespace Api.DI
{
    public static class ServiceProviderExtensions
    {
        private const string TypeLabel = "Service Type";
        private const string FirstIdLabel = "First Instance ID";
        private const string SecondIdLabel = "Second Instance ID";
        private const string ComparisonLabel = "Is it the same instance?";

        public static void TestResolution<TService>(this IServiceProvider provider)
            where TService : notnull, IHasInstanceId
        {

            TService first = provider.GetRequiredService<TService>();
            TService second = provider.GetRequiredService<TService>();

            Console.WriteLine($"{TypeLabel}: {typeof(TService).Name}");
            Console.WriteLine($"  {FirstIdLabel}: {first.InstanceId}");
            Console.WriteLine($"  {SecondIdLabel}: {second.InstanceId}");

            bool isSame = ReferenceEquals(first, second);
            Console.WriteLine($"  {ComparisonLabel}: {isSame}");
            Console.WriteLine();
        }
    }
}
