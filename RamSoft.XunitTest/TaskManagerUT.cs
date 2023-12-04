using AutoFixture;
using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RamSoftTest.Controllers;
using RamSoftTest.Interface;
using RamSoftTest.Model;
using System.Net;
using Xunit;
using FluentAssertions;
using AutoMapper.Execution;
using Azure;

namespace RamSoft.XunitTest
{
    public class TaskManagerUT
    {
        private readonly Mock<ITaskWorker> _mockTaskWorker;
        private readonly Mock<Mapper> _mapper;
        public TaskManagerUT()
        {
            _mockTaskWorker = new Mock<ITaskWorker>();
            _mapper = new Mock<Mapper>();
        }
        [Fact]
        public void GetAllTask_Return_Sucess()
        {
            //arrange
            var fixture = new Fixture();
            //AutoFixture
            var sut = fixture.Build<TaskManagerDto>().Create();
            List<TaskManagerDto> taskManagerDtos = new();
            taskManagerDtos.Add(sut);
            _mockTaskWorker.Setup<Task<IEnumerable<TaskManagerDto>>>(x => x.GetAllTask())
                 .Returns(Task.FromResult<IEnumerable<TaskManagerDto>>((IEnumerable<TaskManagerDto>)taskManagerDtos));
            var taskController = new TaskController(_mockTaskWorker.Object);
            //act
            var result = taskController.GetAllTask();
            //assert
            Assert.NotNull(result);
        }
        [Fact]
        public void GetAllTask_Return_200_OK_Result()
        {
            //arrange
            //List<TaskManagerDto> taskManagerDtos = new();

            _mockTaskWorker.Setup<Task<IEnumerable<TaskManagerDto>>>(x => x.GetAllTask())
                 .Returns(Task.FromResult<IEnumerable<TaskManagerDto>>((IEnumerable<TaskManagerDto>)null));
            var taskController = new TaskController(_mockTaskWorker.Object);
            //act
            var result = (OkObjectResult)taskController.GetAllTask();
            //assert
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async void CreateTask_Return_Sucess()
        {
            //arrange

            var fixture = new Fixture();
            //AutoFixture
            var sut = fixture.Build<TaskModel>().Create();

            _mockTaskWorker.Setup(x => x.CreateTask(sut));

            var taskController = new TaskController(_mockTaskWorker.Object);
            //act
            var response = (OkResult)await taskController.CreateTask(sut);
            //assert
            response.StatusCode.Should().Be(200);
        }

        //[Fact]
        //public async void CreateTask_Return_BadRequest()
        //{
        //    //arrange

        //    TaskModel taskModel = new TaskModel
        //    {
        //        Title = string.Empty
        //    };

        //    _mockTaskWorker.Setup(x => x.CreateTask(taskModel));

        //    var taskController = new TaskController(_mockTaskWorker.Object);
        //    //act
        //    var response = await taskController.CreateTask(taskModel).re;
        //    //assert
        //    _mockTaskWorker.Verify(_ => _.CreateTask(taskModel), Times.Exactly(1));
        //}

        [Fact]
        public void Edit__Return_Sucess()
        {
            var fixture = new Fixture();
            var sut = fixture.Build<TaskManagerDto>().Create();
            int id = 1;
            _mockTaskWorker.Setup(x => x.EditTask(id)).Returns(sut);
            var taskController = new TaskController(_mockTaskWorker.Object);
            var response = (OkObjectResult)taskController.Edit(id);
            response.StatusCode.Should().Be(200);
        }

        [Fact]
        public void Edit_Return_BadRequest()
        {
            var fixture = new Fixture();
            var sut = fixture.Build<TaskManagerDto>().Create();
            int id = 0;
            _mockTaskWorker.Setup(x => x.EditTask(id)).Returns(sut);
            var taskController = new TaskController(_mockTaskWorker.Object);
            var response = (BadRequestResult)taskController.Edit(id);
            response.StatusCode.Should().Be(400);
        }

        [Fact]
        public async void Update_Task_Return_Sucess()
        {
            //arrange

            var fixture = new Fixture();
            //AutoFixture
            var sut = fixture.Build<TaskManagerDto>().Create();

            _mockTaskWorker.Setup(x => x.UpdateTask(sut));

            var taskController = new TaskController(_mockTaskWorker.Object);
            //act
            var response = (OkResult)await taskController.Update(sut);
            //assert
            response.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void Update_Task_Return_BadRequest()
        {
            //arrange

            var fixture = new Fixture();
            //AutoFixture
            var sut = fixture.Build<TaskManagerDto>().Create();
            sut.Id = 0;
            _mockTaskWorker.Setup(x => x.UpdateTask(sut));

            var taskController = new TaskController(_mockTaskWorker.Object);
            //act
            var response = (BadRequestResult)await taskController.Update(sut);
            //assert
            response.StatusCode.Should().Be(400);
        }

        [Fact]
        public async void Delete_Task_Return_Sucess()
        {
            //arrange
            int Id = 1;
            _mockTaskWorker.Setup(x => x.DeleteTask(Id)).Returns(Task.FromResult<string?>("Record deleted")); ;

            var taskController = new TaskController(_mockTaskWorker.Object);
            //act
            var response = (OkResult)await taskController.Delete(Id);
            //assert
            response.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void Delete_Task_Return_BadRequest()
        {
            //arrange
            int Id = 0;
            _mockTaskWorker.Setup(x => x.DeleteTask(Id));

            var taskController = new TaskController(_mockTaskWorker.Object);
            //act
            var response = (BadRequestResult)await taskController.Delete(Id);
            //assert
            response.StatusCode.Should().Be(400);
        }

        [Fact]
        public async void Delete_Task_Return_NotFound()
        {
            //arrange
            int Id = 4;
            _mockTaskWorker.Setup(x => x.DeleteTask(Id)).Returns(Task.FromResult<string?>(null));
            //(Task.FromResult<string>(null));

            var taskController = new TaskController(_mockTaskWorker.Object);
            //act
            var response = (NotFoundResult)await taskController.Delete(Id);
            //assert
            response.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetDesc_Task_Return_Sucess()
        {
            //arrange
            int Id = 2;
            var fixture = new Fixture();
            var sut = fixture.Build<TaskDescDto>().Create();
            _mockTaskWorker.Setup(x => x.GetTaskDesc(Id)).Returns(Task.FromResult<TaskDescDto>(sut));

            var taskController = new TaskController(_mockTaskWorker.Object);
            //act
            var response = (OkObjectResult)await taskController.TaskDesc(Id);
            //assert
            response.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void GetDesc_Task_Return_BadRequest()
        {
            //arrange
            int Id = 0;
            var fixture = new Fixture();
            var sut = fixture.Build<TaskDescDto>().Create();
            _mockTaskWorker.Setup(x => x.GetTaskDesc(Id)).Returns(Task.FromResult<TaskDescDto>(sut)); ;

            var taskController = new TaskController(_mockTaskWorker.Object);
            //act
            var response = (BadRequestResult)await taskController.Delete(Id);
            //assert
            response.StatusCode.Should().Be(400);
        }


    }
}
