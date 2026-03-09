namespace Api.Services.DisposedService
{
    public sealed class DisposedService : IHasInstanceId, IDisposable
    {
        public Guid InstanceId => _instanceId ?? (_instanceId = Guid.NewGuid()).Value;

        private Guid? _instanceId = null;


        private const string Name = nameof(DisposedService);
        private const string CreateString = "created";
        private const string DisposeString = "disposed";

        public void Log()
        {
           WriteDescription(CreateString);
        }

        public void Dispose()
        {
            WriteDescription(DisposeString);
        }

        private void WriteDescription(string action)
        {
            Console.WriteLine($"{Name} {InstanceId} {action}");
        }
    }

}