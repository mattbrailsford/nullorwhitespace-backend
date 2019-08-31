//using AutoMapper;
//using Umbraco.Core;

//namespace NullOrWhitespace.Web.Mapping.Resolvers
//{
//    public class ContentTypesResolver : IValueResolver
//    {
//        public ResolutionResult Resolve(ResolutionResult source)
//        {
//            var contentTypeService = ApplicationContext.Current.Services.ContentTypeService;
//            var contentTypes = contentTypeService.GetAllContentTypes();
//            return source.New(contentTypes);
//        }
//    }
//}
