using EscolaApp.Models;

var escola = new Escola();
int opcao;

do
{
    Console.WriteLine("\n--- MENU ---");
    Console.WriteLine("0. Sair");
    Console.WriteLine("1. Adicionar curso");
    Console.WriteLine("2. Pesquisar curso");
    Console.WriteLine("3. Remover curso");
    Console.WriteLine("4. Adicionar disciplina no curso");
    Console.WriteLine("5. Pesquisar disciplina");
    Console.WriteLine("6. Remover disciplina do curso");
    Console.WriteLine("7. Matricular aluno na disciplina");
    Console.WriteLine("8. Remover aluno da disciplina");
    Console.WriteLine("9. Pesquisar aluno");
    Console.Write("Escolha: ");
    _ = int.TryParse(Console.ReadLine(), out opcao);

    switch (opcao)
    {
        case 1: AdicionarCurso(); break;
        case 2: PesquisarCurso(); break;
        case 3: RemoverCurso(); break;
        case 4: AdicionarDisciplina(); break;
        case 5: PesquisarDisciplina(); break;
        case 6: RemoverDisciplina(); break;
        case 7: MatricularAluno(); break;
        case 8: RemoverAluno(); break;
        case 9: PesquisarAluno(); break;
    }
} while (opcao != 0);

void AdicionarCurso()
{
    Console.Write("Id do curso: ");
    int id = int.Parse(Console.ReadLine()!);
    Console.Write("Descrição: ");
    string desc = Console.ReadLine()!;

    var curso = new Curso { Id = id, Descricao = desc };
    Console.WriteLine(escola.AdicionarCurso(curso)
        ? "Curso adicionado."
        : "Limite de cursos atingido (máx 5).");
}

void PesquisarCurso()
{
    Console.Write("Id do curso: ");
    int id = int.Parse(Console.ReadLine()!);
    var curso = escola.PesquisarCurso(id);

    if (curso is null)
    {
        Console.WriteLine("Curso não encontrado.");
        return;
    }

    Console.WriteLine($"Curso: {curso.Descricao}");
    foreach (var d in curso.GetDisciplinas())
        Console.WriteLine($" - Disciplina {d.Id}: {d.Descricao}");
}

void RemoverCurso()
{
    Console.Write("Id do curso: ");
    int id = int.Parse(Console.ReadLine()!);
    var curso = escola.PesquisarCurso(id);

    if (curso is null)
    {
        Console.WriteLine("Curso não encontrado.");
        return;
    }

    Console.WriteLine(escola.RemoverCurso(curso)
        ? "Curso removido."
        : "Não pode remover: possui disciplinas.");
}

void AdicionarDisciplina()
{
    Console.Write("Id do curso: ");
    int id = int.Parse(Console.ReadLine()!);
    var curso = escola.PesquisarCurso(id);

    if (curso is null)
    {
        Console.WriteLine("Curso não encontrado.");
        return;
    }

    Console.Write("Id da disciplina: ");
    int idD = int.Parse(Console.ReadLine()!);
    Console.Write("Descrição: ");
    string desc = Console.ReadLine()!;

    var d = new Disciplina { Id = idD, Descricao = desc };
    Console.WriteLine(curso.AdicionarDisciplina(d)
        ? "Disciplina adicionada."
        : "Não foi possível adicionar (máx 12).");
}

void PesquisarDisciplina()
{
    Console.Write("Id do curso: ");
    int id = int.Parse(Console.ReadLine()!);
    var curso = escola.PesquisarCurso(id);

    if (curso is null)
    {
        Console.WriteLine("Curso não encontrado.");
        return;
    }

    Console.Write("Id da disciplina: ");
    int idD = int.Parse(Console.ReadLine()!);
    var d = curso.PesquisarDisciplina(idD);

    if (d is null)
    {
        Console.WriteLine("Disciplina não encontrada.");
        return;
    }

    Console.WriteLine($"Disciplina: {d.Descricao}");
    foreach (var a in d.GetAlunos())
        Console.WriteLine($" - Aluno {a.Id}: {a.Nome}");
}

void RemoverDisciplina()
{
    Console.Write("Id do curso: ");
    int id = int.Parse(Console.ReadLine()!);
    var curso = escola.PesquisarCurso(id);

    if (curso is null)
    {
        Console.WriteLine("Curso não encontrado.");
        return;
    }

    Console.Write("Id da disciplina: ");
    int idD = int.Parse(Console.ReadLine()!);
    var d = curso.PesquisarDisciplina(idD);

    if (d is null)
    {
        Console.WriteLine("Disciplina não encontrada.");
        return;
    }

    Console.WriteLine(curso.RemoverDisciplina(d)
        ? "Disciplina removida."
        : "Não pode remover: há alunos matriculados.");
}

void MatricularAluno()
{
    Console.Write("Id do curso: ");
    int id = int.Parse(Console.ReadLine()!);
    var curso = escola.PesquisarCurso(id);
    if (curso is null) { Console.WriteLine("Curso não encontrado."); return; }

    Console.Write("Id da disciplina: ");
    int idD = int.Parse(Console.ReadLine()!);
    var d = curso.PesquisarDisciplina(idD);
    if (d is null) { Console.WriteLine("Disciplina não encontrada."); return; }

    Console.Write("Id do aluno: ");
    int idA = int.Parse(Console.ReadLine()!);
    Console.Write("Nome do aluno: ");
    string nome = Console.ReadLine()!;

    var aluno = new Aluno { Id = idA, Nome = nome };
    Console.WriteLine(d.MatricularAluno(aluno)
        ? "Aluno matriculado."
        : "Não foi possível matricular.");
}

void RemoverAluno()
{
    Console.Write("Id do curso: ");
    int id = int.Parse(Console.ReadLine()!);
    var curso = escola.PesquisarCurso(id);
    if (curso is null) { Console.WriteLine("Curso não encontrado."); return; }

    Console.Write("Id da disciplina: ");
    int idD = int.Parse(Console.ReadLine()!);
    var d = curso.PesquisarDisciplina(idD);
    if (d is null) { Console.WriteLine("Disciplina não encontrada."); return; }

    Console.Write("Id do aluno: ");
    int idA = int.Parse(Console.ReadLine()!);
    var aluno = d.GetAlunos().FirstOrDefault(a => a.Id == idA);

    if (aluno is null) { Console.WriteLine("Aluno não encontrado."); return; }

    Console.WriteLine(d.DesmatricularAluno(aluno)
        ? "Aluno removido."
        : "Não foi possível remover.");
}

void PesquisarAluno()
{
    Console.Write("Id do aluno: ");
    int idA = int.Parse(Console.ReadLine()!);

    foreach (var curso in escola.GetCursos())
    {
        foreach (var d in curso.GetDisciplinas())
        {
            var aluno = d.GetAlunos().FirstOrDefault(a => a.Id == idA);
            if (aluno is not null)
            {
                Console.WriteLine($"Aluno: {aluno.Nome} (ID {aluno.Id})");
                Console.WriteLine($" - Matriculado em: {d.Descricao} (Curso {curso.Descricao})");
            }
        }
    }
}
