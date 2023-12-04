using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RamSoftTest.EFContext;
using RamSoftTest.Interface;
using RamSoftTest.Model;

namespace RamSoftTest.Service
{
    public class TaskWorker : ITaskWorker
    {
        private readonly EFCoreDBContext _eFCoreDBContext;
        private IMapper _mapper;
        public TaskWorker(EFCoreDBContext eFCoreDBContext, IMapper mapper)
        { 
            _eFCoreDBContext = eFCoreDBContext;
            _mapper = mapper;
        }

        public async Task CreateTask(TaskModel taskCreateModel)
        {
                _eFCoreDBContext.TaskManager.Add(_mapper.Map<TaskManager>(taskCreateModel));
               await _eFCoreDBContext.SaveChangesAsync();
        }

        public async Task<string?> DeleteTask(int taskId)
        {
            var data = _eFCoreDBContext.TaskManager.SingleOrDefault(x => x.Id == taskId);
            if (data != null)
            {
                _eFCoreDBContext.TaskManager.Remove(data);
                await _eFCoreDBContext.SaveChangesAsync();
                return ("Record Deleted Successfully");
            }
            return null;
            
        }

        public TaskManagerDto? EditTask(int Id)
        {
            var data =  _eFCoreDBContext.TaskManager.SingleOrDefault(x => x.Id == Id);
            if (data?.Id>0 ||data!=null)
            {
                var mapResult = _mapper.Map<TaskManagerDto>(data);
                return mapResult;
            }
            return null;
        }

        public async Task<IEnumerable<TaskManagerDto>> GetAllTask()
        {
            List<TaskManagerDto> taskManagerViewModels = new ();
            var taskDetails = await _eFCoreDBContext.TaskManager.ToListAsync();
             foreach(var task in taskDetails)
            {
              var mapResult =  _mapper.Map<TaskManagerDto>(task);
                taskManagerViewModels.Add(mapResult);
            }
            return taskManagerViewModels;
        }

        public async Task<TaskDescDto> GetTaskDesc(int taskId)
        {

            var data = await _eFCoreDBContext.TaskManager.SingleOrDefaultAsync(x => x.Id == taskId);
            if (data?.Id > 0 || data != null)
            {
                var mapResult = _mapper.Map<TaskDescDto>(data);
                return mapResult;
            }
            return null;
        }
    

        public async Task UpdateTask(TaskManagerDto taskManagerDto)
        {
            
            var mapResult = _mapper.Map<TaskManager>(taskManagerDto);
            _eFCoreDBContext.Entry(mapResult).State=EntityState.Modified;
            await _eFCoreDBContext.SaveChangesAsync();
            
        }

        
    }
}
