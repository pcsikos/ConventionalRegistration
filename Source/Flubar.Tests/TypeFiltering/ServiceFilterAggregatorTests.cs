using Flubar.TypeFiltering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flubar.Tests.TypeFiltering
{
    public partial class ServiceFilterAggregatorTests
    {
        private ServiceFilterAggregator serviceFilterAggregator;

        [TestInitialize]
        public void Initialize()
        {
            serviceFilterAggregator = new ServiceFilterAggregator(new[] { new ServiceExtractor() });
        }
    }
}
