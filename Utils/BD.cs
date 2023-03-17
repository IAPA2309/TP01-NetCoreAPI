using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;

namespace Pizzas.API.Models{
    public static class BD {
        private static string _connectionString = "Server=A-PHZ2-AMI-009;DataBase=DAI-Pizzas;Trusted_Connection=True;";

        public static List<Pizza> GetAll() {
            string SQL = "SELECT Id, Nombre, LibreGluten, Importe, Descripcion FROM Pizzas";
            List<Pizza> returnList = new List<Pizza>();
            
            using (SqlConnection db = new SqlConnection(_connectionString)) {
              returnList = db.Query<Pizza>(SQL).ToList();
            }
            return returnList;
        }

        public static Pizza GetById(int id) {
            Pizza pizza = null;
            string SQL = "SELECT Id, Nombre, LibreGluten, Importe, Descripcion FROM Pizzas WHERE Id = @idPizza";

            using (SqlConnection db = new SqlConnection(_connectionString)) {
                pizza = db.QueryFirstOrDefault<Pizza>(SQL, new { idPizza = id });
            }
            return pizza;
        }

        public static int Insert(Pizza pizza) {
            int intRowsAffected = 0;
            string SQL = "INSERT INTO Pizzas (Nombre, LibreGluten, Importe, Descripcion) VALUES (@nombre, @libreGluten, @importe, @descripcion)";
                
            using (SqlConnection db = new SqlConnection(_connectionString)) {
                intRowsAffected = db.Execute(SQL, new {  nombre = pizza.Nombre, libreGluten = pizza.LibreGluten, importe = pizza.Importe,descripcion = pizza.Descripcion });
            }
            return intRowsAffected;
        }

        public static int UpdateById(Pizza pizza) {
            int intRowsAffected = 0;
            string SQL = "UPDATE Pizzas SET Nombre = @nombre, LibreGluten = @libreGluten, Importe = @importe, Descripcion = @descripcion WHERE Id = @idPizza";

            using (var db = new SqlConnection(_connectionString)) {
                intRowsAffected = db.Execute(SQL, new { idPizza = pizza.Id, nombre = pizza.Nombre, libreGluten = pizza.LibreGluten, importe = pizza.Importe, descripcion = pizza.Descripcion });
            }
            return intRowsAffected;
        }
        public static int DeleteById(int id) {
            int intRowsAffected = 0;
            string SQL = "DELETE FROM Pizzas WHERE Id = @idPizza";
            
            using (SqlConnection db = new SqlConnection(_connectionString)) {
                intRowsAffected = db.Execute(SQL, new { idPizza = id });
            }
            return intRowsAffected;
        }
    }
}