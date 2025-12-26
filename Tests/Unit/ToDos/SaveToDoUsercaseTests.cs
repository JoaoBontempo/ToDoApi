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
    public class SaveToDoUsercaseTests
    {
        private readonly Mock<ITodoGateway> _repositoryMock;
        private readonly SaveToDoUsecase _useCase;

        public SaveToDoUsercaseTests()
        {
            _repositoryMock = new Mock<ITodoGateway>();
            _useCase = new SaveToDoUsecase(_repositoryMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public async Task ShouldValidateToDoEmptyOrBlankTitle(string title)
        {
            ToDo toDo = new()
            {
                Title = title,
            };

            Func<Task> act = async () => await _useCase.Execute(toDo);

            await act.Should()
                     .ThrowAsync<AppInvalidDataException>()
                     .WithMessage(AppMessages.ToDoValidations.TITLE_IS_REQUIRED);

            _repositoryMock.Verify(
                r => r.Insert(It.IsAny<ToDo>()),
                Times.Never
            );
        }

        [Fact]
        public async Task ShouldValidateToDoLargeTitle()
        {
            ToDo toDo = new()
            {
                Title = "This todo has a large title, higher than 100, and shouldn't be saved. If this test fails, it means that the validation have to be fixed, otherwise it will cause problems",
            };

            Func<Task> act = async () => await _useCase.Execute(toDo);

            await act.Should()
                     .ThrowAsync<AppInvalidDataException>()
                     .WithMessage(AppMessages.ToDoValidations.TITLE_LENGTH_SHOULD_BE_LESS_THAN_100);

            _repositoryMock.Verify(
                r => r.Insert(It.IsAny<ToDo>()),
                Times.Never
            );
        }

        [Fact]
        public async Task ShouldSaveNewToDoSuccessfully()
        {
            string title = "Inserting ToDo";

            ToDo toDo = new()
            {
                Title = title
            };
            ToDo savedToDo = new()
            {
                Id = 1,
                Title = title,
            };
            _repositoryMock
                .Setup(r => r.Insert(It.IsAny<ToDo>()))
                .ReturnsAsync(savedToDo);
            ToDo result = await _useCase.Execute(toDo);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Title.Should().Be(title);
            _repositoryMock.Verify(
                r => r.Insert(It.Is<ToDo>(t => t.Title.Equals(title))),
                Times.Once
            );
        }

        [Fact]
        public async Task ShouldNotSaveFinishedAtBeforeCreatedAt()
        {
            ToDo toDo = new()
            {
                Id = 1,
                Title = "Tarefa com erro",
                CreatedAt = DateTime.Now,
                FinishedAt = DateTime.Now.AddHours(-1)
            };

            Func<Task> act = async () => await _useCase.Execute(toDo);

            await act.Should()
                     .ThrowAsync<AppInvalidDataException>()
                     .WithMessage(AppMessages.ToDoValidations.FINISHED_AT_SHOULD_BE_AFTER_CREATED_AT);

            _repositoryMock.Verify(
                r => r.Update(It.IsAny<ToDo>()),
                Times.Never
            );
        }
    }
}
