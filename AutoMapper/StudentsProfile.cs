using AutoMapper;
using AutoMapperDemo01.Dtos;
using AutoMapperDemo01.Models;

namespace AutoMapperDemo01.AutoMapper
{
    /// <summary>
    /// 继承自Profile类
    /// </summary>
    public class StudentsProfile:Profile
    {
        /// <summary>
        /// 构造函数中实现映射
        /// </summary>
        public StudentsProfile() 
        {
            //Mapping
            // 第一次参数是源类型（这里是Model类型），第二个参数是目标类型（这里是DTO类
            CreateMap<Student, StudentDTO>()
                .ForMember(destinationMember: des => des.StudentID, memberOptions: opt => { opt.MapFrom(mapExpression: map => map.ID); })
                .ForMember(destinationMember: des => des.StudentName, memberOptions: opt => { opt.MapFrom(mapExpression: map => map.Name); })
                .ForMember(destinationMember: des => des.StudentAge, memberOptions: opt => { opt.MapFrom(mapExpression: map => map.Age); })
                .ForMember(destinationMember: des => des.StudentGender, memberOptions: opt => { opt.MapFrom(mapExpression: map => map.Gender); })
                .ReverseMap();
                ;
        }
    }
}
