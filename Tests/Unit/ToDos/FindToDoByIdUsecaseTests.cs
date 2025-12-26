using Application.Usecases.ToDos;
using Domain.Entity;
using Domain.Gateway;
using FluentAssertions;
using Moq;
using Shared.Constants;
using Shared.Exceptions;
using Xunit;

namespace Tests.Unit.ToDos
{
    public class FindToDoByIdUsecaseTests
    {
        private readonly Mock<ITodoGateway> _repositoryMock;
        private readonly FindToDoByIdUsecase _useCase;
        public FindToDoByIdUsecaseTests()
        {
            _repositoryMock = new Mock<ITodoGateway>();
            _useCase = new FindToDoByIdUsecase(_repositoryMock.Object);
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenTodoDoesNotExist()
        {
            _repositoryMock
                .Setup(r => r.FindById(It.IsAny<int>()))
                .ReturnsAsync((ToDo)null);

            Func<Task> act = async () => await _useCase.Execute(1);

            await act.Should()
                .ThrowAsync<AppEntityNotFoundException>()
                .WithMessage(AppMessages.ToDoValidations.TODO_NOT_FOUND);
        }

    }
}
