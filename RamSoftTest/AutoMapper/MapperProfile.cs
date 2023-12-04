using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using RamSoftTest.Model;

namespace RamSoftTest.AutoMapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile() {
            CreateMap<TaskModel, TaskManager>().
                     ForMember(des => des.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(des => des.DueDate, opt => opt.MapFrom(src => src.DueDate))
                    .ForMember(des => des.AssignTo, opt => opt.MapFrom(src => src.AssignTo))
                    .ForMember(des => des.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(des => des.ReportTo, opt => opt.MapFrom(src => src.ReportTo))
                    .ForMember(des => des.Priority, opt => opt.MapFrom(src => src.Priority))
                    .ForMember(des => des.Title,opt => opt.MapFrom(src => src.Title));

            CreateMap<TaskManager, TaskManagerDto>().
                     ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(des => des.AssignTo, opt => opt.MapFrom(src => src.AssignTo))
                    .ForMember(des => des.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(des => des.ReportTo, opt => opt.MapFrom(src => src.ReportTo))
                    .ForMember(des => des.Priority, opt => opt.MapFrom(src => src.Priority))
                    .ForMember(des => des.DueDate, opt => opt.MapFrom(src => src.DueDate))
                    .ForMember(des => des.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(des => des.Title, opt => opt.MapFrom(src => src.Title));
            CreateMap<TaskManagerDto,TaskManager>().
                     ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(des => des.AssignTo, opt => opt.MapFrom(src => src.AssignTo))
                    .ForMember(des => des.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(des => des.ReportTo, opt => opt.MapFrom(src => src.ReportTo))
                    .ForMember(des => des.Priority, opt => opt.MapFrom(src => src.Priority))
                    .ForMember(des => des.DueDate, opt => opt.MapFrom(src => src.DueDate))
                    .ForMember(des => des.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(des => des.Title, opt => opt.MapFrom(src => src.Title));

            CreateMap<TaskManager, TaskDescDto>().
                   ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(des => des.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
