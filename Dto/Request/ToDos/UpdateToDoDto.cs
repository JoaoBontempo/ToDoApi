using Domain.Enum;
using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Dto.Request.ToDos
{
    public class UpdateToDoDto : ToDoRequestDto
    {
        [Required(ErrorMessage = AppMessages.ToDoValidations.ID_IS_REQUIRED)]
        public int? Id { get; set; }

        [Required(ErrorMessage = AppMessages.ToDoValidations.STATUS_IS_REQUIRED)]
        [EnumDataType(typeof(ToDoStatus), ErrorMessage = AppMessages.ToDoValidations.INVALID_STATUS)]
        public ToDoStatus Status { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}
