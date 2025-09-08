namespace EscolaApp.Models;

public class Escola
{
    private readonly List<Curso> cursos = new();

    public bool AdicionarCurso(Curso curso)
    {
        if (cursos.Count >= 5) return false;
        cursos.Add(curso);
        return true;
    }

    public Curso? PesquisarCurso(int id)
        => cursos.FirstOrDefault(c => c.Id == id);

    public bool RemoverCurso(Curso curso)
    {
        if (curso.GetDisciplinas().Count > 0) return false;
        return cursos.Remove(curso);
    }

    public IReadOnlyList<Curso> GetCursos() => cursos;
}
