using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using BookStore.Business.Notificacoes;
using BookStore.Business.Services;
using Moq;
using System.Linq.Expressions;

namespace BookStore.Tests;
public class AutorServiceTests
{
    private readonly Mock<IAutorRepository> _mockAutorRepository;
    private readonly Mock<ILivroRepository> _mockLivroRepository;
    private readonly Mock<INotificador> _mockNotificador;
    private readonly AutorService _autorService;

    public AutorServiceTests()
    {
        _mockAutorRepository = new Mock<IAutorRepository>();
        _mockLivroRepository = new Mock<ILivroRepository>();
        _mockNotificador = new Mock<INotificador>();

        _autorService = new AutorService(
            _mockAutorRepository.Object,
            _mockLivroRepository.Object,
            _mockNotificador.Object);
    }

    [Fact]
    [Trait("Categoria", "AutorServiceTests")]
    public async Task AdicionarAsync_AutorValido_DeveInserirAutor()
    {
        //Arrange
        var autor = new Autor { Nome = "Autor Teste" };

        _mockAutorRepository
            .Setup(x => x.ExistsByExpressionAsync(It.IsAny<Expression<Func<Autor, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        //Act
        await _autorService.AdicionarAsync(autor);

        //Assert
        _mockAutorRepository.Verify(x => x.InsertAsync(autor, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Categoria", "AutorServiceTests")]
    public async Task AdicionarAsync_AutorJaExiste_DeveNotificarErro()
    {
        //Arrange
        var autor = new Autor { Nome = "Duplicado" };

        _mockAutorRepository
            .Setup(x => x.ExistsByExpressionAsync(It.IsAny<Expression<Func<Autor, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _mockNotificador
            .Setup(x => x.TemNotificacao())
            .Returns(true);

        //Act
        await _autorService.AdicionarAsync(autor);

        //Assert
        Assert.True(_mockNotificador.Object.TemNotificacao());
        _mockNotificador.Verify(x => x.Handle(It.Is<Notificacao>(n => n.Mensagem == "Já existe um autor com este nome infomado.")), Times.Once);
        _mockAutorRepository.Verify(x => x.InsertAsync(It.IsAny<Autor>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Categoria", "AutorServiceTests")]
    public async Task AtualizarAsync_AutorNaoEncontrado_DeveNotificarErro()
    {
        //Arrange
        var autor = new Autor { Id = 1, Nome = "Atualizar" };

        _mockAutorRepository
            .Setup(x => x.ExistsByExpressionAsync(It.IsAny<Expression<Func<Autor, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _mockNotificador
            .Setup(x => x.TemNotificacao())
            .Returns(true);

        //Act
        await _autorService.AtualizarAsync(autor);

        //Assert
        Assert.True(_mockNotificador.Object.TemNotificacao());
        _mockNotificador.Verify(x => x.Handle(It.Is<Notificacao>(n => n.Mensagem == $"Autor {autor.Id} não encontrado.")), Times.Once);
        _mockAutorRepository.Verify(x => x.UpdateAsync(It.IsAny<Autor>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Categoria", "AutorServiceTests")]
    public async Task RemoverAsync_AutorRelacionado_DeveNotificarErro()
    {
        //Arrange
        var autorId = 1;

        _mockLivroRepository
            .Setup(x => x.ExistsByExpressionAsync(It.IsAny<Expression<Func<Livro, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _mockNotificador
            .Setup(x => x.TemNotificacao())
            .Returns(true);

        //Act
        await _autorService.RemoverAsync(autorId);

        // Assert
        Assert.True(_mockNotificador.Object.TemNotificacao());
        _mockNotificador.Verify(x => x.Handle(It.Is<Notificacao>(n => n.Mensagem == $"Exclusão do Autor: {autorId} não permitida, o registro já está relacionado à um livro")), Times.Once);
        _mockAutorRepository.Verify(x => x.RemoveAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Categoria", "AutorServiceTests")]
    public async Task RemoverAsync_AutorNaoRelacionado_DeveRemoverAutor()
    {
        //Arrange
        var autorId = 1;

        _mockLivroRepository
            .Setup(x => x.ExistsByExpressionAsync(It.IsAny<Expression<Func<Livro, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        //Act
        await _autorService.RemoverAsync(autorId);

        //Assert
        _mockAutorRepository.Verify(x => x.RemoveAsync(autorId, It.IsAny<CancellationToken>()), Times.Once);
    }
}
