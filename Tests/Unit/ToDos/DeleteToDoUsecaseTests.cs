using Application.Usecases.ToDos;
using Domain.Entity;
using Domain.Gateway;
using Moq;
using Xunit;

namespace Tests.Unit.ToDos
{
    public class DeleteToDoUsecaseTests
    {
        private readonly Mock<ITodoGateway> _repositoryMock;
        private readonly DeleteToDoUsecase _useCase;
        public DeleteToDoUsecaseTests()
        {
            _repositoryMock = new Mock<ITodoGateway>();
            _useCase = new DeleteToDoUsecase(_repositoryMock.Object);
        }

        [Fact]
        public async Task ShouldDeleteTodoWhenExists()
        {
            var id = 1;

            _repositoryMock
                .Setup(r => r.FindById(id))
                .ReturnsAsync(new ToDo { Id = id });

            await _useCase.Execute(id);

            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }
    }
}
