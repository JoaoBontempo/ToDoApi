using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Dto.Request.ToDos
{
    public abstract class ToDoRequestDto
    {
        [Required(ErrorMessage = AppMessages.ToDoValidations.TITLE_IS_REQUIRED)]
        [StringLength(100, ErrorMessage = AppMessages.ToDoValidations.TITLE_LENGTH_SHOULD_BE_LESS_THAN_100)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
