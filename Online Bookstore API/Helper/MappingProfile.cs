using AutoMapper;
using Online_Bookstore_API.Models.DTOs;

namespace Online_Bookstore_API.Helper
{
    public class MappingProfile :Profile
    {
        public MappingProfile() {
            // CreateMap<UserInfoDto, ApplicationUser>();   // Don't Work ! because of await _userManager.UpdateAsync(User);
            CreateMap<ApplicationUser , UserInfoDto >();
            CreateMap<BookDto, Book>()
                .ForMember(dest => dest.IsAvailable, src => src.MapFrom(src => src.Copies > 0 ? true : false));
            //  .ForMember(dest => dest.Id ,src=> src.MapFrom(src=> src.) ) //// when Edit
            //  .ReversMap();  // from book to dto  or   dto to Book

            CreateMap<RegisterModel, ApplicationUser>();
        }
    }
}
