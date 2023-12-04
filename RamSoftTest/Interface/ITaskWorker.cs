using RamSoftTest.Model;

namespace RamSoftTest.Interface
{
    public interface ITaskWorker
    {
        Task CreateTask(Model.TaskModel taskCreateModel);
        Task<IEnumerable<TaskManagerDto>> GetAllTask();
        Task<string?> DeleteTask(int taskId);
        Task UpdateTask(TaskManagerDto taskManager);
        Task<TaskDescDto> GetTaskDesc(int taskId);
        TaskManagerDto? EditTask(int Id);
    }
}
