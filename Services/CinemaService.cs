using System;
using DEV_MANHA.Models;
using DEV_MANHA.Models.Enums;
using DEV_MANHA.Repositories;

namespace DEV_MANHA.Services;

public class CinemaService
{
    private readonly FilmeRepository _filmeRepo;
    private readonly SessaoRepository _sessaoRepo;

    public CinemaService()
    {
        _filmeRepo = new FilmeRepository();
        _sessaoRepo = new SessaoRepository();
    }

    public void Executar()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Sistema de Filmes e Sessões");
            Console.WriteLine("1 - Cadastrar Filme");
            Console.WriteLine("2 - Listar Filmes");
            Console.WriteLine("3 - Atualizar Filme");
            Console.WriteLine("4 - Excluir Filme");
            Console.WriteLine("5 - Cadastrar Sessão");
            Console.WriteLine("6 - Listar Sessões de um Filme");
            Console.WriteLine("7 - Atualizar Sessão");
            Console.WriteLine("8 - Excluir Sessão");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");

            var opcao = Console.ReadLine();
            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    CadastrarFilme();
                    break;
                case "2":
                    ListarFilmes();
                    break;
                case "3":
                    AtualizarFilme();
                    break;
                case "4":
                    ExcluirFilme();
                    break;
                case "5":
                    CadastrarSessao();
                    break;
                case "6":
                    ListarSessoes();
                    break;
                case "7":
                    AtualizarSessao();
                    break;
                case "8":
                    ExcluirSessao();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    private void CadastrarFilme()
    {
        Console.Write("Título: ");
        string titulo = Console.ReadLine() ?? "";

        Console.WriteLine(
            "Gêneros disponíveis: " + string.Join(", ", Enum.GetNames(typeof(Genero)))
        );
        Console.Write("Gênero: ");
        if (!Enum.TryParse<Genero>(Console.ReadLine(), true, out var genero))
        {
            Console.WriteLine("⚠ Gênero inválido!");
            return;
        }

        Console.Write("Ano de lançamento (ex: 2025): ");
        if (!int.TryParse(Console.ReadLine(), out var ano))
        {
            Console.WriteLine("⚠ Ano inválido!");
            return;
        }

        var filme = new Filme
        {
            Titulo = titulo,
            Genero = genero,
            Ano = ano,
        };
        var id = _filmeRepo.Inserir(filme);
        Console.WriteLine($"Filme cadastrado com ID {id}.");
    }

    private void ListarFilmes()
    {
        var filmes = _filmeRepo.Listar();
        if (filmes.Count == 0)
        {
            Console.WriteLine("Nenhum filme cadastrado.");
            return;
        }
        Console.WriteLine("📽 Filmes:");
        foreach (var f in filmes)
            Console.WriteLine(f);
    }

    private void AtualizarFilme()
    {
        ListarFilmes();
        Console.Write("ID do filme para atualizar: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }
        var f = _filmeRepo.ObterPorId(id);
        if (f == null)
        {
            Console.WriteLine("Filme não encontrado.");
            return;
        }

        Console.Write($"Novo título ({f.Titulo}): ");
        var titulo = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(titulo))
            f.Titulo = titulo;

        Console.Write($"Novo gênero ({f.Genero}): ");
        var gStr = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(gStr) && Enum.TryParse<Genero>(gStr, true, out var g))
            f.Genero = g;

        Console.Write($"Novo ano ({f.Ano}): ");
        var anoStr = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(anoStr) && int.TryParse(anoStr, out var ano))
            f.Ano = ano;

        var ok = _filmeRepo.Atualizar(f);
        Console.WriteLine(ok ? "Atualizado." : "Nada alterado.");
    }

    private void ExcluirFilme()
    {
        ListarFilmes();
        Console.Write("ID do filme para excluir: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }
        var ok = _filmeRepo.Excluir(id);
        Console.WriteLine(
            ok ? "Filme excluído (sessões em cascade se existirem)." : "Filme não encontrado."
        );
    }

    private void CadastrarSessao()
    {
        ListarFilmes();
        Console.Write("ID do filme para criar sessão: ");
        if (!int.TryParse(Console.ReadLine(), out var idFilme))
        {
            Console.WriteLine("ID inválido");
            return;
        }
        Console.Write("Data (yyyy-MM-dd): ");
        if (!DateOnly.TryParse(Console.ReadLine(), out var data))
        {
            Console.WriteLine("Data inválida");
            return;
        }
        Console.Write("Hora (HH:mm): ");
        if (!TimeOnly.TryParse(Console.ReadLine(), out var hora))
        {
            Console.WriteLine("Hora inválida");
            return;
        }

        var sessao = new Sessao
        {
            IdFilme = idFilme,
            Data = data,
            Hora = hora,
        };
        var id = _sessaoRepo.Inserir(sessao);
        Console.WriteLine($"Sessão cadastrada com ID {id}.");
    }

    private void ListarSessoes()
    {
        ListarFilmes();
        Console.Write("ID do filme para listar sessões: ");
        if (!int.TryParse(Console.ReadLine(), out var idFilme))
        {
            Console.WriteLine("ID inválido");
            return;
        }
        var sessoes = _sessaoRepo.ListarPorFilme(idFilme);
        if (sessoes.Count == 0)
        {
            Console.WriteLine("Nenhuma sessão.");
            return;
        }
        foreach (var s in sessoes)
            Console.WriteLine(s);
    }

    private void AtualizarSessao()
    {
        ListarSessoes();
        Console.Write("ID da sessão para atualizar: ");
        if (!int.TryParse(Console.ReadLine(), out var idSessao))
        {
            Console.WriteLine("ID inválido");
            return;
        }
        var s = _sessaoRepo.ObterPorId(idSessao);
        if (s == null)
        {
            Console.WriteLine("Sessão não encontrada");
            return;
        }

        Console.Write($"Nova data ({s.Data:yyyy-MM-dd}) (ENTER para manter): ");
        var dataStr = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(dataStr) && DateOnly.TryParse(dataStr, out var novaData))
            s.Data = novaData;

        Console.Write($"Nova hora ({s.Hora:HH:mm}) (ENTER para manter): ");
        var horaStr = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(horaStr) && TimeOnly.TryParse(horaStr, out var novaHora))
            s.Hora = novaHora;

        var ok = _sessaoRepo.Atualizar(s);
        Console.WriteLine(ok ? "Sessão atualizada." : "Nada alterado.");
    }

    private void ExcluirSessao()
    {
        ListarSessoes();
        Console.Write("ID da sessão para excluir: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("ID inválido");
            return;
        }
        var ok = _sessaoRepo.Excluir(id);
        Console.WriteLine(ok ? "Sessão excluída." : "Sessão não encontrada.");
    }
}
