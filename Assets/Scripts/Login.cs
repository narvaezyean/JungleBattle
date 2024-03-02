using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Login : MonoBehaviour
{
    public InputField userTxt;
    public InputField passwordTxt;
    public InputField ConfpasswordTxt;
    public InputField emailTxt;

    public GameObject _login;
    public GameObject _registro;

    public void Ingresar()
    {
        string _logeo = "`usuarios` WHERE `user` LIKE '" + userTxt.text +
                "' AND `pass` LIKE '" + passwordTxt.text + "'";


        AdminMYSQL _adminMYSQL = GameObject.Find("AdminBaseDatos").GetComponent<AdminMYSQL>();
        MySqlDataReader Resultado = _adminMYSQL.Select(_logeo);

        if (Resultado.HasRows)
        {
            Debug.Log("Login Correcto");
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("Login Incorrecto verifique los datos");
        }
    }

    public void registrarNuevoUsuario()
    {
        if (userTxt.text.Length >= 3 && userTxt.text.Length <= 12)
        {
            if (passwordTxt.text == ConfpasswordTxt.text)
            {
                string _logeo = "`usuarios` WHERE `user` LIKE '" + userTxt.text + "'";
                AdminMYSQL _adminMYSQL = GameObject.Find("AdminBaseDatos").GetComponent<AdminMYSQL>();
                MySqlDataReader Resultado = _adminMYSQL.Select(_logeo);

                if (Resultado.HasRows)
                {
                    Debug.Log("El Usuario ya existe");
                    Resultado.Close();
                }
                else
                {
                    Resultado.Close();
                    _logeo = "`usuarios` (`id`, `user`, `pass`,`email`) VALUES (NULL, '" + userTxt.text + "', '" + passwordTxt.text + "', '" + emailTxt.text + "')";
                    Resultado = _adminMYSQL.Insert(_logeo);
                    Debug.Log("Usuario creado con exito");
                    Resultado.Close();
                    abrirCerrarRegistro();
                }
            }
            else
            {
                Debug.Log("La contraseña no es valida");
            }
        }
        else
        {
            Debug.Log("El usuario debe tener minimo 3 maximo 12 caracteres");
        }
    }

    public void abrirCerrarRegistro()
    {
        if (_login.activeSelf)
        {
            _login.SetActive(false);
            _registro.SetActive(true);
        }
        else
        {
            _login.SetActive(true);
            _registro.SetActive(false);
        }

    }

}
