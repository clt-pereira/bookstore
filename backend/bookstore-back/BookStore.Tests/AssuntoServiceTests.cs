using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using BookStore.Business.Notificacoes;
using BookStore.Business.Services;
using Moq;
using System.Linq.Expressions;

namespace BookStore.Tests;
public class AssuntoServiceTests
{
    private readonly Mock<IAssuntoRepository> _mockAssuntoRepository;
    private readonly Mock<ILivroRepository> _mockLivroRepository;
    private readonly Mock<INotificador> _mockNotificador;
    private readonly AssuntoService _assuntoService;

    public AssuntoServiceTests()
    {
        _mockAssuntoRepository = new Mock<IAssuntoRepository>();
        _mockLivroRepository = new Mock<ILivroRepository>();
        _mockNotificador = new Mock<INotificador>();

        _assuntoService = new AssuntoService(
            _mockAssuntoRepository.Object,
            _mockLivroRepository.Object,
            _mockNotificador.Object);
    }

    [Fact]
    [Trait("Categoria", "AssuntoServiceTests")]
    public async Task AdicionarAsync_AssuntoValido_DeveInserirAssunto()
    {
        //Arrange
        var assunto = new Assunto { Descricao = "Teste" };

        _mockAssuntoRepository
            .Setup(x => x.ExistsByExpressionAsync(It.IsAny<Expression<Func<Assunto, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        //Act
        await _assuntoService.AdicionarAsync(assunto);

        //Assert
        _mockAssuntoRepository.Verify(x => x.InsertAsync(assunto, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Categoria", "AssuntoServiceTests")]
    public async Task AdicionarAsync_AssuntoJaExiste_DeveNotificarErro()
    {
        //Arrange
        var assunto = new Assunto { Descricao = "Duplicado" };

        _mockAssuntoRepository
            .Setup(x => x.ExistsByExpressionAsync(It.IsAny<Expression<Func<Assunto, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _mockNotificador
            .Setup(x => x.TemNotificacao())
            .Returns(true);

        //Act
        await _assuntoService.AdicionarAsync(assunto);

        //Assert
        Assert.True(_mockNotificador.Object.TemNotificacao());
        _mockNotificador.Verify(x => x.Handle(It.Is<Notificacao>(n => n.Mensagem == "Já existe um assunto com a descrição informada.")), Times.Once);
        _mockAssuntoRepository.Verify(x => x.InsertAsync(It.IsAny<Assunto>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Categoria", "AssuntoServiceTests")]
    public async Task AtualizarAsync_AssuntoNaoEncontrado_DeveNotificarErro()
    {
        //Arrange
        var assunto = new Assunto { Id = 1, Descricao = "Atualizar" };

        _mockAssuntoRepository
            .Setup(x => x.ExistsByExpressionAsync(It.IsAny<Expression<Func<Assunto, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _mockNotificador
            .Setup(x => x.TemNotificacao())
            .Returns(true);

        //Act
        await _assuntoService.AtualizarAsync(assunto);

        //Assert
        Assert.True(_mockNotificador.Object.TemNotificacao());
        _mockNotificador.Verify(x => x.Handle(It.Is<Notificacao>(n => n.Mensagem == $"Assunto {assunto.Id} não encontrado.")), Times.Once);
        _mockAssuntoRepository.Verify(x => x.UpdateAsync(It.IsAny<Assunto>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Categoria", "AssuntoServiceTests")]
    public async Task RemoverAsync_AssuntoRelacionado_DeveNotificarErro()
    {
        //Arrange
        var assuntoId = 1;

        _mockLivroRepository
            .Setup(x => x.ExistsByExpressionAsync(It.IsAny<Expression<Func<Livro, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _mockNotificador
            .Setup(x => x.TemNotificacao())
            .Returns(true);

        //Act
        await _assuntoService.RemoverAsync(assuntoId);

        // Assert
        Assert.True(_mockNotificador.Object.TemNotificacao());
        _mockNotificador.Verify(x => x.Handle(It.Is<Notificacao>(n => n.Mensagem == $"Exclusão do Assunto: {assuntoId} não permitida, o registro já está relacionado à um livro")), Times.Once);
        _mockAssuntoRepository.Verify(x => x.RemoveAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Categoria", "AssuntoServiceTests")]
    public async Task RemoverAsync_AssuntoNaoRelacionado_DeveRemoverAssunto()
    {
        //Arrange
        var assuntoId = 1;

        _mockLivroRepository
            .Setup(x => x.ExistsByExpressionAsync(It.IsAny<Expression<Func<Livro, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        //Act
        await _assuntoService.RemoverAsync(assuntoId);

        //Assert
        _mockAssuntoRepository.Verify(x => x.RemoveAsync(assuntoId, It.IsAny<CancellationToken>()), Times.Once);
    }
}
