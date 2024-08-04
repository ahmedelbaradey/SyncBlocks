using AutoMapper;
using Entities.DataModels;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Block;
using Shared.DataTransferObjects.Journal;
using Shared.DataTransferObjects.Permisson;
using Shared.DataTransferObjects.SharedObject;
using Shared.DataTransferObjects.User;
using Shared.DataTransferObjects.UserDevice;
using Shared.DataTransferObjects.UserObject;
using Shared.DataTransferObjects.UserObjectPermission;

namespace APINET8.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyForCreationDto, Company>();
            //.ForMember(c => c.FullAddress,
            //opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<EmployeeForUpdateDto, Employee>();
            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();

            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionForCreationDto, Permission>();

            CreateMap<User, UserDto>();
            CreateMap<UserForCreationDto, User>();

            CreateMap<UserDevice, UserDeviceDto>();
            CreateMap<UserDeviceForCreationDto, UserDevice>();

            CreateMap<SharedObject, SharedObjectDto>();
            CreateMap<SharedObjectForCreationDto, SharedObject>();

            CreateMap<Journal, JournalDto>();
            CreateMap<JournalForCreationDto, Journal>();

            CreateMap<Block, BlockDto>();
            CreateMap<BlockForCreationDto, Block>();

            CreateMap<UserObject, UserObjectDto>();
            CreateMap<UserObjectForCreationDto, UserObject>();

            CreateMap<UserObjectPermission, UserObjectPermissionDto>();
            CreateMap<UserObjectPermissionForCreationDto, UserObjectPermission>();

        }
    }
}
