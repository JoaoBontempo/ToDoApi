using Domain.Enum;
using Shared.Constants;
using Shared.Exceptions;

namespace Domain.Entity
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public ToDoStatus Status { get; set; }

        public void EnsureIsValid()
        {
            if (string.IsNullOrWhiteSpace(this.Title))
                throw new AppInvalidDataException(AppMessages.ToDoValidations.TITLE_IS_REQUIRED);

            if(this.Title.Length > 100)
                throw new AppInvalidDataException(AppMessages.ToDoValidations.TITLE_LENGTH_SHOULD_BE_LESS_THAN_100);

            if (FinishedAt.HasValue && FinishedAt.Value < CreatedAt)
                throw new AppInvalidDataException(AppMessages.ToDoValidations.FINISHED_AT_SHOULD_BE_AFTER_CREATED_AT);
        }
    }
}
