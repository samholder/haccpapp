using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Hacapp.Data.DataContexts
{
    public class TestDbConfiguration : DbConfiguration
    {
        public TestDbConfiguration()
        {
            var localDbConnectionFactory = new LocalDbConnectionFactory("v11.0");
            SetDefaultConnectionFactory(localDbConnectionFactory);
        }
    }
}