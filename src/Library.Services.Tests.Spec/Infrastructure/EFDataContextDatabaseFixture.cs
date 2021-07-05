using Library.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Library.Services.Tests.Spec.Infrastructure
{
    [Collection(nameof(ConfigurationFixture))]
    public class EFDataContextDatabaseFixture : DatabaseFixture
    {

        public EFDataContextDatabaseFixture(ConfigurationFixture configuration)
        {
        }

        public EFDataContext CreateDataContext()
        {
            return new EFDataContext()/*(@"server=.;database=Library_Test;trusted_connection=true;")*/;
        }
    }
}
