using System.Data;
using DEV_MANHA.Data;
using DEV_MANHA.Models;
using DEV_MANHA.Models.Data;
using DEV_MANHA.Models.Enums;

namespace DEV_MANHA.Repositories;

public class FilmeRepository
{
    public int Inserir(Filme f)
    {
        const string sql =
            @"
            INSERT INTO Filmes (Titulo, Genero, Ano)
            VALUES (@Titulo,@Genero,@Ano);
            SELECT CAST(SCOPE_IDENTITY() AS int);";

        using var conn = Db.GetConnection();
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        cmd.AddParameter("@Titulo", f.Titulo);
        cmd.AddParameter("@Genero", f.Genero.ToString());
        cmd.AddParameter("@Ano", f.Ano);

        return (int)(cmd.ExecuteScalar() ?? 0);
    }

    public List<Filme> Listar()
    {
        const string sql = "SELECT IdFilme, Titulo, Genero, Ano FROM Filmes ORDER BY Titulo";
        using var conn = Db.GetConnection();
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        using var dr = cmd.ExecuteReader();
        var lista = new List<Filme>();
        while (dr.Read())
        {
            lista.Add(
                new Filme
                {
                    Id = dr.GetInt32(0),
                    Titulo = dr.GetString(1),
                    Genero = Enum.Parse<Genero>(dr.GetString(2)),
                    Ano = dr.GetInt32(3),
                }
            );
        }
        return lista;
    }

    public Filme? ObterPorId(int id)
    {
        const string sql = "SELECT IdFilme, Titulo, Genero, Ano FROM Filmes WHERE IdFilme=@Id";

        using var conn = Db.GetConnection();
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.AddParameter("@Id", id);

        using var dr = cmd.ExecuteReader();
        if (!dr.Read())
            return null;

        return new Filme
        {
            Id = dr.GetInt32(0),
            Titulo = dr.GetString(1),
            Genero = Enum.Parse<Genero>(dr.GetString(2)),
            Ano = dr.GetInt32(3),
        };
    }

    public bool Atualizar(Filme f)
    {
        const string sql =
            @"
            UPDATE Filmes 
            SET Titulo=@Titulo, Genero=@Genero, Ano=@Ano
            WHERE IdFilme=@Id";

        using var conn = Db.GetConnection();
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        cmd.AddParameter("@Id", f.Id);
        cmd.AddParameter("@Titulo", f.Titulo);
        cmd.AddParameter("@Genero", f.Genero.ToString());
        cmd.AddParameter("@Ano", f.Ano);

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Excluir(int id)
    {
        const string sql = "DELETE FROM Filmes WHERE IdFilme=@Id";

        using var conn = Db.GetConnection();
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.AddParameter("@Id", id);

        return cmd.ExecuteNonQuery() > 0;
    }
}
