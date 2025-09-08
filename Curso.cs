namespace EscolaApp.Models;

public class Curso
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    private readonly List<Disciplina> disciplinas = new();

    public bool AdicionarDisciplina(Disciplina disciplina)
    {
        if (disciplinas.Count >= 12) return false;
        disciplinas.Add(disciplina);
        return true;
    }

    public Disciplina? PesquisarDisciplina(int id)
        => disciplinas.FirstOrDefault(d => d.Id == id);

    public bool RemoverDisciplina(Disciplina disciplina)
    {
        if (disciplina.GetAlunos().Count > 0) return false;
        return disciplinas.Remove(disciplina);
    }

    public IReadOnlyList<Disciplina> GetDisciplinas() => disciplinas;
}
