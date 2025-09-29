using Listas_Agenda_WFA_.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Listas_Agenda_WFA_.Forms
{
    public class SearchForm : Form
    {
        private readonly Contatos _contatos;

        private Label lblSearch;
        private TextBox txtSearchEmail;
        private Button btnSearch;

        private GroupBox grpDetails;
        private Label lblNome;
        private TextBox txtNome;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblData;
        private TextBox txtDia;
        private TextBox txtMes;
        private TextBox txtAno;

        private Label lblTelefones;
        private ListBox lstTelefones;
        private Button btnAddPhone;
        private Button btnRemovePhone;

        private Button btnSave;
        private Button btnDelete;
        private Button btnClose;

        private Contato? currentContato;

        public SearchForm(Contatos contatos)
        {
            _contatos = contatos ?? throw new ArgumentNullException(nameof(contatos));
            InitializeComponent();
            ToggleDetails(false);
        }

        private void InitializeComponent()
        {
            this.Text = "Pesquisar Contato";
            this.Size = new Size(640, 480);
            this.StartPosition = FormStartPosition.CenterParent;

            lblSearch = new Label() { Text = "Email do contato:", Location = new Point(12, 15), AutoSize = true };
            txtSearchEmail = new TextBox() { Location = new Point(120, 12), Width = 300 };
            btnSearch = new Button() { Text = "Pesquisar", Location = new Point(430, 10), Width = 90 };
            btnSearch.Click += BtnSearch_Click;

            // GroupBox detalhes
            grpDetails = new GroupBox() { Text = "Detalhes do Contato", Location = new Point(12, 50), Size = new Size(600, 330) };

            lblNome = new Label() { Text = "Nome:", Location = new Point(12, 25), AutoSize = true };
            txtNome = new TextBox() { Location = new Point(80, 22), Width = 480 };

            lblEmail = new Label() { Text = "Email:", Location = new Point(12, 60), AutoSize = true };
            txtEmail = new TextBox() { Location = new Point(80, 57), Width = 300, ReadOnly = true };

            lblData = new Label() { Text = "Nascimento (dd/mm/aaaa):", Location = new Point(12, 95), AutoSize = true };
            txtDia = new TextBox() { Location = new Point(180, 92), Width = 40, MaxLength = 2 };
            txtMes = new TextBox() { Location = new Point(230, 92), Width = 40, MaxLength = 2 };
            txtAno = new TextBox() { Location = new Point(280, 92), Width = 70, MaxLength = 4 };

            lblTelefones = new Label() { Text = "Telefones:", Location = new Point(12, 130), AutoSize = true };
            lstTelefones = new ListBox() { Location = new Point(15, 155), Size = new Size(540, 120) };
            lstTelefones.DisplayMember = "ToString"; // exibe ToString do Telefone

            btnAddPhone = new Button() { Text = "Adicionar Telefone", Location = new Point(15, 285), Width = 150 };
            btnAddPhone.Click += BtnAddPhone_Click;

            btnRemovePhone = new Button() { Text = "Remover Telefone", Location = new Point(180, 285), Width = 150 };
            btnRemovePhone.Click += BtnRemovePhone_Click;

            btnSave = new Button() { Text = "Salvar Alterações", Location = new Point(15, 310), Width = 150 };
            btnSave.Click += BtnSave_Click;

            btnDelete = new Button() { Text = "Excluir Contato", Location = new Point(180, 310), Width = 150, BackColor = Color.LightCoral };
            btnDelete.Click += BtnDelete_Click;

            grpDetails.Controls.AddRange(new Control[] {
                lblNome, txtNome,
                lblEmail, txtEmail,
                lblData, txtDia, txtMes, txtAno,
                lblTelefones, lstTelefones, btnAddPhone, btnRemovePhone, btnSave, btnDelete
            });

            btnClose = new Button() { Text = "Fechar", Location = new Point(520, 400), Width = 90 };
            btnClose.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] {
                lblSearch, txtSearchEmail, btnSearch,
                grpDetails, btnClose
            });
        }

        private void ToggleDetails(bool enabled)
        {
            grpDetails.Enabled = enabled;
            if (!enabled)
            {
                txtNome.Text = "";
                txtEmail.Text = "";
                txtDia.Text = "";
                txtMes.Text = "";
                txtAno.Text = "";
                lstTelefones.Items.Clear();
                currentContato = null;
            }
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            string email = txtSearchEmail.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Informe o email para pesquisa.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var c = _contatos.Pesquisar(new Contato("", email, new Data(1, 1, 2000)));
            // Nota: se sua Contatos.Pesquisar espera string, troque por _contatos.Pesquisar(email)
            if (c == null)
            {
                // tentar outra assinatura caso Contatos.Pesquisar aceite string
                var pesquisarPorEmailMethod = typeof(Contatos).GetMethod("Pesquisar", new Type[] { typeof(string) });
                if (pesquisarPorEmailMethod != null)
                {
                    c = (Contato?)pesquisarPorEmailMethod.Invoke(_contatos, new object[] { email });
                }
            }

            if (c == null)
            {
                MessageBox.Show("Contato não encontrado.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ToggleDetails(false);
                return;
            }

            currentContato = c;
            PopulateFields(c);
            ToggleDetails(true);
        }

        private void PopulateFields(Contato c)
        {
            txtNome.Text = c.Nome;
            txtEmail.Text = c.Email;
            txtDia.Text = c.DtNasc.Dia.ToString();
            txtMes.Text = c.DtNasc.Mes.ToString();
            txtAno.Text = c.DtNasc.Ano.ToString();

            lstTelefones.Items.Clear();
            foreach (var t in c.Telefones)
                lstTelefones.Items.Add(t);
        }

        private void BtnAddPhone_Click(object? sender, EventArgs e)
        {
            // solicitar tipo
            string tipo = ShowInputDialog("Tipo do telefone (ex: Celular, Comercial):", "Tipo");
            if (tipo == null) return;
            string numero = ShowInputDialog("Número do telefone:", "Número");
            if (numero == null) return;

            var isPrincipal = MessageBox.Show("Definir este telefone como principal?", "Principal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

            // se já existe contato carregado, atualiza o objeto; senão só adiciona ao listbox (não esperado)
            var tel = new Telefone(tipo, numero, isPrincipal);

            if (currentContato != null)
            {
                if (isPrincipal)
                {
                    // desmarcar os outros
                    foreach (Telefone t in currentContato.Telefones)
                        t.Principal = false;
                }
                currentContato.AdicionarTelefone(tel);
                // atualizar listbox
                lstTelefones.Items.Add(tel);
                // se principal, atualizar exibição para mostrar apenas um principal
                RefreshListBox();
            }
            else
            {
                // fallback: adicionar temporariamente ao listbox
                if (isPrincipal)
                {
                    for (int i = 0; i < lstTelefones.Items.Count; i++)
                    {
                        if (lstTelefones.Items[i] is Telefone t) t.Principal = false;
                    }
                }
                lstTelefones.Items.Add(tel);
            }
        }

        private void BtnRemovePhone_Click(object? sender, EventArgs e)
        {
            if (lstTelefones.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione um telefone para remover.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Remover o telefone selecionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            if (currentContato != null)
            {
                var t = lstTelefones.SelectedItem as Telefone;
                if (t != null)
                {
                    currentContato.Telefones.Remove(t);
                }
            }

            lstTelefones.Items.RemoveAt(lstTelefones.SelectedIndex);
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (currentContato == null)
            {
                MessageBox.Show("Nenhum contato carregado para salvar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // validar e aplicar alterações
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Nome obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtDia.Text, out int dia) ||
                !int.TryParse(txtMes.Text, out int mes) ||
                !int.TryParse(txtAno.Text, out int ano))
            {
                MessageBox.Show("Data inválida.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var novaData = new Data();
                novaData.SetData(dia, mes, ano);

                // criar um contato temporário com as alterações (Contatos.Alterar usa o email para localizar)
                var novo = new Contato(txtNome.Text.Trim(), txtEmail.Text.Trim(), novaData);

                // copiar telefones do listbox (cada item é Telefone)
                foreach (var item in lstTelefones.Items)
                {
                    if (item is Telefone t)
                    {
                        novo.AdicionarTelefone(new Telefone(t.Tipo, t.Numero, t.Principal));
                    }
                }

                // tentar alterar via Contatos
                bool alterou = _contatos.Alterar(novo);
                if (alterou)
                {
                    MessageBox.Show("Contato alterado com sucesso.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // recarregar contato atualizado
                    currentContato = _contatos.Pesquisar(novo);
                    PopulateFields(currentContato!);
                }
                else
                {
                    MessageBox.Show("Falha ao alterar contato (contato não encontrado).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show($"Data inválida: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (currentContato == null)
            {
                MessageBox.Show("Nenhum contato carregado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resp = MessageBox.Show($"Deseja realmente excluir o contato '{currentContato.Nome}' ({currentContato.Email})?", "Confirmar exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resp != DialogResult.Yes) return;

            bool ok;
            // tentar remover por assinatura string/email ou por objeto (verificar método disponível)
            var removerPorString = typeof(Contatos).GetMethod("Remover", new Type[] { typeof(string) }) != null;
            if (removerPorString)
                ok = _contatos.Remover(currentContato.Email);
            else
                ok = _contatos.Remover(currentContato);

            if (ok)
            {
                MessageBox.Show("Contato removido.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ToggleDetails(false);
            }
            else
            {
                MessageBox.Show("Não foi possível remover o contato.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Atualiza visual do listbox para refletir mudanças (principal marcado)
        private void RefreshListBox()
        {
            var items = lstTelefones.Items.Cast<object>().ToArray();
            lstTelefones.Items.Clear();
            foreach (var it in items)
                lstTelefones.Items.Add(it);
        }

        // prompt simples para entrada de texto
        private string? ShowInputDialog(string text, string caption)
        {
            using var prompt = new Form()
            {
                Width = 420,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent
            };
            var lbl = new Label() { Left = 10, Top = 10, Text = text, AutoSize = true };
            var input = new TextBox() { Left = 10, Top = 35, Width = 380 };
            var ok = new Button() { Text = "OK", Left = 220, Width = 80, Top = 70, DialogResult = DialogResult.OK };
            var cancel = new Button() { Text = "Cancelar", Left = 310, Width = 80, Top = 70, DialogResult = DialogResult.Cancel };

            prompt.Controls.Add(lbl);
            prompt.Controls.Add(input);
            prompt.Controls.Add(ok);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = ok;
            prompt.CancelButton = cancel;

            return prompt.ShowDialog(this) == DialogResult.OK ? input.Text : null;
        }
    }
}
