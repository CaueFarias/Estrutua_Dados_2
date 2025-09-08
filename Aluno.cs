namespace EscolaApp.Models;

public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    private readonly List<Disciplina> disciplinasMatriculadas = new();

    public bool PodeMatricular(Curso? curso = null)
        => disciplinasMatriculadas.Count < 6;

    public void AdicionarDisciplina(Disciplina disciplina)
        => disciplinasMatriculadas.Add(disciplina);

    public void RemoverDisciplina(Disciplina disciplina)
        => disciplinasMatriculadas.Remove(disciplina);

    public IReadOnlyList<Disciplina> GetDisciplinas()
        => disciplinasMatriculadas;
}
