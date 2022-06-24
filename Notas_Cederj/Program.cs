using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Notas_Cederj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var arquivoGeral = "multipla_escolha.txt";
            var novoArquivo = "computação_niteroi.txt";

            using (var fluxoArquivo = new FileStream(arquivoGeral, FileMode.Open))
            using (var leitor = new StreamReader(fluxoArquivo))
            using (var fluxoNovoArquivo = new FileStream(novoArquivo, FileMode.Create))
            using (var escritor = new StreamWriter(fluxoNovoArquivo))
            {
                while (!leitor.EndOfStream)
                {
                    var linha = leitor.ReadLine();
                    var linhaSelecionada = SelecionarLinha(linha);
                    
                    if (linhaSelecionada != null)
                    {
                        escritor.WriteLine(linhaSelecionada);
                        escritor.Flush();
                    }
                }
            }

            var arquivoRedacao = "redação.txt";
            var redacaoComputacao = "redação_computação.txt";

            using (var fluxoRedacao = new FileStream(arquivoRedacao, FileMode.Open))
            using (var leitorRedacao = new StreamReader(fluxoRedacao))
            using (var fluxoRedacaoComputacao = new FileStream(redacaoComputacao, FileMode.Create))
            using (var escritorRedacaoComputacao = new StreamWriter(fluxoRedacaoComputacao))
            {
                while (!leitorRedacao.EndOfStream)
                {
                    var redacao = leitorRedacao.ReadLine();
                    var selecionaComputacao = SelecionarLinha(redacao);

                    if (selecionaComputacao != null)
                    {
                        escritorRedacaoComputacao.WriteLine(selecionaComputacao);
                        escritorRedacaoComputacao.Flush();
                    }
                }
            }

            var arquivoComputacao = "computação_niteroi.txt";
            var arquivoHabilitados = "habilitados_redação.txt";
            
            using (var fluxoComputacao = new FileStream(arquivoComputacao, FileMode.Open))
            using (var leitorComputacao = new StreamReader(fluxoComputacao))
            using (var fluxoHabilitados = new FileStream(arquivoHabilitados, FileMode.Create))
            using (var escritorHabilitados = new StreamWriter(fluxoHabilitados))
           
            {
                while (!leitorComputacao.EndOfStream)
                {
                    var candidato = leitorComputacao.ReadLine();
                    var selecionaHabilitado = VerificarHabilitado(candidato);

                    if (selecionaHabilitado != null)
                    {
                        escritorHabilitados.WriteLine(selecionaHabilitado);
                        escritorHabilitados.Flush();
                    }
                }
            }

            var arquivoNotas = "habilitados_redação.txt";
            var arquivoNotasRedacao = "redação_computação.txt";

            using (var fluxoNotas = new FileStream(arquivoNotas, FileMode.Open))
            using (var leitorNotas = new StreamReader(fluxoNotas))
            using (var fluxoNotasRedacao = new FileStream(arquivoNotasRedacao, FileMode.Open))
            using (var leitorNotasRedacao = new StreamReader(fluxoNotasRedacao))
            {
                var notas = new List<Double>();

                while (!leitorNotas.EndOfStream)
                {
                    var habilitado = leitorNotas.ReadLine();
                    string[] campos = habilitado.Split(",");

                    var nota = Double.Parse(campos[6]);

                    notas.Add(nota);
                }

                var notasRedacao = new List<Double>();
                var nomes = new List<String>();

                while (!leitorNotasRedacao.EndOfStream)
                {
                    var candidato = leitorNotasRedacao.ReadLine();
                    string[] camposRedacao = candidato.Split(",");

                    var notaRedacao = Double.Parse(camposRedacao[6]);
                    var nome = camposRedacao[2];

                    notasRedacao.Add(notaRedacao);
                    nomes.Add(nome);
                }

                Console.WriteLine("Médias por ordem de matricula");
                for(var i = 0; i < notas.Count; i++ )
                {
                    Console.WriteLine($"{nomes[i]}: {notas[i] + notasRedacao[i] * 0.37}");
                }

                var medias = new List<Double>();
                for(var i = 0; i < notasRedacao.Count;i++)
                {
                    var media = notas[i] + notasRedacao[i] * 0.37;
                    medias.Add(media);
                }

                medias.Sort();
                medias.Reverse();

                Console.WriteLine("\nMédias em ordem decrescente:");
                foreach (var media in medias)
                {
                    Console.WriteLine(media);
                }

            }
                Console.ReadLine();
        }

        static string SelecionarLinha(string linha)
        {
            string[] campos = linha.Split(",");
            var curso = campos[3];
            var polo = campos[4];

            if (curso == "Computação" && polo == "Niterói")
            {
                return linha;
            }
            else
            {
                return null;
            }
        } 
        
        static string VerificarHabilitado(string candidato)
        {
            string[] campos = candidato.Split(",");
            var elimina = campos[6];

            if (elimina != "FALTOSO" && elimina != "ELIMINADO")
            {
                return candidato;
            }
            else
            {
                return null;
            }
        }
    }
}
