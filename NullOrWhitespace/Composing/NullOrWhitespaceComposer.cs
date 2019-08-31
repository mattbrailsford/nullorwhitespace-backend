using NullOrWhitespace.Web.Mapping;
using Our.Umbraco.HeadRest.Composing;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Mapping;

namespace NullOrWhitespace.Composing
{
    [ComposeAfter(typeof(HeadRestComposer))]
    public class NullOrWhitespaceComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components()
                .Append<NullOrWhitespaceComponent>();

            composition.WithCollectionBuilder<MapDefinitionCollectionBuilder>()
                .Add<NullOrWhitespaceMapDefinition>();
        }
    }
}
