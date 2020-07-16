using System;
using System.IO;
using Aula37ASP_E_Players.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aula37ASP_E_Players.Controllers
{
    public class NoticiaController : Controller
    {
        Noticia noticiaModel = new Noticia();
        public IActionResult Index()
        {
            ViewBag.Noticias = noticiaModel.ReadAll();
            return View();
        }
        public IActionResult Cadastrar(IFormCollection form)
        {
            Noticia novaNoticia = new Noticia();
            novaNoticia.IdNoticia = Int32.Parse(form["IdNoticia"]);
            novaNoticia.Titulo    = form["Titulo"];
            novaNoticia.Imagem   = form["Imagem"];

            var file    = form.Files[0];
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticias");

            if(file != null){
                if(!Directory.Exists(folder)){
                Directory.CreateDirectory(folder);
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName );
            using(var stream = new FileStream(path, FileMode.Create)){
                file.CopyTo(stream);
            }
            novaNoticia.Imagem = file.FileName;
            }
            else{
                novaNoticia.Imagem   = "padrao.png";
            }

            noticiaModel.Create(novaNoticia);
            ViewBag.Noticias = noticiaModel.ReadAll();

            return LocalRedirect("~/Noticia");
        }

        [Route("[Controller]/{id}")]
        public IActionResult Excluir(int id){
            noticiaModel.Delete(id);
            ViewBag.Noticia = noticiaModel.ReadAll();
            return LocalRedirect("~/Noticia");
        }
        

    }
}