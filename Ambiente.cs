using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Ambiente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public Queue<Log> Logs { get; set; }

    public Ambiente()
    {
        Logs = new Queue<Log>();
    }

    public Ambiente(int id, string nome)
    {
        Id = id;
        Nome = nome;
        Logs = new Queue<Log>();
    }

    public void RegistrarLog(Log log)
    {
        if (Logs.Count >= 100)
            Logs.Dequeue(); // remove o mais antigo

        Logs.Enqueue(log);
    }

    public override string ToString()
    {
        return $"{Id} - {Nome} (Logs: {Logs.Count})";
    }
}

