using UnityEngine;
using MySql.Data.MySqlClient;

public class AdminMYSQL : MonoBehaviour
{
    public string servidorBaseDatos;
    public string nombreBaseDatos;
    public string usuarioBaseDatos;
    public string contraseñaBaseDatos;

    private string datosConexion;
    private MySqlConnection conexion;

    // Start is called before the first frame update
    void Start()
    {
        datosConexion = "Server=" + servidorBaseDatos
                      + ";Database=" + nombreBaseDatos
                      + ";Uid=" + usuarioBaseDatos
                      + ";Pwd=" + contraseñaBaseDatos
                      + ";";

        conectarServidoBaseDatos();

    }
    private void conectarServidoBaseDatos()
    {
        conexion = new MySqlConnection(datosConexion);

        try
        {
            conexion.Open();
            Debug.Log("Conexion Realizada");
        }
        catch (MySqlException error){

            Debug.LogError("no pudo conectarse a la base de datos: " + error);
        }

        
    }

    public MySqlDataReader Select(string _select)
    {
        MySqlCommand cmd = conexion.CreateCommand();
        cmd.CommandText = "SELECT * FROM " + _select;
        MySqlDataReader Resultado = cmd.ExecuteReader();
        return Resultado;
    }

    public MySqlDataReader Insert(string _insert)
    {
        MySqlCommand cmd = conexion.CreateCommand();
        cmd.CommandText = "INSERT INTO " + _insert;
        MySqlDataReader Resultado = cmd.ExecuteReader();
        return Resultado;
    }

}
