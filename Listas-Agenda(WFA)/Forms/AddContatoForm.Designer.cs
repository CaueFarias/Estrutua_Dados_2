using Listas_Agenda_WFA_.Models;

namespace Listas_Agenda_WFA_.Forms
public Contato ContatoCriado { get; private set; }

private void btnSalvar_Click(object sender, EventArgs e)
{
    var data = new Data(int.Parse(txtDia.Text), int.Parse(txtMes.Text), int.Parse(txtAno.Text));
    var contato = new Contato(txtNome.Text, txtEmail.Text, data);

    // adicionar telefones (supondo que foram incluídos em uma lista temporária)
    foreach (Telefone t in lstTelefones.Items)
        contato.AdicionarTelefone(t);

    ContatoCriado = contato;
    DialogResult = DialogResult.OK;
    Close();
}
