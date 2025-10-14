using Listas_GerenciamentoProj.Models;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Listas_GerenciamentoProj.Forms
{
    public class MainForm : Form
    {
        private Projetos _projetos = new();
        private BindingList<Projeto> _bindingProjetos;

        private ListBox lstProjetos;
        private Button btnAddProjeto, btnRemoveProjeto, btnManageTasks, btnResumo;

        public MainForm()
        {
            InitializeComponent();
            _bindingProjetos = new BindingList<Projeto>(_projetos.Listar());
            lstProjetos.DataSource = _bindingProjetos;
            lstProjetos.DisplayMember = "Nome";
        }

        private void InitializeComponent()
        {
            this.Text = "Gerenciador de Projetos";
            this.Size = new Size(700, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            lstProjetos = new ListBox() { Location = new Point(12, 12), Size = new Size(460, 360) };

            btnAddProjeto = new Button() { Text = "Adicionar Projeto", Location = new Point(490, 20), Size = new Size(180, 40) };
            btnRemoveProjeto = new Button() { Text = "Remover Projeto", Location = new Point(490, 70), Size = new Size(180, 40) };
            btnManageTasks = new Button() { Text = "Gerenciar Tarefas", Location = new Point(490, 130), Size = new Size(180, 40) };
            btnResumo = new Button() { Text = "Resumo Geral", Location = new Point(490, 190), Size = new Size(180, 40) };

            btnAddProjeto.Click += BtnAddProjeto_Click;
            btnRemoveProjeto.Click += BtnRemoveProjeto_Click;
            btnManageTasks.Click += BtnManageTasks_Click;
            btnResumo.Click += BtnResumo_Click;

            this.Controls.AddRange(new Control[] { lstProjetos, btnAddProjeto, btnRemoveProjeto, btnManageTasks, btnResumo });
        }

        private void BtnAddProjeto_Click(object? sender, EventArgs e)
        {
            using var f = new AddProjectForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                var p = f.ProjetoCriado;
                if (_projetos.Adicionar(p))
                {
                    _bindingProjetos.Add(p);
                }
                else
                {
                    MessageBox.Show("Projeto com este ID já existe.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnRemoveProjeto_Click(object? sender, EventArgs e)
        {
            if (lstProjetos.SelectedItem is Projeto p)
            {
                if (p.Tarefas.Count > 0)
                {
                    MessageBox.Show("Não é possível remover projeto que possui tarefas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_projetos.Remover(p))
                {
                    _bindingProjetos.Remove(p);
                }
            }
            else MessageBox.Show("Selecione um projeto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnManageTasks_Click(object? sender, EventArgs e)
        {
            if (lstProjetos.SelectedItem is Projeto p)
            {
                using var f = new ManageTasksForm(_projetos, p.Id);
                f.ShowDialog();
                // refresh listbox in case name changed elsewhere
                lstProjetos.Refresh();
            }
            else MessageBox.Show("Selecione um projeto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnResumo_Click(object? sender, EventArgs e)
        {
            int totalProjetos = _projetos.Listar().Count;
            int totalAbertas = _projetos.Listar().Sum(pr => pr.Tarefas.Count(t => t.Status == "Aberta"));
            int totalFechadas = _projetos.Listar().Sum(pr => pr.Tarefas.Count(t => t.Status == "Fechada"));
            int totalCanceladas = _projetos.Listar().Sum(pr => pr.Tarefas.Count(t => t.Status == "Cancelada"));
            int totalTarefas = totalAbertas + totalFechadas + totalCanceladas;
            double percConcluidas = totalTarefas == 0 ? 0 : (totalFechadas / (double)totalTarefas) * 100.0;

            var msg =
                $"Projetos: {totalProjetos}\n" +
                $"Tarefas Abertas: {totalAbertas}\n" +
                $"Tarefas Fechadas: {totalFechadas}\n" +
                $"Tarefas Canceladas: {totalCanceladas}\n" +
                $"Total tarefas: {totalTarefas}\n" +
                $"% concluídas: {percConcluidas:F2}%";

            MessageBox.Show(msg, "Resumo Geral", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

