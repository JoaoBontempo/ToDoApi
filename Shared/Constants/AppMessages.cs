namespace Shared.Constants
{
    public static class AppMessages
    {
        public static class ToDoValidations
        {
            public const string TITLE_IS_REQUIRED = "O título da tarefa é obrigatório";
            public const string TITLE_LENGTH_SHOULD_BE_LESS_THAN_100 = "O título da tarefa deve ter no máximo 100 caracteres";
            public const string FINISHED_AT_SHOULD_BE_AFTER_CREATED_AT = "A data de conclusão da tarefa deve ser posterior à sua data de criação";
            public const string STATUS_IS_REQUIRED = "O status é obrigatório";
            public const string INVALID_STATUS = "O status informado é inválido";
            public const string ID_IS_REQUIRED = "O ID da tarefa não foi informado";
            public const string TODO_NOT_FOUND = "Tarefa não encontrada";
            public const string TODO_IS_REQUIRED = "Nenhum dado da tarefa foi informado";
        }
    }
}
