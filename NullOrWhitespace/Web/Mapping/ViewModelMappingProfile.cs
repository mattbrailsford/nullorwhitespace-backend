using AutoMapper;
using NullOrWhitespace.Models;
using NullOrWhitespace.Web.ViewModels;

namespace NullOrWhitespace.Web.Mapping
{
    public class ViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<HomePage, HomePageViewModel>();

            Mapper.CreateMap<StandardPage, StandardPageViewModel>();

            Mapper.CreateMap<BlogPage, BlogPageViewModel>();

            Mapper.CreateMap<BlogPostPage, BlogPostPageViewModel>();
        }
    }
}
