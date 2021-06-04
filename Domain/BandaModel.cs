using System;
using System.Threading;

namespace Domain
{
    public class BandaModel
    {
    public Guid IdBanda { get; set; }   // auto property
    public string NomeBanda { get; set; }   //propriedades automaticas
    public DateTime InicioBanda { get; set; }
    public int ParticipantesBanda { get; set; }
    public bool FazendoShow { get; set; }

        //Construtor para a classe armazenar conteudo da coleção em variaveis
    public BandaModel(Guid id, string nome, DateTime inicio, int participantes, bool ativos)
    {
        IdBanda = id;
        NomeBanda = nome;
        InicioBanda = inicio;
        ParticipantesBanda = participantes;
        FazendoShow = ativos;
    }
        //Regras de negocio
    public static double ShowDeAniversario(DateTime dataAniversario)
    {
        var dataAtual = DateTime.Now.Date;
        var diferencaAnos = dataAtual.Year - dataAniversario.Year;
        var aniversarioAnoAtual = dataAniversario.AddYears(diferencaAnos);
        var qtdeDiasParaAniversario = aniversarioAnoAtual - dataAtual;

        return qtdeDiasParaAniversario.Days;
    }

    public static double AnosDeCarreira(DateTime anosAtivos)
    {
        var dataAtual = DateTime.Now.Date;
        var diferencaAnos = dataAtual.Year - anosAtivos.Year;
        return diferencaAnos;
    }

    public static string MensagemShowDeAniversario(double qtdeDias, double qtdeAnos)
    {
        if (double.IsNegative(qtdeDias))
            return $"A banda tem {qtdeAnos} ano(s) de carreira. \nO show de comemoração este ano foi há {-qtdeDias} dia(s) atras!";
        else if (qtdeDias.Equals(0d))
        {
            Console.WriteLine($"A banda tem {qtdeAnos} ano(s) de carreira. \nO show é hoje a noite!");
            return $"{FelizAniversario(qtdeDias)}";
        }
        else
            return $"A banda tem {qtdeAnos} ano(s) de carreira. \nFaltam {qtdeDias} dia(s) para o Show de aniversario desta banda!";
    }

    public static Double FelizAniversario(double rodar)
    {
        double comparar;
        comparar = rodar;

        if (comparar == 0d)
        {
            Thread.Sleep(200); Console.Beep(264, 125);
            Thread.Sleep(25); Console.Beep(264, 125);
            Thread.Sleep(12); Console.Beep(297, 500);
            Thread.Sleep(12); Console.Beep(264, 500);
            Thread.Sleep(12); Console.Beep(352, 500);
            Thread.Sleep(12); Console.Beep(330, 1000);
            Thread.Sleep(25); Console.Beep(264, 125);
            Thread.Sleep(25); Console.Beep(264, 125);
            Thread.Sleep(12); Console.Beep(297, 500);
            Thread.Sleep(12); Console.Beep(264, 500);
            Thread.Sleep(12); Console.Beep(396, 500);
            Thread.Sleep(12); Console.Beep(352, 1000);
            Thread.Sleep(25); Console.Beep(264, 125);
            Thread.Sleep(25); Console.Beep(264, 125);
            Thread.Sleep(12); Console.Beep(440, 500);
            Thread.Sleep(12); Console.Beep(352, 250);
            Thread.Sleep(12); Console.Beep(352, 125);
            Thread.Sleep(12); Console.Beep(330, 500);
            Thread.Sleep(12); Console.Beep(297, 1000);
            Thread.Sleep(25); Console.Beep(466, 125);
            Thread.Sleep(25); Console.Beep(466, 125);
            Thread.Sleep(12); Console.Beep(440, 500);
            Thread.Sleep(12); Console.Beep(352, 500);
            Thread.Sleep(12); Console.Beep(396, 500);
            Thread.Sleep(12); Console.Beep(352, 1000);
        }
        return comparar;
    }
}
}
