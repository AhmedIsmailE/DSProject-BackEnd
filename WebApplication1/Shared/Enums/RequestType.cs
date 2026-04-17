namespace WebApplication1.Shared.Enums
{
    public enum RequestType
    {
        // User features
        LOGIN,
        REGISTER,
        DEPOSIT_CASH,
        GET_PROFILE,

        // Product features
        ADD_PRODUCT,
        EDIT_PRODUCT,
        DELETE_PRODUCT,
        SEARCH_PRODUCTS,
        MANAGE_INVENTORY, // Added for updating stock
        GET_STORE_PRODUCTS, // Added to handle the "interface for other stores" via sockets

        // Order & Report features
        PURCHASE_ITEM,
        GET_REPORT
    }
}
