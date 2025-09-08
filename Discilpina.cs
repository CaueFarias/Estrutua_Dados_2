namespace EscolaApp.Models;

public class Disciplina
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    private readonly List<Aluno> alunos = new();

    public bool MatricularAluno(Aluno aluno)
    {
        if (alunos.Count >= 15) return false;
        if (!aluno.PodeMatricular()) return false;

        alunos.Add(aluno);
        aluno.AdicionarDisciplina(this);
        return true;
    }

    public bool DesmatricularAluno(Aluno aluno)
    {
        if (alunos.Remove(aluno))
        {
            aluno.RemoverDisciplina(this);
            return true;
        }
        return false;
    }

    public IReadOnlyList<Aluno> GetAlunos() => alunos;
}
