using MDD4All.SpecIF.DataProvider.Contracts;

namespace MDD4All.SpecIF.DataProvider.Base
{
    public class SpecIfDataProviderFactory : ISpecIfDataProviderFactory
    {
        public ISpecIfMetadataReader MetadataReader { get; set; }
        public ISpecIfMetadataWriter MetadataWriter { get; set; }
        public ISpecIfDataReader DataReader { get; set; }
        public ISpecIfDataWriter DataWriter { get; set; }
    }
}
