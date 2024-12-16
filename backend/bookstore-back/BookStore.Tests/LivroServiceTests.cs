using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using BookStore.Business.Services;
using Moq;
using System.Linq.Expressions;

namespace BookStore.Tests;
public class LivroServiceTests
{
    private readonly Mock<ILivroRepository> _mockLivroRepository;
    private readonly Mock<IAutorRepository> _mockAutorRepository;
    private readonly Mock<IAssuntoRepository> _mockAssuntoRepository;
    private readonly Mock<INotificador> _mockNotificador;
    private readonly LivroService _livroService;

    public LivroServiceTests()
    {
        _mockLivroRepository = new Mock<ILivroRepository>();
        _mockAutorRepository = new Mock<IAutorRepository>();
        _mockAssuntoRepository = new Mock<IAssuntoRepository>();
        _mockNotificador = new Mock<INotificador>();

        _livroService = new LivroService(
            _mockLivroRepository.Object,
            _mockAutorRepository.Object,
            _mockAssuntoRepository.Object,
            _mockNotificador.Object);
    }

    [Fact]
    [Trait("Categoria", "LivroServiceTests")]
    public async Task AdicionarAsync_DeveAdicionarLivroQuandoValido()
    {
        // Arrange
        var livro = new Livro { Titulo = "Novo Livro", Autores = new List<Autor> { new Autor { Id = 1 } }, Assuntos = new List<Assunto> { new Assunto { Id = 1 } } };

        _mockLivroRepository
            .Setup(x => x.ExistsByExpressionAsync(It.IsAny<Expression<Func<Livro, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _mockAutorRepository
            .Setup(x => x.FindByExpressionAsync(It.IsAny<Expression<Func<Autor, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(livro.Autores);

        _mockAssuntoRepository
            .Setup(x => x.FindByExpressionAsync(It.IsAny<Expression<Func<Assunto, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(livro.Assuntos);

        // Act
        await _livroService.AdicionarAsync(livro);

        // Assert
        _mockLivroRepository.Verify(x => x.InsertAsync(livro, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Categoria", "LivroServiceTests")]
    public async Task AtualizarAsync_DeveAtualizarLivroQuandoExistir()
    {
        // Arrange
        var livro = new Livro { Id = 1, Titulo = "Livro Atualizado", Autores = new List<Autor> { new Autor { Id = 1 } }, Assuntos = new List<Assunto> { new Assunto { Id = 1 } } };

        _mockLivroRepository
            .Setup(x => x.ExistsByExpressionAsync(It.IsAny<Expression<Func<Livro, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        await _livroService.AtualizarAsync(livro);

        // Assert
        _mockLivroRepository.Verify(x => x.RemoveAsync(livro.Id, It.IsAny<CancellationToken>()), Times.Once);
        _mockLivroRepository.Verify(x => x.InsertAsync(livro, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Categoria", "LivroServiceTests")]
    public async Task RemoverAsync_DeveRemoverLivroPorId()
    {
        // Arrange
        var livroId = 1;

        // Act
        await _livroService.RemoverAsync(livroId);

        // Assert
        _mockLivroRepository.Verify(x => x.RemoveAsync(livroId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Categoria", "LivroServiceTests")]
    public async Task AtualizarAsync_LivroNaoExiste_DeveNotificarEPararExecucao()
    {
        // Arrange
        var livro = new Livro { Id = 1, Titulo = "Livro Inexistente" };

        _mockNotificador
            .Setup(x => x.TemNotificacao())
            .Returns(true);

        _mockLivroRepository
            .Setup(x => x.ExistsByExpressionAsync(It.IsAny<Expression<Func<Livro, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        await _livroService.AtualizarAsync(livro, CancellationToken.None);

        // Assert
        Assert.True(_mockNotificador.Object.TemNotificacao());
        _mockLivroRepository.Verify(x => x.InsertAsync(It.IsAny<Livro>(), It.IsAny<CancellationToken>()), Times.Never);
        _mockLivroRepository.Verify(x => x.RemoveAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
