using System;
using System.IO;
using Aula37ASP_E_Players.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aula37ASP_E_Players.Controllers
{
    public class EquipeController : Controller
    {
        Equipe equipeModel = new Equipe();
        public IActionResult Index()
        {
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }
        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe novaEquipe = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse(form["IdEquipe"]);
            novaEquipe.Nome     = form["Nome"];
            novaEquipe.Imagem   = form["Imagem"];

            var file    = form.Files[0];
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

            if(file != null){
                if(!Directory.Exists(folder)){
                Directory.CreateDirectory(folder);
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName );
            using(var stream = new FileStream(path, FileMode.Create)){
                file.CopyTo(stream);
            }
            novaEquipe.Imagem = file.FileName;
            }
            else{
                novaEquipe.Imagem   = "padrao.png";
            }

            equipeModel.Create(novaEquipe);
            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe");
        }

        [Route("[Controller]/{id}")]
        public IActionResult Excluir(int id){
            equipeModel.Delete(id);
            ViewBag.Equipes = equipeModel.ReadAll();
            return LocalRedirect("~/Equipe");

        }
    }
}