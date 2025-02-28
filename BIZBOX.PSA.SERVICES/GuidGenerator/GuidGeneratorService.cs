namespace BIZBOX.PSA.SERVICES.GuidGenerator
{
    using System.Security.Cryptography;

    namespace V2_QMS_Integration.Services.GuidGenerator
    {
        public class GuidGeneratorService : IGuidGeneratorService
        {
#pragma warning disable SYSLIB0023 // Type or member is obsolete
            private static readonly RNGCryptoServiceProvider _rng = new();
#pragma warning restore SYSLIB0023 // Type or member is obsolete

            private static Guid NewGuid()
            {
                byte[] randomBytes = new byte[10];
                _rng.GetBytes(randomBytes);

                long timestamp = DateTime.UtcNow.Ticks / 10000L;
                byte[] timestampBytes = BitConverter.GetBytes(timestamp);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(timestampBytes);
                }

                byte[] guidBytes = new byte[16];

                Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                Array.Reverse(guidBytes, 0, 4);
                Array.Reverse(guidBytes, 4, 2);

                return new Guid(guidBytes);
            }

            public string Get()
            {
                return NewGuid().ToString("n");
            }
        }

        public interface IGuidGeneratorService
        {
            string Get();
        }
    }

}
