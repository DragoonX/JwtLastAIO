using System.Collections.Generic;
using JwtExercise.DTO;
using JwtExercise.Interfaces;
using JwtExercise.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IUnitOfWork _unitOfWork;

        public ValuesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            return new JsonResult(_unitOfWork.userRepository.List());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return new JsonResult(_unitOfWork.userRepository.Find(id));
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] List<UserDto> userDtos)
        {
            foreach (var item in userDtos)
            {
                User user = new User()
                {
                    firstname=item.firstname,
                    lastname=item.lastname,
                    username=item.username
                };

                var res = _unitOfWork.userRepository.FindUsername(user.username);
                if (res == null)
                {
                    _unitOfWork.userRepository.Insert(user);
                }
            }
            _unitOfWork.Complete();
            return Get();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var res = _unitOfWork.userRepository.Delete(id);
            if (res==true)
                _unitOfWork.Complete();
            return Get();
        }
    }
}
