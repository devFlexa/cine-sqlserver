using DEV_MANHA.Models.Data;
using DEV_MANHA.Services;

var cs = @"Server=localhost\SQLEXPRESS;Database=CinemaDB;Trusted_Connection=True;";

Db.ConnectionString = cs;

var service = new CinemaService();
service.Executar();
