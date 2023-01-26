using MDD4All.SpecIF.DataProvider.Contracts.DataStreams;

namespace MDD4All.SpecIF.DataProvider.Base.DataStreams
{
    public class SpecIfStreamDataPublisherProvider : ISpecIfStreamDataPublisherProvider
    {
        public ISpecIfDataPublisher StreamDataPublisher { get; set; }
    }
}
