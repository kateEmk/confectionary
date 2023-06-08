using Confectionery.Mappers;
using Mapster;

namespace Confectionery.Mapers
{
    public class AdapterConfig
    {
        public static TypeAdapterConfig Register()
        {
            var config = new TypeAdapterConfig();

            new RegistrationMapper().Register(config);

            return config;
        }
    }
}