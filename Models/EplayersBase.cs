using System.Collections.Generic;
using System.IO;

namespace Aula37ASP_E_Players.Models
{
    public class EplayersBase
    {
        /// <summary>
        /// Cria o csv 
        /// </summary>
        /// <param name="PATH"></param>
        public void CreateFolderAndFile(string PATH){
            string folder  = PATH.Split("/")[0];

            if(!Directory.Exists(folder)){
                Directory.CreateDirectory(folder);
            }

            if(!File.Exists(PATH)){
                File.Create(PATH).Close();
            }
        }
        public List<string> ReadAllLinesCSV(string PATH){
            List<string> linhas = new List<string>();
            using(StreamReader file = new StreamReader(PATH)){
                string linha;
                while((linha = file.ReadLine()) !=null){
                    linhas.Add(linha);
                }
            }
            return linhas;
        }
        public void RewriteCSV(string PATH, List<string> linhas){
            using(StreamWriter output = new StreamWriter(PATH)){
                foreach (var item in linhas)
                {
                    output.Write(item + "\n");
                }
            }
        }
    }
}