using AutoMapper;
using AutoMapperDemo;
using AutoMapperDemo01.Dtos;
using AutoMapperDemo01.Models;
using AutoMapperDemo01.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace AutoMapperDemo01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// 通过构造函数实现依赖注入
        /// </summary>
        /// <param name="mapper"></param>
        public StudentController(IMapper mapper)
        {
            _mapper = mapper;
        }

       
        [HttpGet("GetDTO")]
        public async Task<List<StudentDTO>> GetDto()
        {
            List<StudentDTO> list = new List<StudentDTO>();
            List<Student> listStudent = await Task.Run<List<Student>>(() =>
            {
                return Data.GetList();
            });
            // 循环给属性赋值
            /* foreach (var item in listStudent)
               {
                   StudentDTO dto = new StudentDTO();
                   dto.ID = item.ID;
                   dto.Name = item.Name;
                   dto.Age = item.Age;
                   dto.Gender = item.Gender;
                   // 加入到集合中
                   list.Add(dto);
               }
            */// 使用AutoMapper进行映射
            list = _mapper.Map<List<StudentDTO>>(listStudent);
            return list;
        }
        
        /// <summary>
        /// 卡片資料操作
        /// </summary>
        private readonly StudentRepository _studentRepository;
        /// <summary>
        /// 建構式
        /// </summary>
        public StudentController()
        {
            this._studentRepository = new StudentRepository();
        }
        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Student> GetList()
        {
            return this._studentRepository.GetList();
        }
        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Student Get([FromRoute] int id)
        {
            var result = this._studentRepository.Get(id);
            if (result is null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return result;
        }
        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Insert([FromBody] StudentParameter parameter)
        {
            var result = this._studentRepository.Create(parameter);
            if (result > 0)
            {
                return Ok();
            }
            return StatusCode(500);
        }
        /// <summary>
        /// 更新卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] StudentParameter parameter)
        {
            var targetstudent = this._studentRepository.Get(id);
            if (targetstudent is null)
            {
                return NotFound();
            }

            var isUpdateSuccess = this._studentRepository.Update(id, parameter);
            if (isUpdateSuccess)
            {
                return Ok();
            }
            return StatusCode(500);
        }
        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            this._studentRepository.Delete(id);
            return Ok();
        }
    }
}
