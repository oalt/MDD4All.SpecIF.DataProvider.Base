using MDD4All.SpecIF.DataProvider.Contracts.DataStreams;

namespace MDD4All.SpecIF.DataProvider.Base.DataStreams
{
    public class SpecIfStreamDataSubscriberProvider : ISpecIfStreamDataSubscriberProvider
    {
        public ISpecIfDataSubscriber StreamDataSubscriber { get; set; }
    }
}
