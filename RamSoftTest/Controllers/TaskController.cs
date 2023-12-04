using Microsoft.AspNetCore.Mvc;
using RamSoftTest.Interface;
using RamSoftTest.Model;
using System.Net;

namespace RamSoftTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskWorker _worker;
        public TaskController(ITaskWorker worker) 
        {
            _worker=worker;
        }

        [HttpGet]
        [Route("/AllTask")]
        public IActionResult GetAllTask() 
        {
            try
            {
                var res = _worker.GetAllTask().Result;
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        [HttpPost]
        [Route("/CreateTask")]
        public async Task<IActionResult> CreateTask(TaskModel taskCreateModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await  _worker.CreateTask(taskCreateModel);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet]
        [Route("/EditTask")]
        public IActionResult Edit([FromQuery]int id)
        {
            try
            {
                if (id<=0)
                {

                    return BadRequest();
                }
                var res= _worker.EditTask(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPut]
        [Route("/UpdateTask")]
        public async Task<IActionResult> Update(TaskManagerDto taskManagerViewModel)
        {
            try
            {

                if (!ModelState.IsValid ||taskManagerViewModel.Id<=0)
                {

                    return BadRequest();
                }
                await _worker.UpdateTask(taskManagerViewModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpDelete]
        [Route("/DeleteTask")]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            try
            {
                if (id<=0)
                {
                    return BadRequest();
                }
               var res= await _worker.DeleteTask(id);
                if(string.IsNullOrEmpty(res))
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetTaskDescription")]
        public async Task<IActionResult> TaskDesc([FromQuery] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
              var result=  await _worker.GetTaskDesc(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
