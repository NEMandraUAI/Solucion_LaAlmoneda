using BE.Entidades;
using BE.Excepciones;
using DAL.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioBLL
    {
        private readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();

        private static readonly HashSet<int> _sesionesActivas = new HashSet<int>();
        public UsuarioBE Login(string email, string contrasenaPlana)
        {
            UsuarioBE user = _usuarioDAL.ObtenerPorEmail(email);
            if (user == null)
                throw new LoginException("Credenciales incorrectas o usuario inexistente.");
            string hashContrasena = HashearSHA256(contrasenaPlana);
            if (user.Contrasena != hashContrasena)
                throw new LoginException("Credenciales incorrectas.");
            lock (_sesionesActivas)
            {
                if (_sesionesActivas.Contains(user.Id))
                    throw new LoginException($"El usuario {user.Nombre} ya tiene una sesión abierta en esta máquina.");
                _sesionesActivas.Add(user.Id);
            }
            return user;
        }
        public void Logout(int usuarioId)
        {
            lock (_sesionesActivas)
            {
                if (_sesionesActivas.Contains(usuarioId))
                {
                    _sesionesActivas.Remove(usuarioId);
                }
            }
        }
        private string HashearSHA256(string textoPlano)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(textoPlano));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
