using Location.ResponseModels;
using Microsoft.Extensions.Configuration;

namespace Worker.Api.Configuration
{
    class GoogleLocationApiKey: IApiKey
    {
        private readonly IConfiguration configuration;

        public GoogleLocationApiKey(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Get()
        {
            return configuration["GoogleLocationApi:Key"];
        }
    }
}