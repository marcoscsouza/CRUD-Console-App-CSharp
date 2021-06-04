using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    //operação do CRUD em coleção
    public class BandaRepository
    {
        private static List<BandaModel> ListaBanda = new List<BandaModel>(); //coleção

        public void Adicionar(BandaModel bandaModel)// comando Create 
        {
            ListaBanda.Add(bandaModel);
        }

        public BandaModel EscolherNome(string nome = null) //Comando Read --- Pegar apenas um item da classe
        {
            if (string.IsNullOrWhiteSpace(nome))    // tratar as exceções nulas
            {
                return ListaBanda.Find(x =>
                x.NomeBanda.ToLower().Contains(nome.ToLower()));
                
            }
            return ListaBanda.FirstOrDefault(x => x.NomeBanda.ToLower().Contains(nome.ToLower()));
        }
        public List<BandaModel> Pesquisar(string termoPesquisado) // Comando Read --- procura na lista inteira o nome
        {
            return ListaBanda.Where(x =>
            x.NomeBanda.ToLower().Contains(termoPesquisado.ToLower())).ToList();
        }

        public IEnumerable<BandaModel> ListarTodos(string buscar = null)  // Comanod Read --- Mostrar tudo que esta na coleção
        {
            if (string.IsNullOrWhiteSpace(buscar))
            {
                return ListaBanda;
            }
            return ListaBanda.Where(x =>
            x.NomeBanda.Contains(buscar, StringComparison.OrdinalIgnoreCase));
        }

        public void Atualizar(BandaModel bandaModel)    // Comando Update (ainda não feito)
        {
            var banda = EscolherNome(bandaModel.NomeBanda);

            if (banda == null)
            {
                return;
            }
            banda.NomeBanda = bandaModel.NomeBanda;
            banda.InicioBanda = bandaModel.InicioBanda;
            banda.FazendoShow = bandaModel.FazendoShow;
            banda.ParticipantesBanda = bandaModel.ParticipantesBanda;
        }

        public void Remover(string nome)    // comando Delete
        {
            var banda = EscolherNome(nome);
            ListaBanda.Remove(banda);
        }

    }
}
