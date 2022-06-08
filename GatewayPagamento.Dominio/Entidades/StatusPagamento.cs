using System.ComponentModel;

namespace GatewayPagamento.Dominio.Entidades
{
    public enum StatusPagamento// : int
    {
        [Description("Não definido")]
        NaoDefinido = 0,

        [Description("Saldo indisponível")]
        SaldoIndisponivel = 1,

        [Description("Pedido já pago")]
        PedidoJaPago = 2,

        [Description("Cartão inexistente")]
        CartaoInexistente = 3,

        [Description("Pagamento OK")]
        PagamentoOK = 4
    }
}