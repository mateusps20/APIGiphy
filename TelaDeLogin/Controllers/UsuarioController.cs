using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelaDeLogin.Data;
using TelaDeLogin.Models;

namespace TelaDeLogin.Controllers
{
    [Route("usuarios")]
    public class UsuarioController : Controller
    {
        DataContext db = new DataContext();

        [Route("")]
        [Route("~/")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        [Route("Cadastrar")]
        public IActionResult Cadastrar()
        {
            return View("Cadastrar");
        }

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar(Usuarios usuarios)
        {
            db.Usuarios.Add(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Home()
        {
            return RedirectToAction("Index", "Giphy");
        }

        [HttpPost]
        [Route("Index")]
        public IActionResult Home(string usuario, string senha)
        {
            ViewBag.usuarios = db.Usuarios.Where(x => x.Usuario.Contains(usuario) && x.Senha.Contains(senha)).FirstOrDefault();
            if(ViewBag.usuarios != null)
            {
                return RedirectToAction("Index", "Giphy");
            }
            else
            {
                ViewBag.Erro = "Credenciais Incorretas!";
                return View("Index");
            }
            
        }

        
    }
}
