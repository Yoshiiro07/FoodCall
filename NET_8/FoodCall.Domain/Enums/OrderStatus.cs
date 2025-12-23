public enum OrderStatus
{
    Pending,        // Aguardando confirmação do restaurante
    Confirmed,      // Restaurante aceitou
    Preparing,      // Na cozinha
    OutForDelivery, // Com o entregador
    Delivered,      // Entregue
    Cancelled       // Cancelado
}