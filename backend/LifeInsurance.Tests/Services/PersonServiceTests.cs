using AutoFixture;
using FluentAssertions;
using LifeInsurance.Application.Persons.Services;
using LifeInsurance.Domain.Entities;
using LifeInsurance.Domain.Interfaces;
using Moq;

namespace LifeInsurance.Tests.Services
{
    public class PersonServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly PersonService _personService;

        public PersonServiceTests()
        {
            _fixture = new Fixture();
            _personRepositoryMock = new Mock<IPersonRepository>();
            _personService = new PersonService(_personRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldAssignNewIdAndSetIsActiveToTrue()
        {
            // Arrange
            var person = _fixture.Build<Person>().Without(p => p.Id).With(p => p.IsActive, false).Create();
            _personRepositoryMock.Setup(r => r.AddAsync(person, CancellationToken.None)).Returns(Task.CompletedTask);

            // Act
            var result = await _personService.CreateAsync(person, CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty();
            person.IsActive.Should().BeTrue();
            _personRepositoryMock.Verify(r => r.AddAsync(person, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenPersonNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _personRepositoryMock.Setup(r => r.GetByIdAsync(id, CancellationToken.None)).ReturnsAsync((Person?)null);

            //Act
            var result = await _personService.DeleteAsync(id, CancellationToken.None);

            //Assert
            result.Should().BeFalse();
            _personRepositoryMock.Verify(r => r.GetByIdAsync(id, CancellationToken.None), Times.Once);
            _personRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Person>(), CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenPersonExists()
        {
            // Arrange
            var person = _fixture.Create<Person>();
            _personRepositoryMock.Setup(r => r.GetByIdAsync(person.Id, CancellationToken.None)).ReturnsAsync(person);
            _personRepositoryMock.Setup(r => r.DeleteAsync(person, CancellationToken.None)).Returns(Task.CompletedTask);

            // Act
            var result = await _personService.DeleteAsync(person.Id, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _personRepositoryMock.Verify(r => r.GetByIdAsync(person.Id, CancellationToken.None), Times.Once);
            _personRepositoryMock.Verify(r => r.DeleteAsync(person, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPersons()
        {
            // Arrange
            var persons = _fixture.CreateMany<Person>(3).ToList();
            _personRepositoryMock.Setup(r => r.GetAllAsync(CancellationToken.None)).ReturnsAsync(persons);

            // Act
            var result = await _personService.GetAllAsync(CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(persons);
            result.Should().HaveCount(3);
            _personRepositoryMock.Verify(r => r.GetAllAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenPersonNotFound()
        {
            // Arrange
            var person = _fixture.Create<Person>();
            _personRepositoryMock.Setup(r => r.GetByIdAsync(person.Id, CancellationToken.None)).ReturnsAsync((Person?)null);

            // Act
            var result = await _personService.UpdateAsync(person, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _personRepositoryMock.Verify(r => r.GetByIdAsync(person.Id, CancellationToken.None), Times.Once);
            _personRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Person>(), CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenPersonExists()
        {
            // Arrange
            var person = _fixture.Create<Person>();
            var existingPerson = _fixture.Build<Person>().With(p => p.Id, person.Id).Create();
            _personRepositoryMock.Setup(r => r.GetByIdAsync(person.Id, CancellationToken.None)).ReturnsAsync(existingPerson);
            _personRepositoryMock.Setup(r => r.UpdateAsync(existingPerson, CancellationToken.None)).Returns(Task.CompletedTask);

            // Act
            var result = await _personService.UpdateAsync(person, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _personRepositoryMock.Verify(r => r.GetByIdAsync(person.Id, CancellationToken.None), Times.Once);
            _personRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Person>(p =>
                p.FullName == person.FullName &&
                p.Identification == person.Identification &&
                p.Age == person.Age &&
                p.Gender == person.Gender &&
                p.IsActive == person.IsActive &&
                p.Drives == person.Drives &&
                p.UsesGlasses == person.UsesGlasses &&
                p.IsDiabetic == person.IsDiabetic &&
                p.OtherDiseases == person.OtherDiseases), CancellationToken.None), Times.Once);
        }
    }
}
