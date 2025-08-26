using System.Data;
using DEV_MANHA.Data;
using DEV_MANHA.Models;
using DEV_MANHA.Models.Data;

namespace DEV_MANHA.Repositories;

public class SessaoRepository
{
    public int Inserir(Sessao s)
    {
        const string sql =
            @"
            INSERT INTO Sessoes (IdFilme, Data, Hora)
            VALUES (@IdFilme, @Data, @Hora);
            SELECT CAST(SCOPE_IDENTITY() AS int);";

        using var conn = Db.GetConnection();
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        cmd.AddParameter("@IdFilme", s.IdFilme);
        cmd.AddParameter("@Data", s.Data.ToDateTime(TimeOnly.MinValue));
        cmd.AddParameter("@Hora", s.Hora.ToTimeSpan());

        return (int)(cmd.ExecuteScalar() ?? 0);
    }

    public List<Sessao> ListarPorFilme(int idFilme)
    {
        const string sql =
            @"
            SELECT IdSessao, IdFilme, Data, Hora 
            FROM Sessoes 
            WHERE IdFilme=@IdFilme
            ORDER BY Data, Hora";

        using var conn = Db.GetConnection();
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.AddParameter("@IdFilme", idFilme);

        using var dr = cmd.ExecuteReader();
        var lista = new List<Sessao>();
        while (dr.Read())
        {
            lista.Add(
                new Sessao
                {
                    Id = dr.GetInt32(0),
                    IdFilme = dr.GetInt32(1),
                    Data = DateOnly.FromDateTime(dr.GetDateTime(2)),
                    Hora = TimeOnly.FromTimeSpan((TimeSpan)dr.GetValue(3)),
                }
            );
        }
        return lista;
    }

    public Sessao? ObterPorId(int id)
    {
        const string sql = "SELECT IdSessao, IdFilme, Data, Hora FROM Sessoes WHERE IdSessao=@Id";

        using var conn = Db.GetConnection();
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.AddParameter("@Id", id);

        using var dr = cmd.ExecuteReader();
        if (!dr.Read())
            return null;

        return new Sessao
        {
            Id = dr.GetInt32(0),
            IdFilme = dr.GetInt32(1),
            Data = DateOnly.FromDateTime(dr.GetDateTime(2)),
            Hora = TimeOnly.FromTimeSpan((TimeSpan)dr.GetValue(3)),
        };
    }

    public bool Atualizar(Sessao s)
    {
        const string sql =
            @"
            UPDATE Sessoes 
            SET IdFilme=@IdFilme, Data=@Data, Hora=@Hora 
            WHERE IdSessao=@IdSessao";

        using var conn = Db.GetConnection();
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        cmd.AddParameter("@IdFilme", s.IdFilme);
        cmd.AddParameter("@Data", s.Data.ToDateTime(TimeOnly.MinValue));
        cmd.AddParameter("@Hora", s.Hora.ToTimeSpan());
        cmd.AddParameter("@IdSessao", s.Id);

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Excluir(int id)
    {
        const string sql = "DELETE FROM Sessoes WHERE IdSessao=@IdSessao";
        using var conn = Db.GetConnection();
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.AddParameter("@IdSessao", id);

        return cmd.ExecuteNonQuery() > 0;
    }
}
