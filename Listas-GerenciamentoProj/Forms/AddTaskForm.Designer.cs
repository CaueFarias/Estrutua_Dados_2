using Listas_GerenciamentoProj.Forms;
using Listas_GerenciamentoProj.Models;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Listas_GerenciamentoProj.Forms
{
    public class ManageTasksForm : Form
    {
        private Projetos _projetos;
        private Projeto _projeto;
        private BindingList<Tarefa> _bindingTarefas;

        private DataGridView dgv;
        private Button btnAddTask, btnConcluir, btnCancelar, btnReabrir, btnFechar;
        private ComboBox cbFilterStatus;
        private ComboBox cbFilterPrioridade;
        private Label lblProject;

        public ManageTasksForm(Projetos projetos, int projetoId)
        {
            _projetos = projetos;
            _projeto = projetos.Buscar(projetoId) ?? throw new ArgumentException("Projeto não encontrado");
            _bindingTarefas = new BindingList<Tarefa>(_projeto.Tarefas);
            InitializeComponent();
            RefreshGrid();
        }

        private void InitializeComponent()
        {
            this.Text = $"Tarefas - Projeto: {_projeto.Nome}";
            this.Size = new Size(900, 520);
            this.StartPosition = FormStartPosition.CenterParent;

            lblProject = new Label() { Text = _projeto.Nome, Location = new Point(12, 12), AutoSize = true, Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold) };

            dgv = new DataGridView() { Location = new Point(12, 40), Size = new Size(760, 400), ReadOnly = true, AutoGenerateColumns = false, AllowUserToAddRows = false };
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Título", DataPropertyName = "Titulo", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Prioridade", DataPropertyName = "Prioridade", Width = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Status", DataPropertyName = "Status", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Criada", DataPropertyName = "DataCriacao", Width = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Conclusão", DataPropertyName = "DataConclusao", Width = 150 });

            btnAddTask = new Button() { Text = "Adicionar Tarefa", Location = new Point(790, 40), Size = new Size(90, 40) };
            btnConcluir = new Button() { Text = "Concluir", Location = new Point(790, 100), Size = new Size(90, 40) };
            btnCancelar = new Button() { Text = "Cancelar", Location = new Point(790, 160), Size = new Size(90, 40) };
            btnReabrir = new Button() { Text = "Reabrir", Location = new Point(790, 220), Size = new Size(90, 40) };
            btnFechar = new Button() { Text = "Fechar", Location = new Point(790, 420), Size = new Size(90, 40) };

            cbFilterStatus = new ComboBox() { Location = new Point(12, 450), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cbFilterStatus.Items.AddRange(new object[] { "Todos", "Aberta", "Fechada", "Cancelada" });
            cbFilterStatus.SelectedIndex = 0;
            cbFilterStatus.SelectedIndexChanged += (s, e) => RefreshGrid();

            cbFilterPrioridade = new ComboBox() { Location = new Point(230, 450), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cbFilterPrioridade.Items.AddRange(new object[] { "Todas", "1-Alta", "2-Média", "3-Baixa" });
            cbFilterPrioridade.SelectedIndex = 0;
            cbFilterPrioridade.SelectedIndexChanged += (s, e) => RefreshGrid();

            btnAddTask.Click += BtnAddTask_Click;
            btnConcluir.Click += BtnConcluir_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnReabrir.Click += BtnReabrir_Click;
            btnFechar.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblProject, dgv, btnAddTask, btnConcluir, btnCancelar, btnReabrir, btnFechar, cbFilterStatus, cbFilterPrioridade });
        }

        private void BtnAddTask_Click(object? sender, EventArgs e)
        {
            using var f = new AddTaskForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                var t = f.TarefaCriada;
                // ensure unique id within project
                if (_projeto.Tarefas.Any(x => x.Id == t.Id))
                {
                    MessageBox.Show("Já existe tarefa com esse ID no projeto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _projeto.AdicionarTarefa(t);
                _bindingTarefas.Add(t);
                RefreshGrid();
            }
        }

        private Tarefa? GetSelectedTask()
        {
            if (dgv.CurrentRow?.DataBoundItem is Tarefa t) return t;
            return null;
        }

        private void BtnConcluir_Click(object? sender, EventArgs e)
        {
            var t = GetSelectedTask();
            if (t == null) { MessageBox.Show("Selecione uma tarefa."); return; }
            t.Concluir();
            dgv.Refresh();
            RefreshGrid();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            var t = GetSelectedTask();
            if (t == null) { MessageBox.Show("Selecione uma tarefa."); return; }
            t.Cancelar();
            dgv.Refresh();
            RefreshGrid();
        }

        private void BtnReabrir_Click(object? sender, EventArgs e)
        {
            var t = GetSelectedTask();
            if (t == null) { MessageBox.Show("Selecione uma tarefa."); return; }
            t.Reabrir();
            dgv.Refresh();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            var lista = _projeto.Tarefas.AsEnumerable();
            var status = cbFilterStatus.SelectedItem?.ToString();
            var prioridadeIndex = cbFilterPrioridade.SelectedIndex;

            if (!string.IsNullOrEmpty(status) && status != "Todos")
                lista = lista.Where(t => t.Status == status);

            if (prioridadeIndex == 1) lista = lista.Where(t => t.Prioridade == 1);
            if (prioridadeIndex == 2) lista = lista.Where(t => t.Prioridade == 2);
            if (prioridadeIndex == 3) lista = lista.Where(t => t.Prioridade == 3);

            dgv.DataSource = new BindingList<Tarefa>(lista.ToList());
        }
    }
}
